using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaerController : MonoBehaviour
{

    public float moveSpeed;

    public float jumpForce = 350f;
    private Animator animator = default;

    private bool isRun;
    private bool isDown;
    private bool isDownAndRun;
    private bool isJumping = false;
    private CapsuleCollider capsuleCollider;

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
        Debug.Log(isDown);

        playerAnimation();          // 플레이어가 보여줄 애니메이션

        if(Input.GetKeyDown(KeyCode.Z))
        {
            // 공격
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            // 점프
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
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            boxCollider.size = new Vector2(1.3f, 0.9f);
            boxCollider.offset = new Vector2(0f, -0.7f);
            isDown = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isDown = false;
            isDownAndRun = false;
            boxCollider.size = new Vector2(0.7f, 2.1f);
            boxCollider.offset = new Vector2(0.385f, -0.2f);
        }

    }

    private void playerAnimation()      // 플레이어 지상 애니메이션
    {
        if (!isDown)
        {
            moveSpeed = 5f;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                isRun = true;


                animator.SetBool("Run", isRun);
                Debug.Assert(animator != null);
                if (Input.GetKeyDown (KeyCode.DownArrow))
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

    private void playerAttackAniamtoin()
    {

    }
    private void playerJumpAnimation()
    {

    }
}

//capsuleCollider = GetComponent<CapsuleCollider>();

//// 크기 변경 예시
//Vector3 newSize = new Vector3(1.0f, 2.0f, 1.0f);
//capsuleCollider.radius = newSize.x;
//capsuleCollider.height = newSize.y;
//capsuleCollider.direction = 1; // 0: X축, 1: Y축, 2: Z축

//// 위치 변경 예시
//Vector3 newPosition = new Vector3(0.0f, 1.0f, 0.0f);
//capsuleCollider.center = newPosition;