using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public enum EnemyType    // �� Ÿ�� ������
{
    Basic,
    Target,
    Follower,

}
public class Enemy : MonoBehaviour
{
    // ��ǥ: ���� �Ʒ��� �̵���Ű�� �ʹ�
    // �Ӽ�:
    // - �ӷ�
    public float Speed;
    public int Health = 2;

    private Vector2 _dir;

    public Transform playerTransform;
    public float followSpeed = 5f;

    [Header("������ ������")]
    public GameObject Item_Health;
    public GameObject Item_Speed;

    public Animator MyAnimator;

    public GameObject ExplosionVFXPrefab;


    // ��ǥ:
    // Basic Ÿ��: �Ʒ��� �̵�
    // Target Ÿ��: ó�� �¾�� �� �÷��̾ �ִ� �������� �̵�
    // �Ӽ�
    // - EnemyType Ÿ��
    // ���� ����:
    // 2. ������ ���� �̵��Ѵ�.
    public EnemyType EType;

    private GameObject _target;



    void Start()
    {
        // ĳ��: ���� ���� �����͸� �� ����� ��ҿ� �����صΰ� �ʿ��� �� ������ ���°�
        // ������ �� �÷��̾ ã�Ƽ� ����صд�. 
        _target = GameObject.Find("Player");

        MyAnimator = GetComponent<Animator>();

        if (EType == EnemyType.Target)
        {

            // 1. ������ �� ������ ���Ѵ�. (�÷��̾ �ִ� ����)

            // 1-1. �÷��̾ ã�´�.
            //GameObject target = GameObject.Find("Player");
            //GameObject.FindGameObjectsWithTag("Player");

            // 1-2. ������ ���Ѵ�. (target - me)
            _dir = _target.transform.position - this.transform.position;
            // _dir.Normalize(); // ������ ũ�⸦ 1�� �����.

            // 1. ������ ���Ѵ�
            // tan@ = y/x   -> @ = y/x*atan
            float radian = Mathf.Atan2(_dir.y, _dir.x);
            Debug.Log(radian);  // ȣ���� -> ���� ��
            float degree = radian * Mathf.Rad2Deg;
            Debug.Log(degree);

            // 2. ������ �°� ȸ���Ѵ�.
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90)); // �̹��� ���ҽ��� �°� 90�� ���Ѵ�.
            // transform.LookAt(_target.transform); => ������ �Ⱦ��� �̰� �ᵵ ��.

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

            // 1. ������ ���Ѵ�
            // tan@ = y/x   -> @ = y/x*atan
            float radian = Mathf.Atan2(_dir.y, _dir.x);
            Debug.Log(radian);  // ȣ���� -> ���� ��
            float degree = radian * Mathf.Rad2Deg;
            Debug.Log(degree);

            // 2. ������ �°� ȸ���Ѵ�.
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90)); // �̹��� ���ҽ��� �°� 90�� ���Ѵ�.
            // transform.LookAt(_target.transform); => ������ �Ⱦ��� �̰� �ᵵ ��.
            transform.eulerAngles = new Vector3(0, 0, degree + 90);
        }

        // 2. �̵� ��Ų��.
        transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;
    }

    // ��ǥ: �浹�ϸ� ���� �÷��̾ �����ϰ� �ʹ�.
    // ���� ����:
    // 1. ���࿡ �浹�� �Ͼ��

    // �浹�� �Ͼ�� ȣ��Ǵ� �̺�Ʈ �Լ�
    private void OnCollisionEnter2D(Collision2D collision)
    {


        // �浹�� �������� ��
        Debug.Log("Enter");

        // �浹�� ���� ������Ʈ�� �±׸� Ȯ��
        Debug.Log(collision.collider.tag); // Player or Bullet

        if (collision.collider.tag == "Player")
        {
            // �÷��̾� ��ũ��Ʈ�� �����´�.
            Player player = collision.collider.GetComponent<Player>();
            // �÷��̾� ü���� -= 1
            player.Health -= 1;


            GameObject heart1 = GameObject.Find("Heart1");
            GameObject heart2 = GameObject.Find("Heart2");
            GameObject heart3 = GameObject.Find("Heart3");


            // �÷��̾��� ������ �پ�� ������ ü�� ������Ʈ�� �ϳ��� �ı���
            if (player.Health == 2)
            {
                Destroy(heart1);
            }
            if (player.Health == 1)
            {
                Destroy(heart2);
            }
            if (player.Health == 0)
            {
                Destroy(heart3);
            }



            /*
            // - ������ �����
            GameObject item = Instantiate(Item);
            // - ��ġ�� ���� ��ġ�� ����
            item.transform.position = this.transform.position;
            */
            Destroy(this.gameObject);



            // �÷��̾� ü���� ���ٸ�..
            if (player.Health <= 0)
            {
                // �� ����

                Destroy(collision.collider.gameObject);
                // ��ǥ: 50% Ȯ���� ü�� �÷��ִ� ������, 50% Ȯ���� �̵��ӵ� �÷��ִ� ������

                /*
                // - ������ �����
                GameObject item = Instantiate(Item);
                // - ��ġ�� ���� ��ġ�� ����
                item.transform.position = this.transform.position;
                */
            }


        }


        else if (collision.collider.tag == "Bullet")
        {
            Bullet bullet = collision.collider.GetComponent<Bullet>();
            Destroy(collision.collider.gameObject);

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
            //Debug.Log($"�浹��: {Health}");

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

            //Debug.Log($"�浹��: {Health}");

            if (Health <= 0)
            {
                //Debug.Log($"����");

                // �� ����
                Death();

            }

            MyAnimator.Play("Hit");
        }



        // 2. �浹�� �ʿ� ���� �����Ѵ�.
        // ���װ� (�÷��̾�)
        //Destroy(collision.collider.gameObject);
        // ������ (�� �ڽ�)
        //Destroy(this.gameObject);



    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // �浹 ���� �� �Ź� (������� ��)
        //Debug.Log("Stay");

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // �浹�� ������ ��
        // Debug.Log("Exit");

    }

    public void Death()
    {
        // �� ����
        Destroy(this.gameObject);
        GameObject vfx = Instantiate(ExplosionVFXPrefab);
        vfx.transform.position = this.transform.position;

        // ��ǥ: ���ھ ������Ű��ʹ�.
        // 1. ������ ScoreManager ���� ������Ʈ�� ã�ƿ´�. 
        GameObject smGameObject = GameObject.Find("ScoreManager");
        // 2. ScoreManager ���� ������Ʈ���� ScoreManager��ũ��Ʈ ������Ʈ�� ���´�.]
        ScoreManager scoreManager = smGameObject.GetComponent<ScoreManager>();
        // 3. ������Ʈ�� Score �Ӽ��� ������Ų��. 
        int score = scoreManager.GetScore();
        scoreManager.SetScore(score +1);
        Debug.Log(scoreManager.GetScore());
    }

    public void MakeItem()
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            Destroy(this.gameObject);
            // - ü�� �÷��ִ� ������ �����
            GameObject item_health = GameObject.Instantiate(Item_Health);

            // - ��ġ�� ���� ��ġ�� ����
            item_health.transform.position = this.transform.position;
        }

        else
        {
            Destroy(this.gameObject);

            // - �̵��ӵ� �÷��ִ� ������ �����
            GameObject item_speed = GameObject.Instantiate(Item_Speed);
            // - ��ġ�� ���� ��ġ�� ����
            item_speed.transform.position = this.transform.position;
        }
    }
}