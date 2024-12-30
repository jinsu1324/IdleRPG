using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum EnemyState
{
    Move,
    Attack,
}

public class Enemy : SerializedMonoBehaviour
{
    private ObjectPool<Enemy> _pool;                            // 자신을 반환할 풀 참조
    private EnemyID _enemyID;                                   // ID
    private HPComponent _hpComponent;                           // HP 컴포넌트
    private HPBar _hpBar;                                       // HP 바
    private AttackComponentCollision _attackComponentCollision; // 어택 컴포넌트 프로젝타일 충돌타입
    private AnimComponent _animComponent;                       // 애님 컴포넌트
    private MoveComponent _moveComponent;                       // 무브 컴포넌트
    private BlinkOnHit _blinkOnHit;                             // 데미지 받았을 때 스프라이트 깜빡여주는 컴포넌트

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(ObjectPool<Enemy> pool, EnemyData enemyData, int statPercentage)
    {
        _pool = pool;
        _enemyID = (EnemyID)Enum.Parse(typeof(EnemyID), enemyData.ID);

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
        Init_BlinkOnHit();

        FieldTargetManager.AddFieldEnemyList(_hpComponent); // 필드타겟 리스트에 추가
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
    /// 충돌방식 공격 컴포넌트 초기화
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
    /// BlinkOnHit 컴포넌트 초기화
    /// </summary>
    private void Init_BlinkOnHit()
    {
        _blinkOnHit = GetComponent<BlinkOnHit>();
        _blinkOnHit.Init();
    }

    /// <summary>
    /// 죽었을 때, 에너미에서 처리해야할 것들 처리
    /// </summary>
    private void EnemyDeadTask()
    {
        StageManager.Instance.AddKillCount();   // 킬 카운트 증가
        EnemyDropGoldManager.AddGoldByEnemy(_enemyID); // 골드 추가
        FieldTargetManager.RemoveFieldEnemyList(_hpComponent); // 필드타겟 리스트에서 삭제



        QuestManager.Instance.UpdateQuestProgress(QuestType.KillEnemy, 1);

        ReturnPool();   // 풀로 돌려보내기
    }

    /// <summary>
    /// 풀로 반환
    /// </summary>
    public void ReturnPool()
    {
        _pool.ReturnObject(this);
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
