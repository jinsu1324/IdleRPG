using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SerializedMonoBehaviour
{
    [SerializeField]
    private List<Transform> _spawnPosList;                          // ���ʹ� ���� ������

    [SerializeField]
    private Dictionary<EnemyID, Enemy> _enemyPrefabDict;            // ���ʹ� ������ ��ųʸ�              
    
    private Dictionary<EnemyID, ObjectPool<Enemy>> _enemyPoolDict;  // ���ʹ� ������Ʈ Ǯ ��ųʸ�        
    private EnemyManager _enemyManager;                             // ���ʹ� �Ŵ���
    private EnemyDatasSO _enemyDatasSO;                                // ���� ������ ��ũ���ͺ� ������Ʈ

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Initialize(EnemyManager enemyManager, EnemyDatasSO enemyDatasSO)
    {
        _enemyManager = enemyManager;
        _enemyDatasSO = enemyDatasSO;

        _enemyPoolDict = new Dictionary<EnemyID, ObjectPool<Enemy>>();
        foreach (var kvp in _enemyPrefabDict)
            _enemyPoolDict[kvp.Key] = new ObjectPool<Enemy>(kvp.Value, 10, this.transform);
    }
   

    /// <summary>
    /// ���ʹ� Ÿ�Կ� �´� ���ʹ� ����
    /// </summary>
    public void SpawnEnemies(EnemyID enemyID, int count, int statPercentage)
    {
        ObjectPool<Enemy> pool = _enemyPoolDict[enemyID];
        EnemyData enemyData = _enemyDatasSO.GetDataByID(enemyID.ToString());

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = pool.GetObject();
            enemy.transform.position = _spawnPosList[i].position;

            // Enemy ����, ���� �̺�Ʈ ����
            _enemyManager.SubscribeToEnemy(enemy);

            // Enemy �ʱ�ȭ
            enemy.Initialize(pool, enemyData, statPercentage);
        }
    }
}
