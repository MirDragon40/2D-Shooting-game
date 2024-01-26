using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    // [�Ѿ� �߻� ����]
    // ��ǥ: �Ѿ��� ���� �߻��ϰ� �ʹ�.
    // �Ӽ�:
    // - �Ѿ� ������
    // - �ѱ� ��ġ
    // ���� ����
    // 1. �߻� ��ư�� ������
    // 2. ���������κ��� �Ѿ��� �������� �����,
    // 3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.

    [Header("�Ѿ� ������")]
    public GameObject BulletPrefab;  // �Ѿ� ������
    public GameObject SubBullet;      // ���� �Ѿ� ������

    [Header("�ѱ���")]
    public GameObject[] Muzzles = new GameObject[2];  // �ѱ�

    [Header("���� �ѱ���")]
    public GameObject[] SubMuzzles = new GameObject[2];   // ���� �ѱ�

    [Header("Ÿ�̸�")]
    public float shootInterval = 10f;  // �Ѿ��� 0.6�ʸ��� �� ���� �߻�
    private float lastShootTime;

    [Header("�ڵ� ���")]
    public bool AutoMode = false;

    
    // Ǯ��(Ÿ�̸�)
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
            Debug.Log("�ڵ� ���� ���");
            AutoMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("���� ���� ���");
            AutoMode = false;
        }



        /**
        // 1. �߻� ��ư�� ������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 2. ���������κ��� �Ѿ��� �������� �����,
            GameObject bullet1 = GameObject.Instantiate(BulletPrefab);   // ���⿡�� Game Object�� ������ �����ϴ�. 
            GameObject bullet2 = GameObject.Instantiate(BulletPrefab);   // ���⿡�� Game Object�� ������ �����ϴ�. 


            // 3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.
            bullet1.transform.position = Muzzles[0].transform.position;
            bullet2.transform.position = Muzzles[1].transform.position;

        }
        **/

        if (Input.GetKeyDown(KeyCode.F2))
        {
            // 1. �߻� ��ư�� ������ �ִ� ���� 
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // 2. ������������ �Ѿ��� ��� �������� �Ѵ�.

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



        // Ÿ�̸� ���
        Timer -= Time.deltaTime;

        // 1. Ÿ�̸Ӱ� 0���� ���� ���¿��� �߻� ��ư�� ������
        bool ready = AutoMode || Input.GetKeyDown(KeyCode.Space);
        if (Timer <= 0 && ready)
        {
            // Ÿ�̸� �ʱ�ȭ
            Timer = COOL_TIME;

            // 2. ���������κ��� �Ѿ��� �����.
            GameObject bullet1 = Instantiate(BulletPrefab);
            GameObject bullet2 = Instantiate(BulletPrefab);

            // 3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.
            bullet1.transform.position = Muzzles[0].transform.position;
            bullet2.transform.position = Muzzles[1].transform.position;


            // ����� Ŭ��
            FireSource.Play();

            // ��ǥ: �ѱ� ���� ��ŭ �Ѿ��� �����, ���� �Ѿ�����ġ�� �� �ѱ��� ��ġ�� �ٲ۴�.
            for (int i = 0; i < Muzzles.Length; i++)
            {
                // 1.  �Ѿ��� �����
                //GameObject bullet = Instantiate(BulletPrefab);

                // 2. ��ġ�� �����Ѵ�. 
                //bullet.transform.position = Muzzles[i].transform.position;
            }


           /* GameObject subBullet1 = Instantiate(SubBullet);
            GameObject subBullet2 = Instantiate(SubBullet);

            // 3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.
            subBullet1.transform.position = SubMuzzles[0].transform.position;
            subBullet2.transform.position = SubMuzzles[1].transform.position;*/

            // ��ǥ: �ѱ� ���� ��ŭ �Ѿ��� �����, ���� �Ѿ�����ġ�� �� �ѱ��� ��ġ�� �ٲ۴�.
            for (int i = 0; i < SubMuzzles.Length; i++)
            {
                // 1.  �Ѿ��� �����
                GameObject subBullet = Instantiate(SubBullet);

                // 2. ��ġ�� �����Ѵ�. 
                subBullet.transform.position = SubMuzzles[i].transform.position;
            }

            
        }


        BoomTimer += Time.deltaTime;
        // 3�� ��ư�� ������

        if (BoomTimer >= BOOM_COOL_TIME && Input.GetKeyDown(KeyCode.Alpha3))
        {
            //GameObject boomObject = Instantiate(BoomPrefab);
            //boomObject.transform.position = Vector2.zero;
            //Debug.Log("�ñر�");

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
