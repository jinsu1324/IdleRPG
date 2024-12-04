using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : SingletonBase<EnemyManager>
{
    private List<Enemy> _fieldEnemyList = new List<Enemy>();    // 필드에 스폰되어 있는 에너미 리스트

    /// <summary>
    /// 에너미 이벤트 구독
    /// </summary>
    public void Subscribe_EnemyEvents(Enemy enemy)
    {
        enemy.OnEnemySpawn += AddFieldEnemyList;    
        enemy.OnEnemyDie += OnEnemyDieHandler;      
    }

    /// <summary>
    /// 적이 죽을때 이벤트가 실행될때 처리할 것들
    /// </summary>
    private void OnEnemyDieHandler(Enemy enemy)
    {
        // 적 필드 리스트에서 제거
        RemoveFieldEnemyList(enemy);

        // 모든 구독 해제
        enemy.OnEnemySpawn -= AddFieldEnemyList;
        enemy.OnEnemyDie -= OnEnemyDieHandler;
    }

    /// <summary>
    /// 필드 에너미 리스트에 에너미 추가
    /// </summary>
    public void AddFieldEnemyList(Enemy enemy)
    {
        _fieldEnemyList.Add(enemy);
    }

    /// <summary>
    /// 필드 에너미 리스트에서 에너미 삭제
    /// </summary>
    public void RemoveFieldEnemyList(Enemy enemy)
    {
        _fieldEnemyList.Remove(enemy);
    }

    /// <summary>
    /// 특정위치에서 가장 가까운 적 반환
    /// </summary>
    public Enemy GetClosestEnemy(Vector3 pos)
    {
        Enemy closestEnemy = 
            _fieldEnemyList.OrderBy(enemy => Vector3.Distance(pos, enemy.transform.position)).FirstOrDefault();
        return closestEnemy;
    }

    /// <summary>
    /// 가장 가까운 살아있는 적 찾기
    /// </summary>
    public Enemy GetClosestLivingEnemy(Vector3 pos) 
    {
        // 살아있는 적들만 필터링 후 거리 기준으로 정렬
        Enemy closestEnemy = _fieldEnemyList.
            Where(enemy => enemy != null && !enemy.IsDead). // 살아있는 적만 필터링
            OrderBy(enemy => Vector3.Distance(pos, enemy.transform.position)).     // 거리 기준 정렬
            FirstOrDefault(); // 가장 가까운 적 반환

        return closestEnemy;
    }
}
