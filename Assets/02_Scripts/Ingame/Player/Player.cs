using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Start
    /// </summary>
    private void OnEnable()
    {
        PlayerManager.Instance.OnStatChanged += ChangeComponentsValue;
    }

    /// <summary>
    /// �ʱ�ȭ
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

        _hpComponent.Init(maxHp); // HP ������Ʈ �ʱ�ȭ
        _hpComponent.OnTakeDamaged += TakeDamage;

        
        _hpBar.Init(maxHp); // HP�� �ʱ�ȭ

        
        ProjectileAttack projectileAttack = new ProjectileAttack(_projectilePrefab, transform);
        AttackComponentArgs args = new AttackComponentArgs()
        {
            Attack = projectileAttack,
            AttackPower = attackPower,
            AttackSpeed = attackSpeed
        };
        _attackComponent.Init(args); // Attack ������Ʈ �ʱ�ȭ

        
        _animComponent.Init(); // �ִ� ������Ʈ �ʱ�ȭ
    }


    private void ChangeComponentsValue(OnStatChangedArgs? args)
    {
        _hpComponent.ChangeMaxHp(args?.MaxHp ?? 0); // null �̸� 0(�⺻�� ��������) �� �Ҵ�
        _attackComponent.ChangeAttackPower(args?.AttackPower ?? 0);
        _attackComponent.ChangeAttackSpeed(args?.AttackSpeed ?? 0);
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

    private void OnDestroy()
    {
        PlayerManager.Instance.OnStatChanged -= ChangeComponentsValue;
    }
}
