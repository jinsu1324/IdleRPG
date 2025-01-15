using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPComponent_Enemy : HPComponent
{
    public Action OnDeadEnemy;      // ���ʹ� �׾����� �̺�Ʈ

    /// <summary>
    /// ������ �޾��� �� �½�ũ�� ó��
    /// </summary>
    protected override void TaskDamaged(float atk, bool isCritical)
    {
        DamageTextManager.Instance.ShowDamageText(atk, transform.position, isCritical); // ������ �ؽ�Ʈ ����
        FXManager.Instance.SpawnFX(FXName.FX_Enemy_Damaged, transform); // ����Ʈ ����

        if (CurrentHp <= 0) // ���������� �½�ũ �� ó�� �� - ü�� �� �������� ����ó��
            Die();
    }

    /// <summary>
    /// �׾����� �½�ũ�� ó��
    /// </summary>
    protected override void TaskDie()
    {
        FXManager.Instance.SpawnFX(FXName.FX_Enemy_Die, transform);    // ����Ʈ ����
        OnDeadEnemy?.Invoke(); // �̺�Ʈ �˸�
    }
}
