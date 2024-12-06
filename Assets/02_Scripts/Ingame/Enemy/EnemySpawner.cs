using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : SerializedMonoBehaviour
{
    [SerializeField]
    private List<Transform> _spawnPosList;                          // 에너미 생성 포지션

    [SerializeField]
    private Dictionary<EnemyID, Enemy> _enemyPrefabDict;            // 에너미 프리팹 딕셔너리              

    private Dictionary<EnemyID, ObjectPool<Enemy>> _enemyPoolDict;  // 에너미 오브젝트 풀 딕셔너리

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        SetEnemyPoolDict();
    }

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageChanged += SpawnEnemies;    // 스테이지 변경될 때, 적들 스폰
    }

    /// <summary>
    /// 에너미 풀 딕셔너리 셋팅
    /// </summary>
    private void SetEnemyPoolDict()
    {
        _enemyPoolDict = new Dictionary<EnemyID, ObjectPool<Enemy>>();
        foreach (var kvp in _enemyPrefabDict)
            _enemyPoolDict[kvp.Key] = new ObjectPool<Enemy>(kvp.Value, 10, this.transform);
    }

    /// <summary>
    /// 에너미 타입에 맞는 에너미 스폰
    /// </summary>
    public void SpawnEnemies(OnStageChangedArgs args)
    {
        EnemyID enemyID = args.EnemyID;
        int count = args.Count;
        int statPercentage = args.StatPercantage;

        ObjectPool<Enemy> pool = _enemyPoolDict[enemyID];
        EnemyData enemyData = DataManager.Instance.EnemyDatasSO.GetDataByID(enemyID.ToString());

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = pool.GetObject();
            enemy.transform.position = _spawnPosList[i].position;
            //enemy.transform.position = _spawnPosList[Random.Range(0, _spawnPosList.Count)].position; 랜덤위치 테스트
            enemy.Init(pool, enemyData, statPercentage); // Enemy 초기화
        }
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageChanged -= SpawnEnemies;
    }
}
