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

    public event Action OnEnemyClear;                           // ���� �� �׿��� �� �̺�Ʈ

    private List<Enemy> _fieldEnemyList = new List<Enemy>();    // �ʵ忡 �����Ǿ� �ִ� ���ʹ� ����Ʈ
    private int _targetCount;                                   // �׿��� �ϴ� ��ǥ �� ����
    private int _killCount;                                     // ���� �� ����

    /// <summary>
    /// ��ǥ + ���� �� ���� ����
    /// </summary>
    public void ResetCounts(int targetCount)
    {
        _targetCount = targetCount;
        _killCount = 0;
    }

    /// <summary>
    /// ų ī��Ʈ 1 ����
    /// </summary>
    private void Plus_KillCount()
    {
        Debug.Log("Plus_KillCount");

        _killCount++;

        // �� �� �׿����� �̺�Ʈ ȣ��
        if (_killCount >= _targetCount)
        {
            OnEnemyClear?.Invoke();
        }
    }

    /// <summary>
    /// ���ʹ� �̺�Ʈ ����
    /// </summary>
    public void Subscribe_EnemyEvents(Enemy enemy)
    {
        enemy.OnEnemySpawn += AddFieldEnemyList;    
        enemy.OnEnemyDie += OnEnemyDieHandler;      
    }

    /// <summary>
    /// ���� ������ �̺�Ʈ�� ����ɶ� ó���� �͵�
    /// </summary>
    private void OnEnemyDieHandler(Enemy enemy)
    {
        // �� �ʵ� ����Ʈ���� ����
        RemoveFieldEnemyList(enemy);

        // ų ī��Ʈ ����
        Plus_KillCount();               

        // ��� ���� ����
        enemy.OnEnemySpawn -= AddFieldEnemyList;
        enemy.OnEnemyDie -= OnEnemyDieHandler;
    }

    /// <summary>
    /// �ʵ� ���ʹ� ����Ʈ�� ���ʹ� �߰�
    /// </summary>
    public void AddFieldEnemyList(Enemy enemy)
    {
        _fieldEnemyList.Add(enemy);
    }

    /// <summary>
    /// �ʵ� ���ʹ� ����Ʈ���� ���ʹ� ����
    /// </summary>
    public void RemoveFieldEnemyList(Enemy enemy)
    {
        _fieldEnemyList.Remove(enemy);
    }

    /// <summary>
    /// Ư����ġ���� ���� ����� �� ��ȯ
    /// </summary>
    public Enemy GetClosestEnemy(Vector3 pos)
    {
        Enemy closestEnemy = 
            _fieldEnemyList.OrderBy(enemy => Vector3.Distance(pos, enemy.transform.position)).FirstOrDefault();
        return closestEnemy;
    }

    /// <summary>
    /// ���� ����� ����ִ� �� ã��
    /// </summary>
    public Enemy GetClosestLivingEnemy(Vector3 pos) 
    {
        // ����ִ� ���鸸 ���͸� �� �Ÿ� �������� ����
        Enemy closestEnemy = _fieldEnemyList.
            Where(enemy => enemy != null && !enemy.IsDead && !enemy.IsGoingToDie). // ����ִ� ���� ���͸� (���� ���������� ����)
            OrderBy(enemy => Vector3.Distance(pos, enemy.transform.position)).     // �Ÿ� ���� ����
            FirstOrDefault(); // ���� ����� �� ��ȯ

        return closestEnemy;
    }
}
