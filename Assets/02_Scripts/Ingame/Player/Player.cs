using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 프리팹
/// </summary>
public class Player : SerializedMonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;  // 투사체 프리팹 

    private HPComponent _hpComponent;                       // HP 컴포넌트
    private HPBar _hpBar;                                   // HP 바
    private AttackComponent _attackComponent;               // 공격 컴포넌트
    private AnimComponent _animComponent;                   // 애님 컴포넌트

    /// <summary>
    /// Start
    /// </summary>
    private void OnEnable()
    {
        PlayerManager.Instance.OnStatChanged += ChangeComponentsValue;
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(OnStatChangedArgs statArgs)
    {
        _hpComponent = GetComponent<HPComponent>();
        _hpBar = GetComponentInChildren<HPBar>();
        _attackComponent = GetComponent<AttackComponent>();
        _animComponent = GetComponent<AnimComponent>();

        int attackPower = statArgs.AttackPower;
        int attackSpeed = statArgs.AttackSpeed;
        int maxHp = statArgs.MaxHp;

        _hpComponent.Init(maxHp); // HP 컴포넌트 초기화
        _hpComponent.OnTakeDamaged += TakeDamage;

        
        _hpBar.Init(maxHp); // HP바 초기화

        
        ProjectileAttack projectileAttack = new ProjectileAttack(_projectilePrefab, transform);
        AttackComponentArgs args = new AttackComponentArgs()
        {
            Attack = projectileAttack,
            AttackPower = attackPower,
            AttackSpeed = attackSpeed
        };
        _attackComponent.Init(args); // Attack 컴포넌트 초기화

        
        _animComponent.Init(); // 애님 컴포넌트 초기화
    }


    private void ChangeComponentsValue(OnStatChangedArgs? args)
    {
        _hpComponent.ChangeMaxHp(args?.MaxHp ?? 0); // null 이면 0(기본값 설정가능) 이 할당
        _attackComponent.ChangeAttackPower(args?.AttackPower ?? 0);
        _attackComponent.ChangeAttackSpeed(args?.AttackSpeed ?? 0);
    }



    /// <summary>
    /// 데미지 받음
    /// </summary>
    public void TakeDamage(OnTakeDamagedArgs args)
    {
        if (args.CurrentHp <= 0)
            Die();
    }

    /// <summary>
    /// 죽음
    /// </summary>
    private void Die()
    {
        Debug.Log("플레이어 죽었습니다!");
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnStatChanged -= ChangeComponentsValue;
    }
}
