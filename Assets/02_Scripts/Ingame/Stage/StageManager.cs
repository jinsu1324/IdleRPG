using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static event Action<EnemyID, int, int> OnStageStart;             // �������� ���� �� �̺�Ʈ

    private StageDataManager _stageDataManager = new StageDataManager();    // �������� ������ �Ŵ���

    private int _chapter;
    private int _stage;
    private string _appearEnemy;
    private int _count;
    private int _statPercentage;

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _stageDataManager.Initialize();
        SpawnEnemyTest();
    }

    private void SpawnEnemyTest()
    {
        _chapter = 1;
        _stage = 1;

        StageData currentStageData = _stageDataManager.GetStageData(_chapter, _stage);

        

        _appearEnemy = currentStageData.AppearEnemy;
        _count = currentStageData.Count;
        _statPercentage = currentStageData.StatPercentage;

        EnemyID appearEnemyID = (EnemyID)Enum.Parse(typeof(EnemyID), _appearEnemy);

        OnStageStart?.Invoke(appearEnemyID, _count, _statPercentage);
    }
}
