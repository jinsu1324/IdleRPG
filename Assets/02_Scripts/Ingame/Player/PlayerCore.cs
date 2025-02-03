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
    [SerializeField] private AnimComponent _animComponent;                      // �ִϸ��̼� ������Ʈ

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

        StageManager.OnStageBuilding += _attackComponent.IsAttackStop_True;    // �������� �������϶� -> �����ߴ� true
        StageManager.OnStageBuilding += _animComponent.MoveAnimStart;   // �������� �������� �� -> ������ �ִϸ��̼� ����

        StageManager.OnStageBuildFinish += _attackComponent.IsAttackStop_False; // �������� ���� �������� -> �����ߴ� false
        StageManager.OnStageBuildFinish += _animComponent.MoveAnimStop; // �������� ���� �������� -> ������ �ִϸ��̼� ����
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

        StageManager.OnStageBuilding -= _attackComponent.IsAttackStop_True;
        StageManager.OnStageBuilding -= _animComponent.MoveAnimStart;

        StageManager.OnStageBuildFinish -= _attackComponent.IsAttackStop_False;
        StageManager.OnStageBuildFinish -= _animComponent.MoveAnimStop;
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
