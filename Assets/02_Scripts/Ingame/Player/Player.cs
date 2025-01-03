using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// �÷��̾� ������
/// </summary>
public class Player : SerializedMonoBehaviour
{
    private HPComponent _hpComponent;                               // HP ������Ʈ
    private HPBar _hpBar;                                           // HP ��
    private AttackComponentProjectile _attackComponentProjectile;   // ���� ������Ʈ ������Ÿ�� �߻�Ÿ��
    private AnimComponent _animComponent;                           // �ִ� ������Ʈ
    private BlinkOnHit _blinkOnHit;                                 // ������ �޾��� �� ��������Ʈ �������ִ� ������Ʈ
    private EquipItemComponent _equipItemComponent;                 // ���� ������ ������Ʈ
    
    /// <summary>
    /// �ʱ�ȭ
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
    /// HP ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Init_HPComponent(int maxHp)
    {
        _hpComponent = GetComponent<HPComponent>();
        _hpComponent.Init(maxHp); // HP ������Ʈ �ʱ�ȭ

        _hpComponent.OnDead -= PlayerDeadTask;  // �̺�Ʈ �ߺ����� (�÷��̾� �����Ҷ��� OnDisable�� �ȵǱ� ����)
        _hpComponent.OnDead += PlayerDeadTask;  // �׾��� ��, �÷��̾�� ó���ؾ��� �͵� ó��

        PlayerStats.OnPlayerStatChanged -= _hpComponent.ChangeMaxHp; // �̺�Ʈ �ߺ����� (�÷��̾� �����Ҷ��� OnDisable�� �ȵǱ� ����)
        PlayerStats.OnPlayerStatChanged += _hpComponent.ChangeMaxHp; // �÷��̾� ���� ����Ǿ��� ��, �ִ�ü�� ����
    }

    /// <summary>
    /// HP�� ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Init_HPBar(int maxHp)
    {
        _hpBar = GetComponentInChildren<HPBar>();
        _hpBar.Init(maxHp);
    }

    /// <summary>
    /// ���� ������Ʈ ������Ÿ�� �߻�Ÿ�� �ʱ�ȭ
    /// </summary>
    private void Init_AttackComponentProjectile(int attackPower, int attackSpeed, int criticalRate, int criticalMultiple)
    {
        _attackComponentProjectile = GetComponent<AttackComponentProjectile>();
        _attackComponentProjectile.Init(attackPower, attackSpeed, criticalRate, criticalMultiple);

        // �÷��̾� ���� ����Ǿ��� ��, ���ݷ� ���ݼӵ� ����
        PlayerStats.OnPlayerStatChanged -= _attackComponentProjectile.ChangeAttackPower; 
        PlayerStats.OnPlayerStatChanged += _attackComponentProjectile.ChangeAttackPower;

        PlayerStats.OnPlayerStatChanged -= _attackComponentProjectile.ChangeAttackSpeed;
        PlayerStats.OnPlayerStatChanged += _attackComponentProjectile.ChangeAttackSpeed;
    }

    /// <summary>
    /// Anim ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Init_AnimComponent()
    {
        _animComponent = GetComponent<AnimComponent>();
        _animComponent.Init();
    }

    /// <summary>
    /// BlinkOnHit ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Init_BlinkOnHit()
    {
        _blinkOnHit = GetComponent<BlinkOnHit>();   
        _blinkOnHit.Init();
    }

    /// <summary>
    /// EquipItemComponent �ʱ�ȭ
    /// </summary>
    private void Init_EquipItemComponent()
    {
        _equipItemComponent = GetComponent<EquipItemComponent>();
    }

    /// <summary>
    /// �÷��̾� �׾��� �� ó���� �͵�
    /// </summary>
    private void PlayerDeadTask()
    {
        StageManager.Instance.DefeatRestartGame();
    }

    /// <summary>
    /// OnDisable �̺�Ʈ ���� ����
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
