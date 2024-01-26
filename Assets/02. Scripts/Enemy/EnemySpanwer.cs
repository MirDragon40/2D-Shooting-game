using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    // ����: �����ð����� ���� ���������κ��� �����ؼ� �� ��ġ�� ���ٳ��� �ʹ�.
    // �ʿ�Ӽ�
    // - �� ������
    // - �����ð�
    // - ����ð� (Ȥ�� Ÿ�̸�)

    [Header("�� ������")]
    public GameObject EnemyPrefab;        // - Basic
    public GameObject EnemyTargetPrefab;  // - Target
    public GameObject FollowerPrefab;     // - Follow




    public float SpawnTime = 1.2f;
    public float CurrentTimer = 0f;

    float RandomRate;

    private void SetRandomTime()
    {
        SpawnTime = Random.Range(MinTime, MaxTime);

    }

    // ��ǥ: �� ���� �ð��� �����ϰ� �ϰ� �ʹ�.
    // �ʿ� �Ӽ�:
    // - �ּ� �ð�
    // - �ִ� �ð�
    public float MinTime = 0.5f;
    public float MaxTime = 1.5f;

    private void Start()
    {

        // ������ �� �� ���� �ð��� �����ϰ� �����Ѵ�.
        SetRandomTime();

    }

    // ���� ����:
    // 1. �ð��� �帣�ٰ� 
    // 2. ���࿡ �ð��� �����ð��� �Ǹ�

    void Update()
    {
        // ���� ����:
        // 1. �ð��� �帣�ٰ�
        CurrentTimer += Time.deltaTime;

        // 2. ���࿡ ���� �ð��� �Ǹ�
        if (CurrentTimer >= SpawnTime)
        {


            // Ÿ�̸� �ʱ�ȭ
            CurrentTimer = 0f;

            SetRandomTime();

            RandomRate = Random.Range(0f, 10f);

            // GameObject enemy = null;

            if (RandomRate > 4f)
            {
                // 3. ���������κ��� �Ϲ� ���� �����Ѵ�.
                GameObject EnemyBasic = Instantiate(EnemyPrefab);

                // 4. ������ ���� ��ġ�� �� ��ġ�� �ٲ۴�.
                EnemyBasic.transform.position = transform.position;
            }
            else if (RandomRate <= 4f && RandomRate > 3f)
            {
                GameObject EnemyFollower = Instantiate(FollowerPrefab);
                EnemyFollower.transform.position = transform.position;

            }
            else 
            {
                // 3. ���������κ��� Ÿ�� ���� �����Ѵ�.
                GameObject EnemyTarget = Instantiate(EnemyTargetPrefab);
                
                // 4. ������ ���� ��ġ�� �� ��ġ�� �ٲ۴�.
                EnemyTarget.transform.position = transform.position;
            }

        }
    }
}



// �ƹ����� �ۼ����� 
// �������� ���� �������µ�...
