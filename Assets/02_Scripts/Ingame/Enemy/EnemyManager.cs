using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : SingletonBase<EnemyManager>
{
    private List<Enemy> _fieldEnemyList = new List<Enemy>();    // �ʵ忡 �����Ǿ� �ִ� ���ʹ� ����Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        Enemy.OnEnemySpawn += AddFieldEnemyList;  // �� ������ ��, �ʵ� �� ����Ʈ�� �߰�
        Enemy.OnEnemyDie += RemoveFieldEnemyList;  // �� ���� ��, �ʵ� �� ����Ʈ���� ����
    }

    /// <summary>
    /// �ʵ� ���ʹ� ����Ʈ�� ���ʹ� �߰�
    /// </summary>
    public void AddFieldEnemyList(EnemyEventArgs args)
    {
        _fieldEnemyList.Add(args.Enemy);
    }

    /// <summary>
    /// �ʵ� ���ʹ� ����Ʈ���� ���ʹ� ����
    /// </summary>
    public void RemoveFieldEnemyList(EnemyEventArgs args)
    {
        _fieldEnemyList.Remove(args.Enemy);
    }

    /// <summary>
    /// Ư����ġ���� ���� ����� �� ��ȯ
    /// </summary>
    public Enemy GetClosestEnemy(Vector3 pos)
    {
        Enemy closestEnemy = 
            _fieldEnemyList.OrderBy(enemy => Vector3.Distance(pos, enemy.transform.position)).FirstOrDefault();
        return closestEnemy;
    }

    /// <summary>
    /// ���� ����� ����ִ� �� ã��
    /// </summary>
    public Enemy GetClosestLivingEnemy(Vector3 pos) 
    {
        // ����ִ� ���鸸 ���͸� �� �Ÿ� �������� ����
        Enemy closestEnemy = _fieldEnemyList.
            Where(enemy => enemy != null && !enemy.IsDead). // ����ִ� ���� ���͸�
            OrderBy(enemy => Vector3.Distance(pos, enemy.transform.position)).     // �Ÿ� ���� ����
            FirstOrDefault(); // ���� ����� �� ��ȯ

        return closestEnemy;
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
