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
    private HPComponent _hpComponent;                   // HP ������Ʈ
    private HPBar _hpBar;                               // HP ��


    public bool IsDead { get; private set; }            // ���� �׾�����

    public static event Action<EnemyEventArgs> OnEnemySpawn;            // ������ �̺�Ʈ
    public static event Action<EnemyEventArgs> OnEnemyDie;              // �׾����� �̺�Ʈ

    private ObjectPool<Enemy> _pool;                    // �ڽ��� ��ȯ�� Ǯ ����
    private EnemyData _enemyData;                       // ������

    private int _currentHp;                             // ü��
    private int _maxHp;                                 // �ִ�ü��
    private int _attackPower;                           // ���ݷ�
    private int _attackSpeed;                           // ���ݼӵ�
    private int _moveSpeed;                             // �̵��ӵ�

    private float _attackCooldown;                      // ���� ��Ÿ��
    private float _time;                                // ��Ÿ�� �ð� ����
    private Player _targetPlayer;                       // ���� Ÿ�� �÷��̾�
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
        _currentHp = (_enemyData.MaxHp * statPercentage) / 100;
        _maxHp = (_enemyData.MaxHp * statPercentage) / 100;
        _attackPower = (_enemyData.AttackPower * statPercentage) / 100;
        _attackSpeed = _enemyData.AttackSpeed;
        _moveSpeed = _enemyData.MoveSpeed;
        _attackCooldown = 1f / _attackSpeed;

        // ������ �ʱ�ȭ
        IsDead = false;
        _time = 0f;
        _targetPlayer = null;
        _currentState = EnemyState.Move;
        _isFirstAttack = true;


        _hpBar = GetComponentInChildren<HPBar>();
        _hpBar.Init(_maxHp);

        _hpComponent = GetComponent<HPComponent>();
        _hpComponent.Init(_maxHp);
        _hpComponent.OnTakeDamaged += CheckDead; // ������ �޾��� ��, �׾����� üũ

        EnemyEventArgs args = new EnemyEventArgs() { Enemy = this };
        OnEnemySpawn?.Invoke(args); // ���� �̺�Ʈ ȣ��
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
        if (_targetPlayer != null)
        {
            _targetPlayer.GetComponent<HPComponent>().TakeDamage(_attackPower);
        }
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
    /// 
    /// </summary>
    public void CheckDead(OnTakeDamagedArgs args)
    {
        if (args.CurrentHp <= 0)
            Die();
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Die()
    {
        
        IsDead = true; // �׾����� true��

        GoldManager.Instance.AddCurrency(1000); // �÷��̾��� ��� �߰� // Todo : �� ������ �����ͷ� ����

        EnemyEventArgs args = new EnemyEventArgs() { Enemy = this, Count = 1 };
        OnEnemyDie?.Invoke(args); // ��� �̺�Ʈ ȣ��  

        _pool.ReturnObject(this); // Ǯ�� ��ȯ
    }

    /// <summary>
    /// ���� HP ��������
    /// </summary>
    public int GetCurrentHP()
    {
        return _currentHp;
    }

}
