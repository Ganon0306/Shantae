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
    [SerializeField] private int blowKissCount = 3;
    [SerializeField] private float blowKissSpeed = 11.0f;

    // ������ ���� ����
    private Vector2 startPosition;

    private GameObject[] blowKisses;
    private Vector2 poolPosition_blowKiss = new Vector2(-2.0f, -10.0f);

    Vector2 rightDirection;
    Vector2 leftDirection;

    private int alreadyRun = default;
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

        for (int i = 0; i < blowKissCount; i++)
        {
            // 20���� blowKiss ����
            blowKisses[i] = Instantiate(blowKissPrefab, poolPosition_blowKiss,
                Quaternion.identity);
        }

        // Empress Siren�� ���ʿ��� ������ ��
        rightDirection = transform.right;
        // Empress Siren�� �����ʿ��� ������ ��
        leftDirection = -(transform.right);
    }

    public void StartRightKisses()
    {
        StartCoroutine(RightKisses());
    }

    IEnumerator RightKisses()
    {
        alreadyRun = 1;
        yield return new WaitForSeconds(0.05f);
        alreadyRun++;
        yield return new WaitForSeconds(3.5f);
        alreadyRun++;
    }

    private void Update()
    {
        if(alreadyRun == 1)
        {
            blowKisses[0].transform.position =
                    new Vector2(transform.position.x + 1.2f, transform.position.y + 0.7f);
        }
        else if(alreadyRun == 2)
        {
             blowKisses[0].transform.Translate
                            (Vector2.right * blowKissSpeed * Time.deltaTime);
        }
        else if(alreadyRun == 3)
        {
            blowKisses[0].transform.position = poolPosition_blowKiss;
        }
    }
}
