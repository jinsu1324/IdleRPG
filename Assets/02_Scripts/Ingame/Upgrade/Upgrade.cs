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
    public string Name;            // 이름 
    public int Level;              // 레벨
    public float Value;            // 실제 업그레이드 값
    public float ValueIncrease;    // 레벨 오르면 증가하는 값
    public int Cost;               // 업그레이드 비용
    public int CostIncrease;       // 레벨 오르면 증가하는 업그레이드 비용

    /// <summary>
    /// 초기화
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
    /// 레벨업
    /// </summary>
    public void LevelUp()
    {
        Level++;
        Value += ValueIncrease;
        Cost += CostIncrease;

        UpdatePlayerStats();

        UpgradeQuestUpdate();   // 업그레이드 관련 퀘스트 업데이트
    }

    /// <summary>
    /// PlayerStats에 값 업데이트
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
        PlayerStats.UpdateStatModifier(args); // 플레이어 스탯에 적용
    }

    /// <summary>
    /// 업그레이드 관련 퀘스트 업데이트
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
