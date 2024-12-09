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
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        Enemy.OnEnemySpawn += AddFieldEnemyList;  // �� ������ ��, �ʵ� Ÿ�� ����Ʈ�� �߰�
        Enemy.OnEnemyDie += RemoveFieldEnemyList;  // �� ���� ��, �ʵ� Ÿ�� ����Ʈ���� ����
    }

    /// <summary>
    /// �ʵ� Ÿ�� ����Ʈ�� �߰�
    /// </summary>
    public void AddFieldEnemyList(EnemyEventArgs args)
    {
        IDamagable target = args.Enemy.GetComponent<IDamagable>();
        if (target != null)
            _fieldTargetList.Add(target);
    }

    /// <summary>
    /// �ʵ� ���ʹ� ����Ʈ���� ����
    /// </summary>
    public void RemoveFieldEnemyList(EnemyEventArgs args)
    {
        IDamagable target = args.Enemy.GetComponent<IDamagable>();
        if(target != null)
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
                Enemy enemy = hpComponent.GetComponent<Enemy>();
                enemy.ReturnPool(); // �ʵ� ������ Ǯ�� ��������
            }
        }

        _fieldTargetList.Clear(); // ����Ʈ ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        Enemy.OnEnemySpawn -= AddFieldEnemyList;
        Enemy.OnEnemyDie -= RemoveFieldEnemyList; 
    }
}
