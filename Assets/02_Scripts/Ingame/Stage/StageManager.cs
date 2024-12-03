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

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }   // 싱글톤 인스턴스

    public event Action<OnStageChangedArgs> OnStageChanged;     // 스테이지 변경 시 이벤트

    // Todo 임시데이터
    private int _currentChapter = 1;                            // 현재 챕터
    private int _currentStage = 1;                              // 현재 스테이지
    private int _targetCount;                                   // 죽여야 하는 목표 적 숫자
    private int _killCount;                                     // 죽인 적 숫자

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
        StageBuildAndStart();   // 스테이지 시작
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
        int count = stageData.Count;
        int statPercentage = stageData.StatPercentage;

        // 잡아야하는 적 숫자 정보들 리셋
        ResetTargetCount(count);

        // 스테이지 변경 이벤트 실행 (적 스폰, UI 업데이트)
        OnStageChangedArgs args = new OnStageChangedArgs() 
        { 
            CurrentChapter = _currentChapter, 
            CurrentStage = _currentStage, 
            EnemyID = appearEnemyID, 
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
