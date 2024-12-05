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
    public static event Action<EnemyEventArgs> OnEnemySpawn;    // ������ �̺�Ʈ
    public static event Action<EnemyEventArgs> OnEnemyDie;      // �׾����� �̺�Ʈ

    private ObjectPool<Enemy> _pool;                            // �ڽ��� ��ȯ�� Ǯ ����

    private HPComponent _hpComponent;                           // HP ������Ʈ
    private HPBar _hpBar;                                       // HP ��
    private AttackComponentCollision _attackComponentCollision; // ���� ������Ʈ ������Ÿ�� �浹Ÿ��
    private AnimComponent _animComponent;                       // �ִ� ������Ʈ
    private MoveComponent _moveComponent;                       // ���� ������Ʈ

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(ObjectPool<Enemy> pool, EnemyData enemyData, int statPercentage)
    {
        _pool = pool;

        // ���� ����
        int maxHp = (enemyData.MaxHp * statPercentage) / 100;
        int attackPower = (enemyData.AttackPower * statPercentage) / 100;
        int attackSpeed = enemyData.AttackSpeed;
        int moveSpeed = enemyData.MoveSpeed;

        Init_HPComponent(maxHp);
        Init_HPBar(maxHp);
        Init_AttackComponentCollision(attackPower, attackSpeed);
        Init_AnimComponent();
        Init_MoveComponent(moveSpeed);

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
    /// �浹��� ���� ������Ʈ  �ʱ�ȭ
    /// </summary>
    private void Init_AttackComponentCollision(int attackPower, int attackSpeed)
    {
        _attackComponentCollision = GetComponent<AttackComponentCollision>();
        _attackComponentCollision.Init(attackPower, attackSpeed);
    }

    /// <summary>
    /// Anim ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Init_AnimComponent()
    {
        _animComponent = GetComponent<AnimComponent>();
        _animComponent.Init();
    }

    /// <summary>
    /// ���� ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Init_MoveComponent(int moveSpeed)
    {
        _moveComponent = GetComponent<MoveComponent>();
        _moveComponent.Init(moveSpeed);
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

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        if (_hpComponent != null)
            _hpComponent.OnDead -= EnemyDeadTask;
    }
}
