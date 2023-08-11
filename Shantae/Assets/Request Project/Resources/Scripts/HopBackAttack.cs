using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopBackAttack : MonoBehaviour
{
    private GameObject hopBackPrefab;
    private GameObject[] hopBacks;
    private int hopBackCount = 8;
    private Vector2 poolPosition_hopBack = new Vector2(3f, 10.0f);
    private Vector2 firePosition;
    private bool readyRun = false;
    private bool stopRun = false;

    // Start is called before the first frame update
    void Start()
    {
        hopBackPrefab = Resources.Load<GameObject>("Prefabs/HopBack Attack");
        hopBacks = new GameObject[hopBackCount];

        for (int i = 0; i < hopBackCount; i++)
        {
            hopBacks[i] = Instantiate
                (hopBackPrefab, poolPosition_hopBack, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EmpressMoving.hopBack == true && readyRun == false)
        {
            // �߻� ��ġ�� �̵�
            firePosition = new Vector2(transform.position.x + 2.67f,
                transform.position.y + 0.64f);

            StartCoroutine(LocateFirePosition());
        }

        // ����ź�� ������ Ư�� �ð��� ������ && �ִϸ��̼� ���� ���°� �ƴϴ�
        if (stopRun == true && EmpressMoving.hopBack == false)
        {
            StopCoroutine(startMovingTimer());

            for (int i = 0; i < hopBackCount; i++)
            {
                // ������Ʈ Ǯ�� ����
                hopBacks[i].transform.position = poolPosition_hopBack;
                // �̵� ��ũ��Ʈ ����
                hopBacks[i].GetComponent<HopBackBullet>().enabled = false;
            }

            readyRun = false;
        }
    }

    IEnumerator LocateFirePosition()
    {
        // �ڷ�ƾ�� �����ϸ� �߻� ��ġ �̵� �ڵ尡 ���۵��� �ʵ��� ��. 
        readyRun = true;

        for (int i = 0; i < hopBackCount; i++)
        {
            // Ư�� �������� �߻� ��ġ�� �̵�
            hopBacks[i].transform.position = firePosition;

            // �̵� ����
            hopBacks[i].GetComponent<HopBackBullet>().enabled = true;

            yield return new WaitForSeconds(0.15f);
        }

        // ��ü ����ź �߻� �Ϸ�, ����ź ���� Ÿ�̸� ����
        StartCoroutine(startMovingTimer());
    }

    IEnumerator startMovingTimer()
    {
        StopCoroutine(LocateFirePosition());

        // 5�� �ڿ� ����ź�� ������ �����
        yield return new WaitForSeconds(5.0f);
        stopRun = true;
    }
}
