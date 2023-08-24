using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// FireSpread ��ũ��Ʈ�� ������, ���� Coral Siren�� �ٴڰ� �浹�ߴ��� �����ϴ� Ŭ����
/// </summary>

public class FrontGrounded : MonoBehaviour
{
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
        if (collision.gameObject.CompareTag("SandStep") == true ||
            (collision.gameObject.CompareTag("SandPiece")) == true)
        {
            coralSiren_Front_Sanded = true;
        }
    }
}
