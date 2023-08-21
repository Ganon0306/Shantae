using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBreak : MonoBehaviour
{
    public UnityEngine.Transform centerPoint; // ȸ�� �߽���
    public float rotationSpeed; // ȸ�� �ӵ� 
    public bool isClockwise; // �ð���� ȸ�� ����

    private float floatSpeed = 200.0f; // ������Ʈ�� ���� �ö󰡴� �ӵ�

    private Rigidbody2D rb;

    private bool isTimerStarted = false;
    private float startTime;
    private float elapsedTime;
    //private GameObject unBreak;
    //private Acceesory acceesory;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 7.0f);
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
        if (elapsedTime < 0.1f)
        {
            Vector2 newPosition = rb.position + Vector2.up * floatSpeed * Time.deltaTime;
            rb.MovePosition(newPosition);
        }
        else
        {
            rb.gravityScale = 0.5f;
        }

        if (isClockwise)
        {
            transform.RotateAround(centerPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);


        }
        else if (!isClockwise)
        {
            transform.RotateAround(centerPoint.position, Vector3.back, rotationSpeed * Time.deltaTime);
        }

    }
}
