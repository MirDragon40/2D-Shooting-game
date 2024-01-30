using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulletType   // 총알 타입에 대한 열거형(상수를 기억하기 좋게 하나의 이름으로 그룹화하는 것)
{
    Main = 1,
    Sub = 2,
    Pet = 3
}
public class Bullet : MonoBehaviour
{
    //public int BType = 0;
    //public int BulletType = 0; // 0이면 주총알, 1이면 보조총알, 2면 펫이쏘는 총알
    public BulletType BType;
    //public BulletType BType2 = BulletType.Sub;



    // [총알 이동 구현]
    // 목표: 총알을 위로 계속 이동시키고 싶다.
    // 속성: 
    // -  속력
    // 구현 순서
    // 1. 이동할 방향을 구한다.
    // 2. 이동한다.


    public float Speed;


    void Start()
    {

    }

    void Update()
    {
        // 1. 이동할 방향을 구한다.
;        Vector2 dir = Vector2.up;   // new Vector2(0,1);

        // 2. 이동한다.
        //gameObject.transform.Translate(dir * Speed * Time.deltaTime); 
        // 새로운 위치 =  현재위치 * 속도 * 시간
        gameObject.transform.position += (Vector3)(dir * Speed) * Time.deltaTime;
    }

}
