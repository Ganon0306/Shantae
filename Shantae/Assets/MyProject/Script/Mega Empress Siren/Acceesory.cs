using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceesory : MonoBehaviour
{
   // public bool startTimer = false;

    public GameObject accesoryBreak;
    public GameObject triggerAcceesory;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerAcceesory != null)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else if (triggerAcceesory == null)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        // �ڽ� ������Ʈ ī��Ʈ
        int activeObjectCount = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject childObject = transform.GetChild(i).gameObject;
            if (childObject.activeSelf) // ������Ʈ�� Ȱ��ȭ�� �������� Ȯ��
            {
                activeObjectCount++;
            }
        }
        // �ڽ� ������Ʈ ī��Ʈ

        if (activeObjectCount == 0 && triggerAcceesory == null)
        {
            //startTimer = true;
            accesoryBreak.SetActive(true);
            Destroy(gameObject);
        }
    }
}
