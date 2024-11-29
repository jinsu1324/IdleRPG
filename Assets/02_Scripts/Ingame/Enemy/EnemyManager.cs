using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public event Action OnEnemyClear;                           // 적을 다 죽였을 때 이벤트

    private List<Enemy> _fieldEnemyList = new List<Enemy>();    // 필드에 스폰되어 있는 에너미 리스트
    private int _targetCount;                                   // 죽여야 하는 목표 적 숫자
    private int _killCount;                                     // 죽인 적 숫자

    /// <summary>
    /// 목표 + 죽인 적 숫자 리셋
    /// </summary>
    public void ResetCounts(int targetCount)
    {
        _targetCount = targetCount;
        _killCount = 0;
    }

    /// <summary>
    /// 킬 카운트 1 증가
    /// </summary>
    private void Plus_KillCount()
    {
        Debug.Log("Plus_KillCount");

        _killCount++;

        // 적 다 죽였으면 이벤트 호출
        if (_killCount >= _targetCount)
        {
            OnEnemyClear?.Invoke();
        }
    }

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

        // 킬 카운트 증가
        Plus_KillCount();               

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
            Where(enemy => enemy != null && !enemy.IsDead && !enemy.IsGoingToDie). // 살아있는 적만 필터링 (죽을 예정인적도 제외)
            OrderBy(enemy => Vector3.Distance(pos, enemy.transform.position)).     // 거리 기준 정렬
            FirstOrDefault(); // 가장 가까운 적 반환

        return closestEnemy;
    }
}
