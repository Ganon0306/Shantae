using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ���� Empress Siren�� ������ ���� Ŭ����
/// </summary>

public class CeilingAttack : MonoBehaviour
{
    public static CeilingAttack instance;

    #region õ�� ���� ������
    private GameObject ceilingBallPrefab;
    private int ceilingCount = 2;
    [SerializeField] private float ceilingBallSpeed = 11.0f;
    private GameObject[] ceilingBalls;
    private Vector2 poolPosition_ceiling = new Vector2(0, -10f);

    // ���� ������ ���� ��ġ(Empress Siren�� �� ��ġ)
    Vector2 ceiling_OriginPosition = default;

    Transform ceiling_first = default;
    Transform ceiling_second = default;

    // ����
    Vector2 firstDestination_Left = default;
    Vector2 secondDestination_Left = default;
    Vector2 thirdDestination_Left = default;

    //������
    Vector2 firstDestination_Right = default;
    Vector2 secondDestination_Right = default;
    Vector2 thirdDestination_Right = default;

    private int ceilingMoveIndex = 0;
    private int ceilingMoveIndex_Right = 0;

    private bool leftFinish = false;
    private bool rightFinish = false;
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
            // 2���� ceilingBall�� ���� 
            ceilingBalls[i] = Instantiate(ceilingBallPrefab, poolPosition_ceiling,
                Quaternion.identity);
        }

        // transform ���� ����
        ceiling_first = ceilingBalls[0].transform;
        ceiling_second = ceilingBalls[1].transform;

        // ���� ������ ������ ������ ����
        firstDestination_Left = new Vector2(-7.25f, 3.35f);
        secondDestination_Left = new Vector2(-7.25f, -3.0f);
        thirdDestination_Left = new Vector2(-0.7f, -3.0f);

        //������ ������ ������ ������ ����
        firstDestination_Right = new Vector2(7.0f, 3.35f);
        secondDestination_Right = new Vector2(7.0f, -3.0f);
        thirdDestination_Right = new Vector2(0.6f, -3.0f);
    }

    private void Update()
    {
        // �� ���� �����ϵ��� �ϱ� ����. (��ġ ����� �ٲ��� �� ��!!!)
        if (leftFinish == true && rightFinish == true)
        {
            leftFinish = false;
            rightFinish = false;

            EmpressMoving.ceiling = false;
        }

        if (EmpressMoving.ceiling == true)
        {
            LeftCeilingAttack();
            RightCeilingAttack();
        }
    }

    void LeftCeilingAttack()
    {
        if (ceilingMoveIndex == 0 && leftFinish == false)
        {
            // ���� ������ �����ϴ� ���� ���� ����
            ceiling_OriginPosition = new Vector2(transform.position.x, 3.35f);

            // ������Ʈ Ǯ�� �ִ� ���� �� ������ �̵� 
            ceiling_first.position = ceiling_OriginPosition;

            ceilingMoveIndex++;
        }

        else if (ceilingMoveIndex == 1)
        {
            ceiling_first.position = Vector2.MoveTowards(ceiling_first.position,
            firstDestination_Left, ceilingBallSpeed * Time.deltaTime);

            if (Vector2.Distance(ceiling_first.position, firstDestination_Left) < 0.01f)
            {
                ceilingMoveIndex++;
            }
        }

        else if (ceilingMoveIndex == 2)
        {
            ceiling_first.position = Vector2.MoveTowards(ceiling_first.position,
            secondDestination_Left, ceilingBallSpeed * Time.deltaTime);

            if (Vector2.Distance(ceiling_first.position, secondDestination_Left) < 0.01f)
            {
                ceilingMoveIndex++;
            }
        }

        else if (ceilingMoveIndex == 3)
        {
            ceiling_first.position = Vector2.MoveTowards(ceiling_first.position,
            thirdDestination_Left, ceilingBallSpeed * Time.deltaTime);

            if (Vector2.Distance(ceiling_first.position, thirdDestination_Left) < 0.01f)
            {
                ceilingMoveIndex++;
            }
        }

        // ������Ʈ Ǯ�� ����
        else if (ceilingMoveIndex == 4)
        {
            ceiling_first.position = poolPosition_ceiling;
            
            leftFinish = true;
            ceilingMoveIndex = 0;
        }
    }

    void RightCeilingAttack()
    {
        if (ceilingMoveIndex_Right == 0 && rightFinish == false)
        {
            // ������Ʈ Ǯ�� �ִ� ���� �� ������ �̵� 
            ceiling_second.position = ceiling_OriginPosition;

            ceilingMoveIndex_Right++;
        }

        else if (ceilingMoveIndex_Right == 1)
        {
            ceiling_second.position = Vector2.MoveTowards(ceiling_second.position,
            firstDestination_Right, ceilingBallSpeed * Time.deltaTime);

            if (Vector2.Distance(ceiling_second.position, firstDestination_Right) < 0.01f)
            {
                ceilingMoveIndex_Right++;
            }
        }

        else if (ceilingMoveIndex_Right == 2)
        {
            ceiling_second.position = Vector2.MoveTowards(ceiling_second.position,
            secondDestination_Right, ceilingBallSpeed * Time.deltaTime);

            if (Vector2.Distance(ceiling_second.position, secondDestination_Right) < 0.01f)
            {
                ceilingMoveIndex_Right++;
            }
        }

        else if (ceilingMoveIndex_Right == 3)
        {
            ceiling_second.position = Vector2.MoveTowards(ceiling_second.position,
            thirdDestination_Right, ceilingBallSpeed * Time.deltaTime);

            if (Vector2.Distance(ceiling_second.position, thirdDestination_Right) < 0.01f)
            {
                ceilingMoveIndex_Right++;
            }
        }

        // ������Ʈ Ǯ�� ����
        else if (ceilingMoveIndex_Right == 4)
        {
            ceiling_second.position = poolPosition_ceiling;
            
            rightFinish = true;
            ceilingMoveIndex_Right = 0;
        }
    }
}
