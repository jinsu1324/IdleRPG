using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 에너미 코어
/// </summary>
public class EnemyCore : ObjectPoolObj
{
    private EnemyID _enemyID;                                               // ID
    [SerializeField] private HPComponent _hpComponent;                      // HP 컴포넌트
    [SerializeField] private AttackComponent_ColliderType _attackComponent; // 공격 컴포넌트 (충돌 타입)
    [SerializeField] private MoveComponent moveComponent;                   // 무브 컴포넌트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _hpComponent.OnTakeDamage += EnemyTakeDamageTask; // 데미지 받았을때 -> 에너미에서만 처리할것들 처리
        _hpComponent.OnDie += EnemyDieTask; // 죽었을때 -> 에너미 죽음에 필요한 태스크들 처리
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _hpComponent.OnTakeDamage -= EnemyTakeDamageTask;
        _hpComponent.OnDie -= EnemyDieTask;

    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(EnemyData enemyData, float statPercentage)
    {
        _enemyID = (EnemyID)Enum.Parse(typeof(EnemyID), enemyData.ID);

        // 스탯 재 계산
        float maxHp = enemyData.MaxHp * statPercentage;
        float attackPower = enemyData.AttackPower * statPercentage;
        float attackSpeed = enemyData.AttackSpeed;
        float moveSpeed = enemyData.MoveSpeed;

        // HP 컴포넌트 초기화
        HPInitArgs hpInitArgs = new HPInitArgs() 
        { 
            MaxHp = maxHp
        };
        _hpComponent.Init(hpInitArgs);
        
        // 공격 컴포넌트 초기화
        AttackInitArgs attackInitArgs = new AttackInitArgs()
        {
            AttackPower = attackPower,
            AttackSpeed = attackSpeed,
        };
        _attackComponent.Init(attackInitArgs);

        // 무브 컴포넌트 초기화
        moveComponent.Init(moveSpeed);

        // 필드타겟 리스트에 추가
        FieldTargetManager.AddFieldEnemyList(GetComponent<HPComponent>()); 
    }

    /// <summary>
    /// 에너미가 데미지 받았을때 처리할 것들
    /// </summary>
    private void EnemyTakeDamageTask(TakeDamageArgs args)
    {
        DamageTextManager.Instance.ShowDamageText(args.Damage, transform.position, args.IsCritical); // 데미지 텍스트 띄우기
        FXManager.Instance.SpawnFX(FXName.FX_Enemy_Damaged, transform); // 이펙트 띄우기
    }

    /// <summary>
    /// 에너미가 죽었을때 처리할 것들
    /// </summary>
    private void EnemyDieTask()
    {
        StageManager.Instance.AddKillCount();   // 킬 카운트 증가
        EnemyDataManager.AddGoldByEnemy(_enemyID); // 골드 추가
        FieldTargetManager.RemoveFieldEnemyList(GetComponent<HPComponent>()); // 필드타겟 리스트에서 삭제
        QuestManager.AddValue_KillEnemyQuest(QuestType.KillEnemy, 1); // 적 죽이기 퀘스트에 업데이트

        CurrencyIconMover.Instance.MoveCurrency_Multi(CurrencyType.Gold, transform.position); // 골드 이동 애니메이션

        FXManager.Instance.SpawnFX(FXName.FX_Enemy_Die, transform);    // 이펙트 띄우기

        ReturnPool(); // 풀로 돌려보내기
    }

    /// <summary>
    /// 풀로 반환
    /// </summary>
    public void ReturnPool() => BackTrans();
}
