using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    public float Timer = 0f;   // �ð��� üũ�� ����
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

    }     // 0: ü���� �÷��ִ� Ÿ��, 1: ���ǵ带 �÷��ִ� Ÿ��

    public ItemType MyType;

    public float Timer2 = 0f;
    public float Delaytime2 = 3f;

    private Vector2 _dir;

    public Animator MyAnimator;

    private void Awake()
    {
        MyAnimator = GetComponent<Animator>();

        // �������� ���ڴ� ���� ����ȯ�� �ȴ�. 
        MyAnimator.SetInteger("ItemType", (int)MyType);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enter");
    }

    // (�ٸ� �ݶ��̴��� ����) Ʈ���Ű� �ߵ��� ��
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log("Ʈ���� ����!");
        

        // ����: �÷��̾��� ü���� �ø��� �ʹ�.
        // ����:
        // 1. �÷��̾� ��ũ��Ʈ �޾ƿ���
        // GameObject playerGameObject = GameObject.Find("Player");
        // Player player = playerGameObject.GetComponent<Player>();

        // �浹�� ���� �ݶ��̴����� Player ������Ʈ ��������: 

        // 2. �÷��̾� ü�� �ø���

        /*
        if (otherCollider.tag == "Player")
        {
        }
        */
    }
    // (�ٸ� �ݶ��̴��� ����) Ʈ���Ű� �ߵ� ���� ��

    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        Debug.Log("Ʈ���� ��!");
        Timer += Time.deltaTime;

        Debug.Log($"Ÿ�̸�: {Timer}"); 

        if (Timer >= Delaytime)
        {
            Player player = otherCollider.gameObject.GetComponent<Player>();

            if (MyType == ItemType.Health)
            {
                player.Health++;
                player.PlayItem1Sound();
                GameObject vfx = Instantiate(ItemVFXPrefab_H);
                vfx.transform.position = this.transform.position;


                Debug.Log($"���� �÷��̾� ü��: {player.Health}");


            }

            else if (MyType == ItemType.Speed)
            {
                PlayerMove playerMove = otherCollider.GetComponent<PlayerMove>();
                playerMove.Speed += 1;
                player.PlayItem2Sound();
                GameObject vfx = Instantiate(ItemVFXPrefab_S);
                vfx.transform.position = this.transform.position;



            }

            Timer = 0f;

            Destroy(this.gameObject);

        }

    }
    // (�ٸ� �ݶ��̴��� ����) Ʈ���Ű� ������ ��
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        Debug.Log("Ʈ���� ����!");
        Timer = 0f;


    }
    void ItemFollow()
    {
        // 1. �÷��̾� ���ӿ�����Ʈ�� ã��
        GameObject target = GameObject.Find("Player");

        // 2. ������ ���ϰ�
        Vector3 _dir = target.transform.position - this.transform.position;
        _dir.Normalize();

        // 3. ���ǵ忡 �°� �̵�
        this.transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;}

    private void Update()
    {

        Timer2 += Time.deltaTime;

        if (Timer2 >= Delaytime2)
        {
            ItemFollow();
        }

    }
}
