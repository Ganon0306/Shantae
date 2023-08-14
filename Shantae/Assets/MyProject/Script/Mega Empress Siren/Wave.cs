using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Wave : MonoBehaviour
{
    public UnityEngine.Transform centerPoint; // ȸ�� �߽���
    public float rotationSpeed; // ȸ�� �ӵ� (��/��)
    private float rotationTime = 0f; // ȸ�� �ð�
    private bool isClockwise = true; // �ð���� ȸ�� ����
    public float rotating;
    float speed;
    public float wait;

    private void Start()
    {
        speed = rotationSpeed;
    }
    private void Update()
    {
        rotationTime += Time.deltaTime;

        // �ð���� ȸ��
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        if (isClockwise)
        {
            
            transform.RotateAround(centerPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);

            if (rotationTime > rotating)
            {
                rotationSpeed = 0;
                yield return new WaitForSeconds(wait);         
                    rotationSpeed = speed;
                rotationTime = 0f;
                isClockwise = false;
            }

        }
        // �ݽð���� ȸ��
        else if (!isClockwise)
        {
            
            transform.RotateAround(centerPoint.position, Vector3.back, rotationSpeed * Time.deltaTime);

            if (rotationTime > rotating)
            {
                rotationSpeed = 0;
                yield return new WaitForSeconds(wait);
                rotationSpeed = speed;

                rotationTime = 0f;

                isClockwise = true;
            }

        }
    }
}
