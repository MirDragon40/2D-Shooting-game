using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    // [총알 발사 제작]
    // 목표: 총알을 만들어서 발사하고 싶다.
    // 속성:
    // - 총알 프리팹
    // - 총구 위치
    // 구현 순서
    // 1. 발사 버튼을 누르면
    // 2. 프리팹으로부터 총알을 동적으로 만들고,
    // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.

    [Header("총알 프리팹")]
    public GameObject BulletPrefab;  // 총알 프리팹
    public GameObject SubBullet;      // 보조 총알 프리팹



    // 목표: 태어날 때 '풀'에다가 메인 총알을 (풀 사이즈)개 생성한다.
    // 속성:
    // - 풀 사이즈
    public int PoolSize = 20;
    // - 오브젝트(총알) 풀
    public List<Bullet> _bulletPool = null;   // 디폴트 값이 null이다.

    // 순서:
    // 1. 태어날 때: Awake
    private void Awake()
    {
        // 2. 오브젝트 풀 할당해주고..
        _bulletPool = new List<Bullet>();

        // 3. 총알 프리팹으로부터 총알을 풀 사이즈만큼 생성해준다.
        // 3-1. 메인 총알
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bullet = Instantiate(BulletPrefab);

            // 4. 생성한 총알을 풀에다가 넣는다.
            _bulletPool.Add(bullet.GetComponent<Bullet>());

            // 5. 생성된 총알이 바로 보이므로 삭제해준다.
            bullet.SetActive(false);  // 끈다.
        }
        // 3-2. 서브 총알
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bullet = Instantiate(SubBullet);

            _bulletPool.Add(bullet.GetComponent<Bullet>());

            bullet.SetActive(false);
        }
    }





    [Header("총구들")]
    //public GameObject[] Muzzles = new GameObject[2];  // 총구
    public List<GameObject> Muzzles;
    

    [Header("보조 총구들")]
    //public GameObject[] SubMuzzles = new GameObject[2];
    public List<GameObject> SubMuzzles;  // 보조 총구

    [Header("타이머")]
    public float shootInterval = 10f;  // 총알을 0.6초마다 한 번씩 발사
    private float lastShootTime;

    [Header("자동 모드")]
    public bool AutoMode = false;

    
    // 풀이(타이머)
    public float Timer = 10f;
    public const float COOL_TIME = 0.3f;

    public float BoomTimer;
    public const float BOOM_COOL_TIME = 5f;

    public float Timer3;
    public const float COOL_TIME2 = 3f;

    public AudioSource FireSource;

    public GameObject BoomPrefab;

    private void Start()
    {
        Timer = 0f;
        AutoMode = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("자동 공격 모드");
            AutoMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("수동 공격 모드");
            AutoMode = false;
        }



        /**
        // 1. 발사 버튼을 누르면
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 2. 프리팹으로부터 총알을 동적으로 만들고,
            GameObject bullet1 = GameObject.Instantiate(BulletPrefab);   // 여기에서 Game Object는 생략이 가능하다. 
            GameObject bullet2 = GameObject.Instantiate(BulletPrefab);   // 여기에서 Game Object는 생략이 가능하다. 


            // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.
            bullet1.transform.position = Muzzles[0].transform.position;
            bullet2.transform.position = Muzzles[1].transform.position;

        }
        **/

        if (Input.GetKeyDown(KeyCode.F2))
        {
            // 1. 발사 버튼을 누르고 있는 동안 
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // 2. 일정간격으로 총알을 계속 나오도록 한다.

                if (Time.time - lastShootTime >= shootInterval)
                {
                    GameObject bullet1 = GameObject.Instantiate(BulletPrefab);
                    GameObject bullet2 = GameObject.Instantiate(BulletPrefab);

                    bullet1.transform.position = Muzzles[0].transform.position;
                    bullet2.transform.position = Muzzles[1].transform.position;
                    lastShootTime = Time.time;

                }

            }
        }


        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject bullet1 = GameObject.Instantiate(BulletPrefab);
            GameObject bullet2 = GameObject.Instantiate(BulletPrefab);

            bullet1.transform.position = Muzzles[0].transform.position;
            bullet2.transform.position = Muzzles[1].transform.position;

        }



        // 타이머 계산
        Timer -= Time.deltaTime;

        // 1. 타이머가 0보다 작은 상태에서 발사 버튼을 누르면
        bool ready = AutoMode || Input.GetKeyDown(KeyCode.Space);
        if (Timer <= 0 && ready)
        {
            // 타이머 초기화
            Timer = COOL_TIME;

            // 2. 프리팹으로부터 총알을 만든다.
            //GameObject bullet1 = Instantiate(BulletPrefab);
            //GameObject bullet2 = Instantiate(BulletPrefab);

            // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.
            //bullet1.transform.position = Muzzles[0].transform.position;
            //bullet2.transform.position = Muzzles[1].transform.position;


            // 오디오 클립
            FireSource.Play();


            // 꺼낸 총알의위치를 각 총구의 위치로 바꾼다.
            for (int i = 0; i < Muzzles.Count; i++)
            {
                // 1.  총알을 만들고
                //GameObject bullet = Instantiate(BulletPrefab);


                // 2. 위치를 설정한다. 
                //bullet.transform.position = Muzzles[i].transform.position;

                // 목표: 총구 개수 만큼 총알을 풀에서 꺼내 쓴다.
                // 순서:
                // 1. 꺼져있는 (비활성화 되어있는) 총알을 찾아 꺼낸다.
                Bullet bullet = null;
                foreach (Bullet b in _bulletPool)  
                {
                    // 만약에 꺼져(비활성화되어) 있고 && 메인 총알이라면
                    if (b.gameObject.activeInHierarchy == false && b.BType == BulletType.Main)
                    {
                        bullet = b;
                        break; // 찾았기 때문에 그 뒤로는 찾을 필요가 없다.
                    }
                }
                // 2. 꺼낸 총알의 위치를 각 총구의 위치로 바꾼다.
                bullet.transform.position = Muzzles[i].transform.position;

                // 3. 총알을 킨다. (발사한다)
                bullet.gameObject.SetActive(true);

            }


            /* GameObject subBullet1 = Instantiate(SubBullet);
             GameObject subBullet2 = Instantiate(SubBullet);

             // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.
             subBullet1.transform.position = SubMuzzles[0].transform.position;
             subBullet2.transform.position = SubMuzzles[1].transform.position;*/

            // 목표: 총구 개수 만큼 총알을 만들고, 만든 총알의위치를 각 총구의 위치로 바꾼다.
            for (int i = 0; i < SubMuzzles.Count; i++)
            {
                // 1.  총알을 만들고
                Bullet bullet = null;
                foreach (Bullet b in _bulletPool)
                {
                    if (b.gameObject.activeInHierarchy == false && b.BType == BulletType.Sub)
                    {
                        bullet = b;
                        break; 
                    }
                }
                

                // 2. 위치를 설정한다. 
                bullet.transform.position = SubMuzzles[i].transform.position;
                bullet.gameObject.SetActive(true);
            }

            
        }


        BoomTimer += Time.deltaTime;
        // 3번 버튼을 누르면

        if (BoomTimer >= BOOM_COOL_TIME && Input.GetKeyDown(KeyCode.Alpha3))
        {
            //GameObject boomObject = Instantiate(BoomPrefab);
            //boomObject.transform.position = Vector2.zero;
            //Debug.Log("궁극기");

            BoomTimer = 0;

            GameObject boomObject = Instantiate(BoomPrefab);
            boomObject.transform.position = Vector2.zero;
            boomObject.transform.position = new Vector2(0, 0);
            boomObject.transform.position = new Vector2(0, 1.6f);


            Timer3 += Time.deltaTime;

            if (Timer3 >= COOL_TIME2)
            {
                Destroy(boomObject);
                Timer3 = 0;
            }

        }

    }
}
