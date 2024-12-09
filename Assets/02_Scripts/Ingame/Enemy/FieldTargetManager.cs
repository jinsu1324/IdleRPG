using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 필드 타겟들 관리자
/// </summary>
public class FieldTargetManager
{
    private static List<IDamagable> _fieldTargetList = new List<IDamagable>();    // 필드에 스폰되어 있는 타겟 리스트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        Enemy.OnEnemySpawn += AddFieldEnemyList;  // 적 스폰될 때, 필드 타겟 리스트에 추가
        Enemy.OnEnemyDie += RemoveFieldEnemyList;  // 적 죽을 때, 필드 타겟 리스트에서 제거
    }

    /// <summary>
    /// 필드 타겟 리스트에 추가
    /// </summary>
    public void AddFieldEnemyList(EnemyEventArgs args)
    {
        IDamagable target = args.Enemy.GetComponent<IDamagable>();
        if (target != null)
            _fieldTargetList.Add(target);
    }

    /// <summary>
    /// 필드 에너미 리스트에서 삭제
    /// </summary>
    public void RemoveFieldEnemyList(EnemyEventArgs args)
    {
        IDamagable target = args.Enemy.GetComponent<IDamagable>();
        if(target != null)
            _fieldTargetList.Remove(target);
    }

    /// <summary>
    /// 가장 가까운 살아있는 타겟 찾기
    /// </summary>
    public static IDamagable GetClosestLivingTarget(Vector3 pos) 
    {
        // 살아있는 적들만 필터링 후 거리 기준으로 정렬
        IDamagable closestTarget = _fieldTargetList.
            Where(target => target != null && !target.IsDead). // 살아있는 타겟만 필터링
            OrderBy(target => Vector3.Distance(pos, (target as Component).transform.position)).     // 거리 기준 정렬
            FirstOrDefault(); // 가장 가까운 타겟 반환

        return closestTarget;
    }

    /// <summary>
    /// 필드 타겟 모두 제거
    /// </summary>
    public static void ClearAllFieldTarget()
    {
        foreach(IDamagable target in _fieldTargetList)
        {
            if (target is HPComponent hpComponent)
            {
                Enemy enemy = hpComponent.GetComponent<Enemy>();
                enemy.ReturnPool(); // 필드 적들을 풀로 돌려보냄
            }
        }

        _fieldTargetList.Clear(); // 리스트 비우기
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
