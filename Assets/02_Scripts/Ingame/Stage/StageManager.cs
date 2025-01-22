using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 변경시 이벤트에 필요한 것들 구조체
/// </summary>
public struct OnStageChangedArgs
{
    public int CurrentStage;        // 현재 스테이지
    public EnemyID EnemyID;         // 등장하는 적 ID
    public int Count;               // 등장하는 적 수
    public float StatPercantage;    // 등장적 스탯 퍼센티지
}

/// <summary>
/// 스테이지 로직 관리
/// </summary>
public class StageManager : SingletonBase<StageManager>
{
    public static event Action<OnStageChangedArgs> OnStageChanged;  // 스테이지 변경 시 이벤트
    
    private int _targetCount;                                       // 죽여야 하는 목표 적 숫자
    private int _killCount;                                         // 죽인 적 숫자

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        StageBuildAndStart();
    }

    /// <summary>
    /// 스테이지 만들고 시작하기
    /// </summary>
    private void StageBuildAndStart()
    {
        // 현재 스테이지에 맞는 스테이지 데이터 가져오기
        StageData stageData = StageDataManager.GetStageData(CurrentStageData.Stage);

        // 데이터에서 필요한 정보들 할당
        EnemyID appearEnemyID = (EnemyID)Enum.Parse(typeof(EnemyID), stageData.AppearEnemy);
        int targetCount = stageData.Count;
        float statPercentage = stageData.StatPercentage;

        OnStageChangedArgs args = new OnStageChangedArgs() 
        { 
            CurrentStage = CurrentStageData.Stage, 
            EnemyID = appearEnemyID, 
            Count = targetCount, 
            StatPercantage = statPercentage
        };

        ResetTargetCount(args); // 목표 + 죽인 적 숫자 리셋
        OnStageChanged?.Invoke(args);

        PlayerResetService.PlayerReset(); // 플레이어 리셋
    }
    
    /// <summary>
    /// 킬 카운트 증가
    /// </summary>
    public void AddKillCount()
    {
        if (CurrentStageData.StageType == StageType.Normal)  // 일반모드면, 
        {
            _killCount++;
            
            if (_killCount >= _targetCount) // 타겟 다잡으면 스테이지 레벨업
            {
                CurrentStageData.StageLevelUp();
                StageBuildAndStart();
            }
        }
        else if (CurrentStageData.StageType == StageType.Infinite) // 무한모드면, 
        {
            _killCount++;
            
            if (_killCount >= _targetCount) // 타겟 다잡아도 계속 도르마무
            {
                StageBuildAndStart();
            }
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
    /// 현재 무한 스테이지인지 반환
    /// </summary>
    public bool IsInfiniteStage()
    {
        if (CurrentStageData.StageType == StageType.Infinite)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 패배 시 게임재시작
    /// </summary>
    public void DefeatRestartGame()
    {
        CurrentStageData.SetStageType_Infinite();    // 무한모드로 변경
        StartCoroutine(RestartGameCoroutine()); // 대기 후 게임 재시작
    }

    /// <summary>
    /// 도전버튼 눌렀을 시 게임재시작
    /// </summary>
    public void ChallangeRestartGame()
    {
        CurrentStageData.SetStageType_Normal();  // 일반모드로 변경
        StartCoroutine(RestartGameCoroutine()); // 대기 후 게임 재시작
    }

    /// <summary>
    /// 일정시간 대기 후 게임 재시작 코루틴
    /// </summary>
    private IEnumerator RestartGameCoroutine()
    {
        // 일시정지
        GameTimeController.Pause(); 

        // UI 팝업 표시 (예: "게임 종료" UI 활성화)
        
        yield return new WaitForSecondsRealtime(2f); // Time.timeScale = 0에서도 동작

        // 게임 다시 시작
        GameTimeController.Resume();
        RestartGame();
    }

    /// <summary>
    /// 실제 게임 재시작 처리들
    /// </summary>
    private void RestartGame()
    {
        // 필드 정리: 모든 몬스터 제거
        FieldTargetManager.ClearAllFieldTarget();

        if (CurrentStageData.StageType == StageType.Infinite) // 무한모드면 이전스테이지로
            CurrentStageData.StageLevelDown();
        else if (CurrentStageData.StageType == StageType.Normal) // 일반모드면 다음스테이지로
            CurrentStageData.StageLevelUp();

        // 스테이지 재시작
        StageBuildAndStart();
    }
}
