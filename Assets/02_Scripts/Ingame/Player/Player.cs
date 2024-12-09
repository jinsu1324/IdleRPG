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
    public static event Action OnPlayerDie;                         // �÷��̾� �׾����� �̺�Ʈ

    private HPComponent _hpComponent;                               // HP ������Ʈ
    private HPBar _hpBar;                                           // HP ��
    private AttackComponentProjectile _attackComponentProjectile;   // ���� ������Ʈ ������Ÿ�� �߻�Ÿ��
    private AnimComponent _animComponent;                           // �ִ� ������Ʈ
    private BlinkOnHit _blinkOnHit;                                 // ������ �޾��� �� ��������Ʈ �������ִ� ������Ʈ

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
        int criticalRate = statArgs.Critical;

        Init_HPComponent(maxHp);
        Init_HPBar(maxHp);
        Init_AttackComponentProjectile(attackPower, attackSpeed, criticalRate);
        Init_AnimComponent();
        Init_BlinkOnHit();
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
    private void Init_AttackComponentProjectile(int attackPower, int attackSpeed, int criticalRate)
    {
        _attackComponentProjectile = GetComponent<AttackComponentProjectile>();
        _attackComponentProjectile.Init(attackPower, attackSpeed, criticalRate);
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
    /// ������Ʈ�� ��ġ ����
    /// </summary>
    private void ChangeComponentsValue(OnStatChangedArgs args)
    {
        _hpComponent.ChangeMaxHp(args.MaxHp);
        _attackComponentProjectile.ChangeAttackPower(args.AttackPower);
        _attackComponentProjectile.ChangeAttackSpeed(args.AttackSpeed);
    }

    /// <summary>
    /// �÷��̾� �׾��� �� ó���� �͵�
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
