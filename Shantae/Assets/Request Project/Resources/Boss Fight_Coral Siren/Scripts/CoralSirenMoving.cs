using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���� Coral Siren�� ���� ������ ���ϴ� Ŭ����. 
/// </summary>

public class CoralSirenMoving : MonoBehaviour
{
    public static CoralSirenMoving instance;

    private int randomAttack = default;
    public static bool fireBomb = false;
    public static bool dash = false;
    public static bool fireSpread = false;
    private Animator animator;
    public static Vector2 newBossPosition;

    public static bool firstPatternDone = false;
    public static bool secondPatternDone = false;
    public static bool thirdPatternDone = false;
    private bool patternFinished = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // ���� ����
        StartCoroutine(RandomMoving());
    }

    private void Update()
    {
        // �� �Ѹ��Ⱑ ������ �ʱ�ȭ ��ȣ �Ѹ���
        if (FireSpread.allStop == true)
        {
            fireSpread = false;
        }

        /// <problem> ��ź ������ �ʱ�ȭ�� �̷������ �ʴ´�. allBack ������ ����
        // ���� ���� ���� �ڵ�
        if (firstPatternDone == true || secondPatternDone == true 
            || thirdPatternDone == true)
        {
            if (patternFinished == false)
            {
                StopCoroutine(RandomMoving());
                StartCoroutine(RandomMoving());
                patternFinished = true;
            }

            firstPatternDone = false;
            secondPatternDone = false;
            thirdPatternDone = false;

            patternFinished = false;
        }
    }

    // Update is called once per frame
    IEnumerator RandomMoving()
    {
        randomAttack = Random.Range(0, 3);

        yield return new WaitForSeconds(3f);

        if (randomAttack == 0)
        {
            // ��ź �߻�
            animator.SetBool("Fire Bomb", true);

            fireBomb = true;

            // ��ź �߻� �Ϸ�
            if (FireBomb.doneFire == true)
            {
                animator.SetBool("Fire Bomb", false);

                fireBomb = false;
            }
        }

        else if (randomAttack == 1)
        {
            // ��� �غ� (DashCharging)
            dash = true;
           
        }

        else if (randomAttack == 2)
        {
            // �� �Ѹ��� �غ� (FireSpread)
            fireSpread = true;
        }
    }
}
