using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BombUpDown : MonoBehaviour
{
    private float upSpeed = 10f;
    private float downSpeed = 25.0f;
    private bool alreadyScaleUp = false;
    private Animator bombAnimator;
    private bool backThePool = false;

    private Vector2 poolPosition_bomb = new Vector2(0f, 10f);

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        bombAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ���� �߻�
        // ��ź�� �Ʒ��� ���� ����

        transform.Translate(Vector2.up * upSpeed * Time.deltaTime);

        // ��ź�� ���� ��ǥ y�� 7�� �����ϸ�
        if (transform.position.y >= 7f && alreadyScaleUp == false)
        {
            Debug.Log("������");
            transform.localScale = transform.localScale * 1.5f;
            alreadyScaleUp = true;
        }

        //Ư�� ��ǥ���� ��ź�� Ű���ٸ� �Ʒ��� ������
        if (alreadyScaleUp == true)
        {
            transform.Translate(Vector2.down * downSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("�浹!");

        // ���� �ٴ� �ݶ��̴��� �浹�ϸ�
        if (collision.gameObject.CompareTag("Ground"))
        {
            // �ִϸ��̼� ���
            transform.GetComponent<Animator>().enabled = true;

            StartCoroutine(BombAnimationTimer());
        }

        // �ִϸ��̼� ����� ������ �ڽĿ�����Ʈ(0) ������ ���� Ǯ�� ����
        if (backThePool == true)
        {
            Debug.Log("Ǯ�� �̵�");
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            transform.position = poolPosition_bomb;

            backThePool = false;
        }
    }

    IEnumerator BombAnimationTimer()
    {
        yield return new WaitForSeconds
               (bombAnimator.GetCurrentAnimatorStateInfo(0).length);

        backThePool = true;
    }
}
