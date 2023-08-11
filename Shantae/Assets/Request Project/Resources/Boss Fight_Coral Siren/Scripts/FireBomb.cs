using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : MonoBehaviour
{
    private GameObject bombPrefab;
    private GameObject[] bombs;

    private int firstBombCount = 6;
    private int secondBombCount = 15;
    private int thirdBombCount = 20;
    private int maxBombCount = default;

    private bool readyFire = false;
    private bool doneFire = false;

    private Vector2 poolPosition_bomb = new Vector2(0f, 10f);
    private Vector2 firePosition;

    private void Start()
    {
        bombPrefab = Resources.Load<GameObject>
            ("Boss Fight_Coral Siren/Prefabs/Bomb");

        // ���ÿ� ȭ�鿡 �����ϴ� ��ź�� �� (35��)
        maxBombCount = secondBombCount + thirdBombCount;
        bombs = new GameObject[maxBombCount];

        for (int i = 0; i < thirdBombCount; i++)
        {
            bombs[i] = Instantiate(bombPrefab, poolPosition_bomb, Quaternion.identity);
            Debug.Assert(bombs[i] != null);

            bombs[i].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            Debug.Assert(bombs[i].transform.GetChild(0).GetComponent<SpriteRenderer>()
                != null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CoralSirenMoving.fireBomb == true && readyFire == false)
        {
            StartCoroutine(BombsFire());
        }

        // y = 6.5f�� �����ϸ� Scale Ȯ�� + ������ ������ + ���� �ٴ��� Collider�� �浹�ϸ�
        // Animation ����� �Բ� Ǯ�� ����

        // �߻簡 ������ �ڷ�ƾ ���� 
        if (doneFire == false)
        {
            StopCoroutine(BombsFire());
        }
    }

    IEnumerator BombsFire()
    {
        readyFire = true;

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

        doneFire = false;
    }
}
