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
    private HPComponent _hpComponent;                               // HP 컴포넌트
    private HPBar _hpBar;                                           // HP 바
    private AttackComponentProjectile _attackComponentProjectile;   // 어택 컴포넌트 프로젝타일 발사타입
    private AnimComponent _animComponent;                           // 애님 컴포넌트
    private BlinkOnHit _blinkOnHit;                                 // 데미지 받았을 때 스프라이트 깜빡여주는 컴포넌트
    private EquipItemComponent _equipItemComponent;                 // 장착 아이템 컴포넌트
    
    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(PlayerStatArgs args)
    {
        Init_HPComponent(args.MaxHp);
        Init_HPBar(args.MaxHp);
        Init_AttackComponentProjectile(args.AttackPower, args.AttackSpeed, args.CriticalRate, args.CriticalMultiple);
        Init_AnimComponent();
        Init_BlinkOnHit();
        Init_EquipItemComponent();
    }

    /// <summary>
    /// HP 컴포넌트 초기화
    /// </summary>
    private void Init_HPComponent(int maxHp)
    {
        _hpComponent = GetComponent<HPComponent>();
        _hpComponent.Init(maxHp); // HP 컴포넌트 초기화

        _hpComponent.OnDead -= PlayerDeadTask;  // 이벤트 중복방지 (플레이어 리셋할때는 OnDisable이 안되기 때문)
        _hpComponent.OnDead += PlayerDeadTask;  // 죽었을 때, 플레이어에서 처리해야할 것들 처리

        PlayerStats.OnPlayerStatChanged -= _hpComponent.ChangeMaxHp; // 이벤트 중복방지 (플레이어 리셋할때는 OnDisable이 안되기 때문)
        PlayerStats.OnPlayerStatChanged += _hpComponent.ChangeMaxHp; // 플레이어 스탯 변경되었을 때, 최대체력 변경
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
    /// 어택 컴포넌트 프로젝타일 발사타입 초기화
    /// </summary>
    private void Init_AttackComponentProjectile(int attackPower, int attackSpeed, int criticalRate, int criticalMultiple)
    {
        _attackComponentProjectile = GetComponent<AttackComponentProjectile>();
        _attackComponentProjectile.Init(attackPower, attackSpeed, criticalRate, criticalMultiple);

        // 플레이어 스탯 변경되었을 때, 공격력 공격속도 변경
        PlayerStats.OnPlayerStatChanged -= _attackComponentProjectile.ChangeAttackPower; 
        PlayerStats.OnPlayerStatChanged += _attackComponentProjectile.ChangeAttackPower;

        PlayerStats.OnPlayerStatChanged -= _attackComponentProjectile.ChangeAttackSpeed;
        PlayerStats.OnPlayerStatChanged += _attackComponentProjectile.ChangeAttackSpeed;
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
    /// BlinkOnHit 컴포넌트 초기화
    /// </summary>
    private void Init_BlinkOnHit()
    {
        _blinkOnHit = GetComponent<BlinkOnHit>();   
        _blinkOnHit.Init();
    }

    /// <summary>
    /// EquipItemComponent 초기화
    /// </summary>
    private void Init_EquipItemComponent()
    {
        _equipItemComponent = GetComponent<EquipItemComponent>();
    }

    /// <summary>
    /// 플레이어 죽었을 때 처리할 것들
    /// </summary>
    private void PlayerDeadTask()
    {
        StageManager.Instance.DefeatRestartGame();
    }

    /// <summary>
    /// OnDisable 이벤트 구독 해제
    /// </summary>
    private void OnDisable()
    {
        if (_hpComponent != null)
        {
            _hpComponent.OnDead -= PlayerDeadTask;
            PlayerStats.OnPlayerStatChanged -= _hpComponent.ChangeMaxHp;
        }

        if (_attackComponentProjectile != null)
        {
            PlayerStats.OnPlayerStatChanged -= _attackComponentProjectile.ChangeAttackPower;
            PlayerStats.OnPlayerStatChanged -= _attackComponentProjectile.ChangeAttackSpeed;
        }
    }
}
