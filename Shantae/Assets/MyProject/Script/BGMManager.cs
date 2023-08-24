using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource bgm;
    public static bool playing = true;            // ������ ������ ���� �ڵ忡�� �ǵ��� bool��;
    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();

        bgm.loop = true;

        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playing)
        {
            bgm.Stop();
        }
    }
}
