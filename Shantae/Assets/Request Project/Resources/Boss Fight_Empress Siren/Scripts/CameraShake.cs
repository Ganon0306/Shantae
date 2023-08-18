using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ŭ����: ���� ������Ʈ ��� �� ī�޶� ��鸮�� ȿ��
/// </summary>

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    // Camera ������Ʈ���� �� ��
    private const float firstShakeTime = 0.15f;
    private const float secondShakeTime = 0.1f;
    private const float shakeSpeed = 10.0f;
    private const float firstShakeAmount = 0.1f;
    private const float secondShakeAmount = 0.05f;

    private Vector3 originalPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // ����ī�޶� �ı� ����
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

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    public IEnumerator ShakeThisCam()
    {
        ///<summary>
        /// ī�޶� ���� �ڷ�ƾ. 
        ///</summary>
        
        Vector3 originPosition = transform.position;

        float elapsed = 0.0f;

        while (elapsed < firstShakeTime)
        {
            float yOffset = Mathf.Sin(Time.time * shakeSpeed) * firstShakeAmount;
            transform.position = new Vector3
                (originPosition.x, originPosition.y - yOffset, -10f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // ���� ��ǥ�� ����
        transform.position = originalPosition;

        elapsed = 0.0f;

        while (elapsed < secondShakeTime)
        {
            float yOffset = Mathf.Sin(Time.time * shakeSpeed) * secondShakeAmount;
            transform.position = new Vector3
                (originPosition.x, originPosition.y - yOffset, -10f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // ���� ��ǥ�� ����
        transform.position = originalPosition;

        StopCoroutine(ShakeThisCam());
    }
}
