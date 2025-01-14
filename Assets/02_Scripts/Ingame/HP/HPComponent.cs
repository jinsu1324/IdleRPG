using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 데미지 받았을때 필요한 것들 구조체
/// </summary>
public struct OnTakeDamagedArgs
{
    public int CurrentHp;
    public int MaxHp;
}

/// <summary>
/// HP컴포넌트 베이스
/// </summary>
public abstract class HPComponent : MonoBehaviour, IDamagable
{
    public Action<OnTakeDamagedArgs> OnTakeDamaged; // 데미지 받았을때 이벤트
    public int CurrentHp { get; set; }              // 현재 체력 
    public int MaxHp { get; set; }                  // 최대 체력
    public bool IsDead { get; private set; }        // 죽었는지

    /// <summary>
    /// 초기화
    /// </summary>
    public virtual void Init(int hp)
    {
        IsDead = false;

        MaxHp = hp;
        CurrentHp = MaxHp;
    }

    /// <summary>
    /// 데미지 받음
    /// </summary>
    public virtual void TakeDamage(int atk, bool isCritical)
    {
        // 죽었으면 그냥 무시
        if (IsDead) 
            return;

        // 체력 닳기
        CurrentHp -= atk;   
        
        // 이벤트 알림
        OnTakeDamagedArgs args = new OnTakeDamagedArgs() { CurrentHp = this.CurrentHp, MaxHp = this.MaxHp };
        OnTakeDamaged?.Invoke(args);
    }

    /// <summary>
    /// 죽음
    /// </summary>
    public virtual void Die()
    {
        if (IsDead) 
            return;

        IsDead = true;
    }
}
