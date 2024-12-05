using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public struct EnemyEventArgs
{
    public Enemy Enemy;
    public int Count;
}

public enum EnemyState
{
    Move,
    Attack,
}

public class Enemy : SerializedMonoBehaviour
{
    public static event Action<EnemyEventArgs> OnEnemySpawn;    // 스폰시 이벤트
    public static event Action<EnemyEventArgs> OnEnemyDie;      // 죽었을때 이벤트

    private ObjectPool<Enemy> _pool;                            // 자신을 반환할 풀 참조

    private HPComponent _hpComponent;                           // HP 컴포넌트
    private HPBar _hpBar;                                       // HP 바
    private AttackComponentCollision _attackComponentCollision; // 어택 컴포넌트 프로젝타일 충돌타입
    private AnimComponent _animComponent;                       // 애님 컴포넌트
    private MoveComponent _moveComponent;                       // 무브 컴포넌트

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(ObjectPool<Enemy> pool, EnemyData enemyData, int statPercentage)
    {
        _pool = pool;

        // 스탯 셋팅
        int maxHp = (enemyData.MaxHp * statPercentage) / 100;
        int attackPower = (enemyData.AttackPower * statPercentage) / 100;
        int attackSpeed = enemyData.AttackSpeed;
        int moveSpeed = enemyData.MoveSpeed;

        Init_HPComponent(maxHp);
        Init_HPBar(maxHp);
        Init_AttackComponentCollision(attackPower, attackSpeed);
        Init_AnimComponent();
        Init_MoveComponent(moveSpeed);

        EnemyEventArgs args = new EnemyEventArgs() { Enemy = this };
        OnEnemySpawn?.Invoke(args); // 스폰 이벤트 호출
    }

    /// <summary>
    /// HP 컴포넌트 초기화
    /// </summary>
    private void Init_HPComponent(int maxHp)
    {
        _hpComponent = GetComponent<HPComponent>();
        _hpComponent.Init(maxHp);
        _hpComponent.OnDead += EnemyDeadTask; // 죽었을 때, 에너미에서 처리해야할 것들 처리
    }

    /// <summary>
    /// HP바 컴포넌트 초기화
    /// </summary>
    private void Init_HPBar(int maxHp)
    {
        _hpBar = GetComponentInChildren<HPBar>();
        _hpBar.Init(maxHp);
    }

    /// <summary>
    /// 충돌방식 공격 컴포넌트  초기화
    /// </summary>
    private void Init_AttackComponentCollision(int attackPower, int attackSpeed)
    {
        _attackComponentCollision = GetComponent<AttackComponentCollision>();
        _attackComponentCollision.Init(attackPower, attackSpeed);
    }

    /// <summary>
    /// Anim 컴포넌트 초기화
    /// </summary>
    private void Init_AnimComponent()
    {
        _animComponent = GetComponent<AnimComponent>();
        _animComponent.Init();
    }

    /// <summary>
    /// 무브 컴포넌트 초기화
    /// </summary>
    private void Init_MoveComponent(int moveSpeed)
    {
        _moveComponent = GetComponent<MoveComponent>();
        _moveComponent.Init(moveSpeed);
    }

    /// <summary>
    /// 죽었을 때, 에너미에서 처리해야할 것들 처리
    /// </summary>
    private void EnemyDeadTask()
    {
        GoldManager.Instance.AddCurrency(1000); // 플레이어의 골드 추가 // Todo : 얼마 얻을지 데이터로 빼기

        EnemyEventArgs args = new EnemyEventArgs() { Enemy = this, Count = 1 };
        OnEnemyDie?.Invoke(args); // 사망 이벤트 호출  

        _pool.ReturnObject(this); // 풀로 반환
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        if (_hpComponent != null)
            _hpComponent.OnDead -= EnemyDeadTask;
    }
}
