using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    private GameObject coralSiren_Parent;
    public static bool coralDamaged = false;

    private void Awake()
    {
        // �θ� ������Ʈ Coral Siren�� ���� ������Ʈ
        coralSiren_Parent = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            Debug.Log("������ ���� ����!");

            coralDamaged = true;
        }
    }
}
