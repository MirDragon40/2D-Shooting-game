using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public enum EnemyType    // 적 타입 열거형
{
    Basic,
    Target,
    Follower,

}
public class Enemy : MonoBehaviour
{
    // 목표: 적을 아래로 이동시키고 싶다
    // 속성:
    // - 속력
    public float Speed;
    public int Health = 2;

    private Vector2 _dir;

    public Transform playerTransform;
    public float followSpeed = 5f;

    [Header("아이템 프리팹")]
    public GameObject Item_Health;
    public GameObject Item_Speed;

    public Animator MyAnimator;

    public GameObject ExplosionVFXPrefab;


    // 목표:
    // Basic 타입: 아래로 이동
    // Target 타입: 처음 태어났을 때 플레이어가 있는 방향으로 이동
    // 속성
    // - EnemyType 타입
    // 구현 순서:
    // 2. 방향을 향해 이동한다.
    public EnemyType EType;

    private GameObject _target;



    void Start()
    {
        // 캐싱: 자주 쓰는 데이터를 더 가까운 장소에 저장해두고 필요할 때 가져다 쓰는거
        // 시작할 때 플레이어를 찾아서 기억해둔다. 
        _target = GameObject.Find("Player");

        MyAnimator = GetComponent<Animator>();

        if (EType == EnemyType.Target)
        {

            // 1. 시작할 때 방향을 구한다. (플레이어가 있는 방향)

            // 1-1. 플레이어를 찾는다.
            //GameObject target = GameObject.Find("Player");
            //GameObject.FindGameObjectsWithTag("Player");

            // 1-2. 방향을 구한다. (target - me)
            _dir = _target.transform.position - this.transform.position;
            // _dir.Normalize(); // 방향의 크기를 1로 만든다.

            // 1. 각도를 구한다
            // tan@ = y/x   -> @ = y/x*atan
            float radian = Mathf.Atan2(_dir.y, _dir.x);
            //Debug.Log(radian);  // 호도법 -> 라디안 값
            float degree = radian * Mathf.Rad2Deg;
            //Debug.Log(degree);

            // 2. 각도에 맞게 회전한다.
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90)); // 이미지 리소스에 맞게 90을 더한다.
            // transform.LookAt(_target.transform); => 위에꺼 안쓰고 이거 써도 됨.

        }


        else
        {
            _dir = Vector2.down;
        }
    }

    void Update()
    {
        if (EType == EnemyType.Follower)
        {
            //GameObject target = GameObject.Find("Player");

            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize();

            // 1. 각도를 구한다
            // tan@ = y/x   -> @ = y/x*atan
            float radian = Mathf.Atan2(_dir.y, _dir.x);
            //Debug.Log(radian);  // 호도법 -> 라디안 값
            float degree = radian * Mathf.Rad2Deg;
            //Debug.Log(degree);

            // 2. 각도에 맞게 회전한다.
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90)); // 이미지 리소스에 맞게 90을 더한다.
            // transform.LookAt(_target.transform); => 위에꺼 안쓰고 이거 써도 됨.
            transform.eulerAngles = new Vector3(0, 0, degree + 90);
        }

        // 2. 이동 시킨다.
        transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;
    }

    // 목표: 충돌하면 적과 플레이어를 삭제하고 싶다.
    // 구현 순서:
    // 1. 만약에 충돌이 일어나면

    // 충돌이 일어나면 호출되는 이벤트 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {


        // 충돌을 시작했을 때
        //Debug.Log("Enter");

        // 충돌한 게임 오브젝트의 태그를 확인
        //Debug.Log(collision.collider.tag); // Player or Bullet

        if (collision.collider.tag == "Player")
        {
            // 플레이어 스크립트를 가져온다.
            Player player = collision.collider.GetComponent<Player>();
            // 플레이어 체력을 -= 1
            player.SubPlayerHealth(1);


            GameObject heart1 = GameObject.Find("Heart1");
            GameObject heart2 = GameObject.Find("Heart2");
            GameObject heart3 = GameObject.Find("Heart3");


            // 플레이어의 생명이 줄어들 때마다 체력 오브젝트가 하나씩 파괴됨
            if (player.GetplayerHealth() == 2)
            {
                Destroy(heart1);
            }
            if (player.GetplayerHealth() == 1)
            {
                Destroy(heart2);
            }
            if (player.GetplayerHealth() == 0)
            {
                Destroy(heart3);
            }



            /*
            // - 아이템 만들고
            GameObject item = Instantiate(Item);
            // - 위치를 나의 위치로 수정
            item.transform.position = this.transform.position;
            */
            Destroy(this.gameObject);



            // 플레이어 체력이 적다면..
            if (player.GetplayerHealth() <= 0)
            {
                // 나 죽자

                Destroy(collision.collider.gameObject);
                // 목표: 50% 확률로 체력 올려주는 아이템, 50% 확률로 이동속도 올려주는 아이템

                /*
                // - 아이템 만들고
                GameObject item = Instantiate(Item);
                // - 위치를 나의 위치로 수정
                item.transform.position = this.transform.position;
                */

            }


        }


        else if (collision.collider.CompareTag("Bullet"))
        {
            Bullet bullet = collision.collider.GetComponent<Bullet>();


            // 총알 삭제
            // Destroy(collision.collider.gameObject);
            collision.collider.gameObject.SetActive(false);

            if (EType == EnemyType.Follower)
            {
                //GameObject itemEnemy = GameObject.Find("Enemy_Follow");

                Item item = gameObject.GetComponent<Item>();
                GameObject player = GameObject.Find("Player");


                MakeItem();

            }


            /*
            if (bullet.BType == 0)
            {
                Health -= 2;

            }
            else if (bullet.BType == 1)
            {
                Health -= 1;
            }
            */

            //Debug.Log(bullet.BType);
            //Debug.Log($"충돌전: {Health}");

            switch (bullet.BType)
            {

                case BulletType.Main:
                {
                    Health -= 2;
                    break;
                }

                case BulletType.Sub:
                {
                    Health -= 1;
                    break;
                }
            }

            //Debug.Log($"충돌후: {Health}");

            if (Health <= 0)
            {
                //Debug.Log($"죽음");

                // 나 죽자
                Death();

            }

            MyAnimator.Play("Hit");
        }



        // 2. 충돌한 너와 나를 삭제한다.
        // 너죽고 (플레이어)
        //Destroy(collision.collider.gameObject);
        // 나죽자 (나 자신)
        //Destroy(this.gameObject);



    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌 중일 때 매번 (닿아있을 때)
        //Debug.Log("Stay");

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 충돌이 끝났을 때
        // Debug.Log("Exit");

    }

    public void Death()
    {
        // 나 죽자
        gameObject.SetActive(false);
        //Destroy(this.gameObject);
        GameObject vfx = Instantiate(ExplosionVFXPrefab);
        vfx.transform.position = this.transform.position;

        // 목표: 스코어를 증가시키고싶다.
        // 1. 씬에서 ScoreManager 게임 오브젝트를 찾아온다. 
        // GameObject smGameObject = GameObject.Find("ScoreManager");
        // 2. ScoreManager 게임 오브젝트에서 ScoreManager스크립트 컴포넌트를 얻어온다.
        // ScoreManager scoreManager = smGameObject.GetComponent<ScoreManager>();
        // 3. 컴포넌트의 Score 속성을 증가시킨다. 
        // int score = scoreManager.GetScore();
        // scoreManager.SetScore(score +1);
        // Debug.Log(scoreManager.GetScore());

        // 싱글톤 객체 참조로 변경
        // ScoreManager.Instance.AddScore();
        ScoreManager.Instance.Score += 1;
        

    }

    public void MakeItem()
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            Destroy(this.gameObject);
            // - 체력 올려주는 아이템 만들고
            GameObject item_health = GameObject.Instantiate(Item_Health);

            // - 위치를 나의 위치로 수정
            item_health.transform.position = this.transform.position;
        }

        else
        {
            Destroy(this.gameObject);

            // - 이동속도 올려주는 아이템 만들고
            GameObject item_speed = GameObject.Instantiate(Item_Speed);
            // - 위치를 나의 위치로 수정
            item_speed.transform.position = this.transform.position;
        }
    }
}