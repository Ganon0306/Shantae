using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            NextMegaEmpress();
        }
    }

    private void NextMegaEmpress()
    {
        Debug.Log("Empress Siren���� 2�������� �̵�");
    }
}
