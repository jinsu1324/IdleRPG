using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// ���ʹ� �ھ�
/// </summary>
public class EnemyCore : ObjectPoolObj
{
    private EnemyID _enemyID;                                               // ID
    [SerializeField] private HPComponent _hpComponent;                      // HP ������Ʈ
    [SerializeField] private AttackComponent_ColliderType _attackComponent; // ���� ������Ʈ (�浹 Ÿ��)
    [SerializeField] private MoveComponent moveComponent;                   // ���� ������Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _hpComponent.OnTakeDamage += EnemyTakeDamageTask; // ������ �޾����� -> ���ʹ̿����� ó���Ұ͵� ó��
        _hpComponent.OnDie += EnemyDieTask; // �׾����� -> ���ʹ� ������ �ʿ��� �½�ũ�� ó��
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _hpComponent.OnTakeDamage -= EnemyTakeDamageTask;
        _hpComponent.OnDie -= EnemyDieTask;

    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(EnemyData enemyData, float statPercentage)
    {
        _enemyID = (EnemyID)Enum.Parse(typeof(EnemyID), enemyData.ID);

        // ���� �� ���
        float maxHp = enemyData.MaxHp * statPercentage;
        float attackPower = enemyData.AttackPower * statPercentage;
        float attackSpeed = enemyData.AttackSpeed;
        float moveSpeed = enemyData.MoveSpeed;

        // HP ������Ʈ �ʱ�ȭ
        HPInitArgs hpInitArgs = new HPInitArgs() 
        { 
            MaxHp = maxHp
        };
        _hpComponent.Init(hpInitArgs);
        
        // ���� ������Ʈ �ʱ�ȭ
        AttackInitArgs attackInitArgs = new AttackInitArgs()
        {
            AttackPower = attackPower,
            AttackSpeed = attackSpeed,
        };
        _attackComponent.Init(attackInitArgs);

        // ���� ������Ʈ �ʱ�ȭ
        moveComponent.Init(moveSpeed);

        // �ʵ�Ÿ�� ����Ʈ�� �߰�
        FieldTargetManager.AddFieldEnemyList(GetComponent<HPComponent>()); 
    }

    /// <summary>
    /// ���ʹ̰� ������ �޾����� ó���� �͵�
    /// </summary>
    private void EnemyTakeDamageTask(TakeDamageArgs args)
    {
        DamageTextManager.Instance.ShowDamageText(args.Damage, transform.position, args.IsCritical); // ������ �ؽ�Ʈ ����
        FXManager.Instance.SpawnFX(FXName.FX_Enemy_Damaged, transform); // ����Ʈ ����
    }

    /// <summary>
    /// ���ʹ̰� �׾����� ó���� �͵�
    /// </summary>
    private void EnemyDieTask()
    {
        StageManager.Instance.AddKillCount();   // ų ī��Ʈ ����
        EnemyDataManager.AddGoldByEnemy(_enemyID); // ��� �߰�
        FieldTargetManager.RemoveFieldEnemyList(GetComponent<HPComponent>()); // �ʵ�Ÿ�� ����Ʈ���� ����
        QuestManager.AddValue_KillEnemyQuest(QuestType.KillEnemy, 1); // �� ���̱� ����Ʈ�� ������Ʈ

        CurrencyIconMover.Instance.MoveCurrency_Multi(CurrencyType.Gold, transform.position); // ��� �̵� �ִϸ��̼�

        FXManager.Instance.SpawnFX(FXName.FX_Enemy_Die, transform);    // ����Ʈ ����

        ReturnPool(); // Ǯ�� ����������
    }

    /// <summary>
    /// Ǯ�� ��ȯ
    /// </summary>
    public void ReturnPool() => BackTrans();
}
