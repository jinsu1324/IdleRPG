using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SerializedMonoBehaviour
{
    private ObjectPool<Enemy> _pool; // �ڽ��� ��ȯ�� Ǯ ����

    public event Action<Enemy> OnEnemySpawned;
    public event Action<Enemy> OnEnemyDied;

    private int _atk;         // ���ݷ�
    private int _atkSpeed;    // ���ݼӵ�
    private int _hp;          // ü��
    private int _maxHp;       // �ִ�ü��
    private int _speed;       // �̵��ӵ�


    public void Initialize(ObjectPool<Enemy> pool)
    {
        _pool = pool;

        // ���� �̺�Ʈ ȣ��
        OnEnemySpawned?.Invoke(this);
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
