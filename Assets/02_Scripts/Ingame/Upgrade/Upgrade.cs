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
            Source = this
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
                QuestManager.Instance.UpdateQuestProgress(QuestType.UpgradeAttackPower, 1);
                break;
            case StatType.AttackSpeed:
                QuestManager.Instance.UpdateQuestProgress(QuestType.UpgradeAttackSpeed, 1);
                break;
            case StatType.MaxHp:
                QuestManager.Instance.UpdateQuestProgress(QuestType.UpgradeMaxHp, 1);
                break;
            case StatType.CriticalRate:
                QuestManager.Instance.UpdateQuestProgress(QuestType.UpgradeCriticalRate, 1);
                break;
            case StatType.CriticalMultiple:
                QuestManager.Instance.UpdateQuestProgress(QuestType.UpgradeCriticalMultiple, 1);
                break;
        }
    }
}
