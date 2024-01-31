using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    // 역할: 일정시간마다 적을 프리팹으로부터 생성해서 내 위치에 갖다놓고 싶다.
    // 필요속성
    // - 적 프리팹
    // - 일정시간
    // - 현재시간 (혹은 타이머)

    [Header("적 프리팹")]
    public GameObject EnemyPrefab;        // - Basic
    public GameObject EnemyTargetPrefab;  // - Target
    public GameObject FollowerPrefab;     // - Follow

    // 풀사이즈: 15 (15 * 3 = 45)
    public int PoolSize = 15;
    // 풀 (창고)
    public List<Enemy> EnemyPool;



    public float SpawnTime = 1.2f;
    public float CurrentTimer = 0f;

    float RandomRate;

    private void SetRandomTime()
    {
        SpawnTime = Random.Range(MinTime, MaxTime);

    }

    // 목표: 적 생성 시간을 랜덤하게 하고 싶다.
    // 필요 속성:
    // - 최소 시간
    // - 최대 시간
    public float MinTime = 0.5f;
    public float MaxTime = 1.5f;

    private void Awake()
    {
        EnemyPool = new List<Enemy>();
        // (생성 -> 끄고 -> 넣는다) * PoolSize(15).
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject enemyObject = Instantiate(EnemyPrefab); // 베이직 생성
            enemyObject.SetActive(false);
            EnemyPool.Add(enemyObject.GetComponent<Enemy>());
        }
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject enemyObject = Instantiate(EnemyTargetPrefab); // 베이직 생성
            enemyObject.SetActive(false);
            EnemyPool.Add(enemyObject.GetComponent<Enemy>());
        }
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject enemyObject = Instantiate(FollowerPrefab); // 베이직 생성
            enemyObject.SetActive(false);
            EnemyPool.Add(enemyObject.GetComponent<Enemy>());
        }

    }

    private void Start()
    {

        // 시작할 때 적 생성 시간을 랜덤하게 설정한다.
        SetRandomTime();

    }

    // 구현 순서:
    // 1. 시간이 흐르다가 
    // 2. 만약에 시간이 일정시간이 되면

    void Update()
    {
        // 구현 순서:
        // 1. 시간이 흐르다가
        CurrentTimer += Time.deltaTime;

        // 2. 만약에 일정 시간이 되면
        if (CurrentTimer >= SpawnTime)
        {


            // 타이머 초기화
            CurrentTimer = 0f;

            SetRandomTime();

            RandomRate = Random.Range(0f, 10f);

            // GameObject enemy = null;
            Enemy enemy = null;
            if (RandomRate > 4f)
            {
                // 3. 프리팹으로부터 일반 적을 생성한다.
                GameObject EnemyBasic = Instantiate(EnemyPrefab);

                // 4. 생성한 적의 위치를 내 위치로 바꾼다.
                EnemyBasic.transform.position = transform.position;

                foreach (Enemy e in EnemyPool)
                {
                    if (e.gameObject.activeInHierarchy && e.EType == EnemyType.Basic)
                    {
                        enemy = e;
                        break;
                    }
                }

            }

            else if (RandomRate <= 4f && RandomRate > 3f)
            {
                GameObject EnemyFollower = Instantiate(FollowerPrefab);
                EnemyFollower.transform.position = transform.position;
                foreach (Enemy e in EnemyPool)
                {
                    if(e.gameObject.activeInHierarchy && e.EType == EnemyType.Follower)
                    {
                        enemy = e;
                        break;
                    }
                }

            }
            else 
            {
                // 3. 프리팹으로부터 타겟 적을 생성한다.
                GameObject EnemyTarget = Instantiate(EnemyTargetPrefab);
                
                // 4. 생성한 적의 위치를 내 위치로 바꾼다.
                EnemyTarget.transform.position = transform.position;
                EnemyTarget.gameObject.SetActive(true);


                foreach (Enemy e in EnemyPool)
                {
                    if (!e.gameObject.activeInHierarchy && e.EType == EnemyType.Target)
                    {
                        enemy = e;
                        break;
                    }
                }

            }

        }
    }
}



// 아무내용 작성했음 
// 무ㅑㅐㅇ 오ㅐ 오류나는데...
