using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Coral Siren�� �ڿ��� ������ �翷���� ���� �߻��ϴ� Ŭ����
/// </summary>

public class FireSpread : MonoBehaviour
{
    #region Coral Siren�� ������ ����
    Transform coralSiren_Back;
    Transform coralSiren_Front;
    Animator coralSiren_Back_Animator;
    Animator coralSiren_Front_Animator;

    Vector2 coralSiren_Back_OriginPosition = default;
    Vector2 coralSiren_Front_OriginPosition = default;

    float moveSpeed = 5f;
    float upSpeed = 13f;
    float fallSpeed = 16f;

    private bool moveDone = false;
    private bool firstDestination = false;
    private bool secondDestination = false;
    private bool thirdDestination = false;
    private bool alreadyChoice = false;

    public static bool allStop = false;

    private float firstFirePosition = default;
    private float secondFirePosition = default;
    private float thirdFirePosition = default;
    private float fourthFirePosition = default;
    private float whatFirePosition = default;

    private int selectedRandom = default;
    private float coralPositionX = default;
    private float coralPositionY = default;
    #endregion

    #region ���� �߻��ϴ� ����
    private GameObject fireBallPrefab;
    private GameObject[] fireBalls;
    private GameObject fireBall_Left;
    private GameObject fireBall_Right;

    private GameObject mainHole;
    public GameObject hole1;
    public GameObject hole2;
    public GameObject hole3;
    public GameObject hole4;

    private Vector2 poolPosition_fireBalls = new Vector2(-2, 10f);

    private bool launchFireBalls = false;
    private float fireBallSpeed = 10f;

    // bool�� Ȱ��ȭ ���� �浹�� üũ�ϴ� ���� ���� ����.
    public static bool collisionCheck = false;
    #endregion


    private int childCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        // �� Coral Siren
        coralSiren_Back = FindObjectOfType<CoralSirenMoving>().transform;
        Debug.Assert(coralSiren_Back != null);
        // �� Coral Siren
        coralSiren_Front = GameObject.Find("Coral Siren_Front").transform;
        Debug.Assert(coralSiren_Front != null);

        // �� Coral Siren Animator
        coralSiren_Back_Animator =
            FindObjectOfType<CoralSirenMoving>().GetComponent<Animator>();
        // �� Coral Siren Animator
        coralSiren_Front_Animator =
            GameObject.Find("Coral Siren_Front").GetComponent<Animator>();

        // �� Coral Siren�� Origin Position
        coralSiren_Back_OriginPosition = coralSiren_Back.position;
        // �� Coral Siren�� Origin Position
        coralSiren_Front_OriginPosition = coralSiren_Front.position;

        firstFirePosition = -5.8f;
        secondFirePosition = -1.91f;
        thirdFirePosition = 1.88f;
        fourthFirePosition = 5.79f;


        // �Ҳ� ���ݱ�
        fireBalls = new GameObject[2];

        fireBallPrefab = Resources.Load<GameObject>
            ("Boss Fight_Coral Siren/Prefabs/Fire Spread Attacks");
        Debug.Assert(fireBallPrefab != null);

        for(int i = 0; i < 2; i++)
        {
            fireBalls[i] = Instantiate(fireBallPrefab, poolPosition_fireBalls, 
                Quaternion.identity);
            Debug.Assert(fireBalls[i] != null);
        }

        fireBalls[1].transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;

        fireBall_Right = fireBalls[0];
        fireBall_Left = fireBalls[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (CoralSirenMoving.fireSpread == true && firstDestination == false)
        {
            if (moveDone == false)
            {
                if (alreadyChoice == false)
                {
                    selectedRandom = Random.Range(0, 4);
                    alreadyChoice = true;
                }

                // ���� ��� ���ϱ� (�� ���� ��)
                if (selectedRandom == 0)
                {
                    whatFirePosition = firstFirePosition;
                    mainHole = hole1;
                }
                else if (selectedRandom == 1)
                {
                    whatFirePosition = secondFirePosition;
                    mainHole = hole2;
                }
                else if (selectedRandom == 2)
                {
                    whatFirePosition = thirdFirePosition;
                    mainHole = hole3;
                }
                else if (selectedRandom == 3)
                {
                    whatFirePosition = fourthFirePosition;
                    mainHole = hole4;
                }

                coralSiren_Back_Animator.SetBool("Fire Bomb", true);

                if(whatFirePosition < coralSiren_Back.position.x)
                {
                    coralSiren_Back.GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (whatFirePosition > coralSiren_Back.position.x)
                {
                    coralSiren_Back.GetComponent<SpriteRenderer>().flipX = false;
                }

                // �߻� ��ġ�� �̵�
                coralSiren_Back.position =
                Vector2.MoveTowards(coralSiren_Back.position,
                new Vector2(whatFirePosition, coralSiren_Back.position.y),
                moveSpeed * Time.deltaTime);

                coralPositionX = coralSiren_Back.position.x;
                coralPositionY = coralSiren_Back.position.y;

                // �߻� ��ġ�� �����ϸ�
                if (Mathf.Abs(whatFirePosition - coralPositionX) <= 0.1f)
                {
                    coralSiren_Back_Animator.SetBool("Spread Fire Charge", true);
                    coralSiren_Back_Animator.SetBool("Fire Bomb", false);

                    // ����
                    coralSiren_Back.position =
                        new Vector2(whatFirePosition, coralSiren_Back.position.y);

                    moveDone = true;
                }
            }

            if (moveDone == true)
            {
                // �� Coral Siren�� ���
                coralSiren_Back.position =
                    Vector2.MoveTowards(coralSiren_Back.position,
                    new Vector2(coralSiren_Back.position.x, 7f), upSpeed * Time.deltaTime);
            }

            if (coralSiren_Back.position.y >= 7f && firstDestination == false)
            {
                // �� Coral Siren�� ���� ���� �����ϸ�
                coralSiren_Back_Animator.SetBool("Spread Fire Charge", false);
                coralSiren_Back_Animator.SetBool("Drop", true);

                // �� Coral Siren�� �߶��� ���� ��ǥ �̵�
                coralSiren_Front.position =
                    new Vector2(coralSiren_Back.position.x, 13f);

                firstDestination = true;
            }
        }

        if (CoralSirenMoving.fireSpread == true && firstDestination == true
            && secondDestination == false && collisionCheck == false)
        {
            // �� Coral Siren�� �߶�
            coralSiren_Front.position = Vector2.MoveTowards(coralSiren_Front.position,
                new Vector2(coralSiren_Back.position.x, coralSiren_Front_OriginPosition.y),
                fallSpeed * Time.deltaTime);

            if (Mathf.Abs(coralSiren_Front.position.y - coralSiren_Front_OriginPosition.y) <= 0.01f)
            {
                coralSiren_Front.position = new Vector2
                    (coralSiren_Front.position.x, coralSiren_Front_OriginPosition.y);

                //if (FrontGrounded.coralSiren_Front_Sanded == true) // �𷡿� �浹�ϸ�
                //{
                //    Debug.Log("�� ��� ����");
                //    collisionCheck = true;

                //    StartCoroutine(Sanded());
                //}
                //else if (FrontGrounded.coralSiren_Front_Sanded == false) // �𷡰� ������
                //{
                //    Debug.Log("�� ��� ����");
                //    collisionCheck = true;

                //    StartCoroutine(Grounded());
                //}
                Transform[] childTransforms = mainHole.GetComponentsInChildren<Transform>(true);

                // ù ��° ��Ҵ� �θ� ������Ʈ�̹Ƿ� �����ϰ� ��ȸ
                
                for (int i = 1; i < childTransforms.Length; i++)
                {
                    GameObject childObject = childTransforms[i].gameObject;
                    if (childObject.activeSelf)
                    {
                        childCount += 1;
                    }
                }

                if (childCount == 0) // �𷡰� ������
                {
                    Debug.Log(childCount);

                    Debug.Log("�� ��� ����");
                    collisionCheck = true;

                    StartCoroutine(Grounded());
                }
                else// �𷡿� �浹�ϸ�
                {
                    Debug.Log(childCount);

                    Debug.Log("�� ��� ����");
                    collisionCheck = true;

                    StartCoroutine(Sanded());
                }
                childCount = 0;
                Debug.Log(childCount);

            }
        }

        // �� ���� �̵�
        if (launchFireBalls == true)
        {
            fireBall_Left.transform.Translate
                (Vector2.left * Time.deltaTime * fireBallSpeed);
            fireBall_Right.transform.Translate
                (Vector2.right * Time.deltaTime * fireBallSpeed);

            if (secondDestination == true) // ���� Coral Siren�� �ٽ� ���� ����ϸ�
            {
                launchFireBalls = false;

                fireBall_Left.transform.position = poolPosition_fireBalls;
                fireBall_Right.transform.position = poolPosition_fireBalls;
            }
        }

        // ���� Coral Siren�� ���� �ö󰡶�� ��ȣ�� �޾Ҵٸ�
        if (CoralSirenMoving.fireSpread == true && secondDestination == true
            && thirdDestination == false)
        {
            coralSiren_Front.position = Vector2.MoveTowards(coralSiren_Front.position,
                new Vector2(coralSiren_Front.position.x, 13f),
                fallSpeed * Time.deltaTime);

            // ���� ���� Coral Siren�� ���� ���� �����ߴٸ�
            if (coralSiren_Front.position.y >= 13f)
            {
                coralSiren_Front_Animator.SetTrigger("Fire_GoBack");

                // Ǯ�� ����
                coralSiren_Front.position = coralSiren_Front_OriginPosition;

                thirdDestination = true;
            }
        }

        if (CoralSirenMoving.fireSpread == true && thirdDestination == true)
        {
            // ���� Coral Siren�� ������ ����
            coralSiren_Back.position = Vector2.MoveTowards(coralSiren_Back.position,
                    new Vector2(coralPositionX, coralPositionY),
                    fallSpeed * Time.deltaTime);

            // ���� ����
            if (Mathf.Abs(coralSiren_Back.position.y - coralPositionY) <= 0.1f)
            {
                coralSiren_Back_Animator.SetBool("Fire Spread", true);
                StartCoroutine(NextGrounded());
            }
        }

        if (allStop == true)
        {
            CoralSirenMoving.fireSpread = false;
            CoralSirenMoving.thirdPatternDone = true;

            StopCoroutine(Grounded());
            StopCoroutine(Sanded());
            StopCoroutine(NextGrounded());

            moveDone = false;
            firstDestination = false;
            secondDestination = false;
            thirdDestination = false;
            alreadyChoice = false;
            collisionCheck = false;

            allStop = false;
        }
    }

    IEnumerator Grounded() // ���հŸ�
    {
        coralSiren_Front_Animator.SetBool("Fire_Frail", true);
        // 3�ʰ� ���հŸ�
        yield return new WaitForSeconds(3.0f);
        coralSiren_Front_Animator.SetBool("Fire_Frail", false);

        // Ż�� ��� ���� ���
        yield return new WaitForSeconds
            (coralSiren_Front_Animator.GetCurrentAnimatorStateInfo(0).length);

        coralSiren_Front_Animator.SetBool("Go Back", true);

        secondDestination = true;
    }

    IEnumerator Sanded() // �� ����
    {
        FrontGrounded.coralSiren_Front_Sanded = false;

        // �� ���� ���� 
        coralSiren_Front_Animator.SetBool("Fire", true);

        fireBall_Right.transform.position = new Vector2
            (coralSiren_Front.position.x + 3f, coralSiren_Front.position.y - 1.45f);
        fireBall_Left.transform.position = new Vector2
            (coralSiren_Front.position.x - 3f, coralSiren_Front.position.y - 1.45f);

        launchFireBalls = true;

        yield return new WaitForSeconds
            (coralSiren_Front_Animator.GetCurrentAnimatorClipInfo(0).Length);

        coralSiren_Front_Animator.SetBool("Fire", false);
        secondDestination = true;
    }

    IEnumerator NextGrounded()
    {
        coralSiren_Back_Animator.SetBool("Drop", false);

        // �� Coral Siren�� ���� ��� ���� �ð� 
        yield return new WaitForSeconds(1.5f);

        coralSiren_Back_Animator.SetBool("Spread Fire Charge", false);
        coralSiren_Back_Animator.SetBool("Fire Spread", false);

        allStop = true;
    }
}
