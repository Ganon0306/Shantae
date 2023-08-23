using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllSceneManager : MonoBehaviour
{
    public static AllSceneManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �Ͻ����� ȭ�� �ʿ�
        }
    }

    public IEnumerator OpenLoadingScene()
    {
        SceneManager.LoadScene("Loading Scene", LoadSceneMode.Single);

        yield return new WaitForSeconds(3);

        /// <point> ���� �ϼ� �� 'Lobby'�� ��ü
        SceneManager.LoadScene("Boss Fight_Empress Siren", LoadSceneMode.Single);
    }

    public IEnumerator OpenLoadingScene_Second()
    {
        Debug.Log("�ڷ�ƾ ����!");

        SceneManager.LoadScene("Loading Scene", LoadSceneMode.Single);

        yield return new WaitForSeconds(3);

        /// <point> ���� �ϼ� �� 'Mega Empress Siren'���� ��ü
        SceneManager.LoadScene("Boss Fight_Coral Siren", LoadSceneMode.Single);
    }
}
