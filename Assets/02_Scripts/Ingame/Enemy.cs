using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SerializedMonoBehaviour
{
    public event Action<Enemy> OnEnemySpawned;      // ������ �̺�Ʈ
    public event Action<Enemy> OnEnemyDied;         // �׾����� �̺�Ʈ

    private ObjectPool<Enemy> _pool;                // �ڽ��� ��ȯ�� Ǯ ����
    private EnemyData _enemyData;                   // ������

    private int _hp;                                // ü��
    private int _maxHp;                             // �ִ�ü��
    private int _atk;                               // ���ݷ�
    private int _atkSpeed;                          // ���ݼӵ�
    private int _moveSpeed;                             // �̵��ӵ�

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Initialize(ObjectPool<Enemy> pool, EnemyData enemyData, int statPercentage)
    {
        _pool = pool;
        _enemyData = enemyData;

        // ���� ����
        _hp = (_enemyData.MaxHp * statPercentage) / 100;
        _maxHp = (_enemyData.MaxHp * statPercentage) / 100;
        _atk = (_enemyData.Atk * statPercentage) / 100;
        _atkSpeed = _enemyData.AtkDelay;
        _moveSpeed = _enemyData.MoveSpeed;

        // ���� �̺�Ʈ ȣ��
        OnEnemySpawned?.Invoke(this);

        //Debug.Log(_atk);
    }

    private void Move()
    {

    }

    private void Attack()
    {

    }

    private void Damaged(int atk)
    {
        _hp -= atk;

        if (_hp <= 0)
            Died();
    }

    private void Died()
    {
        // ��� �̺�Ʈ ȣ��
        OnEnemyDied?.Invoke(this);

        // Ǯ�� ��ȯ
        _pool.ReturnObject(this); 
    }

}
