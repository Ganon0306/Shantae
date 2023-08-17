using System.Collections;
using System.Collections.Generic;
using System.Net;
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
    public static bool grabLever = false;
    private Animator animator;
    public static Vector2 newBossPosition;

    public static bool firstPatternDone = false;
    public static bool secondPatternDone = false;
    public static bool thirdPatternDone = false;
    public static bool fourthPatternDone = false;
    private bool patternFinished = false;

    private GameObject sandGroup;
    private GameObject firstSand;
    private GameObject secondSand;
    private GameObject thirdSand;
    private GameObject fourthSand;

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

        sandGroup = GameObject.Find("Sands");
        Debug.Assert(sandGroup != null);

        firstSand = sandGroup.transform.GetChild(0).gameObject;
        secondSand = sandGroup.transform.GetChild(1).gameObject;
        thirdSand = sandGroup.transform.GetChild(2).gameObject;
        fourthSand = sandGroup.transform.GetChild(3).gameObject;

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

        // ������ ���� �׼��� ���ߴٸ� �𷡸� ä��� �ʱ�ȭ ��ȣ �Ѹ���
        if (GrabLever.sandActive == true)
        {
            firstSand.SetActive(true);
            secondSand.SetActive(true);
            thirdSand.SetActive(true);
            fourthSand.SetActive(true);

            grabLever = false;
            GrabLever.sandActive = false;
        }

        // ���� ���� ���� �ڵ�
        if (firstPatternDone == true || secondPatternDone == true 
            || thirdPatternDone == true || fourthPatternDone == true)
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
            fourthPatternDone = false;

            patternFinished = false;
        }
    }

    // Update is called once per frame
    IEnumerator RandomMoving()
    {
        // �ߵ� ���� üũ
        if (firstSand.activeSelf == false || secondSand.activeSelf == false
                || thirdSand == false || fourthSand == false)
        {
            // �� �� �ϳ��� ����ִٸ� �ٷ� �� ä��� ���� ����
            randomAttack = 3;
        }
        else
        {
            randomAttack = Random.Range(0, 3);
        }

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
        else if (randomAttack == 3)
        {
            // �� ä��� �غ�
           grabLever = true;
        }
    }
}
