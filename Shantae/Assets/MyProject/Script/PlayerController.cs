using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public GameObject ground;
    public GameObject jump;
    public GameObject down;
    public GameObject sandCollider;

    private Vector3 originalPosition; // ���� ��ġ�� �����ϴ� ����

    private int playerHP = 50;

    public float jumpForce = 20f;
    public float fallForce;
    private bool isJumping = false;
    private bool octoJump = false;
    private bool isAir = false;
    private float jumpStartTime;
    private int jumpCount = 0;

    private Animator animator = default;

    public GameObject stepSand;
    private bool overSand = false;
    private bool inSand = false;
    private bool drillOn = false;
    private bool drillJump = false;
    private Vector3 headDirection = Vector3.up;
    private List<GameObject> breakSand = new List<GameObject>();            // �ν��� �𷡵��� ����Ʈ (���� ���� Ȱ��ȭ)
    public List<GameObject> deactivatedParents = new List<GameObject>();

    private bool isAttack = false;
    private bool isRun;
    private bool isDown;
    private bool isDownAndRun;
    private bool isDamage = false;
    private bool invincible = false;
    private bool isFlashing = false;

    private Rigidbody2D playerRigid = default;
    private AudioSource playerAudio = default;
    private BoxCollider2D boxCollider;
    public float bottomY;

    // === (��ֺ� ����)
    public static Vector2 playerPosition = default;
    public static bool gotDamage = false;
    // ===

    private bool trigger = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();

        Debug.Assert(playerRigid != null);
        Debug.Assert(animator != null);
    }

    // Update is called once per frame
    void Update()
    {
        // === (��ֺ� ����) �÷��̾��� ��ǥ�� �ǽð����� �Ѹ�.
        playerPosition = transform.position;
        // ===
        
        float playerHeight = GetComponent<Renderer>().bounds.extents.y;
        bottomY = transform.position.y - playerHeight;

        animator.SetBool("isGround", !isAir);
        if (invincible && !isFlashing)
        {
            StartCoroutine(FlashPlayer(0.1f)); 
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            isJumping = false;

            animator.SetBool("Jump", isJumping);
        }


            Vector2 raycastOrigin = new Vector2(transform.position.x,
            transform.position.y - GetComponent<SpriteRenderer>().bounds.extents.y); // �÷��̾��� ������Ʈ �߾ӿ��� �Ʒ��� �������� �Ÿ� ���

        Debug.DrawRay(raycastOrigin, Vector2.down, Color.black);           // �����ɽ� ������ ���ñ���

        if (Physics2D.Raycast(raycastOrigin, Vector2.down, 0.1f))        //�÷��̾ �ٴڿ� �ִ���
        {
            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 0.1f);
            jumpCount = 0;
            if (hit.collider.CompareTag("Damage") || hit.collider.CompareTag("jewel"))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    isAir = false;
                    animator.SetBool("isGround", !isAir);
                    isJumping = false;
                    animator.SetBool("Jump", isJumping);
                }
                else
                {
                    jumpCount += 1;
                    isAir = true;
                    animator.SetBool("isGround", !isAir);
                    isJumping = true;
                    animator.SetBool("Jump", isJumping);
                    transform.Translate(Vector3.down * fallForce * Time.deltaTime);
                }
                
            }
            if (hit.collider.CompareTag("Ground"))
            {
                // �ٴڰ� �浹�� ���
                 octoJump = false;
                animator.SetBool("OctoJump", octoJump);
                isAir = false;
                animator.SetBool("isGround", !isAir);
                overSand = false;
                playerRigid.gravityScale = 1;
            }
            else if (hit.collider.CompareTag("SandStep"))
            {
                isAir = false;
                animator.SetBool("isGround", !isAir);

                overSand = true;
                stepSand = hit.collider.gameObject;     // ����ִ� ���� ������ ����
                playerRigid.gravityScale = 1;

            }
            else
            {

                overSand = false;
                transform.Translate(Vector3.down * fallForce * Time.deltaTime);

            }
        }
        else if (!Physics2D.Raycast(raycastOrigin, Vector2.down, 0.005f))
        {
            // �ٴڰ� �浹���� ���� ���
            if(!drillOn)
            {
            playerRigid.gravityScale = 0;
            }

            isAir = true;
            if (!isJumping && !drillOn)
            {
                if(jumpCount == 0)
                {
                jumpCount += 1;
                }

                transform.Translate(Vector3.down * fallForce * Time.deltaTime);
            }
        }
        playerAnimation();          // �÷��̾ ������ �ִϸ��̼�


        if (Input.GetKeyDown(KeyCode.X) && jumpCount < 3 && !isDown)      // ����
        {
            isJumping = true;
            jumpStartTime = Time.time;
            jumpCount += 1;
            if(jumpCount == 1)
            {
                animator.SetBool("Jump", isJumping);

            }
            else if (jumpCount >= 2)
            {
                octoJump = true;
                animator.SetBool("OctoJump", octoJump);
            }
            if (Input.GetKeyDown(KeyCode.Z))        // ����
            {
                animator.SetBool("Attack", isAttack);

                StartCoroutine(AirAttack(0.15f));
            }
        }

        if (isJumping)
        {
            float jumptime = Time.time - jumpStartTime;

            if (jumptime <= 0.3)
            {
                transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
                if (Input.GetKeyUp(KeyCode.X))
                {
                    isJumping = false;
                    if (jumpCount == 1)
                    {
                        animator.SetBool("Jump", isJumping);
                    }
                    else if (jumpCount >= 2)
                    {
                        octoJump = false;
                        animator.SetBool("OctoJump", octoJump);
                    }
                }
            }
            else
            {
                isJumping = false;
                animator.SetBool("Jump", isJumping);
                if (jumpCount == 1)
                {
                }
                else
                {
                    octoJump = false;
                    animator.SetBool("OctoJump", octoJump);
                }
            }
        }
        else
        {
            if (jumpCount > 0)
            {
                transform.Translate(Vector3.down * fallForce * Time.deltaTime);
            }
        }

        if ((!isAttack || isJumping) && !isDamage)
        {
            if (Input.GetKeyDown(KeyCode.Z) && !drillOn)        // ����
            {
                isAttack = true;

                if (isDown)
                {
                    originalPosition = transform.position;
                    animator.SetBool("Attack", isAttack);
                    StartCoroutine(DownAttack(0.15f));         //�����ϸ� 

                }
                else if (!isAir && !isDown)
                {
                    originalPosition = transform.position;
                    animator.SetBool("Attack", isAttack);

                    StartCoroutine(GroundAttack(0.15f));         //�����ϸ� 
                }
                else if (isAir)
                {
                    animator.SetBool("Attack", isAttack);

                    StartCoroutine(AirAttack(0.15f));
                }
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {

                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            }
            if (Input.GetKey(KeyCode.DownArrow) && !isAir)
            {
                if (!overSand && !drillOn)
                {
                    boxCollider.size = new Vector2(1.5f, 0.9f);
                    boxCollider.offset = new Vector2(0f, -1.45f);
                    isDown = true;
                }
                else if (overSand)
                {
                    playerRigid.gravityScale = 1;
                    stepSand.SetActive(false);      //������(����ִ�)�𷡸� ��Ȱ��ȭ
                    boxCollider.size = new Vector2(0.7f, 0.7f);
                    boxCollider.offset = new Vector2(0f, 0f);
                    drillOn = true;
                    animator.SetBool("Drill", drillOn);

                }
            }
            if (Input.GetKeyUp(KeyCode.DownArrow) && !drillOn)
            {

                isDown = false;
                isDownAndRun = false;
                boxCollider.size = new Vector2(0.7f, 2f);
                boxCollider.offset = new Vector2(0.385f, -0.9f);
            }
        }
       
    }

    private void playerAnimation()      // �÷��̾� ���� �ִϸ��̼�
    {
        if (!isDown)
        {
            moveSpeed = 10f;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                isRun = true;


                animator.SetBool("Run", isRun);
                Debug.Assert(animator != null);
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    boxCollider.size = new Vector2(1.3f, 0.9f);
                    boxCollider.offset = new Vector2(0f, -0.7f);

                    isRun = false;
                    animator.SetBool("Run", isRun);

                    isDownAndRun = true;
                    animator.SetBool("DownRun", isDownAndRun);

                    isDown = true;
                    animator.SetBool("Down", isDown);
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                isRun = true;

                animator.SetBool("Run", isRun);
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    boxCollider.size = new Vector2(1.3f, 0.9f);
                    boxCollider.offset = new Vector2(0f, -0.7f);

                    isRun = false;
                    animator.SetBool("Run", isRun);

                    isDownAndRun = true;
                    animator.SetBool("DownRun", isDownAndRun);

                    isDown = true;
                    animator.SetBool("Down", isDown);

                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                isDown = true;

            }
            else if (!Input.anyKey)
            {
                isRun = false;
                isDown = false;
                animator.SetBool("Run", isRun); 
                animator.SetBool("Down", isDown);

            }
        }
        else
        {
            moveSpeed = 5f;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                isDownAndRun = true;


                animator.SetBool("DownRun", isDownAndRun);
                Debug.Assert(animator != null);
                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    isDown = false;
                    animator.SetBool("Down", isDown);

                    isDownAndRun = false;
                    animator.SetBool("DownRun", isDownAndRun);

                    isRun = true;
                    animator.SetBool("Run", isRun);
                }

            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                isDownAndRun = false;
                animator.SetBool("DownRun", isDownAndRun);

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                isDownAndRun = true;

                animator.SetBool("DownRun", isDownAndRun);
                Debug.Assert(animator != null);
                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    isDown = false;
                    animator.SetBool("Down", isDown);


                    isDownAndRun = false;
                    animator.SetBool("DownRun", isDownAndRun);

                    isRun = true;
                    animator.SetBool("Run", isRun);
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                isDownAndRun = false;
                animator.SetBool("DownRun", isDownAndRun);

            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("Down", isDown);
                Debug.Assert(animator != null);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                isDown = false;
                isDownAndRun = false;
                animator.SetBool("Down", isDown);
                animator.SetBool("DownRun", isDownAndRun);
                if (!drillOn)
                {
                    boxCollider.size = new Vector2(0.7f, 2.1f);
                    boxCollider.offset = new Vector2(0.385f, -0.9f);
                }
            }
            else if (!Input.anyKey)
            {
                isDownAndRun = false;
                isDown = false;
                animator.SetBool("Down", isDown);
                animator.SetBool("DownRun", isDownAndRun);
            }
        }
    }
    #region   //���� ���
    private IEnumerator DownAttack(float delay)
    {
        down.SetActive(true);

        originalPosition = transform.position;
        yield return new WaitForSeconds(delay); // 1�� ���

        isAttack = false;
        down.SetActive(false);

        animator.SetBool("Attack", isAttack);
    }
    private IEnumerator GroundAttack(float delay)
    {
        ground.SetActive(true);

        originalPosition = transform.position;
        yield return new WaitForSeconds(delay); // 1�� ���

        isAttack = false;
        ground.SetActive(false);

        animator.SetBool("Attack", isAttack);
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isDown = false;
            isDownAndRun = false;
            boxCollider.size = new Vector2(0.7f, 2f);
            boxCollider.offset = new Vector2(0.385f, -0.9f);
        }
    }
    private IEnumerator AirAttack(float delay)
    {
        jump.SetActive(true);

        yield return new WaitForSeconds(delay); // 1�� ���

        isAttack = false;
        jump.SetActive(false);
        octoJump = false;

        animator.SetBool("OctoJump", octoJump);
        animator.SetBool("Attack", isAttack);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("Damage"))
        {
            if (!invincible)        // �����ð�
            {
                invincible = true;
                StartCoroutine(HandleInvincibleAndDamage(1.5f, 0.25f));

                // === (��ֺ� ����) �÷��̾��� �������� ���ӸŴ����� ����
                // �����ð� ���� �߰� �ǰ� ������ ������ �ʿ�. 
                gotDamage = true;
                // ===
            }
        }
        if (collision.tag.Equals("Sand"))
        {
            inSand = true;
            trigger = true;
        }
        
        if (collision.tag.Equals("SandPiece") && drillOn)
        {
            Transform parentTransform = collision.transform.parent;

            if (parentTransform != null)
            {
                GameObject sandPiece = collision.gameObject;
                sandPiece.SetActive(false); // �θ� ������Ʈ ��Ȱ��ȭ

                deactivatedParents.Add(sandPiece); // ��Ȱ��ȭ�� �θ� ������Ʈ�� ����Ʈ�� �߰�
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Sand"))
        { 
            inSand = false;
            if (trigger)
            {
                if (stepSand != null)
                {
                    stepSand.SetActive(true);
                }
                trigger = false;
            }

            boxCollider.size = new Vector2(0.7f, 2.1f);
            boxCollider.offset = new Vector2(0.385f, -0.9f);
            drillOn = false;
            animator.SetBool("Drill", drillOn);            
        }
    }

    private IEnumerator HandleInvincibleAndDamage(float invincibleDelay, float damageDelay)
    {
         transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        StartCoroutine(Invincible(invincibleDelay));

        StartCoroutine(Damage(damageDelay));

        yield return new WaitForSeconds(invincibleDelay);


        invincible = false;
    }

    private IEnumerator Damage(float delay)
    {
        isDamage = true;
        animator.SetBool("isDamage", isDamage);

        originalPosition = transform.position;

        yield return new WaitForSeconds(delay);

        isDamage = false;
        animator.SetBool("isDamage", isDamage);
    }

    private IEnumerator Invincible(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
    private IEnumerator FlashPlayer(float interval)
    {
        isFlashing = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Color transparentColor = originalColor;
        transparentColor.a = 0.5f; 

        float blinkTime = 0;

        while (invincible)
        {
            spriteRenderer.color = transparentColor;
            yield return new WaitForSeconds(interval);

            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(interval);
            blinkTime += interval * 2;
        }

        spriteRenderer.color = originalColor;
        isFlashing = false;
    }
}