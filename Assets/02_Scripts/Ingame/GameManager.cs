using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;               // ������ �Ŵ���
    [SerializeField] private EnemySpawner _enemySpawner;             // ���ʹ� ������
    [SerializeField] private EnemyManager _enemyManager;             // ���ʹ� �Ŵ���
    [SerializeField] private StageManager _stageManager;             // �������� �Ŵ���

    private StageDataManager _stageDataManager;                      // �������� ������ �Ŵ���

    /// <summary>
    /// Awake �ʱ�ȭ �� ������� �ۼ��� ��
    /// </summary>
    public void Awake()
    {
        // ���� ������ �� �Ŵ��� �ʱ�ȭ
        _stageDataManager = new StageDataManager();
        _stageDataManager.Initialize(_dataManager.StageData);

        // ������ ����
        _enemySpawner.Initialize(_enemyManager);
        _stageManager.Initialize(_enemySpawner, _dataManager.StageData, _stageDataManager);

        // ���� ����
    }
}