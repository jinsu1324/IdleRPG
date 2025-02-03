using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ����� �̺�Ʈ�� �ʿ��� �͵� ����ü
/// </summary>
public struct StageBuildArgs
{
    public int CurrentStage;        // ���� ��������
    public EnemyID EnemyID;         // �����ϴ� �� ID
    public int Count;               // �����ϴ� �� ��
    public float StatPercantage;    // ������ ���� �ۼ�Ƽ��
}

/// <summary>
/// �������� ���� ����
/// </summary>
public class StageManager : SingletonBase<StageManager>
{
    public static event Action<StageBuildArgs> OnStageBuildStart;       // �������� ���� ���� �̺�Ʈ
    public static event Action<StageBuildArgs> OnStageBuildFinish;      // �������� ���� �Ϸ� �̺�Ʈ
    public static event Action OnStageDefeat;                           // �������� �й� �̺�Ʈ
    public static event Action OnStageChallange;                        // �������� ���� �̺�Ʈ
    
    private int _targetCount;                                       // �׿��� �ϴ� ��ǥ �� ����
    private int _killCount;                                         // ���� �� ����

    /// <summary>
    /// �������� ����� �����ϱ�
    /// </summary>
    public void StageBuildAndStart()
    {
        StartCoroutine(StageBuildAndStart_Coroutine());
    }

    /// <summary>
    /// �������� ���� �� ���� �ڷ�ƾ
    /// </summary>
    private IEnumerator StageBuildAndStart_Coroutine()
    {
        // ���� ���������� �´� �������� ������ ��������
        StageData stageData = StageDataManager.GetStageData(CurrentStageData.Stage);

        // �����Ϳ��� �ʿ��� ������ �Ҵ�
        EnemyID appearEnemyID = (EnemyID)Enum.Parse(typeof(EnemyID), stageData.AppearEnemy);
        int targetCount = stageData.Count;
        float statPercentage = stageData.StatPercentage;

        StageBuildArgs args = new StageBuildArgs()
        {
            CurrentStage = CurrentStageData.Stage,
            EnemyID = appearEnemyID,
            Count = targetCount,
            StatPercantage = statPercentage
        };

        ResetTargetCount(args); // ��ǥ + ���� �� ���� ����
        PlayerResetService.PlayerReset(); // �÷��̾� ����
        
        OnStageBuildStart?.Invoke(args);

        yield return new WaitForSeconds(1.5f);

        OnStageBuildFinish?.Invoke(args);

        //ToastManager.Instance.StartShow_ToastCommon($"Stage {args.CurrentStage}"); // �������� �佺Ʈ�޽���
    }

    /// <summary>
    /// ų ī��Ʈ ����
    /// </summary>
    public void AddKillCount()
    {
        if (CurrentStageData.StageType == StageType.Normal)  // �Ϲݸ���, 
        {
            _killCount++;
            
            if (_killCount >= _targetCount) // Ÿ�� �������� �������� ������
            {
                CurrentStageData.StageLevelUp();
                StageBuildAndStart();
            }
        }
        else if (CurrentStageData.StageType == StageType.Infinite) // ���Ѹ���, 
        {
            _killCount++;
            
            if (_killCount >= _targetCount) // Ÿ�� ����Ƶ� ��� ��������
            {
                StageBuildAndStart();
            }
        }
    }

    /// <summary>
    /// ��ǥ + ���� �� ���� ����
    /// </summary>
    private void ResetTargetCount(StageBuildArgs args)
    {
        _targetCount = args.Count;
        _killCount = 0;
    }

    /// <summary>
    /// ���� ���� ������������ ��ȯ
    /// </summary>
    public bool IsInfiniteStage()
    {
        if (CurrentStageData.StageType == StageType.Infinite)
            return true;
        else
            return false;
    }

    /// <summary>
    /// �й� �� ���������
    /// </summary>
    public void DefeatRestartGame()
    {
        OnStageDefeat?.Invoke();
        CurrentStageData.SetStageType_Infinite();    // ���Ѹ��� ����
        StartCoroutine(RestartGameCoroutine()); // ��� �� ���� �����
    }

    /// <summary>
    /// ������ư ������ �� ���������
    /// </summary>
    public void ChallangeRestartGame()
    {
        OnStageChallange?.Invoke();
        CurrentStageData.SetStageType_Normal();  // �Ϲݸ��� ����
        StartCoroutine(RestartGameCoroutine()); // ��� �� ���� �����
    }

    /// <summary>
    /// �����ð� ��� �� ���� ����� �ڷ�ƾ
    /// </summary>
    private IEnumerator RestartGameCoroutine()
    {
        // �Ͻ�����
        GameTimeController.Pause(); 

        yield return new WaitForSecondsRealtime(2.0f); // Time.timeScale = 0������ ����

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

        if (CurrentStageData.StageType == StageType.Infinite) // ���Ѹ��� ��������������
            CurrentStageData.StageLevelDown();
        else if (CurrentStageData.StageType == StageType.Normal) // �Ϲݸ��� ��������������
            CurrentStageData.StageLevelUp();

        // �������� �����
        StageBuildAndStart();
    }
}
