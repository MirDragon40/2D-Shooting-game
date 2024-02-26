using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip[] Clips;  // 0: 터치 사운드, 1: 체력 아이템, 2: 이동속도 아이템
    public AudioSource MyAudioSource;

    private int _playerHealth;

    public float dieNum = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enermy"))
        {
            PlayTouchSound();
        }

    }
    private int _health = 3;
    private void Start()
    {
        /*// GetComponent<컴포넌트 타입 > (): -> 게임 오브젝트의 컴포넌트를 가져오는 메서드
        // 못받아오면 Null값 반환
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
        Debug.Log(_health);
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

    public int GetplayerHealth()
    {
        return _health;
    }
    public void SetplayerHealth(int health)
    {
        _health = health;
    }

    public void AddPlayerHealth(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _health += amount;
    }

    public void SubPlayerHealth(int health)
    {
        if (health <= 0)
        {
            return;
        }
        _health -= health;

    }
}

