using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// HP컴포넌트 초기화할때 필요한 것들 구조체
/// </summary>
public struct HPInitArgs
{
    public float MaxHp;     // 최대체력
}

/// <summary>
/// 데미지 받았을때 필요한 것들 구조체
/// </summary>
public struct TakeDamageArgs
{
    public float Damage;        // 받은 데미지
    public bool IsCritical;     // 크리티컬인지?
    public float CurrentHp;     // 현재 HP
    public float MaxHp;         // 맥스 HP
}

/// <summary>
/// HP 컴포넌트
/// </summary>
public class HPComponent : MonoBehaviour, IDamagable
{
    public Action<TakeDamageArgs> OnTakeDamage;     // 데미지 받았을때 이벤트
    public Action OnDie;                            // 죽었을 때 이벤트
    public float CurrentHp { get; set; }            // 현재 체력 
    public float MaxHp { get; set; }                // 최대 체력
    public bool IsDead { get; set; }                // 죽었는지
    
    /// <summary>
    /// 초기화
    /// </summary>
    public virtual void Init(HPInitArgs args)
    {
        IsDead = false;

        MaxHp = args.MaxHp;
        CurrentHp = MaxHp;
    }

    /// <summary>
    /// 데미지 받음
    /// </summary>
    public void TakeDamage(TakeDamageArgs args)
    {
        if (IsDead) // 죽었으면 그냥 무시
            return;

        CurrentHp -= args.Damage;

        args.CurrentHp = CurrentHp; // 현재 체력에 관한 정보들도 구조체에 담기
        args.MaxHp = MaxHp;
        
        OnTakeDamage?.Invoke(args); // 데미지 받았을때 이벤트 알림

        if (CurrentHp <= 0)
            Die();
    }

    /// <summary>
    /// 죽음
    /// </summary>
    public void Die()
    {
        if (IsDead) // 이미 죽었으면 무시
            return;

        IsDead = true;
        OnDie?.Invoke(); // 죽었을때 이벤트 알림
    }

    /// <summary>
    /// 최대체력 변경
    /// </summary>
    public void ChangeMaxHp(PlayerStatArgs args) => MaxHp = args.MaxHp;

    /// <summary>
    /// HP 리셋
    /// </summary>
    public void ResetHp() => CurrentHp = MaxHp;

    /// <summary>
    /// 죽었음을 false로 리셋
    /// </summary>
    public void ResetIsDead() => IsDead = false;
}
