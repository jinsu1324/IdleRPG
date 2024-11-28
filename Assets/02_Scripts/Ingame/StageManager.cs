using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private EnemySpawner _enemySpawner;         // �� ������
    private StageDatasSO _stageDatasSO;            // �������� ������
    private StageDataManager _stageDataManager; // �������� ������ �Ŵ���




    private int _chapter;
    private int _stage;
    private string _appearEnemy;
    private int _count;
    private int _statPercentage;





    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Initialize(EnemySpawner enemySpawner, StageDatasSO stageDatasSO, StageDataManager stageDataManager)
    {
        _enemySpawner = enemySpawner;
        _stageDatasSO = stageDatasSO;
        _stageDataManager = stageDataManager;
    }


    private void Start()
    {
        _chapter = 1;
        _stage = 1;

        StageData currentStageData = _stageDataManager.GetStageData(_chapter, _stage);

        _appearEnemy = currentStageData.AppearEnemy;
        _count = currentStageData.Count;
        _statPercentage = currentStageData.StatPercentage;

        EnemyID appearEnemyID = (EnemyID)Enum.Parse(typeof(EnemyID), _appearEnemy);

        _enemySpawner.SpawnEnemies(appearEnemyID, _count, _statPercentage);
    }
}
