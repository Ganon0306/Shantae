using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� Empress Siren�� ���� �� ������ ���� Ŭ����
/// </summary>

public class BlowKissAttack : MonoBehaviour
{
    #region ���� �������� BlowKiss ����
    public static BlowKissAttack instance;

    private GameObject blowKissPrefab;
    // �߻�Ǵ� ��
    private int blowKissCount = 15;
    // ���ư��� �ӵ�
    private float blowKissSpeed = 15.0f;

    private Vector2 firePosition = default;

    private GameObject[] blowKisses;
    private Vector2 poolPosition_blowKiss = new Vector2(-2.0f, -10.0f);

    private bool runCheck = false;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        blowKissPrefab = Resources.Load<GameObject>("Prefabs/BlowKiss Attack");
        Debug.Assert(blowKissPrefab != null);

        blowKisses = new GameObject[blowKissCount]; 
        
        for(int i = 0; i < blowKissCount; i++)
        {
            blowKisses[i] = Instantiate(blowKissPrefab, poolPosition_blowKiss,
                Quaternion.identity);
        }
    }

    private void Update()
    {
        if (EmpressMoving.leftWall == true)
        {
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

            StartCoroutine(StartRightAttack());
        }

        else if(EmpressMoving.leftWall == false)
        {
            if (runCheck == false)
            {
                for (int i = 0; i < blowKissCount; i++)
                {
                    blowKisses[i].transform.position = poolPosition_blowKiss;
                }
            }
        }
    }

    IEnumerator StartRightAttack()
    {
        for (int i = 0; i < blowKissCount; i++)
        {
            float gap = 2.5f;

            blowKisses[i].GetComponent<Renderer>().enabled = true;

            blowKisses[i].transform.position =
                Vector2.MoveTowards
                (blowKisses[i].transform.position, new Vector2(10f, 10f - (gap * i)),
                Time.deltaTime * blowKissSpeed);
            yield return new WaitForSeconds(0.1f);
        }

        runCheck = false;
    }
}
