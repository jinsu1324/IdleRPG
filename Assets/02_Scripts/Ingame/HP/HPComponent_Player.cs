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
    public override void Init(int hp)
    {
        base.Init(hp);

        PlayerStats.OnPlayerStatChanged += ChangeMaxHp; // �÷��̾� ���� ����Ǿ��� �� -> �ִ�ü�� ����
        PlayerResetService.OnReset += ResetHp;  // �÷��̾� �����Ҷ�, HP ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= ChangeMaxHp;
        PlayerResetService.OnReset -= ResetHp;

    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public override void TakeDamage(int atk, bool isCritical)
    {
        base.TakeDamage(atk, isCritical);
        
        if (CurrentHp <= 0) // ���������� �½�ũ �� ó�� �� - ü�� �� �������� ����ó��
            Die();
    }

    /// <summary>
    /// ����
    /// </summary>
    public override void Die()
    {
        base.Die();
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
}
