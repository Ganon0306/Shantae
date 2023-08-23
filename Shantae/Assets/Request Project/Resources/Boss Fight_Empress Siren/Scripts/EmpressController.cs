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
    // �ǰ� �� ���� ��ü�� ����
    private Color originColor = default;
    private Color transparentColor = default;
    // �÷��̾� ������
    private Vector2 playerPosition = default;
    // Empress Siren�� ������
    private Vector2 empressPosition = default;

    private void Start()
    {
        // Empress Siren ���� ������Ʈ�� !null���� Ȯ��
        Debug.Assert(this.gameObject != null);

        animator = GetComponent<Animator>();
        teleportAnimator = transform.GetChild(0).gameObject;
        teleportParticle = transform.GetChild(1).gameObject;

        empressHP = 15f; // �ӽ� ����

        originColor = transform.GetComponent<SpriteRenderer>().color;
        transparentColor = originColor;
        transparentColor.a = 0.5f;
    }

    private void Update()
    {
        // Empress Siren�� �й� Ȯ��
        if (empressHP <= 0)
        {
            // EmpressMoving �ڷ�ƾ ���� �޼��� �߰�
            StopCoroutine(EmpressMoving.instance.RandomMoving());

            StartCoroutine(PlayerWin());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Empress Siren�� �ǰ� Ȯ��
        if (collision.CompareTag("PlayerAttack") && empressHP > 0)
        {
            // �÷��̾ ���� �������� 4���� 9������ ����
            getDamage = Random.Range(4, 10);
            // Empress HP ���. 
            empressHP -= getDamage;

            StartCoroutine(FlashEmpress());
        }
    }

    private IEnumerator FlashEmpress()
    {
        float blinkTime = 1;
        float nowTime = 0;

        while (nowTime < blinkTime)
        {
            nowTime += Time.deltaTime * 30;

            transform.GetComponent<SpriteRenderer>().color = transparentColor;
            yield return new WaitForSeconds(0.1f);

            transform.GetComponent<SpriteRenderer>().color = originColor;
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("���� Ż��");

        transform.GetComponent<SpriteRenderer>().color = originColor;

        StopCoroutine(FlashEmpress());
    }

    private IEnumerator PlayerWin()
    {
        playerPosition = GameObject.FindWithTag("Player").transform.position;
        empressPosition = transform.position;

        GameObject.FindWithTag("Player").GetComponent<PlayerExit>().enabled = true;

        // �й� �� �÷��̾ Empress Siren�� ���ʿ� ��ġ
        if (playerPosition.x < empressPosition.x)
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (playerPosition.x > empressPosition.x) // Empress Siren�� �����ʿ� ��ġ
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }

        animator.SetTrigger("Empress Lose");
        yield return new WaitForSeconds(3);

        teleportAnimator.SetActive(true);
        yield return new WaitForSeconds
            (teleportAnimator.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
        teleportAnimator.SetActive(false);

        transform.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(2f);

        CameraShake.instance.StartCoroutine(CameraShake.instance.OpenTheDoor());

        // �ڱ��ڽ�(Empress Siren ����)
        gameObject.SetActive(false);
    }
}
