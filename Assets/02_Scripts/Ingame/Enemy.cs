using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SerializedMonoBehaviour
{
    private ObjectPool<Enemy> _pool; // 자신을 반환할 풀 참조

    public event Action<Enemy> OnEnemySpawned;
    public event Action<Enemy> OnEnemyDied;

    private int _atk;         // 공격력
    private int _atkSpeed;    // 공격속도
    private int _hp;          // 체력
    private int _maxHp;       // 최대체력
    private int _speed;       // 이동속도


    public void Initialize(ObjectPool<Enemy> pool)
    {
        _pool = pool;

        // 스폰 이벤트 호출
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
        // 사망 이벤트 호출
        OnEnemyDied?.Invoke(this);

        // 풀로 반환
        _pool.ReturnObject(this); 
    }

}
