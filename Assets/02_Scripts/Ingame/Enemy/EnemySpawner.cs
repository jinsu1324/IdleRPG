using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : SerializedMonoBehaviour
{
    [Title("���� ������", Bold = false)]
    [SerializeField] private List<Transform> _spawnPosList;                  // ���ʹ� ���� ������

    [Title("���ʹ� Ǯ", Bold = false)]
    [SerializeField] private Dictionary<EnemyID, ObjectPool> _enemyPoolDict; // ���ʹ� Ǯ

    /// <summary>
    /// OnEnable ����
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageChanged += SpawnEnemies;    // �������� ����� �� -> ���� ����
    }

    /// <summary>
    /// OnDisable ��������
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageChanged -= SpawnEnemies;
    }

    /// <summary>
    /// ���ʹ� Ÿ�Կ� �´� ���ʹ� ����
    /// </summary>
    public void SpawnEnemies(OnStageChangedArgs args)
    {
        EnemyID enemyID = args.EnemyID;
        int count = args.Count;
        float statPercentage = args.StatPercantage;

        ObjectPool pool = _enemyPoolDict[enemyID];
        EnemyData enemyData = EnemyDataManager.GetEnemyData(enemyID.ToString());

        for (int i = 0; i < count; i++)
        {
            EnemyCore enemy = pool.GetObj().GetComponent<EnemyCore>();
            enemy.transform.position = _spawnPosList[i].position;
            enemy.Init(enemyData, statPercentage);
        }
    }
}
