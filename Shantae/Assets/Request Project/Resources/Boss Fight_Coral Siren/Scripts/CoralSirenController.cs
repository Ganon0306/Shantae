using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralSirenController : MonoBehaviour
{
    // �ǰ� �� ���� ��ü�� ����
    private Color originColor = default;
    private Color transparentColor = default;
    // CoralSiren�� HP
    private float coralSirenHP = 15f;

    private bool already = false;

    // Start is called before the first frame update
    void Start()
    {
        originColor = transform.GetComponent<SpriteRenderer>().color;
        transparentColor = originColor;
        transparentColor.a = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (HitController.coralDamaged == true)
        {
            // �÷��̾ ���� �������� 4���� 9������ ����
            int getDamage = Random.Range(4, 10);
            // Empress HP ���. 
            coralSirenHP -= getDamage;

            StartCoroutine(FlashCoral());
        }

        if  (coralSirenHP <= 0 && already == false)
        {
            already = true;

            GameObject coralSiren_Back = GameObject.Find("Coral Siren_Back");

            coralSiren_Back.GetComponent<CoralSirenMoving>().enabled = false;
            coralSiren_Back.transform.position = new Vector2(10f, 10f);

            StartCoroutine(PlayerWin());
        }
    }

    private IEnumerator FlashCoral()
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

        transform.GetComponent<SpriteRenderer>().color = originColor;
        HitController.coralDamaged = false;

        StopCoroutine(FlashCoral());
    }

    private IEnumerator PlayerWin() // Coral Siren �й�
    {
        GameObject player = GameObject.Find("Player");

        bool down = false;

        // ȿ���� �Բ� ���� ��ǥ���� �ٴ� ���� ������. �ִϸ��̼��� Fire_Frail. 
        if (player.transform.position.x < 0)
        {
            if (down == false)
            {
                transform.position = new Vector2(4, 1.6f);
                down = true;
            }

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-3.9f, -1.4f),
                Time.deltaTime * 3);
        }
        else if (player.transform.position.x > 0)
        {
            if (down == false)
            {
                transform.position = new Vector2(-4, 1.6f);
                down = true;
            }

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-3.9f, -1.4f),
                Time.deltaTime * 3);
        }

        yield return null;
    }
}
