using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopBackBullet : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform targetTransform; // �ٸ� ������Ʈ�� Transform

    private void Start()
    {
        Debug.Log("�̵� ����");
        // ���� Ǯ�� ���ư��ٸ� �ش� ��ũ��Ʈ�� ������� �ʵ��� �ϱ�. 
        targetTransform = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        // �ٸ� ������Ʈ�� ���ϴ� ����
        Vector3 targetDirection = targetTransform.position - transform.position;
        targetDirection.Normalize();

        // ������Ʈ�� �ٸ� ������Ʈ�� ���ϴ� �������� ȸ��
        float targetAngle =
            Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, targetRotation, 1.5f);

        // ������Ʈ�� �ٶ󺸴� �������� �̵�
        Vector3 forwardDirection = transform.right;
        transform.position += forwardDirection * moveSpeed * Time.deltaTime;
    }
}
