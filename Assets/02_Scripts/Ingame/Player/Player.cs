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

    




    public void OnEnable()
    {
        Debug.Log("3���� : �� ������Ʈ");
        _hpComponent = GetComponent<HPComponent>();
        _hpBar = GetComponentInChildren<HPBar>();
        _attackComponent = GetComponent<AttackComponent>();
        _animComponent = GetComponent<AnimComponent>();


        Debug.Log("3. ����");
        StatManager.Instance.OnPlayerStatSetting += ComponentInit;
        //StatManager.Instance.OnStatChanged
    }







    private void ComponentInit(OnPlayerStatSettingArgs statArgs)
    {
        Debug.Log("5. �̺�Ʈ �ڵ鷯 ����");

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
