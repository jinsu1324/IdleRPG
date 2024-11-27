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

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Initialize(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;

        _enemyPoolDict = new Dictionary<EnemyID, ObjectPool<Enemy>>();
        foreach (var kvp in _enemyPrefabDict)
            _enemyPoolDict[kvp.Key] = new ObjectPool<Enemy>(kvp.Value, 10, this.transform);
    }
   

    /// <summary>
    /// ���ʹ� Ÿ�Կ� �´� ���ʹ� ����
    /// </summary>
    public void SpawnEnemies(EnemyID enemyType, int count)
    {
        ObjectPool<Enemy> pool = _enemyPoolDict[enemyType];

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = pool.GetObject();
            enemy.transform.position = _spawnPosList[i].position;

            // EnemyManager�� ����
            _enemyManager.SubscribeToEnemy(enemy);

            // Enemy �ʱ�ȭ
            enemy.Initialize(pool);
        }
    }
}
