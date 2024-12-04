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

public class StageManager : SingletonBase<StageManager>
{
    public static event Action<OnStageChangedArgs> OnStageChanged;  // �������� ���� �� �̺�Ʈ

    // Todo �ӽõ�����
    private int _currentChapter = 1;                                // ���� é��
    private int _currentStage = 1;                                  // ���� ��������

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageKillCounter.OnClearAllEnemies += StageLevelUp; // ��ü �� �������� �� �������� ������
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        StageBuildAndStart(); // �������� ����
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
        int targetCount = stageData.Count;
        int statPercentage = stageData.StatPercentage;

        
        OnStageChangedArgs args = new OnStageChangedArgs() 
        { 
            CurrentChapter = _currentChapter, 
            CurrentStage = _currentStage, 
            EnemyID = appearEnemyID, 
            Count = targetCount, 
            StatPercantage = statPercentage
        };

        OnStageChanged?.Invoke(args);  // �������� ���� �̺�Ʈ ���� (�� ����, UI ������Ʈ)

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

        StageBuildAndStart(); // �������� ����������� �������� ����� �����ϱ�
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageKillCounter.OnClearAllEnemies -= StageLevelUp; 
    }
}
