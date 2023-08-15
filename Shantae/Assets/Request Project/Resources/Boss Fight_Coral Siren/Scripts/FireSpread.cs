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
    Transform coralSiren_Back;
    Transform coralSiren_Front;
    Animator coralSiren_Back_Animator;
    Animator coralSiren_Front_Animator;

    Vector2 coralSiren_Back_OriginPosition = default;
    Vector2 coralSiren_Front_OriginPosition = default;

    float upSpeed = 13f;
    float fallSpeed = 16f;

    private bool firstDestination = default;
    private bool secondDestination = default;
    private bool thirdDestination = default;

    public static bool allStop = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (CoralSirenMoving.fireSpread == true && firstDestination == false)
        {
            // �� Coral Siren�� ���
            coralSiren_Back.position = 
                Vector2.MoveTowards(coralSiren_Back.position, 
                new Vector2(coralSiren_Back.position.x, 7f), upSpeed * Time.deltaTime);

            if(coralSiren_Back.position.y >= 7f)
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
            && secondDestination == false)
        {
            // �� Coral Siren�� �߶�
            coralSiren_Front.position = Vector2.MoveTowards(coralSiren_Front.position,
                new Vector2(coralSiren_Back.position.x, coralSiren_Front_OriginPosition.y),
                fallSpeed * Time.deltaTime);
        }

        // ���� Coral Siren�� ���� �浹�ϸ� 
        if (CoralSirenMoving.fireSpread == true && 
            FrontGrounded.coralSiren_Front_Grounded == true)
        {
            StartCoroutine(Grounded());

            FrontGrounded.coralSiren_Front_Grounded = false;
        }

        // ���� Coral Siren�� ���� �ö󰡶�� ��ȣ�� �޾Ҵٸ�
        if (CoralSirenMoving.fireSpread == true && secondDestination == true
            && thirdDestination == false)
        {
            coralSiren_Front.position = Vector2.MoveTowards(coralSiren_Front.position,
                new Vector2(coralSiren_Front.position.x, 13f),
                fallSpeed * Time.deltaTime);

            // ���� ���� Coral Siren�� ���� ���� �����ߴٸ�
            if(coralSiren_Front.position.y >= 13f)
            {
                coralSiren_Front_Animator.SetBool("Go Back", false);

                // Ǯ�� ����
                coralSiren_Front.position = coralSiren_Front_OriginPosition;

                thirdDestination = true;
            }
        }

        if (CoralSirenMoving.fireSpread == true && thirdDestination == true)
        {
            // ���� Coral Siren�� ������ ����
            coralSiren_Back.position = Vector2.MoveTowards(coralSiren_Back.position,
                    coralSiren_Back_OriginPosition, fallSpeed * Time.deltaTime);

            // ���� ����
            if(Vector2.Distance
                (coralSiren_Back.position, coralSiren_Back_OriginPosition) <= 0.1f)
            {
                coralSiren_Back_Animator.SetBool("Fire Spread", true);
                StartCoroutine(NextGrounded());
            }
        }

        if(allStop == true)
        {
            CoralSirenMoving.fireSpread = false;

            StopCoroutine(Grounded());
            StopCoroutine(NextGrounded());

            firstDestination = false;
            secondDestination = false;
            thirdDestination = false;

            allStop = false;
        }
    }

    IEnumerator Grounded()
    {
        // �� ���� ���� 
        coralSiren_Front_Animator.SetBool("Fire", true);
        yield return new WaitForSeconds(4.0f);

        // ���� Coral Siren�� ���� �÷�������
        coralSiren_Front_Animator.SetBool("Fire", false);
        coralSiren_Front_Animator.SetBool("Go Back", true);

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
