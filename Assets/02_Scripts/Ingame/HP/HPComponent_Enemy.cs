using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPComponent_Enemy : HPComponent
{
    public Action OnDeadEnemy;      // ���ʹ� �׾����� �̺�Ʈ

    /// <summary>
    /// ������ ����
    /// </summary>
    public override void TakeDamage(int atk, bool isCritical)
    {
        base.TakeDamage(atk, isCritical);

        DamageTextManager.Instance.ShowDamageText(atk, transform.position, isCritical); // ������ �ؽ�Ʈ ����
        FXManager.Instance.SpawnFX(FXName.FX_Enemy_Damaged, transform); // ����Ʈ ����
        
        if (CurrentHp <= 0) // ���������� �½�ũ �� ó�� �� - ü�� �� �������� ����ó��
            Die();
    }

    /// <summary>
    /// ����
    /// </summary>
    public override void Die()
    {
        base.Die();

        FXManager.Instance.SpawnFX(FXName.FX_Enemy_Die, transform);    // ����Ʈ ����
        
        OnDeadEnemy?.Invoke(); // �̺�Ʈ �˸�
    }
}
