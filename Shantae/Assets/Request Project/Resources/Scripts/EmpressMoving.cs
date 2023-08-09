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
    #region ������ ���� ����
    // ������ ���� ����_�� or õ��
    private float randomValue = default;
    // ���� ���� ����_�ٴڿ���
    private float randomValue_Ground = default;
    // �ִϸ�����
    private Animator animator;
    // �ø��� ���� Sprite Renderer
    private SpriteRenderer spriteRenderer;
    #endregion

    #region ���� ���� ����
    public static bool ceiling = false;
    public static bool leftWall = false;
    public static bool rightWall = false;   
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
        ///<summary>
        /// ������ ���� �̵��� �׿� �´� ������ �����ϴ� �޼���
        /// </summary>
        
        // 3.5�� �� ������ �ൿ�� �����ϵ��� ����.(�Ŀ� �ƽ� ���� �� ���۵ǵ��� ���� �ʿ�)
        yield return new WaitForSeconds(3.5f);

        // ���� Empress Siren�� ü���� 0���� ũ�ٸ� ���� ���� 
        while (EmpressController.empressHP > 0)
        {
            //randomValue = Random.Range(0, 3);
            randomValue = 2.0f;
            randomValue_Ground = Random.Range(0, 2);

            if (randomValue == 0)
            { 
                // ���� ��
                transform.position = new Vector2(-6.6f, 1.54f);
                animator.Play("Float and Kiss");

                leftWall = true;
            }
            else if (randomValue == 1)
            {
                // ������ ��
                transform.position = new Vector2(6.43f, 1.54f);
                spriteRenderer.flipX = true;
                animator.Play("Float and Kiss");

                rightWall = true;
            }
            else if (randomValue == 2)
            {
                // õ��
                transform.position = new Vector2(-0.29f, 2.18f);

                animator.Play("Ceiling");

                yield return new WaitForSeconds(1.3f);

                ceiling = true;
            }

            yield return new WaitForSeconds(1.0f);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);


            // x������ ������ ������ �����·� �ǵ�����
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }

            if (randomValue_Ground == 0)
            {
                // �ٴ� Surf
                transform.position = new Vector2(-4.07f, -1.72f);
                animator.Play("Surf");
            }
            else if (randomValue_Ground == 1)
            {
                // �ٴ� Hop
                transform.position = new Vector2(-4.07f, -1.44f);
                animator.Play("Hopback");
            }

            yield return new WaitForSeconds(1.0f);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
