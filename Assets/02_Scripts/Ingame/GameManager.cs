using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;               // 데이터 매니저
    [SerializeField] private EnemySpawner _enemySpawner;             // 에너미 스포너
    [SerializeField] private EnemyManager _enemyManager;             // 에너미 매니저
    [SerializeField] private StageManager _stageManager;             // 스테이지 매니저

    private StageDataManager _stageDataManager;                      // 스테이지 데이터 매니저

    /// <summary>
    /// Awake 초기화 할 순서대로 작성할 것
    /// </summary>
    public void Awake()
    {
        // 전역 데이터 및 매니저 초기화
        _stageDataManager = new StageDataManager();
        _stageDataManager.Initialize(_dataManager.StageData);

        // 의존성 주입
        _enemySpawner.Initialize(_enemyManager);
        _stageManager.Initialize(_enemySpawner, _dataManager.StageData, _stageDataManager);

        // 게임 시작
    }
}