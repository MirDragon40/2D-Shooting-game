using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    public float Timer = 0f;   // 시간을 체크할 변수
    public float Delaytime = 1f;
    public float Speed;

    public AudioSource CoinItem;
    public AudioSource SpeedItem;

    public GameObject ItemVFXPrefab_H;
    public GameObject ItemVFXPrefab_S;

    public enum ItemType
    {
        Health,
        Speed

    }     // 0: 체력을 올려주는 타입, 1: 스피드를 올려주는 타입

    public ItemType MyType;

    public float Timer2 = 0f;
    public float Delaytime2 = 3f;

    private Vector2 _dir;

    public Animator MyAnimator;

    private void Awake()
    {
        MyAnimator = GetComponent<Animator>();

        // 열거형과 숫자는 서로 형변환이 된다. 
        MyAnimator.SetInteger("ItemType", (int)MyType);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enter");
    }

    // (다른 콜라이더에 의해) 트리거가 발동될 때
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log("트리거 시작!");


        // 목적: 플레이어의 체력을 올리고 싶다.
        // 순서:
        // 1. 플레이어 스크립트 받아오기
        // GameObject playerGameObject = GameObject.Find("Player");
        // Player player = playerGameObject.GetComponent<Player>();

        // 충돌한 상대방 콜라이더에서 Player 컴포넌트 가져오기: 

        // 2. 플레이어 체력 올리기

        /*
        if (otherCollider.tag == "Player")
        {
        }
        */
    }
    // (다른 콜라이더에 의해) 트리거가 발동 중일 때

    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        Debug.Log("트리거 중!");
        Timer += Time.deltaTime;

        Debug.Log($"타이머: {Timer}");

        if (Timer >= Delaytime)
        {
            Player player = otherCollider.gameObject.GetComponent<Player>();

            if (MyType == ItemType.Health)
            {
                player.AddPlayerHealth(1);
                player.PlayItem1Sound();
                GameObject vfx = Instantiate(ItemVFXPrefab_H);
                vfx.transform.position = this.transform.position;


                Debug.Log($"현재 플레이어 체력: {player.GetplayerHealth()}");


            }

            else if (MyType == ItemType.Speed)
            {
                PlayerMove playerMove = otherCollider.GetComponent<PlayerMove>();
                playerMove.SetSpeed(playerMove.GetSpeed() + 1);
                playerMove.AddSpeed(1);
                player.PlayItem2Sound();
                GameObject vfx = Instantiate(ItemVFXPrefab_S);
                vfx.transform.position = this.transform.position;
            }

            Timer = 0f;

            Destroy(this.gameObject);

        }

    }
    // (다른 콜라이더에 의해) 트리거가 끝났을 때
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        Debug.Log("트리거 종료!");
        Timer = 0f;


    }
    void ItemFollow()
    {
        // 1. 플레이어 게임오브젝트를 찾고
        GameObject target = GameObject.Find("Player");

        // 2. 방향을 정하고
        Vector3 _dir = target.transform.position - this.transform.position;
        _dir.Normalize();

        // 3. 스피드에 맞게 이동
        this.transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;
    }

    private void Update()
    {

        Timer2 += Time.deltaTime;

        if (Timer2 >= Delaytime2)
        {
            ItemFollow();
        }

    }
}
