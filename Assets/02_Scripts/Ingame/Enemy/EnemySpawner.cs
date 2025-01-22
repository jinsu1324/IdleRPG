using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : SerializedMonoBehaviour
{
    [Title("생성 포지션", Bold = false)]
    [SerializeField] private List<Transform> _spawnPosList;                  // 에너미 생성 포지션

    [Title("에너미 풀", Bold = false)]
    [SerializeField] private Dictionary<EnemyID, ObjectPool> _enemyPoolDict; // 에너미 풀

    /// <summary>
    /// OnEnable 구독
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageChanged += SpawnEnemies;    // 스테이지 변경될 때 -> 적들 스폰
    }

    /// <summary>
    /// OnDisable 구독해제
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageChanged -= SpawnEnemies;
    }

    /// <summary>
    /// 에너미 타입에 맞는 에너미 스폰
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
