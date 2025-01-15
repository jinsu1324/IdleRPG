using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPComponent_Player : HPComponent
{
    public Action OnDeadPlayer;     // 플레이어 죽었을때 이벤트

    /// <summary>
    /// 초기화
    /// </summary>
    public override void Init(float hp)
    {
        base.Init(hp);

        PlayerStats.OnPlayerStatChanged += ChangeMaxHp; // 플레이어 스탯 변경되었을 때 -> 최대체력 변경
        PlayerResetService.OnReset += ResetHp;  // 플레이어 리셋할때, HP 리셋
        PlayerResetService.OnReset += ResetIsDead;  // 플레이어 리셋할때, 죽었는지 bool값도 리셋
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= ChangeMaxHp;
        PlayerResetService.OnReset -= ResetHp;
        PlayerResetService.OnReset -= ResetIsDead;
    }

    /// <summary>
    /// 데미지 받았을때 태스크들 처리
    /// </summary>
    protected override void TaskDamaged(float atk, bool isCritical)
    {
        if (CurrentHp <= 0) // 데미지받음 태스크 다 처리 후 - 체력 다 떨어지면 죽음처리
            Die();
    }

    /// <summary>
    /// 죽었을때 태스크들 처리
    /// </summary>
    protected override void TaskDie()
    {
        OnDeadPlayer?.Invoke(); // 이벤트 알림
    }

    /// <summary>
    /// 최대체력 변경
    /// </summary>
    public void ChangeMaxHp(PlayerStatArgs args)
    {
        MaxHp = args.MaxHp;
    }

    /// <summary>
    /// HP 리셋
    /// </summary>
    private void ResetHp()
    {
        CurrentHp = MaxHp;
    }

    /// <summary>
    /// 죽었음을 false로 리셋
    /// </summary>
    private void ResetIsDead()
    {
        IsDead = false;
    }
}
