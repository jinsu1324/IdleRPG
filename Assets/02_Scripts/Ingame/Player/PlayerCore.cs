using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// �÷��̾� �ھ�
/// </summary>
public class PlayerCore : SerializedMonoBehaviour
{
    [SerializeField] private HPComponent _hpComponent;                          // HP ������Ʈ
    [SerializeField] private AttackComponent_ProjectileType _attackComponent;   // ���� ������Ʈ (������Ÿ��Ÿ��)

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _hpComponent.OnDie += PlayerDieTask;  // �÷��̾� �׾�����, �÷��̾� ������ �ʿ��� �½�ũ�� ó��

        PlayerStats.OnPlayerStatChanged += _hpComponent.ChangeMaxHp; // �÷��̾� ���� ����Ǿ��� �� -> �÷��̾��� �ִ�ü�� ����
        PlayerStats.OnPlayerStatChanged += Update_PlayerAttackValues; // �÷��̾� ���� ����Ǿ��� �� -> �÷��̾��� ���ݰ��� ��ġ�� ������Ʈ

        PlayerResetService.OnReset += _hpComponent.ResetHp;  // �÷��̾� �����Ҷ� -> �÷��̾��� HP ����
        PlayerResetService.OnReset += _hpComponent.ResetIsDead;  // �÷��̾� �����Ҷ� -> �÷��̾��� �׾����� bool���� ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _hpComponent.OnDie -= PlayerDieTask;

        PlayerStats.OnPlayerStatChanged -= _hpComponent.ChangeMaxHp; 
        PlayerStats.OnPlayerStatChanged -= Update_PlayerAttackValues;

        PlayerResetService.OnReset -= _hpComponent.ResetHp;
        PlayerResetService.OnReset -= _hpComponent.ResetIsDead;
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        // HP ������Ʈ �ʱ�ȭ
        HPInitArgs hpInitArgs = new HPInitArgs() 
        { 
            MaxHp = PlayerStats.GetStatValue(StatType.MaxHp) 
        };
        _hpComponent.Init(hpInitArgs);


        // ���� ������Ʈ �ʱ�ȭ
        AttackInitArgs attackInitArgs = new AttackInitArgs()
        {
            AttackPower = PlayerStats.GetStatValue(StatType.AttackPower),
            AttackSpeed = PlayerStats.GetStatValue(StatType.AttackSpeed)
        };
        _attackComponent.Init(attackInitArgs);
    }

    /// <summary>
    /// �÷��̾��� ���ݰ��� ��ġ�� ������Ʈ
    /// </summary>
    private void Update_PlayerAttackValues(PlayerStatArgs args)
    {
        AttackUpdateArgs attackUpdateArgs = new AttackUpdateArgs()
        {
            AttackPower = args.AttackPower,
            AttackSpeed = args.AttackSpeed,
        };
        _attackComponent.Update_AttackPower(attackUpdateArgs);
        _attackComponent.Update_AttackSpeed(attackUpdateArgs);
    }

    /// <summary>
    /// �÷��̾ �׾��� �� ó���� �͵�
    /// </summary>
    private void PlayerDieTask()
    {
        StageManager.Instance.DefeatRestartGame(); // �������� �й�� �����
    }
}
