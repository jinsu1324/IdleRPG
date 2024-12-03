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

    public event Action OnStageInitCompleted;                   // 스테이지 초기화 완료 시 이벤트
    public event Action<OnStageChangedArgs> OnStageChanged;     // 스테이지 변경 시 이벤트

    // Todo 임시데이터
    private int _currentChapter = 1;                            // 현재 챕터
    private int _currentStage = 1;                              // 현재 스테이지
    private int _targetCount;                                   // 죽여야 하는 목표 적 숫자
    private int _killCount;                                     // 죽인 적 숫자

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        StageBuildAndStart();

        OnStageInitCompleted?.Invoke();
    }
    
    /// <summary>
    /// 스테이지 만들고 시작하기
    /// </summary>
    private void StageBuildAndStart()
    {
        // 현재 챕터와 스테이지에 맞는 스테이지 데이터 가져오기
        StageData stageData = DataManager.Instance.StageDatasSO.GetStageData(_currentChapter, _currentStage);

        // 데이터에서 필요한 정보들 할당
        string appearEnemy = stageData.AppearEnemy;
        EnemyID enemyID = (EnemyID)Enum.Parse(typeof(EnemyID), appearEnemy);
        int count = stageData.Count;
        int statPercentage = stageData.StatPercentage;

        // 잡아야하는 적 숫자 정보들 리셋
        ResetTargetCount(count);

        // 스테이지 변경 이벤트 실행 (적 스폰, UI 업데이트)
        OnStageChangedArgs args = new OnStageChangedArgs() 
        { 
            CurrentChapter = _currentChapter, 
            CurrentStage = _currentStage, 
            EnemyID = enemyID, 
            Count = count, 
            StatPercantage = statPercentage 
        };
        OnStageChanged?.Invoke(args); 

        Debug.Log($"{_currentChapter}-{_currentStage} 시작!");
    }

    /// <summary>
    /// 스테이지 레벨업
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
    /// 킬 카운트 1 증가
    /// </summary>
    public void AddKillCount(int count)
    {
        _killCount += count;

        // 적 다 죽였으면 이벤트 호출
        if (_killCount >= _targetCount)
        {
            StageLevelUp();
            StageBuildAndStart();
        }
    }

    /// <summary>
    /// 목표 + 죽인 적 숫자 리셋
    /// </summary>
    private void ResetTargetCount(int targetCount)
    {
        _targetCount = targetCount;
        _killCount = 0;
    }
}
