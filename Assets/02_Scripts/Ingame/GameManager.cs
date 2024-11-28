using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;               // ������ �Ŵ���
    [SerializeField] private PlayerManager _playerManager;           // �÷��̾� �Ŵ���
    [SerializeField] private EnemySpawner _enemySpawner;             // ���ʹ� ������
    [SerializeField] private EnemyManager _enemyManager;             // ���ʹ� �Ŵ���
    [SerializeField] private StageManager _stageManager;             // �������� �Ŵ���

    [SerializeField] private StatUpgradePanel _statUpgradePanel;    // ���� ���׷��̵� �г�

    private StageDataManager _stageDataManager;                      // �������� ������ �Ŵ���


    /// <summary>
    /// Awake �ʱ�ȭ �� ������� �ۼ��� ��
    /// </summary>
    public void Awake()
    {
        // �÷��̾� �Ŵ��� �ʱ�ȭ
        _playerManager.Initialize(_dataManager);

        // �������� ������ �Ŵ��� �ʱ�ȭ
        _stageDataManager = new StageDataManager();
        _stageDataManager.Initialize(_dataManager.StageDatasSO);

        // ���ʹ� ������ �ʱ�ȭ
        _enemySpawner.Initialize(_enemyManager, _dataManager.EnemyDatasSO);

        // �������� �Ŵ��� �ʱ�ȭ
        _stageManager.Initialize(_enemySpawner, _dataManager.StageDatasSO, _stageDataManager);

        // ���� ���׷��̵� �г� UI �ʱ�ȭ
        _statUpgradePanel.Initialize(_playerManager);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void SaveGame()
    {
        _playerManager.SavePlayerData();
    }

    /// <summary>
    /// ���� ���� �� ���� ȣ��
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}