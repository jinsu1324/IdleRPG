using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;               // 데이터 매니저
    [SerializeField] private PlayerManager _playerManager;           // 플레이어 매니저
    [SerializeField] private EnemySpawner _enemySpawner;             // 에너미 스포너
    [SerializeField] private EnemyManager _enemyManager;             // 에너미 매니저
    [SerializeField] private StageManager _stageManager;             // 스테이지 매니저

    [SerializeField] private StatUpgradePanel _statUpgradePanel;    // 스탯 업그레이드 패널

    private StageDataManager _stageDataManager;                      // 스테이지 데이터 매니저


    /// <summary>
    /// Awake 초기화 할 순서대로 작성할 것
    /// </summary>
    public void Awake()
    {
        // 플레이어 매니저 초기화
        _playerManager.Initialize(_dataManager);

        // 스테이지 데이터 매니저 초기화
        _stageDataManager = new StageDataManager();
        _stageDataManager.Initialize(_dataManager.StageDatasSO);

        // 에너미 스포너 초기화
        _enemySpawner.Initialize(_enemyManager, _dataManager.EnemyDatasSO);

        // 스테이지 매니저 초기화
        _stageManager.Initialize(_enemySpawner, _dataManager.StageDatasSO, _stageDataManager);

        // 스탯 업그레이드 패널 UI 초기화
        _statUpgradePanel.Initialize(_playerManager);
    }

    /// <summary>
    /// 게임 저장
    /// </summary>
    public void SaveGame()
    {
        _playerManager.SavePlayerData();
    }

    /// <summary>
    /// 게임 종료 시 저장 호출
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}