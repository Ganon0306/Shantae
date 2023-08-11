using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigOrb : MonoBehaviour
{
    private float moveSpeed = 5f;   

    private bool isTimerStarted = false;
    private float startTime;
    private float elapsedTime;
    private float originalPositionX;
    private void Start()
    {
        originalPositionX = transform.position.x;
        
    }
    private void Update()
    {
        GameObject player = GameObject.Find("Player");
        GameObject empress = GameObject.Find("Mega Empress Siren");

        if (player != null && empress != null)
        {
            // Player의 좌표값 가져오기
            Vector3 playerPosition = player.transform.position;

            // Empress의 Transform 컴포넌트를 사용하여 x값 가져오기
            float empressX = empress.transform.position.x;

            // 가져온 값들을 사용하여 원하는 작업 수행
            Debug.Log("Player Position: " + playerPosition);
            Debug.Log("Empress X Position: " + empressX);
        }

        if (!isTimerStarted)
        {
            isTimerStarted = true;
            startTime = Time.time;
        }

        if (isTimerStarted)
        {
            elapsedTime = Time.time - startTime;
        }
        if(elapsedTime<= 2f)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = empress.transform.position.x + originalPositionX;
            transform.position = newPosition;
        }
        else if (elapsedTime > 2f && elapsedTime < 10f)
        {
            bool move = false;
            if (!move)
            {
                Vector3 targetPosition = player.transform.position;
                Vector3 objectPosition = transform.position;
                Vector3 direction = targetPosition - objectPosition;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = rotation;
                move = true;
            }
                // 플레이어 위치
                Vector3 targetDirection = player.transform.position - transform.position;
                targetDirection.Normalize();

                // 플레이어 방향 회전
                float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 0.3f);

                // 플레이어에게 이동
                Vector3 forwardDirection = transform.right;
                transform.position += forwardDirection * moveSpeed * Time.deltaTime;
            
        }
        else if (elapsedTime >= 10f && elapsedTime <= 17f)
        {

            bool move = false;
            if (!move)
            {
                if (transform.position.x > 0f)
                {
                    Vector3 newPosition = transform.position + new Vector3(moveSpeed/100, 0, 0);

                    // 새로운 위치로 오브젝트 이동
                    transform.position = newPosition;
                    //transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                    move = true;
                }
                else if (transform.position.x <= 0f)
                {
                    Vector3 newPosition = transform.position + new Vector3(- moveSpeed/100, 0, 0);

                    // 새로운 위치로 오브젝트 이동
                    transform.position = newPosition;
                    //transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                    move = true;
                }
            }
        }
        else if(elapsedTime >17f)
        {
            Destroy(gameObject);
        }
       
    }
}
