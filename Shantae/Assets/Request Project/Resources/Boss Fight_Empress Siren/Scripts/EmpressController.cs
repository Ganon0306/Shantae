using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Empress Siren�� �й� ������ �ٷ�� ��ũ��Ʈ. "PlayerAttack" �±׿� �浹�ϸ� 
/// Empress Siren�� HP�� ����.
/// </summary>
/// 
public class EmpressController : MonoBehaviour
{
    // Empress Siren�� HP
    public static float empressHP = default;

    private void Start()
    {
        // Empress Siren ���� ������Ʈ�� !null���� Ȯ��
        Debug.Assert(this.gameObject != null);

        empressHP = 100f;
    }

    private void Update()
    {
        // Empress Siren�� �й� Ȯ��
        if(empressHP <= 0)
        {
            // EmpressMoving �ڷ�ƾ ���� �޼��� �߰�
            StopCoroutine(EmpressMoving.instance.RandomMoving());

            PlayerWin();
        }
    }

    private void PlayerWin()
    {
        

    }

    private void NextMegaEmpress()
    {
        Debug.Log("Empress Siren���� 2�������� �̵�");
        // �ε��� ���� ���� �������� MegaEmpress Siren �ε��ϱ�. 
    }
}
