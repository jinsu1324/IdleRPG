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
    [SerializeField] private Projectile _projectilePrefab;  // ����ü ������ 

    private HPComponent _hpComponent;                       // HP ������Ʈ
    private HPBar _hpBar;                                   // HP ��
    private AttackComponent _attackComponent;               // ���� ������Ʈ
    private AnimComponent _animComponent;                   // �ִ� ������Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStatContainer.OnStatChanged += ChangeComponentsValue; // ������ ����� ��, ������Ʈ�� �� ����
    }

    /// <summary>
    /// �ʱ�ȭ
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
    /// HP ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Init_HPComponent(int maxHp)
    {
        _hpComponent = GetComponent<HPComponent>();
        _hpComponent.Init(maxHp); // HP ������Ʈ �ʱ�ȭ
        _hpComponent.OnDead += PlayerDeadTask;  // �׾��� ��, �÷��̾�� ó���ؾ��� �͵� ó��
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
    /// Attack ������Ʈ �ʱ�ȭ
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
        _attackComponent.Init(args); // Attack ������Ʈ �ʱ�ȭ
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
    /// ������Ʈ�� ��ġ ����
    /// </summary>
    private void ChangeComponentsValue(OnStatChangedArgs? args)
    {
        _hpComponent.ChangeMaxHp(args?.MaxHp ?? 0); // null �̸� 0(�⺻�� ��������) �� �Ҵ�
        _attackComponent.ChangeAttackPower(args?.AttackPower ?? 0);
        _attackComponent.ChangeAttackSpeed(args?.AttackSpeed ?? 0);
    }

    /// <summary>
    /// �÷��̾� �׾��� �� ó���� �͵�
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
