using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 업그레이드
/// </summary>
[System.Serializable]
public class Upgrade
{
    public static event Action<Upgrade> OnUpgradeChanged;                    // 업그레이드 변경되었을 때 이벤트

    public string UpgradeStatType;      // 어떤 스탯을 업그레이드할지 스탯타입
    public string Name;                 // 이름 
    public int Level;                   // 레벨
    public float Value;                 // 실제 업그레이드 값
    public float ValueIncrease;         // 레벨 오르면 증가하는 값
    public int Cost;                    // 업그레이드 비용
    public int CostIncrease;            // 레벨 오르면 증가하는 업그레이드 비용

    /// <summary>
    /// 초기화
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
    /// 레벨업
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
    /// 이벤트 노티
    /// </summary>
    public void Notify_OnUpgradeChanged()
    {
        OnUpgradeChanged?.Invoke(this);
    }

    /// <summary>
    /// PlayerStats에 값 업데이트
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
        PlayerStats.UpdateStatModifier(args); // 플레이어 스탯에 적용
    }

    /// <summary>
    /// 업그레이드 관련 퀘스트 업데이트
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
