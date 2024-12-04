using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public struct EnemyEventArgs
{
    public Enemy Enemy;
    public int Count;
}

public enum EnemyState
{
    Move,
    Attack,
}

public class Enemy : SerializedMonoBehaviour
{
    public static event Action<EnemyEventArgs> OnEnemySpawn; // ������ �̺�Ʈ
    public static event Action<EnemyEventArgs> OnEnemyDie;   // �׾����� �̺�Ʈ

    private HPComponent _hpComponent;                   // HP ������Ʈ
    private HPBar _hpBar;                               // HP ��


    private ObjectPool<Enemy> _pool;                    // �ڽ��� ��ȯ�� Ǯ ����
    private EnemyData _enemyData;                       // ������

    private int _attackPower;                           // ���ݷ�
    private int _attackSpeed;                           // ���ݼӵ�
    private int _moveSpeed;                             // �̵��ӵ�

    private float _attackCooldown;                      // ���� ��Ÿ��
    private float _time;                                // ��Ÿ�� �ð� ����
    private IDamagable _target;                         // ���� Ÿ��
    private EnemyState _currentState;                   // ���� ����
    private bool _isFirstAttack;                        // ù 1ȸ��������

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(ObjectPool<Enemy> pool, EnemyData enemyData, int statPercentage)
    {
        _pool = pool;
        _enemyData = enemyData;

        // ���� ����
        int maxHp = (_enemyData.MaxHp * statPercentage) / 100;
        _attackPower = (_enemyData.AttackPower * statPercentage) / 100;
        _attackSpeed = _enemyData.AttackSpeed;
        _moveSpeed = _enemyData.MoveSpeed;
        _attackCooldown = 1f / _attackSpeed;

        // ������ �ʱ�ȭ
        _time = 0f;
        _target = null;
        _currentState = EnemyState.Move;
        _isFirstAttack = true;


        Init_HPComponent(maxHp);
        Init_HPBar(maxHp);

        EnemyEventArgs args = new EnemyEventArgs() { Enemy = this };
        OnEnemySpawn?.Invoke(args); // ���� �̺�Ʈ ȣ��
    }

    /// <summary>
    /// HP ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Init_HPComponent(int maxHp)
    {
        _hpComponent = GetComponent<HPComponent>();
        _hpComponent.Init(maxHp);
        _hpComponent.OnDead += EnemyDeadTask; // �׾��� ��, ���ʹ̿��� ó���ؾ��� �͵� ó��
    }

    /// <summary>
    /// HP�� ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Init_HPBar(int maxHp)
    {
        _hpBar = GetComponentInChildren<HPBar>();
        _hpBar.Init(maxHp);
    }



    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Move:
                MoveStateHandler();
                break;
            case EnemyState.Attack:
                AttackStateHandler(); 
                break;
        }
    }

    /// <summary>
    /// ������ ���� ���� ó����
    /// </summary>
    private void MoveStateHandler()
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
    private void AttackStateHandler()
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
        if (_target != null)
        {
            _target.TakeDamage(_attackPower);
        }
    }

    /// <summary>
    /// �÷��̾� �����ؼ� Ÿ�� �����ϰ� ���ݻ��·� ����
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _target = collision.gameObject.GetComponent<IDamagable>();
            _currentState = EnemyState.Attack;
        }
    }


    /// <summary>
    /// �׾��� ��, ���ʹ̿��� ó���ؾ��� �͵� ó��
    /// </summary>
    private void EnemyDeadTask()
    {
        GoldManager.Instance.AddCurrency(1000); // �÷��̾��� ��� �߰� // Todo : �� ������ �����ͷ� ����

        EnemyEventArgs args = new EnemyEventArgs() { Enemy = this, Count = 1 };
        OnEnemyDie?.Invoke(args); // ��� �̺�Ʈ ȣ��  

        _pool.ReturnObject(this); // Ǯ�� ��ȯ
    }

    private void OnDisable()
    {
        if (_hpComponent != null)
            _hpComponent.OnDead -= EnemyDeadTask;
    }
}
