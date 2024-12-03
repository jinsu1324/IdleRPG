using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �÷��̾� ������ �ھ�
/// </summary>
public class Player : SerializedMonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;  // ����ü ������ 

    private HPComponent _hpComponent;                       // HP ������Ʈ
    private HPBar _hpBar;                                   // HP ��
    private AttackComponent _attackComponent;               // ���� ������Ʈ
    private AnimComponent _animComponent;                   // �ִ� ������Ʈ

    private void Awake()
    {
        PlayerManager.Instance.OnStatChanged += UpdateStatComponents;
    }

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


    private void UpdateStatComponents(OnStatChangedArgs args)
    {
        _hpComponent.ChangeMaxHp(args.MaxHp);
        _attackComponent.ChangeAttackPower(args.AttackPower);
        _attackComponent.ChangeAttackSpeed(args.AttackSpeed);
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
        PlayerManager.Instance.OnStatChanged -= UpdateStatComponents;
    }
}
