using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip[] Clips;  // 0: ��ġ ����, 1: ü�� ������, 2: �̵��ӵ� ������
    public AudioSource MyAudioSource;

    public float dieNum = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enermy")
        {
            PlayTouchSound();
        }

    }
    public int Health = 3;
    private void Start()
    {
        /*// GetComponent<������Ʈ Ÿ�� > (): -> ���� ������Ʈ�� ������Ʈ�� �������� �޼���
        // ���޾ƿ��� Null�� ��ȯ
        SpriteRenderer sr = GetComponent<SpriteRenderer> ();
        sr.color = Color.white;

        //Transform tr = GetComponent<Transform>();
        //tr.position = new Vector3(0f, -2.33f, 7f);
        transform.position = new Vector3(0f, -2.33f, 7f);

        PlayerMove playerMove = GetComponent<PlayerMove>();
        Debug.Log(playerMove.Speed);
        playerMove.Speed = 5f;
        Debug.Log(playerMove.Speed);*/

        

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {

        }
    }

    public void PlayTouchSound()
    {
        Debug.Log(0);
        MyAudioSource.clip = Clips[0];
        MyAudioSource.Play();
    }

    public void PlayItem1Sound()
    {
        Debug.Log(1);
        MyAudioSource.clip = Clips[1];
        MyAudioSource.Play();
    }

    public void PlayItem2Sound()
    {
        Debug.Log(2);

        MyAudioSource.clip = Clips[2];
        MyAudioSource.Play();
    }
}
