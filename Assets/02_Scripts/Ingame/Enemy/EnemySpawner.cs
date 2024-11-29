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
        StageManager.OnStageStart += SpawnEnemies;  // 스테이지 시작 시, 적 스폰되도록 구독
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageStart -= SpawnEnemies;  // 구독 해제
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
    public void SpawnEnemies(EnemyID enemyID, int count, int statPercentage)
    {
        ObjectPool<Enemy> pool = _enemyPoolDict[enemyID];
        EnemyData enemyData = DataManager.Instance.EnemyDatasSO.GetDataByID(enemyID.ToString());

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = pool.GetObject();
            enemy.transform.position = _spawnPosList[Random.Range(0, _spawnPosList.Count)].position;

            // Enemy 스폰, 죽음 이벤트 구독
            EnemyManager.Instance.SubscribeToEnemy(enemy);

            // Enemy 초기화
            enemy.Initialize(pool, enemyData, statPercentage);
        }
    }
}
