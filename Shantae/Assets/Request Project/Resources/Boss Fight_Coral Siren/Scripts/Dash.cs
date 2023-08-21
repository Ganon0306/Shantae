using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� Coral Siren�� ��� ���� Ŭ����. �÷��̾ �� ������ �ִٸ� ��������, �� ������ �ִٸ�
/// �������� ������ �����Ѵ�.
/// </summary>

public class Dash : MonoBehaviour
{
    Transform coralSiren_Back;
    Transform coralSiren_Front;
    Vector2 coralSiren_Back_OriginPosition = default;
    Vector2 coralSiren_Back_RightDestination = default;
    Vector2 coralSiren_Front_LeftDestination = default;
    Vector2 coralSiren_Back_RightDestination_LeftVer = default;
    Vector2 coralSiren_Front_LeftDestination_LeftVer = default;

    private Transform player;
    public static float realPlayerPosition = default;

    float moveSpeed = 15f;

    Animator coralSiren_Back_Animator;
    Animator coralSiren_Front_Animator;

    // ������ ��� ���� ����
    private bool directionCheck = false;
    private bool alreadyRun = false;
    private bool animationFinish = false;

    private bool getFirstDestination = false;
    private bool getSecondDestination = false;
    private bool getThirdDestination = false;

    private bool bossGetGoal = false;

    // Start is called before the first frame update
    void Start()
    {
        // �ڿ� ��ġ�� Coral Siren
        coralSiren_Back = FindObjectOfType<CoralSirenMoving>().transform;
        Debug.Assert(coralSiren_Back != null);
        // �տ� ��ġ�� Coral Siren
        coralSiren_Front = GameObject.Find("Coral Siren_Front").transform;
        Debug.Assert(coralSiren_Front != null);

        // �� Coral Siren�� ���� ��ġ
        coralSiren_Back_OriginPosition = coralSiren_Back.position;

        // �� Coral Siren�� ������
        coralSiren_Back_RightDestination =
            new Vector2(11f, coralSiren_Back.position.y);
        coralSiren_Back_RightDestination_LeftVer =
            new Vector2(-11f, coralSiren_Back.position.y);
        // �� Coral Siren�� ������
        coralSiren_Front_LeftDestination =
            new Vector2(-12f, coralSiren_Front.position.y);
        coralSiren_Front_LeftDestination_LeftVer =
            new Vector2(12f, coralSiren_Front.position.y);

        // �� Coral Siren�� �ִϸ�����
        coralSiren_Back_Animator =
            FindObjectOfType<CoralSirenMoving>().GetComponent<Animator>();

        // �� Coral Siren�� �ִϸ�����
        coralSiren_Front_Animator =
            GameObject.Find("Coral Siren_Front").GetComponent<Animator>();

        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (CoralSirenMoving.dash == true && directionCheck == false)
        {
            realPlayerPosition = player.position.x;

            directionCheck = true;
        }

        // ���� ���
        if (realPlayerPosition < 0)
        {
            if (CoralSirenMoving.dash == true && animationFinish == false)
            {
                StartCoroutine(RightAnimation());
            }

            if (CoralSirenMoving.dash == true && getFirstDestination == false
                && animationFinish == true)
            {
                StopCoroutine(RightAnimation());

                // �ڿ� ��ġ�� ������ ���������� �̵�
                coralSiren_Back.position = Vector2.MoveTowards
                    (coralSiren_Back.position, coralSiren_Back_RightDestination,
                    moveSpeed * Time.deltaTime);

                // �ڿ� ��ġ�� ������ �̵��� �Ϸ��ϸ�
                if (Vector2.Distance
                    (coralSiren_Back.position, coralSiren_Back_RightDestination) <= 0.1f)
                {
                    getFirstDestination = true;

                    // ���� ������ ���� ������� �̵�
                    coralSiren_Back.position =
                        new Vector2(-12f, coralSiren_Back.transform.position.y);

                    // ���� ������ �̵� �� �غ�
                    coralSiren_Front.GetComponent<SpriteRenderer>().flipX = true;
                    coralSiren_Front_Animator.SetBool("Front Dash", true);

                    getSecondDestination = true;
                }
            }

            if (CoralSirenMoving.dash == true && getSecondDestination == true)
            {
                // �տ� ��ġ�� ������ �������� �̵�
                coralSiren_Front.position = Vector2.MoveTowards
                    (coralSiren_Front.position, coralSiren_Front_LeftDestination,
                    moveSpeed * Time.deltaTime);

                // �տ� ��ġ�� ������ �̵��� �Ϸ��ϸ�
                if (Vector2.Distance
                    (coralSiren_Front.position, coralSiren_Front_LeftDestination) <= 0.1f)
                {
                    getSecondDestination = false;
                    getThirdDestination = true;
                }
            }

            if (CoralSirenMoving.dash == true && getThirdDestination == true)
            {
                // �� ���� �̵� ����
                coralSiren_Back.position = Vector2.MoveTowards
                (coralSiren_Back.position, coralSiren_Back_OriginPosition,
                moveSpeed * Time.deltaTime);

                // ���� ������ �������� �����ϸ�
                if (Vector2.Distance
                (coralSiren_Back.position, coralSiren_Back_OriginPosition) <= 0.1f)
                {
                    getThirdDestination = false;

                    coralSiren_Back_Animator.SetBool("Go Dash", false);
                    bossGetGoal = true;

                    // ���� ������ ���� ������� �̵�
                    coralSiren_Front.position =
                        new Vector2(12f, coralSiren_Front.transform.position.y);
                    coralSiren_Front.GetComponent<SpriteRenderer>().flipX = false;
                    coralSiren_Front_Animator.SetBool("Front Dash", false);
                }
            }

            // ���������� ���� Coral Siren�� �����ϸ�
            if (CoralSirenMoving.dash == true && bossGetGoal == true)
            {
                bossGetGoal = false;

                directionCheck = false;
                alreadyRun = false;
                getFirstDestination = false;

                CoralSirenMoving.dash = false;
                animationFinish = false;
                CoralSirenMoving.secondPatternDone = true;
            }
        }

        // ���� ���
        if (realPlayerPosition >= 0)
        {
            if (realPlayerPosition > 0)
            {
                if (CoralSirenMoving.dash == true && animationFinish == false)
                {
                    StartCoroutine(LeftAnimation());
                }

                if (CoralSirenMoving.dash == true && getFirstDestination == false
                    && animationFinish == true)
                {
                    StopCoroutine(LeftAnimation());

                    // �ڿ� ��ġ�� ������ �������� �̵�
                    coralSiren_Back.position = Vector2.MoveTowards
                        (coralSiren_Back.position, coralSiren_Back_RightDestination_LeftVer,
                        moveSpeed * Time.deltaTime);

                    // �ڿ� ��ġ�� ������ �̵��� �Ϸ��ϸ�
                    if (Vector2.Distance
                        (coralSiren_Back.position,
                        coralSiren_Back_RightDestination_LeftVer) <= 0.1f)
                    {
                        getFirstDestination = true;

                        // ���� ������ ���� ������� �̵�
                        coralSiren_Back.position =
                            new Vector2(12f, coralSiren_Back.transform.position.y);

                        // ���� ������ �̵� �� �غ�
                        coralSiren_Front_Animator.SetBool("Front Dash", true);

                        // ���� ������ ù��° ������� �̵�
                        coralSiren_Front.position =
                            new Vector2(-12f, coralSiren_Front.transform.position.y);

                        getSecondDestination = true;
                    }
                }

                if (CoralSirenMoving.dash == true && getSecondDestination == true)
                {
                    // �ڿ� ��ġ�� ������ ���������� �̵�
                    coralSiren_Front.position = Vector2.MoveTowards
                        (coralSiren_Front.position, coralSiren_Front_LeftDestination_LeftVer,
                        moveSpeed * Time.deltaTime);

                    // �ڿ� ��ġ�� ������ �̵��� �Ϸ��ϸ�
                    if (Vector2.Distance
                        (coralSiren_Front.position, coralSiren_Front_LeftDestination_LeftVer)
                        <= 0.1f)
                    {
                        getSecondDestination = false;
                        getThirdDestination = true;
                    }
                }

                if (CoralSirenMoving.dash == true && getThirdDestination == true)
                {
                    // �� ���� �̵� ����
                    coralSiren_Back.position = Vector2.MoveTowards
                    (coralSiren_Back.position, coralSiren_Back_OriginPosition,
                    moveSpeed * Time.deltaTime);

                    // ���� ������ �������� �����ϸ�
                    if (Vector2.Distance
                    (coralSiren_Back.position, coralSiren_Back_OriginPosition) <= 0.1f)
                    {
                        getThirdDestination = false;

                        coralSiren_Back_Animator.SetBool("Go Dash", false);
                        bossGetGoal = true;
                    }
                }

                // ���������� ���� Coral Siren�� �����ϸ�
                if (CoralSirenMoving.dash == true && bossGetGoal == true)
                {
                    coralSiren_Back.transform.GetChild(0).gameObject.SetActive(false);

                    bossGetGoal = false;

                    directionCheck = false;
                    alreadyRun = false;
                    getFirstDestination = false;

                    CoralSirenMoving.dash = false;
                    animationFinish = false;
                    CoralSirenMoving.secondPatternDone = true;
                }
            }
        }

        IEnumerator RightAnimation()
        {
            coralSiren_Back.GetComponent<SpriteRenderer>().flipX = false;

            coralSiren_Back.GetComponent<Animator>().SetBool("Ready Dash", true);

            yield return new WaitForSeconds(1.7f);
            coralSiren_Back.GetComponent<Animator>().SetBool("Ready Dash", false);

            coralSiren_Back.GetComponent<Animator>().SetBool("Go Dash", true);

            animationFinish = true;
        }

        IEnumerator LeftAnimation()
        {
            coralSiren_Back.GetComponent<SpriteRenderer>().flipX = true;

            coralSiren_Back.GetComponent<Animator>().SetBool("Ready Dash", true);

            // �� FX ȿ�� ON
            coralSiren_Back.transform.GetChild(0).gameObject.SetActive(true);

            yield return new WaitForSeconds(1.7f);
            coralSiren_Back.GetComponent<Animator>().SetBool("Ready Dash", false);

            coralSiren_Back.GetComponent<Animator>().SetBool("Go Dash", true);

            animationFinish = true;
        }
    }
}
