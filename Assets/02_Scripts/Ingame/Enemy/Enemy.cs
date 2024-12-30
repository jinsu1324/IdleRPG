using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum EnemyState
{
    Move,
    Attack,
}

public class Enemy : SerializedMonoBehaviour
{
    private ObjectPool<Enemy> _pool;                            // �ڽ��� ��ȯ�� Ǯ ����
    private EnemyID _enemyID;                                   // ID
    private HPComponent _hpComponent;                           // HP ������Ʈ
    private HPBar _hpBar;                                       // HP ��
    private AttackComponentCollision _attackComponentCollision; // ���� ������Ʈ ������Ÿ�� �浹Ÿ��
    private AnimComponent _animComponent;                       // �ִ� ������Ʈ
    private MoveComponent _moveComponent;                       // ���� ������Ʈ
    private BlinkOnHit _blinkOnHit;                             // ������ �޾��� �� ��������Ʈ �������ִ� ������Ʈ

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(ObjectPool<Enemy> pool, EnemyData enemyData, int statPercentage)
    {
        _pool = pool;
        _enemyID = (EnemyID)Enum.Parse(typeof(EnemyID), enemyData.ID);

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
        Init_BlinkOnHit();

        FieldTargetManager.AddFieldEnemyList(_hpComponent); // �ʵ�Ÿ�� ����Ʈ�� �߰�
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
    /// �浹��� ���� ������Ʈ �ʱ�ȭ
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
    /// BlinkOnHit ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Init_BlinkOnHit()
    {
        _blinkOnHit = GetComponent<BlinkOnHit>();
        _blinkOnHit.Init();
    }

    /// <summary>
    /// �׾��� ��, ���ʹ̿��� ó���ؾ��� �͵� ó��
    /// </summary>
    private void EnemyDeadTask()
    {
        StageManager.Instance.AddKillCount();   // ų ī��Ʈ ����
        EnemyDropGoldManager.AddGoldByEnemy(_enemyID); // ��� �߰�
        FieldTargetManager.RemoveFieldEnemyList(_hpComponent); // �ʵ�Ÿ�� ����Ʈ���� ����



        QuestManager.Instance.UpdateQuestProgress(QuestType.KillEnemy, 1);

        ReturnPool();   // Ǯ�� ����������
    }

    /// <summary>
    /// Ǯ�� ��ȯ
    /// </summary>
    public void ReturnPool()
    {
        _pool.ReturnObject(this);
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
