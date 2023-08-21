using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // ������Ʈ Ǯ���� ����� ������
    public int poolSize; // Ǯ ũ��
    public string tagToPool; // �ش� ������Ʈ Ǯ�� �±�

    private List<GameObject> pooledObjects = new List<GameObject>();

    // ������Ʈ Ǯ �ʱ�ȭ
    public void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObj = Instantiate(prefab);
            newObj.SetActive(false);
            pooledObjects.Add(newObj);
        }
    }

    // ������Ʈ Ǯ���� ������Ʈ ��������
    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }

    // ������Ʈ�� Ǯ�� ����������
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
