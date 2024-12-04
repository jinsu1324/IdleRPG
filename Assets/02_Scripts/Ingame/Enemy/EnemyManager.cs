using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : SingletonBase<EnemyManager>
{
    private List<Enemy> _fieldEnemyList = new List<Enemy>();    // �ʵ忡 �����Ǿ� �ִ� ���ʹ� ����Ʈ

    /// <summary>
    /// ���ʹ� �̺�Ʈ ����
    /// </summary>
    public void Subscribe_EnemyEvents(Enemy enemy)
    {
        enemy.OnEnemySpawn += AddFieldEnemyList;    
        enemy.OnEnemyDie += OnEnemyDieHandler;      
    }

    /// <summary>
    /// ���� ������ �̺�Ʈ�� ����ɶ� ó���� �͵�
    /// </summary>
    private void OnEnemyDieHandler(Enemy enemy)
    {
        // �� �ʵ� ����Ʈ���� ����
        RemoveFieldEnemyList(enemy);

        // ��� ���� ����
        enemy.OnEnemySpawn -= AddFieldEnemyList;
        enemy.OnEnemyDie -= OnEnemyDieHandler;
    }

    /// <summary>
    /// �ʵ� ���ʹ� ����Ʈ�� ���ʹ� �߰�
    /// </summary>
    public void AddFieldEnemyList(Enemy enemy)
    {
        _fieldEnemyList.Add(enemy);
    }

    /// <summary>
    /// �ʵ� ���ʹ� ����Ʈ���� ���ʹ� ����
    /// </summary>
    public void RemoveFieldEnemyList(Enemy enemy)
    {
        _fieldEnemyList.Remove(enemy);
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
}
