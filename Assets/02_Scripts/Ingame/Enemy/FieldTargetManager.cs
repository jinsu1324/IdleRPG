using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �ʵ� Ÿ�ٵ� ������
/// </summary>
public class FieldTargetManager
{
    private static List<IDamagable> _fieldTargetList = new List<IDamagable>();    // �ʵ忡 �����Ǿ� �ִ� Ÿ�� ����Ʈ

    /// <summary>
    /// �ʵ� Ÿ�� ����Ʈ�� �߰�
    /// </summary>
    public static void AddFieldEnemyList(IDamagable target)
    {
        _fieldTargetList.Add(target);
    }

    /// <summary>
    /// �ʵ� ���ʹ� ����Ʈ���� ����
    /// </summary>
    public static void RemoveFieldEnemyList(IDamagable target)
    {
        _fieldTargetList.Remove(target);
    }

    /// <summary>
    /// ���� ����� ����ִ� Ÿ�� ã��
    /// </summary>
    public static IDamagable GetClosestLivingTarget(Vector3 pos) 
    {
        // ����ִ� ���鸸 ���͸� �� �Ÿ� �������� ����
        IDamagable closestTarget = _fieldTargetList.
            Where(target => target != null && !target.IsDead). // ����ִ� Ÿ�ٸ� ���͸�
            OrderBy(target => Vector3.Distance(pos, (target as Component).transform.position)).     // �Ÿ� ���� ����
            FirstOrDefault(); // ���� ����� Ÿ�� ��ȯ

        return closestTarget;
    }

    /// <summary>
    /// �ʵ� Ÿ�� ��� ����
    /// </summary>
    public static void ClearAllFieldTarget()
    {
        foreach(IDamagable target in _fieldTargetList)
        {
            if (target is HPComponent hpComponent)
            {
                EnemyCore enemy = hpComponent.GetComponent<EnemyCore>();
                enemy.ReturnPool(); // �ʵ� ������ Ǯ�� ��������
            }
        }

        _fieldTargetList.Clear(); // ����Ʈ ����
    }
}
