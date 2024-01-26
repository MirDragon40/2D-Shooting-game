using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    private void Awake()
    {
        // �ν��Ͻ�ȭ �� ���Ŀ� ȣ��ȴ�.
        // �ַ� ������ ���³� ������ �ʱ�ȭ�� �� ���
        // ������Ʈ���� ���������� ȣ���
        Debug.Log("Awake");
    }

    private void OnEnable()
    {
        // ��� �����Ҷ����� ȣ��ȴ�. (��Ȱ��ȭ �ƴٰ� Ȱ��ȭ �� ��)
        // ����ڰ� ���� �̺�Ʈ�� ������ �� ���� ���δ�.
        Debug.Log("OnEnable");
    }

    private void Start()
    {
        // ������ �� ȣ��ȴ�.
        // �ٸ� ��ũ��Ʈ�� ��� Awake ��� ����� ���Ŀ� ȣ��ȴ�.
        Debug.Log("Start");
    }

    // Input ������Ʈ

    private void Update()
    {
        // �� �����Ӹ��� ȣ��ȴ�.
    }

    // �ڷ�ƾ ������Ʈ

    private void LateUpdate()
    {
        // ��� Ȱ��ȭ�� ��ũ��Ʈ�� Update �Լ��� ȣ��ǰ� ���� �� ���� ȣ��ȴ�.
        // ī�޶� ĳ���͸� ����ٴϴ� ��Ȳ���� �ַ� ����: ĳ���� ���� �����̰�, ī�޶� ������
    }
    private void OnDisable()
    {
        // ��� �Ұ����Ҷ����� ȣ��ȴ�. 
        // �̺�Ʈ ������ ������ �� ���ȴ�.
        Debug.Log("OnDisable");
    }
}
