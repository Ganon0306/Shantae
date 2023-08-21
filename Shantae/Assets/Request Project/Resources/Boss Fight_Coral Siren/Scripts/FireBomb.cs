using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ��ź�� �߻��ϰ� �߻� ������ �����ϴ� Ŭ����
/// </summary>

public class FireBomb : MonoBehaviour
{
    private GameObject bombPrefab;
    private GameObject[] bombs;

    private int firstBombCount = 10;
    private int secondBombCount = 20;
    private int thirdBombCount = 45;
    private int maxBombCount = default; // ������ Ȯ���ϱ� ���� �ӽ÷� �ϳ���

    private float firstInterval = 0.5f;
    private float secondInterval = 0.3f;
    private float thirdInterval = 0.1f;

    private float firstBossSpeed = 3f;
    private float secondBossSpeed = 4.5f;
    private float thirdBossSpeed = 5.5f;

    private int whatBombPattern = default;

    private bool readyFire = false;
    public static bool doneFire = false;
    public static bool wellDone = false;

    private bool firstCoroutineDone = false;
    private bool secondCoroutineDone = false;
    private bool thirdCoroutineDone = false;

    private bool bossNowMove = false;

    private Vector2 poolPosition_bomb = new Vector2(0f, 10f);
    private Vector2 firePosition;

    public static BombUpDown lastBombUpDown;
    private Animator animator;
    private Transform wherePlayer;

    private float wherePlayerX;
    private float wherePlayerY;
    private float whereBossX;
    private float whereBossY;

    private void Start()
    {
        bombPrefab = Resources.Load<GameObject>
            ("Boss Fight_Coral Siren/Prefabs/Bomb");

        // Ǯ�� ������ ��ź�� �� (44���� ��ź�� ���δ�.)
        maxBombCount = thirdBombCount;

        bombs = new GameObject[maxBombCount];

        for (int i = 0; i < maxBombCount; i++)
        {
            bombs[i] = Instantiate(bombPrefab, poolPosition_bomb, Quaternion.identity);
            Debug.Assert(bombs[i] != null);

            bombs[i].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            Debug.Assert(bombs[i].transform.GetChild(0).GetComponent<SpriteRenderer>()
                != null);
        }

        // ������ ��ź�� Ǯ�� ������ ���� Ȯ�εȴٸ�
        lastBombUpDown = bombs[maxBombCount - 1].GetComponent<BombUpDown>();
        Debug.Assert(lastBombUpDown != null);

        animator = transform.GetComponent<Animator>();

        // �߻� ���� ����
        whatBombPattern = 0;

        // �÷��̾� Ʈ������
        wherePlayer = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // �߻� ���� �ڷ�ƾ
        if (CoralSirenMoving.fireBomb == true && readyFire == false)
        {
            // ù��° �ڷ�ƾ ����
            if (whatBombPattern == 0 && firstCoroutineDone == false)
            {
                StartCoroutine(FirstBombsFire());
            }

            // �ι�° �ڷ�ƾ ����
            if (whatBombPattern == 1 && secondCoroutineDone == false)
            {
                StopCoroutine(FirstBombsFire());
                StartCoroutine(SecondBombsFire());
            }

            // ����° �ڷ�ƾ ����
            if (whatBombPattern == 2 && thirdCoroutineDone == false)
            {
                StopCoroutine(SecondBombsFire());
                StartCoroutine(ThirdBombsFire());
            }
        }

        // �߻��ϴ� ���� ������ �÷��̾ ����.
        if (bossNowMove == true && animator.GetBool("Fire Bomb") == true)
        {
            whereBossX = transform.position.x;

            if (Mathf.Abs(whereBossX - PlayerController.playerPosition.x) >= 0.5f)
            {
                if (PlayerController.playerPosition.x < transform.position.x)
                {
                    // ���� ���� �̵�
                    if (firstCoroutineDone == true)
                    {
                        transform.GetComponent<SpriteRenderer>().flipX = true;

                        transform.Translate(Vector2.left * firstBossSpeed * Time.deltaTime);
                    }

                    if (secondCoroutineDone == true)
                    {

                        transform.GetComponent<SpriteRenderer>().flipX = true;

                        transform.Translate(Vector2.left * secondBossSpeed * Time.deltaTime);
                    }

                    if (thirdCoroutineDone == true)
                    {

                        transform.GetComponent<SpriteRenderer>().flipX = true;

                        transform.Translate(Vector2.left * thirdBossSpeed * Time.deltaTime);
                    }
                }
                else if (PlayerController.playerPosition.x > transform.position.x)
                {
                    // ���� ���� �̵�
                    if (firstCoroutineDone == true)
                    {

                        transform.GetComponent<SpriteRenderer>().flipX = false;

                        transform.Translate(Vector2.right * firstBossSpeed * Time.deltaTime);
                    }

                    if (secondCoroutineDone == true)
                    {

                        transform.GetComponent<SpriteRenderer>().flipX = false;

                        transform.Translate(Vector2.right * secondBossSpeed * Time.deltaTime);
                    }

                    if (thirdCoroutineDone == true)
                    {

                        transform.GetComponent<SpriteRenderer>().flipX = false;

                        transform.Translate(Vector2.right * thirdBossSpeed * Time.deltaTime);
                    }
                }
            }
        }

        // ���� �߻��ϸ� �߻� �ڷ�ƾ ���� && ���� ���� ���� �غ�
        if (CoralSirenMoving.fireBomb == true && doneFire == true)
        {
            StopCoroutine(ThirdBombsFire());

            doneFire = false;
        }

        // ������ ��ź�� Ǯ�� ������ ���� Ȯ�εǸ�
        if (lastBombUpDown.allBack == true)
        {
            // ���� Ű���� ����
            CoralSirenMoving.fireBomb = false;
            CoralSirenMoving.firstPatternDone = true;

            // ��� üũ ��� �ʱ�ȭ
            readyFire = false;
            whatBombPattern = 0;
            firstCoroutineDone = false;
            secondCoroutineDone = false;
            thirdCoroutineDone = false;

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
        }
    }

    IEnumerator FirstBombsFire()
    {
        firstCoroutineDone = true;

        animator.SetBool("Fire Bomb", true);

        // �ִϸ��̼� ��ũ
        yield return new WaitForSeconds(1f);

        bossNowMove = true;

        /// <caution> ��ź�� 74�� ������ �Ʒ��� ���� �����غ����� ���� ���̴� ���� ����° �߻翡 
        /// ���̴� ��ź �ִ��� 45�� ���̴�.

        for (int i = 0; i < firstBombCount; i++)
        {
            // �߻� ��ġ��
            firePosition = new Vector2
                (transform.position.x, transform.position.y + 1.5f);
            bombs[i].transform.position = firePosition;

            // �߻� ����
            bombs[i].GetComponent<BombUpDown>().enabled = true;

            yield return new WaitForSeconds(firstInterval);
        }

        // �ִϸ��̼� �⺻ ���·�
        animator.SetBool("Fire Bomb", false);
        bossNowMove = false;

        // ���� �߻���� ���
        yield return new WaitForSeconds(0.7f);

        whatBombPattern = 1;
    }

    IEnumerator SecondBombsFire()
    {
        secondCoroutineDone = true;

        animator.SetBool("Fire Bomb", true);

        // �ִϸ��̼� ��ũ
        yield return new WaitForSeconds(1f);
        bossNowMove = true;

        for (int i = firstBombCount; i < secondBombCount; i++)
        {
            // �߻� ��ġ��
            firePosition = new Vector2
                (transform.position.x, transform.position.y + 1.5f);
            bombs[i].transform.position = firePosition;

            // �߻� ����
            bombs[i].GetComponent<BombUpDown>().enabled = true;

            yield return new WaitForSeconds(secondInterval);
        }

        // �ִϸ��̼� �⺻ ���·�
        animator.SetBool("Fire Bomb", false);
        bossNowMove = false;

        // ���� �߻���� ���
        yield return new WaitForSeconds(0.5f);

        whatBombPattern = 2;
    }

    IEnumerator ThirdBombsFire()
    {
        thirdCoroutineDone = true;

        readyFire = true;

        animator.SetBool("Fire Bomb", true);

        // �ִϸ��̼� ��ũ
        yield return new WaitForSeconds(1f);
        bossNowMove = true;

        for (int i = secondBombCount; i < thirdBombCount; i++)
        {
            // �߻� ��ġ��
            firePosition = new Vector2
                (transform.position.x, transform.position.y + 1.5f);
            bombs[i].transform.position = firePosition;

            // �߻� ����
            bombs[i].GetComponent<BombUpDown>().enabled = true;

            yield return new WaitForSeconds(thirdInterval);
        }

        whatBombPattern = 3;

        // �ִϸ��̼� �⺻ ���·�
        animator.SetBool("Fire Bomb", false);
        bossNowMove = false;

        doneFire = true;
    }
}
