using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ObjectPoolObj
{
    private EnemyID _enemyID;   // ID

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GetComponent<HPComponent_Enemy>().OnDeadEnemy += EnemyDeadTask; // ���ʹ� �׾�����, ���ʹ� ������ �ʿ��� �½�ũ�� ó��
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        GetComponent<HPComponent_Enemy>().OnDeadEnemy -= EnemyDeadTask;
    }
    
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(EnemyData enemyData, int statPercentage)
    {
        _enemyID = (EnemyID)Enum.Parse(typeof(EnemyID), enemyData.ID);

        // ���� ����
        int maxHp = (enemyData.MaxHp * statPercentage) / 100;
        int attackPower = (enemyData.AttackPower * statPercentage) / 100;
        int attackSpeed = enemyData.AttackSpeed;
        int moveSpeed = enemyData.MoveSpeed;

        GetComponent<HPComponent_Enemy>().Init(maxHp); // HP ������Ʈ �ʱⰪ ����
        GetComponent<AttackComponent_Enemy>().Init(attackPower, attackSpeed); // ���� ������Ʈ �ʱⰪ ����
        GetComponent<MoveComponent>().Init(moveSpeed); // ���� ������Ʈ �ʱⰪ ����

        FieldTargetManager.AddFieldEnemyList(GetComponent<HPComponent_Enemy>()); // �ʵ�Ÿ�� ����Ʈ�� �߰�
    }

    /// <summary>
    /// �׾��� ��, ���ʹ̿��� ó���ؾ��� �͵� ó��
    /// </summary>
    private void EnemyDeadTask()
    {
        StageManager.Instance.AddKillCount();   // ų ī��Ʈ ����
        EnemyDropGoldManager.AddGoldByEnemy(_enemyID); // ��� �߰�
        FieldTargetManager.RemoveFieldEnemyList(GetComponent<HPComponent_Enemy>()); // �ʵ�Ÿ�� ����Ʈ���� ����
        QuestManager.Instance.UpdateQuestProgress(QuestType.KillEnemy, 1); // �� ���̱� ����Ʈ�� ������Ʈ
        CurrencyIconMover.Instance.MoveCurrency(CurrencyType.Gold, transform.position); // ��� �̵� �ִϸ��̼�

        ReturnPool(); // Ǯ�� ����������
    }

    /// <summary>
    /// Ǯ�� ��ȯ
    /// </summary>
    public void ReturnPool()
    {
        BackTrans();
    }
}
