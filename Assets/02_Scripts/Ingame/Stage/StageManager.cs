using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static event Action<EnemyID, int, int> OnStageStart;             // 스테이지 시작 시 이벤트

    private StageDataManager _stageDataManager = new StageDataManager();    // 스테이지 데이터 매니저
    private PlayerManager _playerManager;                                   // 플레이어 매니저 담을 변수
    private StageData _currentStageData;                                    // 현재 스테이지 데이터 담을 변수

    private int _chapter;               // 현재 챕터
    private int _stage;                 // 현재 스테이지
    private string _appearEnemyID;      // 나와야 하는 적ID
    private int _enemyCount;            // 스테이지에 나오는 적 숫자
    private int _statPercentage;        // 스테이지에 나오는 적 스탯에 곱해줄 퍼센티지

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // 필요한 매니저들 할당
        _stageDataManager.Initialize();
        _playerManager = PlayerManager.Instance;

        // 적 다 잡았을 때 이벤트에, 스테이지 끝났을때 처리할 함수 등록
        EnemyManager.Instance.OnEnemyClear += StageFinish;

        // 스테이지 셋팅
        StageSetting();
    }
    
    /// <summary>
    /// 스테이지 셋팅
    /// </summary>
    private void StageSetting()
    {
        // 챕터와 스테이지 데이터 가져오기
        _chapter = _playerManager.GetCurrentChapter();
        _stage = _playerManager.GetCurrentStage();

        Debug.Log($"스테이지 셋팅! chapter : {_chapter} - stage : {_stage}");

        // 현재 챕터와 스테이지에 맞는 스테이지 데이터 가져오기
        _currentStageData = _stageDataManager.GetStageData(_chapter, _stage);

        // 데이터에서 필요한 정보들 할당
        _enemyCount = _currentStageData.Count;
        _statPercentage = _currentStageData.StatPercentage;
        _appearEnemyID = _currentStageData.AppearEnemy;
        EnemyID appearEnemyID = (EnemyID)Enum.Parse(typeof(EnemyID), _appearEnemyID);

        // 잡아야하는 적 숫자 정보들 리셋
        EnemyManager.Instance.ResetCounts(_enemyCount);

        // 스테이지 시작 이벤트 호출 (현재 : 적 스폰하는 함수 등록중)
        OnStageStart?.Invoke(appearEnemyID, _enemyCount, _statPercentage);
    }

    /// <summary>
    /// 스테이지 끝났을때 호출될 함수
    /// </summary>
    private void StageFinish()
    {
        // 플레이어 데이터 스테이지 레벨업
        PlayerManager.Instance.StageLevelUp_of_PlayerData();

        // 스테이지 셋팅
        StageSetting();
    }
}
