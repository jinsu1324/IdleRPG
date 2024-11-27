using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SerializedMonoBehaviour
{
    [SerializeField]
    private List<Transform> _spawnPosList;                          // 에너미 생성 포지션

    [SerializeField]
    private Dictionary<EnemyID, Enemy> _enemyPrefabDict;            // 에너미 프리팹 딕셔너리              
    
    private Dictionary<EnemyID, ObjectPool<Enemy>> _enemyPoolDict;  // 에너미 오브젝트 풀 딕셔너리        
    private EnemyManager _enemyManager;                             // 에너미 매니저

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;

        _enemyPoolDict = new Dictionary<EnemyID, ObjectPool<Enemy>>();
        foreach (var kvp in _enemyPrefabDict)
            _enemyPoolDict[kvp.Key] = new ObjectPool<Enemy>(kvp.Value, 10, this.transform);
    }
   

    /// <summary>
    /// 에너미 타입에 맞는 에너미 스폰
    /// </summary>
    public void SpawnEnemies(EnemyID enemyType, int count)
    {
        ObjectPool<Enemy> pool = _enemyPoolDict[enemyType];

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = pool.GetObject();
            enemy.transform.position = _spawnPosList[i].position;

            // EnemyManager에 구독
            _enemyManager.SubscribeToEnemy(enemy);

            // Enemy 초기화
            enemy.Initialize(pool);
        }
    }
}
