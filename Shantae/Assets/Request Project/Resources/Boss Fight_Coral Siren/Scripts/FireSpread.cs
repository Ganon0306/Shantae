using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    Transform coralSiren_Back;
    Transform coralSiren_Front;
    Animator coralSiren_Back_Animator;
    Animator coralSiren_Front_Animator;

    Vector2 coralSiren_Front_OriginPosition = default;

    float upSpeed = 13f;
    float fallSpeed = 16f;

    private bool firstDestination = default;
    private bool secondDestination = default;

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
        coralSiren_Front_OriginPosition = coralSiren_Front.position;
    }

    // Update is called once per frame
    void Update()
    {
        // �� Coral Siren�� ���
        if (CoralSirenMoving.fireSpread == true && firstDestination == false)
        {
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

        // �� Coral Siren�� �ϰ�
        if (CoralSirenMoving.fireSpread == true && firstDestination == true)
        {
            // �� Coral Siren�� �߶�
            coralSiren_Front.position = Vector2.MoveTowards(coralSiren_Front.position,
                new Vector2(coralSiren_Back.position.x, coralSiren_Front_OriginPosition.y),
                fallSpeed * Time.deltaTime);

            // �浹 ��?
        }

      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ���� Coral Siren�� ���� �浹�ϸ� 
        if (collision.gameObject.CompareTag("Ground"))
        {
            firstDestination = false;

            StartCoroutine(Grounded());
            
        }
    }

    IEnumerator Grounded()
    {
        coralSiren_Front_Animator.SetBool("Fire", true);
        yield return new WaitForSeconds(3.0f);
    }

}
