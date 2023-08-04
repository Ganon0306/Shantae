using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    // ��׶��尡 ��鸮�� ���ǵ� 
    private float shakeSpeed = default;

    private void Awake() // �Ŀ� ��� �߰� �� ��簡 ����Ǹ� �ڷ�ƾ�� �����ϵ��� ���� �ɱ� (Update)
    {
        StartCoroutine(DoorTimer());
    }

    IEnumerator DoorTimer()
    {
        // ���� �Է� �� �� �� �� �ݴ� �޼���� �̵�
        yield return new WaitForSeconds(3.0f);
        CloseTwoDoors();
    }

    void CloseTwoDoors()
    {
        //First Background ��Ȱ��ȭ + Second Background�� ����
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        // ���� Camera Shaker �߰�
    }

}
