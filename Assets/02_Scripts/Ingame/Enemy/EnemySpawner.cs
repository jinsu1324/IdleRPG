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
        StageManager.Instance.OnStageChanged += SpawnEnemies;  // 스테이지 시작 시, 적 스폰되도록 구독
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.Instance.OnStageChanged -= SpawnEnemies;  // 구독 해제
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
            //enemy.transform.position = _spawnPosList[Random.Range(0, _spawnPosList.Count)].position;

            // Enemy 스폰, 죽음 이벤트 구독
            EnemyManager.Instance.Subscribe_EnemyEvents(enemy);

            // Enemy 초기화
            enemy.Init(pool, enemyData, statPercentage);
        }
    }
}
