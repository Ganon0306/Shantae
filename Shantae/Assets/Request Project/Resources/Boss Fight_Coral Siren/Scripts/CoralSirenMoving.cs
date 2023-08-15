using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���� Coral Siren�� ���� ������ ���ϴ� Ŭ����
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

        //StartCoroutine(RandomMoving());
    }

    private void Update()
    {
        // �� �Ѹ��Ⱑ ������ �ʱ�ȭ ��ȣ �Ѹ���
        if (FireSpread.allStop == true)
        {
            fireSpread = false;
        }

        // ��ź �߻� ������ �ʱ�ȭ ��ȣ �Ѹ���
        if (FireBomb.doneFire == true)
        {
            animator.SetBool("Fire Bomb", false);

        }

        if (Input.GetKeyDown(KeyCode.W)) // ���� �۵� �׽�Ʈ�� �ӽ� �޼���
        {
            // ��ź �߻�
            animator.SetBool("Fire Bomb", true);

            fireBomb = true;
        }
    }

    // Update is called once per frame
    IEnumerator RandomMoving()
    {
        //randomAttack = Random.Range(0, 3);

        if (randomAttack == 0)
        {
            Debug.Log("��ź �߻� ����");
            // ��ź �߻�
            animator.SetBool("Fire Bomb", true);

            fireBomb = true;

            // ��ź �߻� �Ϸ�
            if (FireBomb.doneFire == true)
            {
                animator.SetBool("Fire Bomb", false);

                randomAttack = 1; // �ӽ�
                fireBomb = false;
            }
        }

        else if (randomAttack == 1)
        {
            Debug.Log("��� ����");
            // ��� �غ� (DashCharging)
            animator.SetBool("Ready Dash", true);
            yield return new WaitForSeconds(1.7f);
            animator.SetBool("Ready Dash", false);

            animator.SetBool("Go Dash", true);
            dash = true;
            
            //yield return new WaitForSeconds(3f);
            // �� coralSiren�� ���� �ٽ� ������ ��
            //animator.SetBool("Go Dash", false);

        }

        else if (randomAttack == 2)
        {
            Debug.Log("�� �Ѹ��� ����");
            // �� �Ѹ��� �غ� (FireSpread)
            animator.SetBool("Spread Fire Charge", true);
            
            fireSpread = true;
        }
    }
}
