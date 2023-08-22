using System.Collections;
using System.Collections.Generic;
// === (��ֺ� ����) ���� �׽�Ʈ �� ���� �߻����� ����.
//using Unity.VisualScripting;
// ===
using UnityEngine;
// === (��ֺ� ����) ���� �׽�Ʈ �� ���� �߻����� ����.
//using static Unity.VisualScripting.Metadata;
// ===

public class Spread : MonoBehaviour
{
    
    private float rotationSpeed = 100; // ȸ�� �ӵ� 
    private bool isClockwise; // �ð���� ȸ�� ����

    private bool isTimerStarted = false;
    private float startTime;
    private float elapsedTime;
    private Transform[] children;

    private void Start()
    {
        // �ڽ� ������Ʈ�� ��������
            children = GetComponentsInChildren<Transform>();
        isClockwise = Random.value > 0.5f;

        foreach (Transform child in children)
        {
            if (child != transform) // �θ� ������Ʈ�� ����
            {
                Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.gravityScale = 0.0f; // �߷� ������
                }
            }
        }
    }

    private void Update()
    {

        if (!isTimerStarted)
        {
            isTimerStarted = true;
            startTime = Time.time;
        }

        if (isTimerStarted)
        {
            elapsedTime = Time.time - startTime;
        }

        if (elapsedTime < 0.5f)
        {
            foreach (Transform child in children)
            {
                if (child != transform)
                {
                    Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 randomForce = new Vector2(Random.Range(-0.5f, 0.5f), 0.001f);
                        rb.AddForce(randomForce, ForceMode2D.Impulse);
                    }
                }
            }
        }
        else
        {
            // �ڽ� ������Ʈ�鿡 �߷� ����
            Transform[] children = GetComponentsInChildren<Transform>();

            foreach (Transform child in children)
            {
                if (child != transform) // �θ� ������Ʈ�� ����
                {
                    Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.gravityScale = 1f;
                    }
                }
            }
        }
        Transform centerPoint = transform;
        // ȸ�� ó��
        foreach (Transform child in children)
        {
            if (child != transform && child != null) // �θ� ������Ʈ�� ����
            {
                
                Transform centerPointChild = child; // �ڽ� ������Ʈ�� �߽����� ȸ��
                if (isClockwise)
                {
                    child.RotateAround(centerPointChild.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    child.RotateAround(centerPointChild.position, Vector3.back, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}
