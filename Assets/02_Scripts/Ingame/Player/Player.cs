using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : SerializedMonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;  // ����ü ������ 
    
    private HPComponent _hpComponent;                       // HP ������Ʈ
    private HPBar _hpBar;                                   // HP ��
    private AttackComponent _attackComponent;               // ���� ������Ʈ
    private AnimComponent _animComponent;                   // �ִ� ������Ʈ


    /// <summary>
    /// �ʱ�ȭ
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
    /// ���� ������Ʈ
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
    /// ������ ����
    /// </summary>
    public void TakeDamage(OnTakeDamagedArgs args)
    {
        if (args.CurrentHp <= 0)
            Die();
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Die()
    {
        Debug.Log("�÷��̾� �׾����ϴ�!");
    }
}
