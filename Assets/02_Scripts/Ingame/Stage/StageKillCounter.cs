using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageKillCounter : MonoBehaviour
{
    private int _targetCount;                           // �׿��� �ϴ� ��ǥ �� ����
    private int _killCount;                             // ���� �� ����

    public static event Action OnClearAllEnemies;       // �� �������� �� �̺�Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageChanged += ResetTargetCount;    // �������� �ٲ� ��, Ÿ��ī��Ʈ ����
        Enemy.OnEnemyDie += AddKillCount;   // �� �׾��� ��, ųī��Ʈ ����
    }

    /// <summary>
    /// ų ī��Ʈ ����
    /// </summary>
    public void AddKillCount(EnemyEventArgs args)
    {
        _killCount += args.Count;

        // �� �� �׿����� �̺�Ʈ ȣ��
        if (_killCount >= _targetCount)
        {
            OnClearAllEnemies?.Invoke();
        }
    }

    /// <summary>
    /// ��ǥ + ���� �� ���� ����
    /// </summary>
    private void ResetTargetCount(OnStageChangedArgs args)
    {
        _targetCount = args.Count;
        _killCount = 0;
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageChanged -= ResetTargetCount;
        Enemy.OnEnemyDie -= AddKillCount;
    }
}
