using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FireSpread ��ũ��Ʈ�� ������, ���� Coral Siren�� �ٴڰ� �浹�ߴ��� �����ϴ� Ŭ����
/// </summary>

public class FrontGrounded : MonoBehaviour
{
    public static bool coralSiren_Front_Grounded = false;
    private bool justOne = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ���� Coral Siren�� ���� �浹�ϸ� 
        if (collision.gameObject.CompareTag("Ground"))
        {
            coralSiren_Front_Grounded = true;
        }
    }
}
