using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ObjectPoolObj
{
    private EnemyID _enemyID;   // ID

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GetComponent<HPComponent_Enemy>().OnDeadEnemy += EnemyDeadTask; // 에너미 죽었을때, 에너미 죽음에 필요한 태스크들 처리
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        GetComponent<HPComponent_Enemy>().OnDeadEnemy -= EnemyDeadTask;
    }
    
    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(EnemyData enemyData, int statPercentage)
    {
        _enemyID = (EnemyID)Enum.Parse(typeof(EnemyID), enemyData.ID);

        // 스탯 셋팅
        int maxHp = (enemyData.MaxHp * statPercentage) / 100;
        int attackPower = (enemyData.AttackPower * statPercentage) / 100;
        int attackSpeed = enemyData.AttackSpeed;
        int moveSpeed = enemyData.MoveSpeed;

        GetComponent<HPComponent_Enemy>().Init(maxHp); // HP 컴포넌트 초기값 설정
        GetComponent<AttackComponent_Enemy>().Init(attackPower, attackSpeed); // 공격 컴포넌트 초기값 설정
        GetComponent<MoveComponent>().Init(moveSpeed); // 무브 컴포넌트 초기값 설정

        FieldTargetManager.AddFieldEnemyList(GetComponent<HPComponent_Enemy>()); // 필드타겟 리스트에 추가
    }

    /// <summary>
    /// 죽었을 때, 에너미에서 처리해야할 것들 처리
    /// </summary>
    private void EnemyDeadTask()
    {
        StageManager.Instance.AddKillCount();   // 킬 카운트 증가
        EnemyDropGoldManager.AddGoldByEnemy(_enemyID); // 골드 추가
        FieldTargetManager.RemoveFieldEnemyList(GetComponent<HPComponent_Enemy>()); // 필드타겟 리스트에서 삭제
        QuestManager.Instance.UpdateQuestProgress(QuestType.KillEnemy, 1); // 적 죽이기 퀘스트에 업데이트
        CurrencyIconMover.Instance.MoveCurrency(CurrencyType.Gold, transform.position); // 골드 이동 애니메이션

        ReturnPool(); // 풀로 돌려보내기
    }

    /// <summary>
    /// 풀로 반환
    /// </summary>
    public void ReturnPool()
    {
        BackTrans();
    }
}
