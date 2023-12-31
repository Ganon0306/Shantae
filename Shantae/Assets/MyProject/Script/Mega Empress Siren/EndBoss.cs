using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBoss : MonoBehaviour
{
    public static bool finish = false;
    public Transform player;

    public GameObject tiara;

    public GameObject blowPrefab;
    private float spawnDelay = 0.5f;
    public float spawnTime;
    private float nextSpawnTime;
    private float count;

    private AudioSource audioSource;
    public AudioClip blow;

    public float blowX;
    public float blowY;

    public Transform endBlow;

    private int blowCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = blow;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {        
        if (finish)
        {
            count += Time.deltaTime;
            if (player != null)
            {
                Vector3 newPosition = new Vector3(0f, 0f, 0f); // 새로운 위치 설정
                player.position = newPosition;
            }

            //폭발이미지 렌덤스폰
            if (Time.time >= nextSpawnTime)
            {

                audioSource.Play();
                SpawnObject();
                if (blowCount < 6)
                {
                    spawnDelay -= 0.03f;
                    blowCount += 1;
                }
                nextSpawnTime = Time.time + spawnDelay;
            }
            if(count >8)
            {
                SceneManager.LoadScene("Lobby");
            }
            
        }
    }
    private void SpawnObject()
    {
        // 렌덤 좌표 생성
        float randomX = Random.Range(endBlow.position.x + blowX, endBlow.position.x - blowX);
        float randomY = Random.Range(endBlow.position.y + blowY, endBlow.position.y - blowY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        // 오브젝트 생성
        GameObject spawnedObject = Instantiate(blowPrefab, spawnPosition, Quaternion.identity);

        // 생성된 오브젝트를 일정 시간 후에 삭제
        Destroy(spawnedObject, spawnTime);
    }
}
