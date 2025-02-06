using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���׷��̵�
/// </summary>
[System.Serializable]
public class Upgrade
{
    public static event Action<Upgrade> OnUpgradeChanged;                    // ���׷��̵� ����Ǿ��� �� �̺�Ʈ

    public string UpgradeStatType;      // � ������ ���׷��̵����� ����Ÿ��
    public string Name;                 // �̸� 
    public int Level;                   // ����
    public float Value;                 // ���� ���׷��̵� ��
    public float ValueIncrease;         // ���� ������ �����ϴ� ��
    public int Cost;                    // ���׷��̵� ���
    public int CostIncrease;            // ���� ������ �����ϴ� ���׷��̵� ���

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(string statType, string name, int level, float value, float valueIncrease, int cost, int costIncrease)
    {
        UpgradeStatType = statType;
        Name = name;
        Level = level;
        Value = value;
        ValueIncrease = valueIncrease;
        Cost = cost;
        CostIncrease = costIncrease;

        Notify_OnUpgradeChanged();
        UpdatePlayerStats();
    }

    /// <summary>
    /// ������
    /// </summary>
    public void LevelUp()
    {
        Level++;
        Value += ValueIncrease;
        Cost += CostIncrease;

        Notify_OnUpgradeChanged();
        UpdatePlayerStats();

        UpgradeQuestUpdate();   
    }

    /// <summary>
    /// �̺�Ʈ ��Ƽ
    /// </summary>
    public void Notify_OnUpgradeChanged()
    {
        OnUpgradeChanged?.Invoke(this);
    }

    /// <summary>
    /// PlayerStats�� �� ������Ʈ
    /// </summary>
    public void UpdatePlayerStats()
    {
        StatType statType = (StatType)Enum.Parse(typeof(StatType), UpgradeStatType);

        Dictionary<StatType, float> dict = new Dictionary<StatType, float>();
        dict[statType] = Value;

        PlayerStatUpdateArgs args = new PlayerStatUpdateArgs()
        {
            DetailStatDict = dict,
            SourceID = UpgradeStatType
        };
        PlayerStats.UpdateStatModifier(args); // �÷��̾� ���ȿ� ����
    }

    /// <summary>
    /// ���׷��̵� ���� ����Ʈ ������Ʈ
    /// </summary>
    private void UpgradeQuestUpdate()
    {
        switch ((StatType)Enum.Parse(typeof(StatType), UpgradeStatType))
        {
            case StatType.AttackPower:
                QuestManager.UpdateQuestStack(QuestType.UpgradeAttackPower, Level);
                break;
            case StatType.AttackSpeed:
                QuestManager.UpdateQuestStack(QuestType.UpgradeAttackSpeed, Level);
                break;
            case StatType.MaxHp:
                QuestManager.UpdateQuestStack(QuestType.UpgradeMaxHp, Level);
                break;
            case StatType.CriticalRate:
                QuestManager.UpdateQuestStack(QuestType.UpgradeCriticalRate, Level);
                break;
            case StatType.CriticalMultiple:
                QuestManager.UpdateQuestStack(QuestType.UpgradeCriticalMultiple, Level);
                break;
        }
    }
}
