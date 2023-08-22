using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ��ü UI�� ESC ���� ���� �Ѱ�. ���� Lobby�� �̵�. 
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // ĵ������ ��Ʈ
    private GameObject canvas_UI;
    private Transform firstHeart_UI;
    private Transform secondHeart_UI;
    private Transform thirdHeart_UI;
    private Transform fourthHeart_UI;

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
            /// <point> GameManager�� ���� Lobby�� �ű⵵�� ��. 
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
                    PlayerController.gotDamage = false;

                    fourthHeart_UI.GetChild(4).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 15)
                {
                    PlayerController.gotDamage = false;

                    fourthHeart_UI.GetChild(3).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 14)
                {
                    PlayerController.gotDamage = false;

                    fourthHeart_UI.GetChild(2).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 13)
                {
                    PlayerController.gotDamage = false;

                    fourthHeart_UI.GetChild(1).gameObject.SetActive(false);
                    fullLife--;

                    /// <point> �� ��Ʈ�� ���ܵξ�� �ϴ� GetChild(0)�� �н�.
                }
            }

            if (fullLife <= 12 && fullLife >= 9) // ����° ��Ʈ
            {
                if (fullLife == 12)
                {
                    PlayerController.gotDamage = false;

                    thirdHeart_UI.GetChild(4).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 11)
                {
                    PlayerController.gotDamage = false;

                    thirdHeart_UI.GetChild(3).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 10)
                {
                    PlayerController.gotDamage = false;

                    thirdHeart_UI.GetChild(2).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 9)
                {
                    PlayerController.gotDamage = false;

                    thirdHeart_UI.GetChild(1).gameObject.SetActive(false);
                    fullLife--;
                }
            }

            if (fullLife <= 8 && fullLife >= 5) // �ι�° ��Ʈ
            {
                if (fullLife == 8)
                {
                    PlayerController.gotDamage = false;

                    secondHeart_UI.GetChild(4).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 7)
                {
                    PlayerController.gotDamage = false;

                    secondHeart_UI.GetChild(3).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 6)
                {
                    PlayerController.gotDamage = false;

                    secondHeart_UI.GetChild(2).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 5)
                {
                    PlayerController.gotDamage = false;

                    secondHeart_UI.GetChild(1).gameObject.SetActive(false);
                    fullLife--;
                }
            }

            if (fullLife <= 4 && fullLife >= 1) // ù��° ��Ʈ
            {
                if (fullLife == 4)
                {
                    PlayerController.gotDamage = false;

                    firstHeart_UI.GetChild(4).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 3)
                {
                    PlayerController.gotDamage = false;

                    firstHeart_UI.GetChild(3).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 2)
                {
                    PlayerController.gotDamage = false;

                    firstHeart_UI.GetChild(2).gameObject.SetActive(false);
                    fullLife--;
                }
                else if (fullLife == 1)
                {
                    PlayerController.gotDamage = false;

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
