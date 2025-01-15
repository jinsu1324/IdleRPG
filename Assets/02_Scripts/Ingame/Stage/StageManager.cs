using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OnStageChangedArgs
{
    public int CurrentChapter;      // ���� é��
    public int CurrentStage;        // ���� ��������
    public EnemyID EnemyID;         // �����ϴ� �� ID
    public int Count;               // �����ϴ� �� ��
    public float StatPercantage;    // ������ ���� �ۼ�Ƽ��
}

public enum StageType
{
    Normal,  // �Ϲ� ��������
    Infinite // ���� ��������
}

public class StageManager : SingletonBase<StageManager>
{
    public static event Action<OnStageChangedArgs> OnStageChanged;  // �������� ���� �� �̺�Ʈ

    private StageType _currentStageType;                            // ���� �������� Ÿ��
    private int _currentChapter = 1;                                // ���� é�� // Todo �ӽõ�����
    private int _currentStage = 1;                                  // ���� ��������
    private int _targetCount;                                       // �׿��� �ϴ� ��ǥ �� ����
    private int _killCount;                                         // ���� �� ����
    private int _stageLevelingCount = 1;                            // ���������� ������ ȯ���� ī��Ʈ

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
        float statPercentage = stageData.StatPercentage;
        
        OnStageChangedArgs args = new OnStageChangedArgs() 
        { 
            CurrentChapter = _currentChapter, 
            CurrentStage = _currentStage, 
            EnemyID = appearEnemyID, 
            Count = targetCount, 
            StatPercantage = statPercentage
        };

        ResetTargetCount(args); // ��ǥ + ���� �� ���� ����
        OnStageChanged?.Invoke(args);

        PlayerResetService.PlayerReset(); // �÷��̾� ����
    }
    
    /// <summary>
    /// ų ī��Ʈ ����
    /// </summary>
    public void AddKillCount()
    {
        if (_currentStageType == StageType.Normal)  // �Ϲݸ���, 
        {
            _killCount++;
            
            if (_killCount >= _targetCount) // Ÿ�� �������� �������� ������
            {
                StageLevelUp();
                StageBuildAndStart();
            }
        }
        else if (_currentStageType == StageType.Infinite) // ���Ѹ���, 
        {
            _killCount++;
            
            if (_killCount >= _targetCount) // Ÿ�� ����Ƶ� ��� ��������
            {
                StageBuildAndStart();
            }
        }
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    private void StageLevelUp()
    {
        _currentStage++;
        _stageLevelingCount++;
        QuestManager.Instance.UpdateQuestProgress(QuestType.ReachStage, 1);

        if (_currentStage > 5)
        {
            _currentStage = 1;
            _currentChapter++;
        }
    }

    /// <summary>
    /// �������� ���� �ٿ�
    /// </summary>
    private void StageLevelDown()
    {
        _currentStage--;

        if (_currentStage <= 0)
        {
            _currentStage = 1;
        }
    }

    /// <summary>
    /// ��ǥ + ���� �� ���� ����
    /// </summary>
    private void ResetTargetCount(OnStageChangedArgs args)
    {
        _targetCount = args.Count;
        _killCount = 0;
    }

    /// <summary>
    /// �������� Ÿ�� �Ϲݸ��� ����
    /// </summary>
    public void SetStageType_Normal()
    {
        _currentStageType = StageType.Normal;
    }

    /// <summary>
    /// �������� Ÿ�� ���Ѹ��� ����
    /// </summary>
    public void SetStageType_Infinite()
    {
        _currentStageType = StageType.Infinite;
    }

    /// <summary>
    /// ���� ���� ������������ ��ȯ
    /// </summary>
    public bool IsInfiniteStage()
    {
        if (_currentStageType == StageType.Infinite)
            return true;
        else
            return false;
    }

    /// <summary>
    /// �й� �� ���������
    /// </summary>
    public void DefeatRestartGame()
    {
        SetStageType_Infinite();    // ���Ѹ��� ����
        StartCoroutine(RestartGameCoroutine()); // ��� �� ���� �����
    }

    /// <summary>
    /// ������ư ������ �� ���������
    /// </summary>
    public void ChallangeRestartGame()
    {
        SetStageType_Normal();  // �Ϲݸ��� ����
        StartCoroutine(RestartGameCoroutine()); // ��� �� ���� �����
    }

    /// <summary>
    /// �����ð� ��� �� ���� ����� �ڷ�ƾ
    /// </summary>
    private IEnumerator RestartGameCoroutine()
    {
        // �Ͻ�����
        GameTimeController.Pause(); 

        // UI �˾� ǥ�� (��: "���� ����" UI Ȱ��ȭ)
        
        yield return new WaitForSecondsRealtime(2f); // Time.timeScale = 0������ ����

        // ���� �ٽ� ����
        GameTimeController.Resume();
        RestartGame();
    }

    /// <summary>
    /// ���� ���� ����� ó����
    /// </summary>
    private void RestartGame()
    {
        // �ʵ� ����: ��� ���� ����
        FieldTargetManager.ClearAllFieldTarget();

        if (_currentStageType == StageType.Infinite) // ���Ѹ��� ��������������
            StageLevelDown();
        else if (_currentStageType == StageType.Normal) // �Ϲݸ��� ��������������
            StageLevelUp();

        // �������� �����
        StageBuildAndStart();
    }
}
