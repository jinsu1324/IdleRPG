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

public enum StageType
{
    Normal,  // 일반 스테이지
    Infinite // 무한 스테이지
}

public class StageManager : SingletonBase<StageManager>
{
    public static event Action<OnStageChangedArgs> OnStageChanged;  // 스테이지 변경 시 이벤트

    private StageType _currentStageType;                            // 현재 스테이지 타입
    private int _currentChapter = 1;                                // 현재 챕터 // Todo 임시데이터
    private int _currentStage = 1;                                  // 현재 스테이지
    private int _targetCount;                                       // 죽여야 하는 목표 적 숫자
    private int _killCount;                                         // 죽인 적 숫자

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        Enemy.OnEnemyDie += AddKillCount;   // 적 죽었을 때, 킬카운트 증가
        Player.OnPlayerDie += PauseAndRestartGame;    // 플레이어 죽었을 때, 게임 재시작
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

        ResetTargetCount(args); // 목표 + 죽인 적 숫자 리셋
        OnStageChanged?.Invoke(args);  // 스테이지 변경 이벤트 실행 (적 스폰, UI 업데이트)

        Debug.Log($"{_currentChapter}-{_currentStage} 시작!");
    }
    
    /// <summary>
    /// 킬 카운트 증가
    /// </summary>
    public void AddKillCount(EnemyEventArgs args)
    {
        if (_currentStageType == StageType.Normal)  // 일반모드면, 타겟 다잡으면 스테이지 레벨업
        {
            _killCount++;
            
            if (_killCount >= _targetCount)
            {
                StageLevelUp();
                
                PlayerSpawner.RestorePlayerStats(); // 플레이어 스탯 리셋

                StageBuildAndStart();
            }
        }
        else if (_currentStageType == StageType.Infinite) // 무한모드면, 타겟 다잡아도 계속 도르마무
        {
            _killCount++;
            
            if (_killCount >= _targetCount)
            {
                PlayerSpawner.RestorePlayerStats();  // 플레이어 스탯 리셋

                StageBuildAndStart();
            }
        }
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
    /// 스테이지 레벨 다운
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
    /// 목표 + 죽인 적 숫자 리셋
    /// </summary>
    private void ResetTargetCount(OnStageChangedArgs args)
    {
        _targetCount = args.Count;
        _killCount = 0;
    }

    /// <summary>
    /// 스테이지 타입 일반모드로 변경
    /// </summary>
    public void SetStageType_Normal()
    {
        _currentStageType = StageType.Normal;
    }

    /// <summary>
    /// 스테이지 타입 무한모드로 변경
    /// </summary>
    public void SetStageType_Infinite()
    {
        _currentStageType = StageType.Infinite;
    }





    /// <summary>
    /// 일시정지 후 게임 재시작
    /// </summary>
    private void PauseAndRestartGame()
    {
        // 일시정지
        GameManager.Instance.Pause();
        
        // UI 팝업 표시 (예: "게임 종료" UI 활성화)

        // 대기 후 게임 재시작
        StartCoroutine(WaitAndRestartGame());
    }

    /// <summary>
    /// 일정시간 대기 후 게임 재시작 코루틴
    /// </summary>
    private IEnumerator WaitAndRestartGame()
    {
        // 2초 대기
        yield return new WaitForSecondsRealtime(2f); // Time.timeScale = 0에서도 동작

        // 게임 다시 시작
        GameManager.Instance.Resume();
        RestartGame();
    }

    /// <summary>
    /// 게임 재시작 프로세스들
    /// </summary>
    private void RestartGame()
    {
        // 필드 정리: 모든 몬스터 제거
        FieldTargetManager.Instance.ClearAllFieldTarget();

        // 플레이어 스탯 초기화
        PlayerSpawner.RestorePlayerStats();

        // 이전 스테이지로 복귀
        StageLevelDown();

        // 무한 스테이지 모드로 설정
        SetStageType_Infinite();

        // 스테이지 재시작
        StageBuildAndStart();
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        Enemy.OnEnemyDie -= AddKillCount;
        Player.OnPlayerDie -= PauseAndRestartGame;
    }
}
