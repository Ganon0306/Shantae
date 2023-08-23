using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Empress Siren�� �й� ������ �ٷ�� ��ũ��Ʈ. "PlayerAttack" �±׿� �浹�ϸ� 
/// Empress Siren�� HP�� ����.
/// </summary>

public class EmpressController : MonoBehaviour
{
    // Empress Siren�� HP
    public static float empressHP = default;
    // �÷��̾� ������ ���� (Empress �ǰ� ����)
    private float getDamage = default;
    // �ִϸ�����
    private Animator animator;
    // Empress Siren�� �ڷ���Ʈ 
    private GameObject teleportAnimator = default;
    private GameObject teleportParticle = default;
    // ��׶��� ��ü�� ����
    private GameObject background = default;

    private void Start()
    {
        // Empress Siren ���� ������Ʈ�� !null���� Ȯ��
        Debug.Assert(this.gameObject != null);

        animator = GetComponent<Animator>();
        teleportAnimator = transform.GetChild(0).gameObject;
        teleportParticle = transform.GetChild(1).gameObject;

        background = GameObject.Find("Backgrounds");

        empressHP = 100f;
    }

    private void Update()
    {
        // Empress Siren�� �й� Ȯ��
        if (empressHP <= 0)
        {
            // �÷��̾ ���� �������� 4���� 9������ ����
            getDamage = Random.Range(4, 10);
            // Empress HP ���. 
            empressHP -= getDamage;

            // EmpressMoving �ڷ�ƾ ���� �޼��� �߰�
            StopCoroutine(EmpressMoving.instance.RandomMoving());

            StartCoroutine(PlayerWin());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /// <problem> �÷��̾��� ������ �� ���� �ʴ´�. �� �ڷ�ƾ���� ���� ����. 
        if (collision.CompareTag("PlayerAttack"))
        {
            StartCoroutine(FlashEmpress());
        }
    }

    private IEnumerator FlashEmpress()
    {
        Debug.Log("�� �ǰ� �ڷ�ƾ ����");

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Color transparentColor = originalColor;
        transparentColor.a = 0.5f;

        float blinkTime = 1;
        float nowTime = 0;

        while (nowTime < blinkTime)
        {
            spriteRenderer.color = transparentColor;
            yield return new WaitForSeconds(0.2f);

            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.2f);
            blinkTime += Time.deltaTime;
        }

        spriteRenderer.color = originalColor;

        StopCoroutine(FlashEmpress());
    }

    private IEnumerator PlayerWin()
    {
        if (empressHP <= 0)
        {
            animator.SetTrigger("Empress Lose");
            yield return new WaitForSeconds(3);

            teleportAnimator.SetActive(true);
            teleportParticle.GetComponent<ParticleSystem>().Play();

            background.transform.GetChild(1).gameObject.SetActive(true);
            background.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("end"))
        {
            AllSceneManager.instance.StartCoroutine(AllSceneManager.instance.OpenLoadingScene_Second());
        }
    }
}
