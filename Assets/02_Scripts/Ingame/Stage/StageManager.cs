using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static event Action<EnemyID, int, int> OnStageStart;             // �������� ���� �� �̺�Ʈ

    private StageDataManager _stageDataManager = new StageDataManager();    // �������� ������ �Ŵ���
    private PlayerManager _playerManager;                                   // �÷��̾� �Ŵ��� ���� ����
    private StageData _currentStageData;                                    // ���� �������� ������ ���� ����

    private int _chapter;               // ���� é��
    private int _stage;                 // ���� ��������
    private string _appearEnemyID;      // ���;� �ϴ� ��ID
    private int _enemyCount;            // ���������� ������ �� ����
    private int _statPercentage;        // ���������� ������ �� ���ȿ� ������ �ۼ�Ƽ��

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // �ʿ��� �Ŵ����� �Ҵ�
        _stageDataManager.Initialize();
        _playerManager = PlayerManager.Instance;

        // �� �� ����� �� �̺�Ʈ��, �������� �������� ó���� �Լ� ���
        EnemyManager.Instance.OnEnemyClear += StageFinish;

        // �������� ����
        StageSetting();
    }
    
    /// <summary>
    /// �������� ����
    /// </summary>
    private void StageSetting()
    {
        // é�Ϳ� �������� ������ ��������
        _chapter = _playerManager.GetCurrentChapter();
        _stage = _playerManager.GetCurrentStage();

        Debug.Log($"�������� ����! chapter : {_chapter} - stage : {_stage}");

        // ���� é�Ϳ� ���������� �´� �������� ������ ��������
        _currentStageData = _stageDataManager.GetStageData(_chapter, _stage);

        // �����Ϳ��� �ʿ��� ������ �Ҵ�
        _enemyCount = _currentStageData.Count;
        _statPercentage = _currentStageData.StatPercentage;
        _appearEnemyID = _currentStageData.AppearEnemy;
        EnemyID appearEnemyID = (EnemyID)Enum.Parse(typeof(EnemyID), _appearEnemyID);

        // ��ƾ��ϴ� �� ���� ������ ����
        EnemyManager.Instance.ResetCounts(_enemyCount);

        // �������� ���� �̺�Ʈ ȣ�� (���� : �� �����ϴ� �Լ� �����)
        OnStageStart?.Invoke(appearEnemyID, _enemyCount, _statPercentage);
    }

    /// <summary>
    /// �������� �������� ȣ��� �Լ�
    /// </summary>
    private void StageFinish()
    {
        // �÷��̾� ������ �������� ������
        PlayerManager.Instance.StageLevelUp_of_PlayerData();

        // �������� ����
        StageSetting();
    }
}
