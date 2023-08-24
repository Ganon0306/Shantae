using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Empress Siren�� �й� ������ �ٷ�� ��ũ��Ʈ. "PlayerAttack" �±׿� �浹�ϸ� 
/// Empress Siren�� HP�� ����.
/// </summary>

public class EmpressController : MonoBehaviour
{
    #region Empress Siren�� �ǰ�, �й� ���� Ȯ�� ����
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

    private bool alreadyRun = false;
    #endregion

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
        if (empressHP <= 0 && alreadyRun == false)
        {
            // EmpressMoving ���� �޼��� �߰�
            transform.GetComponent<EmpressMoving>().enabled = false;

            alreadyRun = true;
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
        GameObject.FindWithTag("Player").transform.GetChild(0).
            GetComponent<PlayerExit>().enabled = true;

        yield return new WaitForSeconds(0.5f);

        playerPosition = GameObject.FindWithTag("Player").transform.position;

        // �й� �� �÷��̾ Empress Siren�� ���ʿ� ��ġ
        if (playerPosition.x < 0)
        {
            Debug.Log("���� ����");
            transform.position = new Vector2(playerPosition.x + 4f, -1.44f);
            transform.GetComponent<SpriteRenderer>().flipX = true;
            animator.SetTrigger("Empress Lose");
        }
        else if (playerPosition.x >= 0) // Empress Siren�� �����ʿ� ��ġ
        {
            Debug.Log("������ ����");
            transform.position = new Vector2(playerPosition.x - 4f, -1.44f);
            transform.GetComponent<SpriteRenderer>().flipX = false;

            animator.SetTrigger("Empress Lose");
        }

        yield return new WaitForSeconds(3.5f);

        teleportAnimator.SetActive(true);
        transform.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds
            (teleportAnimator.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
        teleportAnimator.SetActive(false);

        yield return new WaitForSeconds(3f);

        GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
        CameraShake.instance.StartCoroutine(CameraShake.instance.OpenTheDoor());

        StopCoroutine(PlayerWin());

        // �ڱ��ڽ�(Empress Siren ����)
        gameObject.SetActive(false);
    }
}
