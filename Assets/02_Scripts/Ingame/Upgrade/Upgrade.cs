using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeID
{
    AttackPower,
    AttackSpeed,
    MaxHp,
    CriticalRate,
    CriticalMultiple
}

[System.Serializable]
public class Upgrade : BaseData
{
    public string Name;            // �̸� 
    public int Level;              // ����
    public float Value;            // ���� ���׷��̵� ��
    public float ValueIncrease;    // ���� ������ �����ϴ� ��
    public int Cost;               // ���׷��̵� ���
    public int CostIncrease;       // ���� ������ �����ϴ� ���׷��̵� ���

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(string id, string name, int level, float value, float valueIncrease, int cost, int costIncrease)
    {
        ID = id;
        Name = name;
        Level = level;
        Value = value;
        ValueIncrease = valueIncrease;
        Cost = cost;
        CostIncrease = costIncrease;

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

        UpdatePlayerStats();

        UpgradeQuestUpdate();   // ���׷��̵� ���� ����Ʈ ������Ʈ
    }

    /// <summary>
    /// PlayerStats�� �� ������Ʈ
    /// </summary>
    private void UpdatePlayerStats()
    {
        StatType statType = (StatType)Enum.Parse(typeof(StatType), ID);

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
        switch ((UpgradeID)Enum.Parse(typeof(UpgradeID), ID))
        {
            case UpgradeID.AttackPower:
                QuestManager.Instance.UpdateQuestProgress(QuestType.UpgradeAttackPower, 1);
                break;
            case UpgradeID.AttackSpeed:
                QuestManager.Instance.UpdateQuestProgress(QuestType.UpgradeAttackSpeed, 1);
                break;
            case UpgradeID.MaxHp:
                QuestManager.Instance.UpdateQuestProgress(QuestType.UpgradeMaxHp, 1);
                break;
            case UpgradeID.CriticalRate:
                QuestManager.Instance.UpdateQuestProgress(QuestType.UpgradeCriticalRate, 1);
                break;
            case UpgradeID.CriticalMultiple:
                QuestManager.Instance.UpdateQuestProgress(QuestType.UpgradeCriticalMultiple, 1);
                break;
        }
    }
}
