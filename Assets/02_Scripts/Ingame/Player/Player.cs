using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : SerializedMonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;  // 투사체 프리팹 
    
    private HPComponent _hpComponent;                       // HP 컴포넌트
    private HPBar _hpBar;                                   // HP 바
    private AttackComponent _attackComponent;               // 공격 컴포넌트
    private AnimComponent _animComponent;                   // 애님 컴포넌트


    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        _hpComponent = GetComponent<HPComponent>();
        _hpComponent.OnTakeDamaged += TakeDamage;

        _hpBar = GetComponentInChildren<HPBar>();

        _attackComponent = GetComponent<AttackComponent>();

        _animComponent = GetComponent<AnimComponent>();
        _animComponent.Init();

        UpdateStat();
    }

    /// <summary>
    /// 스탯 업데이트
    /// </summary>
    public void UpdateStat()
    {
        
        _hpComponent.Init(PlayerManager.PlayerData.GetStat(StatID.MaxHp.ToString()).Value);
        _hpBar.Init(PlayerManager.PlayerData.GetStat(StatID.MaxHp.ToString()).Value);



        ProjectileAttack projectileAttack = new ProjectileAttack
        (
            _projectilePrefab,
            transform
        );

        AttackComponentArgs args = new AttackComponentArgs()
        {
            Attack = projectileAttack,
            AttackPower = PlayerManager.PlayerData.GetStat(StatID.AttackPower.ToString()).Value,
            AttackSpeed = PlayerManager.PlayerData.GetStat(StatID.AttackSpeed.ToString()).Value
        };

        _attackComponent.Init(args);
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
}
