using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� Empress Siren�� ���� �� ������ ���� Ŭ����
/// </summary>

public class BlowKissAttack : MonoBehaviour
{
    #region ���� �������� BlowKiss ����
    private GameObject blowKissPrefab;
    // �߻�Ǵ� ��
    private int blowKissCount = 25;

    // ���ư��� �ӵ�
    private float blowKissSpeed = 15.0f;
    private float gap = 0.8f;

    private Vector2 firePosition = default;

    private GameObject[] blowKisses;
    private Vector2 poolPosition_blowKiss = new Vector2(-2.0f, -10.0f);

    private bool runCheck = false;
    private bool runCheck_right = false;
    private bool blowNow = false;

    private bool blowWait = false;
    private int nowBlowNumber = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        blowKissPrefab = Resources.Load<GameObject>
            ("Boss Fight_Empress Siren/Prefabs/BlowKiss Attack");
        Debug.Assert(blowKissPrefab != null);

        blowKisses = new GameObject[blowKissCount];

        for (int i = 0; i < blowKissCount; i++)
        {
            blowKisses[i] = Instantiate(blowKissPrefab, poolPosition_blowKiss,
                Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        #region ���� ������ ����
        if (EmpressMoving.leftWall == true && EmpressMoving.rightWall == false)
        {
            // runCheck�� �߻簡 ���� �������� �˾ƺ��� ���� ����
            if (runCheck == false)
            {
                for (int i = 0; i < blowKissCount; i++)
                {
                    // �߻縦 ���� ��ġ ������ 
                    firePosition = new Vector2
                    (transform.position.x + 1.2f, transform.position.y + 0.6f);

                    blowKisses[i].transform.position = firePosition;
                }

                runCheck = true;
            }
            else if (runCheck == true)
            {
                // �������� �����ٸ�
                for (int i = 0; i < blowKissCount; i++)
                {
                    // �� ���ݱ����� ������ ����
                    blowKisses[i].GetComponent<BlowKissMoving>().blowKissDestination
                        = new Vector2(10.5f, 10f - (gap * i));

                    blowKisses[i].GetComponent<SpriteRenderer>().enabled = false;

                    if (blowNow == false)
                    {
                        StartCoroutine(StartAttack());
                    }
                }
            }
        }
        #endregion

        #region ���� ������ ����
        if (EmpressMoving.rightWall == true && EmpressMoving.leftWall == false)
        {
            // runCheck�� �߻簡 ���� �������� �˾ƺ��� ���� ����
            if (runCheck_right == false)
            {
                for (int i = 0; i < blowKissCount; i++)
                {
                    // �߻縦 ���� ��ġ ������ 
                    firePosition = new Vector2
                    (transform.position.x - 1.2f, transform.position.y + 0.6f);

                    blowKisses[i].transform.position = firePosition;
                }

                runCheck_right = true;
            }
            else if (runCheck_right == true)
            {
                // �������� �����ٸ�
                for (int i = 0; i < blowKissCount; i++)
                {
                    // �� ���ݱ����� ������ ����
                    blowKisses[i].GetComponent<BlowKissMoving>().blowKissDestination
                        = new Vector2(-10.5f, 10f - (gap * i));

                    blowKisses[i].GetComponent<SpriteRenderer>().enabled = false;

                    if (blowNow == false)
                    {
                        StartCoroutine(StartAttack());
                    }
                }
            }
        }
        #endregion
        
        // ������ ������ Ǯ�� ����
        if (EmpressMoving.rightWall == false && EmpressMoving.leftWall == false)
        {
            StopCoroutine(StartAttack());
            runCheck = false;
            runCheck_right = false;
            blowNow = false;

            // �ִϸ��̼��� ������ Ǯ�� ���� 
            for (int i = 0; i < blowKissCount; i++)
            {
                blowKisses[i].transform.position = poolPosition_blowKiss;
                blowKisses[i].GetComponent<BlowKissMoving>().enabled = false;
            }
        }

        // empressHP <= 0�̸� ���� Ǯ�� ����
        if (EmpressController.empressHP <= 0)
        {
            for (int i = 0; i < blowKissCount; i++)
            {
                blowKisses[i].transform.position = poolPosition_blowKiss;
            }
        }
    }

    IEnumerator StartAttack()
    {
        blowNow = true;

        nowBlowNumber = 0;

        while (nowBlowNumber < blowKissCount)
        {
            // �� ���ݱ����� �߻�
            blowKisses[nowBlowNumber].GetComponent<BlowKissMoving>().enabled = true;

            yield return new WaitForSecondsRealtime(0.1f);

            nowBlowNumber++;
        }

        yield return new WaitForSecondsRealtime(1.5f);

        EmpressMoving.leftWall = false;
        EmpressMoving.rightWall = false;
    }
}
