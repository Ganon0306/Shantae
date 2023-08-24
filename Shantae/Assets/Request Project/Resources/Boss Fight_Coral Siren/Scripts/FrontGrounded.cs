using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// FireSpread ��ũ��Ʈ�� ������, ���� Coral Siren�� �ٴڰ� �浹�ߴ��� �����ϴ� Ŭ����
/// </summary>

public class FrontGrounded : MonoBehaviour
{
    public static bool coralSiren_Front_Grounded = false;
    public static bool coralSiren_Front_Sanded = false;
    private bool justOne = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ���� Coral Siren�� �𷡿� �浹�ϸ� 
        if (collision.gameObject.CompareTag("SandStep") ||
            (collision.gameObject.CompareTag("SandPiece")) && coralSiren_Front_Grounded == false)
        {
            Debug.Log("�𷡿� �浹!");
            coralSiren_Front_Grounded = true;
        }

        // ���� ���� Coral Siren�� �׳� ���� �浹�ϸ�
        if (collision.gameObject.CompareTag("Ground") && coralSiren_Front_Sanded == false)
        {
            Debug.Log("���� �浹!");
            coralSiren_Front_Sanded = true;
        }

    }
}
