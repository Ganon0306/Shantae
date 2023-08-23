using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceesorybreak : MonoBehaviour
{
    public UnityEngine.Transform centerPoint; // ȸ�� �߽���
    public float rotationSpeed; // ȸ�� �ӵ� 
    public bool isClockwise; // �ð���� ȸ�� ����

    public float floatSpeed = 200.0f; // ������Ʈ�� ���� �ö󰡴� �ӵ�

    private Rigidbody2D rb;

    private bool isTimerStarted = false;
    private float startTime;
    private float elapsedTime;
    public GameObject unBreak;
    //private Acceesory acceesory;
    private void Start()
    {
        //acceesory = FindObjectOfType<Acceesory>();

        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5.0f);
        
       
    }

    private void Update()
    {
        if (unBreak == null && !isTimerStarted)
        {
            isTimerStarted = true;
            startTime = Time.time;
        }

        if (isTimerStarted)
        {
            elapsedTime = Time.time - startTime;
        }
        if (elapsedTime < 0.9f)
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
