using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    Transform coralSiren_Back;
    Transform coralSiren_Front;
    Vector2 coralSiren_Back_OriginPosition = default;
    Vector2 coralSiren_Back_RightDestination = default;
    Vector2 coralSiren_Front_LeftDestination = default;

    float moveSpeed = 15f;
    float getDistance = default;

    Animator coralSiren_Back_Animator;
    Animator coralSiren_Front_Animator;

    private bool getFirstDestination = false;
    private bool getSecondDestination = false;

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
        // �� Coral Siren�� ������
        coralSiren_Front_LeftDestination =
            new Vector2(-12f, coralSiren_Front.position.y);

        // �� Coral Siren�� �ִϸ�����
        coralSiren_Back_Animator =
            FindObjectOfType<CoralSirenMoving>().GetComponent<Animator>();

        // �� Coral Siren�� �ִϸ�����
        coralSiren_Front_Animator =
            GameObject.Find("Coral Siren_Front").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CoralSirenMoving.dash == true && getFirstDestination == false)
        {
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
                    new Vector2(-11f, coralSiren_Back.transform.position.y);

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
        }

        // �տ� ��ġ�� ������ �̵��� �Ϸ��ϸ�
        if (CoralSirenMoving.dash == true && Vector2.Distance
            (coralSiren_Front.position, coralSiren_Front_LeftDestination) <= 0.1f)
        {
            //getSecondDestination = false;

            coralSiren_Front_Animator.SetBool("Front Dash", false);

            coralSiren_Back.position = Vector2.MoveTowards
            (coralSiren_Back.position, coralSiren_Back_OriginPosition,
            moveSpeed * Time.deltaTime);

            if (Vector2.Distance
            (coralSiren_Back.position, coralSiren_Back_OriginPosition) <= 0.1f)
            {
                coralSiren_Back_Animator.SetBool("Go Dash", false);
            }
        }
    }
}
