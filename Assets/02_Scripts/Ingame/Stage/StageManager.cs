using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OnStageChangedArgs
{
    public int CurrentChapter;  // 현재 챕터
    public int CurrentStage;    // 현재 스테이지
    public EnemyID EnemyID;     // 등장하는 적 ID
    public int Count;           // 등장하는 적 수
    public int StatPercantage;  // 등장적 스탯 퍼센티지
}

public class StageManager : SingletonBase<StageManager>
{
    public static event Action<OnStageChangedArgs> OnStageChanged;  // 스테이지 변경 시 이벤트

    // Todo 임시데이터
    private int _currentChapter = 1;                                // 현재 챕터
    private int _currentStage = 1;                                  // 현재 스테이지

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageKillCounter.OnClearAllEnemies += StageLevelUp; // 전체 적 섬멸했을 때 스테이지 레벨업
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        StageBuildAndStart(); // 스테이지 시작
    }

    /// <summary>
    /// 스테이지 만들고 시작하기
    /// </summary>
    private void StageBuildAndStart()
    {
        // 현재 챕터와 스테이지에 맞는 스테이지 데이터 가져오기
        StageData stageData = DataManager.Instance.StageDatasSO.GetStageData(_currentChapter, _currentStage);

        // 데이터에서 필요한 정보들 할당
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

        OnStageChanged?.Invoke(args);  // 스테이지 변경 이벤트 실행 (적 스폰, UI 업데이트)

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

        StageBuildAndStart(); // 레벨업된 스테이지대로 스테이지 만들고 시작하기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageKillCounter.OnClearAllEnemies -= StageLevelUp; 
    }
}
