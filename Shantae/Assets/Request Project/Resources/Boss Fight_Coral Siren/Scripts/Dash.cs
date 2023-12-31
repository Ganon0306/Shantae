using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보스 Coral Siren의 대시 공격 클래스. 플레이어가 맵 좌측에 있다면 우측으로, 맵 우측에 있다면
/// 좌측으로 공격을 시작한다.
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

    // 보스의 대시 방향 결정
    private bool directionCheck = false;
    private bool alreadyRun = false;
    private bool animationFinish = false;

    private bool getFirstDestination = false;
    private bool getSecondDestination = false;
    private bool getThirdDestination = false;

    private bool bossGetGoal = false;

    private ParticleSystem leftDashParticle;
    private ParticleSystem rightDashParticle;
    private ParticleSystem leftDashParticle_Front;
    private ParticleSystem rightDashParticle_Front;

    private CapsuleCollider2D dashCollider;

    // Start is called before the first frame update
    void Start()
    {
        // 뒤에 위치한 Coral Siren
        coralSiren_Back = FindObjectOfType<CoralSirenMoving>().transform;
        Debug.Assert(coralSiren_Back != null);
        // 앞에 위치한 Coral Siren
        coralSiren_Front = GameObject.Find("Coral Siren_Front").transform;
        Debug.Assert(coralSiren_Front != null);

        // 뒤 Coral Siren의 원래 위치
        coralSiren_Back_OriginPosition = coralSiren_Back.position;

        // 뒤 Coral Siren의 목적지
        coralSiren_Back_RightDestination =
            new Vector2(11f, coralSiren_Back.position.y);
        coralSiren_Back_RightDestination_LeftVer =
            new Vector2(-11f, coralSiren_Back.position.y);
        // 앞 Coral Siren의 목적지
        coralSiren_Front_LeftDestination =
            new Vector2(-12f, coralSiren_Front.position.y);
        coralSiren_Front_LeftDestination_LeftVer =
            new Vector2(12f, coralSiren_Front.position.y);

        // 뒤 Coral Siren의 애니메이터
        coralSiren_Back_Animator =
            FindObjectOfType<CoralSirenMoving>().GetComponent<Animator>();

        // 앞 Coral Siren의 애니메이터
        coralSiren_Front_Animator =
            GameObject.Find("Coral Siren_Front").GetComponent<Animator>();

        player = GameObject.FindWithTag("Player").transform;

        // 좌측으로 대시할 때 파티클 시스템_뒤
        leftDashParticle = coralSiren_Back.GetChild(2).GetComponent<ParticleSystem>();
        Debug.Assert(leftDashParticle != null);
        // 우측으로 대시할 때 파티클 시스템_뒤
        rightDashParticle = coralSiren_Back.GetChild(3).GetComponent<ParticleSystem>();
        Debug.Assert(rightDashParticle != null);
        // 좌측으로 대시할 때 파티클 시스템_앞
        leftDashParticle_Front = 
            coralSiren_Front.GetChild(0).GetComponent<ParticleSystem>();
        // 우측으로 대시할 때 파티클 시스템_앞
        rightDashParticle_Front =
            coralSiren_Front.GetChild(1).GetComponent<ParticleSystem>();

        dashCollider = coralSiren_Front.GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (CoralSirenMoving.dash == true && directionCheck == false)
        {
            Debug.Log("대시 진행");

            realPlayerPosition = player.position.x;

            directionCheck = true;
        }

        // 우측 대시
        if (CoralSirenMoving.dash == true &&  directionCheck == true && realPlayerPosition < 0)
        {
            if (CoralSirenMoving.dash == true && animationFinish == false)
            {
                StartCoroutine(RightAnimation());
            }

            if (CoralSirenMoving.dash == true && getFirstDestination == false
                && animationFinish == true)
            {
                StopCoroutine(RightAnimation());

                // 뒤에 위치한 보스가 오른쪽으로 이동
                coralSiren_Back.position = Vector2.MoveTowards
                    (coralSiren_Back.position, coralSiren_Back_RightDestination,
                    moveSpeed * Time.deltaTime);

                // 뒤에 위치한 보스가 이동을 완료하면
                if (Vector2.Distance
                    (coralSiren_Back.position, coralSiren_Back_RightDestination) <= 0.1f)
                {
                    getFirstDestination = true;

                    // 뒤의 보스는 다음 출발지로 이동
                    coralSiren_Back.position =
                        new Vector2(-12f, coralSiren_Back.transform.position.y);

                    Debug.Log("Dash 애니메이션에 접근했는가");
                    // 앞의 보스는 이동 전 준비
                    coralSiren_Front.GetComponent<SpriteRenderer>().flipX = true;
                    coralSiren_Front_Animator.SetBool("Front Dash", true);

                    getSecondDestination = true;
                }
            }

            if (CoralSirenMoving.dash == true && getSecondDestination == true)
            {
                // 앞에 위치한 보스가 왼쪽으로 이동
                leftDashParticle_Front.Play();

                coralSiren_Front.position = Vector2.MoveTowards
                    (coralSiren_Front.position, coralSiren_Front_LeftDestination,
                    moveSpeed * Time.deltaTime);

                // 앞에 위치한 보스가 이동을 완료하면
                if (Vector2.Distance
                    (coralSiren_Front.position, coralSiren_Front_LeftDestination) <= 0.1f)
                {
                    leftDashParticle_Front.Stop();

                    getSecondDestination = false;
                    getThirdDestination = true;
                }
            }

            if (CoralSirenMoving.dash == true && getThirdDestination == true)
            {
                // 뒤 보스 이동 시작
                coralSiren_Back.position = Vector2.MoveTowards
                (coralSiren_Back.position, coralSiren_Back_OriginPosition,
                moveSpeed * Time.deltaTime);

                // 뒤의 보스가 목적지에 도달하면
                if (Vector2.Distance
                (coralSiren_Back.position, coralSiren_Back_OriginPosition) <= 0.1f)
                {
                    getThirdDestination = false;

                    coralSiren_Back_Animator.SetBool("Go Dash", false);
                    bossGetGoal = true;

                    // 앞의 보스는 다음 출발지로 이동
                    coralSiren_Front.position =
                        new Vector2(12f, coralSiren_Front.transform.position.y);
                    coralSiren_Front.GetComponent<SpriteRenderer>().flipX = false;
                    coralSiren_Front_Animator.SetBool("Front Dash", false);
                }
            }

            // 마지막으로 뒤의 Coral Siren이 도착하면
            if (CoralSirenMoving.dash == true && bossGetGoal == true)
            {
                // FX Off
                coralSiren_Back.transform.GetChild(1).gameObject.SetActive(false);
                // 파티클 Off
                rightDashParticle.Stop();

                bossGetGoal = false;

                directionCheck = false;
                alreadyRun = false;
                getFirstDestination = false;

                CoralSirenMoving.dash = false;
                animationFinish = false;
                CoralSirenMoving.secondPatternDone = true;
            }
        }

        // 좌측 대시
        if (CoralSirenMoving.dash == true && directionCheck == true && realPlayerPosition >= 0)
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

                    // 뒤에 위치한 보스가 왼쪽으로 이동
                    coralSiren_Back.position = Vector2.MoveTowards
                        (coralSiren_Back.position, coralSiren_Back_RightDestination_LeftVer,
                        moveSpeed * Time.deltaTime);

                    // 뒤에 위치한 보스가 이동을 완료하면
                    if (Vector2.Distance
                        (coralSiren_Back.position,
                        coralSiren_Back_RightDestination_LeftVer) <= 0.1f)
                    {
                        getFirstDestination = true;

                        // 뒤의 보스는 다음 출발지로 이동
                        coralSiren_Back.position =
                            new Vector2(12f, coralSiren_Back.transform.position.y);

                        Debug.Log("대시 애니메이션 켜짐");

                        // 앞의 보스는 이동 전 준비
                        coralSiren_Front_Animator.SetBool("Front Dash", true);

                        // 앞의 보스는 첫번째 출발지로 이동
                        coralSiren_Front.position =
                            new Vector2(-12f, coralSiren_Front.transform.position.y);

                        getSecondDestination = true;
                    }
                }

                if (CoralSirenMoving.dash == true && getSecondDestination == true)
                {
                    // 앞에 위치한 보스가 오른쪽으로 이동
                    coralSiren_Front.position = Vector2.MoveTowards
                        (coralSiren_Front.position, coralSiren_Front_LeftDestination_LeftVer,
                        moveSpeed * Time.deltaTime);

                    rightDashParticle_Front.Play();

                    // 앞에 위치한 보스가 이동을 완료하면
                    if (Vector2.Distance
                        (coralSiren_Front.position, coralSiren_Front_LeftDestination_LeftVer)
                        <= 0.1f)
                    {
                        rightDashParticle_Front.Stop();

                        getSecondDestination = false;
                        getThirdDestination = true;
                    }
                }

                if (CoralSirenMoving.dash == true && getThirdDestination == true)
                {
                    // 뒤 보스 이동 시작
                    coralSiren_Back.position = Vector2.MoveTowards
                    (coralSiren_Back.position, coralSiren_Back_OriginPosition,
                    moveSpeed * Time.deltaTime);

                    // 뒤의 보스가 목적지에 도달하면
                    if (Vector2.Distance
                    (coralSiren_Back.position, coralSiren_Back_OriginPosition) <= 0.1f)
                    {
                        getThirdDestination = false;

                        coralSiren_Back_Animator.SetBool("Go Dash", false);
                        bossGetGoal = true;
                    }
                }

                // 마지막으로 뒤의 Coral Siren이 도착하면
                if (CoralSirenMoving.dash == true && bossGetGoal == true)
                {
                    // FX Off
                    coralSiren_Back.transform.GetChild(0).gameObject.SetActive(false);
                    // 파티클 Off
                    leftDashParticle.Stop();

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

        // Coral Siren이 좌측으로 향함
        IEnumerator RightAnimation()
        {
            coralSiren_Back.GetComponent<SpriteRenderer>().flipX = false;

            coralSiren_Back.GetComponent<Animator>().SetBool("Ready Dash", true);

            // 뒤 FX 효과 ON
            coralSiren_Back.transform.GetChild(1).gameObject.SetActive(true);

            yield return new WaitForSeconds(1.7f);
            coralSiren_Back.GetComponent<Animator>().SetBool("Ready Dash", false);

            coralSiren_Back.GetComponent<Animator>().SetBool("Go Dash", true);
            // 파티클 Play
            rightDashParticle.Play();

            animationFinish = true;
        }

        // Coral Siren이 우측으로 향함
        IEnumerator LeftAnimation()
        {
            coralSiren_Back.GetComponent<SpriteRenderer>().flipX = true;

            coralSiren_Back.GetComponent<Animator>().SetBool("Ready Dash", true);

            // 뒤 FX 효과 ON
            coralSiren_Back.transform.GetChild(0).gameObject.SetActive(true);

            yield return new WaitForSeconds(1.7f);
            coralSiren_Back.GetComponent<Animator>().SetBool("Ready Dash", false);

            coralSiren_Back.GetComponent<Animator>().SetBool("Go Dash", true);
            // 파티클 Play
            leftDashParticle.Play();

            animationFinish = true;
        }
    }
}
