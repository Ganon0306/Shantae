using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� Empress Siren�� ������ ���� Ŭ����
/// </summary>

public class EmpressAttack : MonoBehaviour
{
    #region
    public static EmpressAttack instance;
    // õ�� ���� ������
    private GameObject ceilingBallPrefab;
    private int ceilingCount = 2;
    [SerializeField] private float ceilingBallSpeed = 1.0f;
    private GameObject[] ceilingBalls;
    private Vector2 poolPosition_ceiling = new Vector2(0, -10f);
    private bool ceilingBallBroken = false;
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

    private void Start()
    {
        ceilingBallPrefab = Resources.Load<GameObject>("Prefabs/Ceiling Attack");
        Debug.Assert(ceilingBallPrefab != null);
        
        ceilingBalls = new GameObject[ceilingCount];

        for (int i = 0; i < ceilingCount; i++)
        {
            // ������ ����ŭ�� ceilingBall�� ���� 
            ceilingBalls[i] = Instantiate(ceilingBallPrefab, poolPosition_ceiling,
                Quaternion.identity);
        }  
    }

    public void CeilingAttack()
    {
        Transform ceiling_first = ceilingBalls[0].transform;
        ceiling_first.position = new Vector2(0f, 3.35f);
    }
}
