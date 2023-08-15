using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ź�� �߻��ϰ� �߻� ������ �����ϴ� Ŭ����
/// </summary>

public class FireBomb : MonoBehaviour
{
    private GameObject bombPrefab;
    private GameObject[] bombs;

    private int firstBombCount = 6;
    private int secondBombCount = 15;
    private int thirdBombCount = 20;
    private int maxBombCount = default; // ������ Ȯ���ϱ� ���� �ӽ÷� �ϳ���

    private bool readyFire = false;
    public static bool doneFire = false;
    public static bool wellDone = false;

    private Vector2 poolPosition_bomb = new Vector2(0f, 10f);
    private Vector2 firePosition;

    public static BombUpDown lastBombUpDown;
    private Animator animator;

    private void Start()
    {
        bombPrefab = Resources.Load<GameObject>
            ("Boss Fight_Coral Siren/Prefabs/Bomb");

        // ���ÿ� ȭ�鿡 �����ϴ� ��ź�� �� (35��)
        //maxBombCount = secondBombCount + thirdBombCount;
        maxBombCount = 5; //�ӽ�

        bombs = new GameObject[maxBombCount];

        for (int i = 0; i < maxBombCount; i++)
        {
            bombs[i] = Instantiate(bombPrefab, poolPosition_bomb, Quaternion.identity);
            Debug.Assert(bombs[i] != null);

            bombs[i].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            Debug.Assert(bombs[i].transform.GetChild(0).GetComponent<SpriteRenderer>()
                != null);
        }

        lastBombUpDown = bombs[maxBombCount - 1].GetComponent<BombUpDown>();
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // �߻� ���� �ڷ�ƾ
        if (CoralSirenMoving.fireBomb == true && readyFire == false)
        {
            StartCoroutine(BombsFire());
        }

        // ���� �߻��ϸ� �߻� �ڷ�ƾ ���� && ���� ���� �غ�
        if (CoralSirenMoving.fireBomb == true && doneFire == true)
        {
            StopCoroutine(BombsFire());

            doneFire = false;
        }

        // ������ ��ź�� Ǯ�� ������ ���� Ȯ�εǸ�
        if (lastBombUpDown.allBack == true)
        {
            readyFire = false;

            for (int i = 0; i < maxBombCount; i++)
            {
                // ��ź �߻翡 ���� ��� bool ���� �ʱ�ȭ
                bombs[i].GetComponent<BombUpDown>().upDone = false;
                bombs[i].GetComponent<BombUpDown>().falling = false;
                bombs[i].GetComponent<BombUpDown>().backThePool = false;
                bombs[i].GetComponent<BombUpDown>().alreadyRun = false;
                bombs[i].GetComponent<BombUpDown>().allBack = false;

                // ��� ��ź �̵� ��ũ��Ʈ ����
                bombs[i].GetComponent<BombUpDown>().enabled = false;
            }

            // ���� Ű���� ����
            CoralSirenMoving.fireBomb = false;
        }
    }

    IEnumerator BombsFire()
    {
        readyFire = true;

        // �ִϸ��̼� ��ũ
        yield return new WaitForSeconds(1.2f);

        for (int i = 0; i < maxBombCount; i++)
        {
            // �߻� ��ġ��
            firePosition = new Vector2
                (transform.position.x, transform.position.y + 1.5f);
            bombs[i].transform.position = firePosition;

            // �߻� ����
            bombs[i].GetComponent<BombUpDown>().enabled = true;

            yield return new WaitForSeconds(0.5f);
        }

        // ���� �߻���
        doneFire = true;

        // �ִϸ��̼� �⺻ ���·�
        animator.SetBool("Fire Bomb", false);
    }
}
