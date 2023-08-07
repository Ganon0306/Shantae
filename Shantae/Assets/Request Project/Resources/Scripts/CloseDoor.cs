using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ŭ����: ��� �н� �� ���� ���� �� ���� ������ ȿ��
/// </summary>

public class CloseDoor : MonoBehaviour
{
    private void Awake() // �Ŀ� ��� �߰� �� ��簡 ����Ǹ� �ڷ�ƾ�� �����ϵ��� ���� �ɱ� (Update�� ����)
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
        /// <summary>
        /// ���� �����ִ� ��׶��忡�� ���� ���� ��׶���� ��ü 
        /// </summary>>
        
        // First Background ��Ȱ��ȭ + Second Background�� ����
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        // Camera Shaker �̵�
        StartCoroutine(CameraShake.instance.ShakeThisCam());
    }
}
