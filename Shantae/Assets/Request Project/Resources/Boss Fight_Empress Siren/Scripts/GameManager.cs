using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // ����� �ۼ�
    public GameObject body;
    public Transform platform;
    public Transform player;
    public Transform spawner;
    private bool move = false;
    public bool pase2Camera = false;
    // �����

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ESC ��ư�� ���� �� ���� ����. (�Ŀ� ���� �ʿ�)
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        if(body == null)
        {
            pase2_5();
        }
    }

   
    // ����� �ۼ�
    private void  pase2_5()
    {        

        if (player != null)
        {
            if (!move)
            {
                Vector3 newPosition = new Vector3(0f, 15f, 0f); // ���ο� ��ġ ����
                player.position = newPosition;
                move = true;
            }
        }
        //if (spawner != null)                                                    /// ���������� ���� �ڵ�
        //{
        //    Vector3 newPosition = new Vector3(10f, 0f, 0f); // ���ο� ��ġ ����
        //    spawner.position = newPosition;
        //}
        if(platform != null)
        {
            if (!move)
            {
                Vector3 newPosition = new Vector3(0f, 11f, 0f); // ���ο� ��ġ ����
                platform.position = newPosition;
                move = true;
            }
        }
    }

    // ����� �ۼ�

}
