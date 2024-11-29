using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Move,
    Attack,
}

public class Enemy : SerializedMonoBehaviour
{
    public event Action<Enemy> OnEnemySpawn;            // ������ �̺�Ʈ
    public event Action<Enemy> OnEnemyDie;              // �׾����� �̺�Ʈ

    private ObjectPool<Enemy> _pool;                    // �ڽ��� ��ȯ�� Ǯ ����
    private EnemyData _enemyData;                       // ������

    private int _currentHp;                             // ü��
    private int _maxHp;                                 // �ִ�ü��
    private int _attackPower;                           // ���ݷ�
    private int _attackSpeed;                           // ���ݼӵ�
    private int _moveSpeed;                             // �̵��ӵ�

    private float _attackCooldown;                      // ���� ��Ÿ��
    private float _time;                                // ��Ÿ�� �ð� ����
    private Player _targetPlayer = null;                // ���� Ÿ�� �÷��̾�
    private EnemyState _currentState = EnemyState.Move; // ���� ����
    private bool _isFirstAttack = true;                 // ù 1ȸ��������

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Initialize(ObjectPool<Enemy> pool, EnemyData enemyData, int statPercentage)
    {
        _pool = pool;
        _enemyData = enemyData;

        // ���� ����
        _currentHp = (_enemyData.MaxHp * statPercentage) / 100;
        _maxHp = (_enemyData.MaxHp * statPercentage) / 100;
        _attackPower = (_enemyData.AttackPower * statPercentage) / 100;
        _attackSpeed = _enemyData.AttackSpeed;
        _moveSpeed = _enemyData.MoveSpeed;
        _attackCooldown = 1f / _attackSpeed;

        // ���� �̺�Ʈ ȣ��
        OnEnemySpawn?.Invoke(this);
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Move:
                HandleMoveState();
                break;
            case EnemyState.Attack:
                HandleAttackState(); 
                break;
        }
    }

    /// <summary>
    /// ������ ���� ���� ó����
    /// </summary>
    private void HandleMoveState()
    {
        MoveLeft();
    }

    /// <summary>
    /// �������� �̵�
    /// </summary>
    private void MoveLeft()
    {
        transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// ���ݻ��� ���� ó����
    /// </summary>
    private void HandleAttackState()
    {
        if (_isFirstAttack)
        {
            AttackPlayer();
            _isFirstAttack = false; 
        }
        else if (IsAttackCoolTime())
        {
            AttackPlayer();
        }
    }

    /// <summary>
    /// ���� ��Ÿ�� ���
    /// </summary>
    private bool IsAttackCoolTime()
    {
        _time += Time.deltaTime;

        if (_time >= _attackCooldown)
        {
            _time %= _attackCooldown;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    private void AttackPlayer()
    {
        if (_targetPlayer != null)
            _targetPlayer.TakeDamage(_attackPower);
    }

    /// <summary>
    /// �÷��̾� �����ؼ� Ÿ�� �����ϰ� ���ݻ��·� ����
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _targetPlayer = collision.gameObject.GetComponent<Player>();
            _currentState = EnemyState.Attack;
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void TakeDamage(int atk)
    {
        _currentHp -= atk;

        if (_currentHp <= 0)
            Die();
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Die()
    {
        OnEnemyDie?.Invoke(this);  // ��� �̺�Ʈ ȣ��
        _pool.ReturnObject(this); // Ǯ�� ��ȯ
    }

}
