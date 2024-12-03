using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OnStageChangedArgs
{
    public int CurrentChapter;  // ���� é��
    public int CurrentStage;    // ���� ��������
    public EnemyID EnemyID;     // �����ϴ� �� ID
    public int Count;           // �����ϴ� �� ��
    public int StatPercantage;  // ������ ���� �ۼ�Ƽ��
}

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }   // �̱��� �ν��Ͻ�

    public event Action<OnStageChangedArgs> OnStageChanged;     // �������� ���� �� �̺�Ʈ

    // Todo �ӽõ�����
    private int _currentChapter = 1;                            // ���� é��
    private int _currentStage = 1;                              // ���� ��������
    private int _targetCount;                                   // �׿��� �ϴ� ��ǥ �� ����
    private int _killCount;                                     // ���� �� ����

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        StageBuildAndStart();   // �������� ����
    }

    /// <summary>
    /// �������� ����� �����ϱ�
    /// </summary>
    private void StageBuildAndStart()
    {
        // ���� é�Ϳ� ���������� �´� �������� ������ ��������
        StageData stageData = DataManager.Instance.StageDatasSO.GetStageData(_currentChapter, _currentStage);

        // �����Ϳ��� �ʿ��� ������ �Ҵ�
        EnemyID appearEnemyID = (EnemyID)Enum.Parse(typeof(EnemyID), stageData.AppearEnemy);
        int count = stageData.Count;
        int statPercentage = stageData.StatPercentage;

        // ��ƾ��ϴ� �� ���� ������ ����
        ResetTargetCount(count);

        // �������� ���� �̺�Ʈ ���� (�� ����, UI ������Ʈ)
        OnStageChangedArgs args = new OnStageChangedArgs() 
        { 
            CurrentChapter = _currentChapter, 
            CurrentStage = _currentStage, 
            EnemyID = appearEnemyID, 
            Count = count, 
            StatPercantage = statPercentage
        };

        OnStageChanged?.Invoke(args); 

        Debug.Log($"{_currentChapter}-{_currentStage} ����!");
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    private void StageLevelUp()
    {
        _currentStage++;

        if (_currentStage > 5)
        {
            _currentStage = 1;
            _currentChapter++;
        }
    }

    /// <summary>
    /// ų ī��Ʈ 1 ����
    /// </summary>
    public void AddKillCount(int count)
    {
        _killCount += count;

        // �� �� �׿����� �̺�Ʈ ȣ��
        if (_killCount >= _targetCount)
        {
            StageLevelUp();
            StageBuildAndStart();
        }
    }

    /// <summary>
    /// ��ǥ + ���� �� ���� ����
    /// </summary>
    private void ResetTargetCount(int targetCount)
    {
        _targetCount = targetCount;
        _killCount = 0;
    }
}
