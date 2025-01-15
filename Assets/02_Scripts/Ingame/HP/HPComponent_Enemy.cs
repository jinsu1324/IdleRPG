using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPComponent_Enemy : HPComponent
{
    public Action OnDeadEnemy;      // 에너미 죽었을때 이벤트

    /// <summary>
    /// 데미지 받았을 때 태스크들 처리
    /// </summary>
    protected override void TaskDamaged(float atk, bool isCritical)
    {
        DamageTextManager.Instance.ShowDamageText(atk, transform.position, isCritical); // 데미지 텍스트 띄우기
        FXManager.Instance.SpawnFX(FXName.FX_Enemy_Damaged, transform); // 이펙트 띄우기

        if (CurrentHp <= 0) // 데미지받음 태스크 다 처리 후 - 체력 다 떨어지면 죽음처리
            Die();
    }

    /// <summary>
    /// 죽었을때 태스크들 처리
    /// </summary>
    protected override void TaskDie()
    {
        FXManager.Instance.SpawnFX(FXName.FX_Enemy_Die, transform);    // 이펙트 띄우기
        OnDeadEnemy?.Invoke(); // 이벤트 알림
    }
}
