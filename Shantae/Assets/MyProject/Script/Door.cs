using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject LeftD;
    public GameObject RightD;

    private float moveSpeed = 1;
    private bool isMoving = false; 
    private float timer = 0.0f;
    private bool a = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer);
        if(a)
        {
            timer += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow) && isMoving)
        {
            a = true;
            if (timer < 5f)
            {
                // LeftD�� RightD ���� ������Ʈ�� ���ʰ� ���������� �̵���ŵ�ϴ�.
                LeftD.GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed;
                RightD.GetComponent<Rigidbody2D>().velocity = Vector2.right * moveSpeed;
            }
        }
            if ( timer >= 5f)
            {
                LeftD.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                RightD.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            isMoving = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            isMoving = false;
        }
    }
}
