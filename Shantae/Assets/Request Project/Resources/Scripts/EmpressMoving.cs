using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

/// <summary>
/// Ŭ����: ���� Empress Siren�� �������� ���� 
/// �м�: ������ ���� ��, õ��, �ٴڿ����� �� ���� ������ ����. �ٴ��� ������ �� ���� ������ �����ߴٸ�
/// ���� ������ ������ �ٴڿ��� ������.
/// </summary>

public class EmpressMoving : MonoBehaviour
{
    #region
    // ������ ���� ����_�� or õ��
    private float randomValue = default;
    // ���� ���� ����_�ٴڿ���
    private float randomValue_Ground = default;
    // �ִϸ�����
    private Animator animator;
    // �ø��� ���� Sprite Renderer
    private SpriteRenderer spriteRenderer;
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(RandomMoving());
    }

    IEnumerator RandomMoving()
    {
        // 3.5�� �� ������ �ൿ�� �����ϵ��� ����.(�Ŀ� �ƽ� ���� �� ���۵ǵ��� ���� �ʿ�)
        yield return new WaitForSeconds(3.5f);

        // ���� Empress Siren�� ü���� 0���� ũ�ٸ� ���� ���� 
        while (EmpressController.empressHP > 0)
        {
            randomValue = Random.Range(0, 3);
            randomValue_Ground = Random.Range(0, 2);

            if (randomValue == 0)
            { 
                // ���� ��
                transform.position = new Vector2(-6.6f, 1.54f);
                //animator.SetTrigger("Side Wall");
                animator.Play("Float and Kiss");

                Debug.Log("1. ���� ���� �ٱ�");
            }
            else if (randomValue == 1)
            {
                // ������ ��
                transform.position = new Vector2(6.43f, 1.54f);
                spriteRenderer.flipX = true;
                //animator.SetTrigger("Side Wall");
                animator.Play("Float and Kiss");

                Debug.Log("2. ������ ���� �ٱ�");

            }
            else if (randomValue == 2)
            {
                // õ��
                transform.position = new Vector2(-0.29f, 2.18f);
                //animator.SetTrigger("Ceiling");
                animator.Play("Ceiling");

                Debug.Log("3. õ�忡 �ٱ�");
            }

            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            //animator.ResetTrigger("Side Wall");
            //animator.ResetTrigger("Ceiling");

            // x������ ������ ������ �����·� �ǵ�����
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }

            if (randomValue_Ground == 0)
            {
                // �ٴ� Surf
                transform.position = new Vector2(-4.07f, -1.44f);
                //animator.SetTrigger("Ground_Surf");
                animator.Play("Surf");

                Debug.Log("4. Surf ����");
            }
            else if (randomValue_Ground == 1)
            {
                // �ٴ� Hop
                transform.position = new Vector2(-4.07f, -1.44f);
                //animator.SetTrigger("Ground_Hop");
                animator.Play("Hopback");

                Debug.Log("5. Hop ����");
            }

            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            //animator.ResetTrigger("Ground_Surf");
            //animator.ResetTrigger("Ground_Hop");

        }
    }

   
    /// ���� ���� �� ���ĵ� Vector2(-4.07f, -1.44f)
    /// ���� BlowKiss Vector2(-6.6f, 1.54f)
    /// Ʈ���� ���� �� ResetTrigger

}
