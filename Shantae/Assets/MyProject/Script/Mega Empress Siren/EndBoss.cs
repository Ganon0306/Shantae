using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBoss : MonoBehaviour
{
    public static bool finish = false;
    public Transform player;

    public GameObject tiara;

    public GameObject blowPrefab;
    private float spawnDelay = 0.5f;
    public float spawnTime;
    private float nextSpawnTime;

    private int blowCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(tiara == null)
        {
            finish = true;
        }
        if (finish)
        {
            Debug.Log("1");
            if (player != null)
            {
                Vector3 newPosition = new Vector3(0f, 0f, 0f); // ���ο� ��ġ ����
                player.position = newPosition;
            }

            //�����̹��� ��������
            if (Time.time >= nextSpawnTime)
            {
                Debug.Log("2");

                SpawnObject();
                if (blowCount < 7)
                {
                    spawnDelay -= 0.06f;
                    blowCount += 1;
                }
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
    }
    private void SpawnObject()
    {
        // ���� ��ǥ ����
        float randomX = Random.Range(-5.5f, 5.5f);
        float randomY = Random.Range(8f, 21f);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        // ������Ʈ ����
        GameObject spawnedObject = Instantiate(blowPrefab, spawnPosition, Quaternion.identity);

        // ������ ������Ʈ�� ���� �ð� �Ŀ� ����
        Destroy(spawnedObject, spawnTime);
    }
}
