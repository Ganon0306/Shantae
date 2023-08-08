using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public GameObject ground;
    public GameObject jump;
    public GameObject down;


    private Vector3 originalPosition; // ���� ��ġ�� �����ϴ� ����

    private int playerHP = 50;

    public float jumpForce = 10f;
    private bool isJumping = false;
    private bool isAir = false;
    private float jumpStartTime;

    private Animator animator = default;

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
        animator.SetBool("isGround", !isAir);
        if (invincible && !isFlashing)
        {
            StartCoroutine(FlashPlayer(0.1f)); // Adjust the time interval as needed
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, 2f))        //�÷��̾ �ٴڿ� �ִ���
        {
            // �ٴڰ� �浹�� ���
            isAir = false;
        }
        else
        {
            // �ٴڰ� �浹���� ���� ���
            isAir = true;
        }
        playerAnimation();          // �÷��̾ ������ �ִϸ��̼�


        if (Input.GetKeyDown(KeyCode.X) && !isJumping && !isAir && !isDown)      // ����
        {
            isJumping = true;
            jumpStartTime = Time.time;

            animator.SetBool("Jump", isJumping);
            if (Input.GetKeyDown(KeyCode.Z))        // ����
            {
                animator.SetBool("Attack", isAttack);

                StartCoroutine(AirAttack(0.15f));
            }
        }
        if (isJumping)
        {
            float jumptime = Time.time - jumpStartTime;

            if (jumptime <= 1)
            {
                transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
                if (Input.GetKeyUp(KeyCode.X))
                {
                    isJumping = false;
                    animator.SetBool("Jump", isJumping);
                }
            }
            else
            {
                isJumping = false;
                animator.SetBool("Jump", isJumping);
            }
        }

        if ((!isAttack || isJumping) && !isDamage)
        {

            if (Input.GetKeyDown(KeyCode.Z))        // ����
            {
                isAttack = true;

                if (isDown)
                {
                    originalPosition = transform.position;
                    animator.SetBool("Attack", isAttack);
                    StartCoroutine(DownAttack(0.15f));         //�����ϸ� 

                }
                else if (!isJumping && !isDown)
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
                boxCollider.size = new Vector2(1.5f, 0.9f);
                boxCollider.offset = new Vector2(0f, -1.45f);
                isDown = true;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
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
            moveSpeed = 5f;
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
            }
        }
        else
        {
            moveSpeed = 2f;
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

                boxCollider.size = new Vector2(0.7f, 2.1f);
                boxCollider.offset = new Vector2(0.385f, -0.2f);

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
    }
    private IEnumerator AirAttack(float delay)
    {
        jump.SetActive(true);

        yield return new WaitForSeconds(delay); // 1�� ���

        isAttack = false;
        jump.SetActive(false);

        animator.SetBool("Attack", isAttack);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag.Equals("Damage"))
        {
            if (!invincible)
            {
                invincible = true;
                StartCoroutine(HandleInvincibleAndDamage(1.5f, 0.25f));
            }
        }
    }

    private IEnumerator HandleInvincibleAndDamage(float invincibleDelay, float damageDelay)
    {
        // Save the original position before any changes
        //originalPosition = new Vector2(transform.position.x, transform.position.y);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // Start Invincible coroutine
        StartCoroutine(Invincible(invincibleDelay));

        // Start Damage coroutine
        StartCoroutine(Damage(damageDelay));

        // Wait for both coroutines to finish
        yield return new WaitForSeconds(invincibleDelay);

        //originalPosition = transform.position;

        invincible = false;
    }

    private IEnumerator Damage(float delay)
    {
        isDamage = true;
        animator.SetBool("isDamage", isDamage);

        // Save the original position before the delay
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
        transparentColor.a = 0.5f; // Adjust the alpha value to your desired transparency level

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