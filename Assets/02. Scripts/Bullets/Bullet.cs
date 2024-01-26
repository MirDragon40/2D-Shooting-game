using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulletType   // �Ѿ� Ÿ�Կ� ���� ������(����� ����ϱ� ���� �ϳ��� �̸����� �׷�ȭ�ϴ� ��)
{
    Main = 1,
    Sub = 2,
    Pet = 3
}
public class Bullet : MonoBehaviour
{
    //public int BType = 0;
    //public int BulletType = 0; // 0�̸� ���Ѿ�, 1�̸� �����Ѿ�, 2�� ���̽�� �Ѿ�
    public BulletType BType;
    //public BulletType BType2 = BulletType.Sub;



    // [�Ѿ� �̵� ����]
    // ��ǥ: �Ѿ��� ���� ��� �̵���Ű�� �ʹ�.
    // �Ӽ�: 
    // -  �ӷ�
    // ���� ����
    // 1. �̵��� ������ ���Ѵ�.
    // 2. �̵��Ѵ�.


    public float Speed;


    void Start()
    {

    }

    void Update()
    {
        // 1. �̵��� ������ ���Ѵ�.
;        Vector2 dir = Vector2.up;   // new Vector2(0,1);

        // 2. �̵��Ѵ�.
        //gameObject.transform.Translate(dir * Speed * Time.deltaTime); 
        // ���ο� ��ġ =  ������ġ * �ӵ� * �ð�
        transform.position += (Vector3)(dir * Speed) * Time.deltaTime;
    }

}
