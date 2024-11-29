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

    private List<Enemy> _fieldEnemyList = new List<Enemy>();    // �ʵ忡 �����Ǿ� �ִ� ���ʹ� ����Ʈ

    /// <summary>
    /// ���ʹ� �̺�Ʈ ����
    /// </summary>
    public void SubscribeToEnemy(Enemy enemy)
    {
        // ���� ������ �� ����Ʈ�� �߰�
        enemy.OnEnemySpawned += AddFieldEnemyList;

        // ���� ���� �� ����Ʈ���� ���� + ���� ����
        enemy.OnEnemyDied += OnEnemyDiedHandler;
    }

    /// <summary>
    /// ���� ������ �̺�Ʈ�� ����ɶ� ó���� �͵�
    /// </summary>
    private void OnEnemyDiedHandler(Enemy enemy)
    {
        // �� �ʵ� ����Ʈ���� ����
        RemoveFieldEnemyList(enemy);

        // ��� ���� ����
        enemy.OnEnemySpawned -= AddFieldEnemyList;
        enemy.OnEnemyDied -= OnEnemyDiedHandler;
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
}
