using System.Collections;
using System.Collections.Generic;
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

    private List<Enemy> _fieldEnemyList = new List<Enemy>();    // 필드에 스폰되어 있는 에너미 리스트

    /// <summary>
    /// 에너미 이벤트 구독
    /// </summary>
    public void SubscribeToEnemy(Enemy enemy)
    {
        // 적이 스폰될 때 리스트에 추가
        enemy.OnEnemySpawned += AddFieldEnemyList;

        // 적이 죽을 때 리스트에서 제거 + 구독 해제
        enemy.OnEnemyDied += OnEnemyDiedHandler;
    }

    /// <summary>
    /// 적이 죽을때 이벤트가 실행될때 처리할 것들
    /// </summary>
    private void OnEnemyDiedHandler(Enemy enemy)
    {
        // 적 필드 리스트에서 제거
        RemoveFieldEnemyList(enemy);

        // 모든 구독 해제
        enemy.OnEnemySpawned -= AddFieldEnemyList;
        enemy.OnEnemyDied -= OnEnemyDiedHandler;
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
}
