using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SerializedMonoBehaviour
{
    public event Action<Enemy> OnEnemySpawned;      // 스폰시 이벤트
    public event Action<Enemy> OnEnemyDied;         // 죽었을때 이벤트

    private ObjectPool<Enemy> _pool;                // 자신을 반환할 풀 참조
    private EnemyData _enemyData;                   // 데이터

    private int _hp;                                // 체력
    private int _maxHp;                             // 최대체력
    private int _atk;                               // 공격력
    private int _atkSpeed;                          // 공격속도
    private int _moveSpeed;                             // 이동속도

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize(ObjectPool<Enemy> pool, EnemyData enemyData, int statPercentage)
    {
        _pool = pool;
        _enemyData = enemyData;

        // 스탯 셋팅
        _hp = (_enemyData.MaxHp * statPercentage) / 100;
        _maxHp = (_enemyData.MaxHp * statPercentage) / 100;
        _atk = (_enemyData.Atk * statPercentage) / 100;
        _atkSpeed = _enemyData.AtkDelay;
        _moveSpeed = _enemyData.MoveSpeed;

        // 스폰 이벤트 호출
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
        // 사망 이벤트 호출
        OnEnemyDied?.Invoke(this);

        // 풀로 반환
        _pool.ReturnObject(this); 
    }

}
