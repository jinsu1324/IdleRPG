using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStatContainer.OnStatChanged += ChangeComponentsValue; // 스탯이 변경될 때, 컴포넌트들 값 변경
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(OnStatChangedArgs statArgs)
    {
        int attackPower = statArgs.AttackPower;
        int attackSpeed = statArgs.AttackSpeed;
        int maxHp = statArgs.MaxHp;

        Init_HPComponent(maxHp);
        Init_HPBar(maxHp);
        Init_AttackComponent(attackPower, attackSpeed);
        Init_AnimComponent();
    }

    /// <summary>
    /// HP 컴포넌트 초기화
    /// </summary>
    private void Init_HPComponent(int maxHp)
    {
        _hpComponent = GetComponent<HPComponent>();
        _hpComponent.Init(maxHp); // HP 컴포넌트 초기화
        _hpComponent.OnDead += PlayerDeadTask;  // 죽었을 때, 플레이어에서 처리해야할 것들 처리
    }

    /// <summary>
    /// HP바 컴포넌트 초기화
    /// </summary>
    private void Init_HPBar(int maxHp)
    {
        _hpBar = GetComponentInChildren<HPBar>();
        _hpBar.Init(maxHp);
    }

    /// <summary>
    /// Attack 컴포넌트 초기화
    /// </summary>
    private void Init_AttackComponent(int attackPower, int attackSpeed)
    {
        _attackComponent = GetComponent<AttackComponent>();

        ProjectileAttack projectileAttack = new ProjectileAttack(_projectilePrefab, transform);
        AttackComponentArgs args = new AttackComponentArgs()
        {
            Attack = projectileAttack,
            AttackPower = attackPower,
            AttackSpeed = attackSpeed
        };
        _attackComponent.Init(args); // Attack 컴포넌트 초기화
    }

    /// <summary>
    /// Anim 컴포넌트 초기화
    /// </summary>
    private void Init_AnimComponent()
    {
        _animComponent = GetComponent<AnimComponent>();
        _animComponent.Init();

    }

    /// <summary>
    /// 컴포넌트들 수치 변겅
    /// </summary>
    private void ChangeComponentsValue(OnStatChangedArgs? args)
    {
        _hpComponent.ChangeMaxHp(args?.MaxHp ?? 0); // null 이면 0(기본값 설정가능) 이 할당
        _attackComponent.ChangeAttackPower(args?.AttackPower ?? 0);
        _attackComponent.ChangeAttackSpeed(args?.AttackSpeed ?? 0);
    }

    /// <summary>
    /// 플레이어 죽었을 때 처리할 것들
    /// </summary>
    private void PlayerDeadTask()
    {
        Debug.Log("Player Dead Task");
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStatContainer.OnStatChanged -= ChangeComponentsValue;

        if (_hpComponent != null)
            _hpComponent.OnDead -= PlayerDeadTask;
    }
}
