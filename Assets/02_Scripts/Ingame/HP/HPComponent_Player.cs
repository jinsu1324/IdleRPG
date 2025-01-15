using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPComponent_Player : HPComponent
{
    public Action OnDeadPlayer;     // �÷��̾� �׾����� �̺�Ʈ

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public override void Init(float hp)
    {
        base.Init(hp);

        PlayerStats.OnPlayerStatChanged += ChangeMaxHp; // �÷��̾� ���� ����Ǿ��� �� -> �ִ�ü�� ����
        PlayerResetService.OnReset += ResetHp;  // �÷��̾� �����Ҷ�, HP ����
        PlayerResetService.OnReset += ResetIsDead;  // �÷��̾� �����Ҷ�, �׾����� bool���� ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= ChangeMaxHp;
        PlayerResetService.OnReset -= ResetHp;
        PlayerResetService.OnReset -= ResetIsDead;
    }

    /// <summary>
    /// ������ �޾����� �½�ũ�� ó��
    /// </summary>
    protected override void TaskDamaged(float atk, bool isCritical)
    {
        if (CurrentHp <= 0) // ���������� �½�ũ �� ó�� �� - ü�� �� �������� ����ó��
            Die();
    }

    /// <summary>
    /// �׾����� �½�ũ�� ó��
    /// </summary>
    protected override void TaskDie()
    {
        OnDeadPlayer?.Invoke(); // �̺�Ʈ �˸�
    }

    /// <summary>
    /// �ִ�ü�� ����
    /// </summary>
    public void ChangeMaxHp(PlayerStatArgs args)
    {
        MaxHp = args.MaxHp;
    }

    /// <summary>
    /// HP ����
    /// </summary>
    private void ResetHp()
    {
        CurrentHp = MaxHp;
    }

    /// <summary>
    /// �׾����� false�� ����
    /// </summary>
    private void ResetIsDead()
    {
        IsDead = false;
    }
}
