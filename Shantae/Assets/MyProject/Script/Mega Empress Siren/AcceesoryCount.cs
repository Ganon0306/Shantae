using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceesoryCount : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �ڽ� ������Ʈ ī��Ʈ
        int activeObjectCount = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject childObject = transform.GetChild(i).gameObject;
            if (childObject.activeSelf) 
            {
                activeObjectCount++;
            }
        }
        // �ڽ� ������Ʈ ī��Ʈ

        if (activeObjectCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
