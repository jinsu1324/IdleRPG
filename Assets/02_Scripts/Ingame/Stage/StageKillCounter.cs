using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageKillCounter : MonoBehaviour
{
    private int _targetCount;                           // 죽여야 하는 목표 적 숫자
    private int _killCount;                             // 죽인 적 숫자

    public static event Action OnClearAllEnemies;       // 적 섬멸했을 때 이벤트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageChanged += ResetTargetCount;    // 스테이지 바뀔 때, 타겟카운트 리셋
        Enemy.OnEnemyDie += AddKillCount;   // 적 죽었을 때, 킬카운트 증가
    }

    /// <summary>
    /// 킬 카운트 증가
    /// </summary>
    public void AddKillCount(EnemyEventArgs args)
    {
        _killCount += args.Count;

        // 적 다 죽였으면 이벤트 호출
        if (_killCount >= _targetCount)
        {
            OnClearAllEnemies?.Invoke();
        }
    }

    /// <summary>
    /// 목표 + 죽인 적 숫자 리셋
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
