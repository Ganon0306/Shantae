using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

/// <summary>
/// Ŭ����: ���� Empress Siren�� �������� ���� 
/// �м�: ������ ���� ��, õ��, �ٴڿ����� �� ���� ������ ����. 
/// �ٴ��� ������ �� ���� ������ �����ߴٸ�
/// ���� ������ ������ �ٴڿ��� ������.
/// </summary>

public class EmpressMoving : MonoBehaviour
{
    public static EmpressMoving instance;
    private BoxCollider2D empressHitBox;

    #region ������ ���� ����
    // ������ ���� ����_�� or õ��
    private float randomValue = default;
    // ���� ���� ����_�ٴڿ���
    private float randomValue_Ground = default;
    // �ִϸ�����
    private Animator animator;
    // �ø��� ���� Sprite Renderer
    private SpriteRenderer spriteRenderer;
    // �ڷ���Ʈ ȿ���� ���� ���ӿ�����Ʈ (�ִϸ��̼�)
    private GameObject teleportAni;
    private Animator teleportAniTime = default;
    // �ڷ���Ʈ ȿ���� ���� ��ƼŬ
    private ParticleSystem teleportParticle;
    #endregion

    #region ���� ���� ����
    public static bool ceiling = false;
    public static bool leftWall = false;
    public static bool rightWall = false;
    public static bool surf = false;
    public static bool hopBack = false;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        teleportAni = transform.GetChild(0).gameObject;
        Debug.Assert(teleportAni!= null);
        teleportAniTime = teleportAni.GetComponent<Animator>();

        teleportParticle = transform.GetChild(1).GetComponent<ParticleSystem>();
        Debug.Assert(teleportParticle != null);
    }

    private void Start()
    {
        empressHitBox = transform.GetComponent<BoxCollider2D>();

        StartCoroutine(RandomMoving());
    }

    public IEnumerator RandomMoving()
    {
        ///<summary>
        /// ������ ���� �̵��� �׿� �´� ������ �����ϴ� �޼���
        /// </summary>
        
        /// <point> Empress Siren�� ���� ���� �ð�. CameraShake�� ����. (�ƽ� ���� �� ����)
        yield return new WaitForSeconds(3.5f);

        spriteRenderer.flipX = false;

        // ���� Empress Siren�� ü���� 0���� ũ�ٸ� ���� ���� 
        while (EmpressController.empressHP > 0)
        {
            leftWall = false;
            rightWall = false;

            randomValue = Random.Range(0, 3);
            randomValue = 2; // �ӽ�

            if (randomValue == 0)
            {
                // �ڷ���Ʈ �ִϸ��̼� ����ð� ���� ���
                yield return new WaitForSeconds
                    (teleportAniTime.GetCurrentAnimatorStateInfo(0).length - 0.2f);
                teleportAni.SetActive(false);

                // �ݶ��̴� ������ ����
                empressHitBox.size = new Vector2(5, 4.2f);
                // �ݶ��̴� ��ġ ����
                empressHitBox.offset = new Vector2(0.8f, -0.2f);

                // ���� ��
                transform.position = new Vector2(-6.6f, 1.54f);
                animator.Play("Float and Kiss");

                yield return new WaitForSeconds(2.1f);

                leftWall = true;

                yield return new WaitForSeconds
                    (animator.GetCurrentAnimatorStateInfo(0).length - 2.1f);

                teleportAni.SetActive(true);
                teleportParticle.Play();
            }
            else if (randomValue == 1)
            {
                // �ڷ���Ʈ �ִϸ��̼� ����ð� ���� ���
                yield return new WaitForSeconds
                    (teleportAniTime.GetCurrentAnimatorStateInfo(0).length - 0.2f);
                teleportAni.SetActive(false);

                // �ݶ��̴� ������ ����
                empressHitBox.size = new Vector2(5, 4.2f);
                // �ݶ��̴� ��ġ ����
                empressHitBox.offset = new Vector2(0.8f, -0.2f);

                // ������ ��
                transform.position = new Vector2(6.43f, 1.54f);
                spriteRenderer.flipX = true;
                animator.Play("Float and Kiss");

                yield return new WaitForSeconds(2.1f);

                rightWall = true;

                yield return new WaitForSeconds
                    (animator.GetCurrentAnimatorStateInfo(0).length - 2.1f);

                teleportAni.SetActive(true);
                teleportParticle.Play();
            }
            else if (randomValue == 2)
            {
                // �ڷ���Ʈ �ִϸ��̼� ����ð� ���� ���
                yield return new WaitForSeconds
                    (teleportAniTime.GetCurrentAnimatorStateInfo(0).length - 0.2f);
                teleportAni.SetActive(false);

                // �ݶ��̴� ������ ����
                empressHitBox.size = new Vector2(3.2f, 3);
                // �ݶ��̴� ��ġ ����
                empressHitBox.offset = new Vector2(0.3f, 0.2f);

                // õ��
                transform.position = new Vector2(-0.29f, 2.18f);

                animator.Play("Ceiling");

                yield return new WaitForSeconds(1.3f);

                ceiling = true;
                CameraShake.instance.StartCoroutine(CameraShake.instance.CeilingShake());

                yield return new WaitForSeconds
                    (animator.GetCurrentAnimatorStateInfo(0).length - 1.3f);

                teleportAni.SetActive(true);
                teleportParticle.Play();
            }

            randomValue_Ground = Random.Range(0, 2);
            randomValue_Ground = 0; // �ӽ�

            // x������ ������ ������ �����·� �ǵ�����
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }

            if (randomValue_Ground == 0)
            {
                // �ڷ���Ʈ �ִϸ��̼� ����ð� ���� ���
                yield return new WaitForSeconds
                    (teleportAniTime.GetCurrentAnimatorStateInfo(0).length - 0.2f);
                teleportAni.SetActive(false);

                // �ݶ��̴� ������ ����
                empressHitBox.size = new Vector2(6.5f, 4.2f);
                // �ݶ��̴� ��ġ ����
                empressHitBox.offset = new Vector2(0.5f, -0.2f);

                // �ٴ� Surf
                transform.position = new Vector2(-4.07f, -1.72f);
                animator.Play("Surf");

                yield return new WaitForSeconds(0.6f);

                surf = true;
                    
                yield return new WaitForSeconds
                    (animator.GetCurrentAnimatorStateInfo(0).length - 0.6f);

                teleportAni.SetActive(true);
                teleportParticle.Play();
            }
            else if (randomValue_Ground == 1)
            {
                // �ڷ���Ʈ �ִϸ��̼� ����ð� ���� ���
                yield return new WaitForSeconds
                    (teleportAniTime.GetCurrentAnimatorStateInfo(0).length - 0.2f);
                teleportAni.SetActive(false);

                // �ݶ��̴� ������ ����
                empressHitBox.size = new Vector2(4, 4.2f);
                // �ݶ��̴� ��ġ ����
                empressHitBox.offset = new Vector2(-0.2f, -0.2f);

                // �ٴ� Hop
                transform.position = new Vector2(-4.07f, -1.44f);
                animator.Play("Hopback");

                yield return new WaitForSeconds(1.0f);

                hopBack = true;

                yield return new WaitForSeconds
                    (animator.GetCurrentAnimatorStateInfo(0).length - 1.0f);

                teleportAni.SetActive(true);
                teleportParticle.Play();
            }

            surf = false;
            hopBack = false;
        }
    }
}
