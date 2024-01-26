using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ����: ������ �����ϴ� ���� ������
public class ScoreManager : MonoBehaviour
{
    // ��ǥ: ���� ���������� ������ �ø���, ���� ������ UI�� ǥ���ϰ� �ʹ�.
    // �ʿ� �Ӽ�
    // - ���� ������ ǥ���� UI
    public Text ScoreTextUI;
    // - ���� ������ ����� ����
    private int _score = 0;

    public Text BestScoreUI;
    public int BestScore = 0;

    // ��ǥ: ������ ������ �� �ְ� ������ �ҷ�����, UI�� ǥ���ϰ� �ʹ�.
    // ���� ����:
    // 1. ������ ������ ��
    private void Start()
    {
        // 2. �ְ� ������ �ҷ��´�.
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        // 3. UI�� ǥ���Ѵ�. 
        BestScoreUI.text = $"�ְ�����: {BestScore}";

    }


    // ��ǥ: ���� ���������� ������ �ø���, ���� ������ UI�� ǥ���ϰ� �ʹ�.
    // ���� ����
    // 1. ���࿡ ���� ������?
    // 2. ���ھ ���� ��Ų��.
    // 3. ���ھ ȭ�鿡 ǥ���Ѵ�.



    // ��ǥ: score �Ӽ��� ���� ĸ��ȭ (get/set)
    public int GetScore()
    {
        return _score;
    }

    public void SetScore(int score)
    {
        // ��ȿ�� �˻�
        if (score < 0)
        {
            return;
        }
        _score = score;



        // ��ǥ: ���ھ ȭ�鿡 ǥ���Ѵ�.
        ScoreTextUI.text = $"����: {_score}";

        // ��ǥ: �ְ� ������ �����ϰ� UI�� ǥ���ϰ� �ʹ�.
        // 1. ���࿡ ���� ������ �ְ� �������� ũ�ٸ�

        if (_score > BestScore)
        {
            // 2. �ְ� ������ �����ϰ�,
            BestScore = _score;

            // ��ǥ: �ְ� ������ ����
            // 'PlayerPrefs' Ŭ������ ���
            // -> ���� 'Ű(key)'�� '��(Value)' ���·� �����ϴ� Ŭ�����Դϴ�.
            // ������ �� �ִ� ������Ÿ��: int, float, string
            // Ÿ�Ժ��� ����/�ε尡 ������ Set/Get �޼��尡 �ִ�.
            PlayerPrefs.SetInt("BestScore", BestScore);


            // 3. UI�� ǥ���Ѵ�.
            BestScoreUI.text = $"�ְ� ����: {_score}";
        }
    }

}
