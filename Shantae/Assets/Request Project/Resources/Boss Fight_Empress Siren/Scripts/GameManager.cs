using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // ĵ������ ��Ʈ
    private GameObject canvas_UI;
    private Transform firstHeart_UI;
    private Transform secondHeart_UI;
    private Transform thirdHeart_UI;
    private Transform fourthHeart_UI;

    // ���� ī�޶� �ı� ����
    private GameObject mainCamera;

    // �÷��̾� ��� �ǰ� Ƚ��
    private int fullLife = 16;

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

    private void Start()
    {
        // ���� ī�޶� �ı� ����
        mainCamera = GameObject.FindWithTag("MainCamera");
        DontDestroyOnLoad (mainCamera);

        // UI ĵ����
        canvas_UI = GameObject.Find("UI Canvas");
        DontDestroyOnLoad(canvas_UI);

        firstHeart_UI = canvas_UI.transform.GetChild(0);
        secondHeart_UI = canvas_UI.transform.GetChild(1);
        thirdHeart_UI = canvas_UI.transform.GetChild(2);
        fourthHeart_UI = canvas_UI.transform.GetChild(3);
    }

    // Update is called once per frame
    void Update()
    {
        // ESC ��ư�� ���� �� ������ exe ���� ���� ����. (�Ŀ� ���� �ʿ�)
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        if(body == null)
        {
            pase2_5();
        }

        if (PlayerController.gotDamage == true)
        {
            /// <problem> �����ð����� �÷��̾ ���ظ� �Դ� ���� ó�� �ʿ�. 

            if (fullLife <= 16 && fullLife >= 13) // �׹�° ��Ʈ
            {
                if (fullLife == 16)
                {
                    fourthHeart_UI.GetChild(4).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 15)
                {
                    fourthHeart_UI.GetChild(3).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 14)
                {
                    fourthHeart_UI.GetChild(2).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 13)
                {
                    fourthHeart_UI.GetChild(1).gameObject.SetActive(false);
                    fullLife--;

                    /// <point> �� ��Ʈ�� ���ܵξ�� �ϴ� GetChild(0)�� �н�.
                }
            }

            if (fullLife <= 12 && fullLife >= 9) // ����° ��Ʈ
            {
                if (fullLife == 12)
                {
                    thirdHeart_UI.GetChild(4).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 11)
                {
                    thirdHeart_UI.GetChild(3).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 10)
                {
                    thirdHeart_UI.GetChild(2).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 9)
                {
                    thirdHeart_UI.GetChild(1).gameObject.SetActive(false);
                    fullLife--;
                }
            }

            if (fullLife <= 8 && fullLife >= 5) // �ι�° ��Ʈ
            {
                if (fullLife == 8)
                {
                    secondHeart_UI.GetChild(4).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 7)
                {
                    secondHeart_UI.GetChild(3).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 6)
                {
                    secondHeart_UI.GetChild(2).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 5)
                {
                    secondHeart_UI.GetChild(1).gameObject.SetActive(false);
                    fullLife--;
                }
            }

            if (fullLife <= 4 && fullLife >= 1) // ù��° ��Ʈ
            {
                if (fullLife == 4)
                {
                    firstHeart_UI.GetChild(4).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 3)
                {
                    firstHeart_UI.GetChild(3).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 2)
                {
                    firstHeart_UI.GetChild(2).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 1)
                {
                    firstHeart_UI.GetChild(1).gameObject.SetActive(false);
                    fullLife--;
                }
            }

            if (fullLife == 0) // �÷��̾� ���� ó��
            {
                Debug.Log("�÷��̾� ����");
            }
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
