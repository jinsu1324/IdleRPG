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

    private List<Enemy> _fieldEnemyList = new List<Enemy>();    // �ʵ忡 �����Ǿ� �ִ� ���ʹ� ����Ʈ

    /// <summary>
    /// ���ʹ� �̺�Ʈ ����
    /// </summary>
    public void SubscribeToEnemy(Enemy enemy)
    {
        // ���� ������ �� ����Ʈ�� �߰�
        enemy.OnEnemySpawn += AddFieldEnemyList;

        // ���� ���� �� ����Ʈ���� ���� + ���� ����
        enemy.OnEnemyDie += OnEnemyDiedHandler;
    }

    /// <summary>
    /// ���� ������ �̺�Ʈ�� ����ɶ� ó���� �͵�
    /// </summary>
    private void OnEnemyDiedHandler(Enemy enemy)
    {
        // �� �ʵ� ����Ʈ���� ����
        RemoveFieldEnemyList(enemy);

        // ��� ���� ����
        enemy.OnEnemySpawn -= AddFieldEnemyList;
        enemy.OnEnemyDie -= OnEnemyDiedHandler;
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
    public Enemy Get_ClosestEnemy(Vector3 pos)
    {
        Enemy closestEnemy = 
            _fieldEnemyList.OrderBy(enemy => Vector3.Distance(pos, enemy.transform.position)).FirstOrDefault();
        return closestEnemy;
    }
}
