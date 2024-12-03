using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OnStageChangedArgs
{
    public int CurrentChapter;
    public int CurrentStage;
    public EnemyID EnemyID;
    public int Count;
    public int StatPercantage;
}

public class StageManager : MonoBehaviour
{
    #region Singleton
    public static StageManager Instance { get; private set; }

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
    #endregion

    public event Action OnStageInitCompleted;                   // �������� �ʱ�ȭ �Ϸ� �� �̺�Ʈ
    public event Action<OnStageChangedArgs> OnStageChanged;     // �������� ���� �� �̺�Ʈ

    // Todo �ӽõ�����
    private int _currentChapter = 1;                            // ���� é��
    private int _currentStage = 1;                              // ���� ��������
    private int _targetCount;                                   // �׿��� �ϴ� ��ǥ �� ����
    private int _killCount;                                     // ���� �� ����

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        StageBuildAndStart();

        OnStageInitCompleted?.Invoke();
    }
    
    /// <summary>
    /// �������� ����� �����ϱ�
    /// </summary>
    private void StageBuildAndStart()
    {
        // ���� é�Ϳ� ���������� �´� �������� ������ ��������
        StageData stageData = DataManager.Instance.StageDatasSO.GetStageData(_currentChapter, _currentStage);

        // �����Ϳ��� �ʿ��� ������ �Ҵ�
        string appearEnemy = stageData.AppearEnemy;
        EnemyID enemyID = (EnemyID)Enum.Parse(typeof(EnemyID), appearEnemy);
        int count = stageData.Count;
        int statPercentage = stageData.StatPercentage;

        // ��ƾ��ϴ� �� ���� ������ ����
        ResetTargetCount(count);

        // �������� ���� �̺�Ʈ ���� (�� ����, UI ������Ʈ)
        OnStageChangedArgs args = new OnStageChangedArgs() 
        { 
            CurrentChapter = _currentChapter, 
            CurrentStage = _currentStage, 
            EnemyID = enemyID, 
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
