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
    public static event Action OnPlayerDie;                         // 플레이어 죽었을때 이벤트

    private HPComponent _hpComponent;                               // HP 컴포넌트
    private HPBar _hpBar;                                           // HP 바
    private AttackComponentProjectile _attackComponentProjectile;   // 어택 컴포넌트 프로젝타일 발사타입
    private AnimComponent _animComponent;                           // 애님 컴포넌트
    private BlinkOnHit _blinkOnHit;                                 // 데미지 받았을 때 스프라이트 깜빡여주는 컴포넌트

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
        int criticalRate = statArgs.Critical;

        Init_HPComponent(maxHp);
        Init_HPBar(maxHp);
        Init_AttackComponentProjectile(attackPower, attackSpeed, criticalRate);
        Init_AnimComponent();
        Init_BlinkOnHit();
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
    private void Init_AttackComponentProjectile(int attackPower, int attackSpeed, int criticalRate)
    {
        _attackComponentProjectile = GetComponent<AttackComponentProjectile>();
        _attackComponentProjectile.Init(attackPower, attackSpeed, criticalRate);
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
    /// 컴포넌트들 수치 변겅
    /// </summary>
    private void ChangeComponentsValue(OnStatChangedArgs args)
    {
        _hpComponent.ChangeMaxHp(args.MaxHp);
        _attackComponentProjectile.ChangeAttackPower(args.AttackPower);
        _attackComponentProjectile.ChangeAttackSpeed(args.AttackSpeed);
    }

    /// <summary>
    /// 플레이어 죽었을 때 처리할 것들
    /// </summary>
    private void PlayerDeadTask()
    {
        Debug.Log("Player Dead Task");
        OnPlayerDie?.Invoke();
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
