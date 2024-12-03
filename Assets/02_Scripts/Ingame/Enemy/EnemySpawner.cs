using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : SerializedMonoBehaviour
{
    [SerializeField]
    private List<Transform> _spawnPosList;                          // ���ʹ� ���� ������

    [SerializeField]
    private Dictionary<EnemyID, Enemy> _enemyPrefabDict;            // ���ʹ� ������ ��ųʸ�              

    private Dictionary<EnemyID, ObjectPool<Enemy>> _enemyPoolDict;  // ���ʹ� ������Ʈ Ǯ ��ųʸ�
    
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
        StageManager.Instance.OnStageChanged += SpawnEnemies;  // �������� ���� ��, �� �����ǵ��� ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.Instance.OnStageChanged -= SpawnEnemies;  // ���� ����
    }

    /// <summary>
    /// ���ʹ� Ǯ ��ųʸ� ����
    /// </summary>
    private void SetEnemyPoolDict()
    {
        _enemyPoolDict = new Dictionary<EnemyID, ObjectPool<Enemy>>();
        foreach (var kvp in _enemyPrefabDict)
            _enemyPoolDict[kvp.Key] = new ObjectPool<Enemy>(kvp.Value, 10, this.transform);
    }

    /// <summary>
    /// ���ʹ� Ÿ�Կ� �´� ���ʹ� ����
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

            // Enemy ����, ���� �̺�Ʈ ����
            EnemyManager.Instance.Subscribe_EnemyEvents(enemy);

            // Enemy �ʱ�ȭ
            enemy.Init(pool, enemyData, statPercentage);
        }
    }
}
