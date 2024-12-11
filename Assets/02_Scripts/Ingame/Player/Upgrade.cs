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
    public int Value;              // 실제 업그레이드 값
    public int ValueIncrease;      // 레벨 오르면 증가하는 값
    public int Cost;               // 업그레이드 비용
    public int CostIncrease;       // 레벨 오르면 증가하는 업그레이드 비용

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(string id, string name, int level, int value, int valueIncrease, int cost, int costIncrease)
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
    }

    /// <summary>
    /// PlayerStats에 값 업데이트
    /// </summary>
    private void UpdatePlayerStats()
    {
        StatType statType = (StatType)Enum.Parse(typeof(StatType), ID);
        PlayerStats.Instance.UpdateModifier(statType, Value, this); // 업그레이드 소스를 고유하게 설정



        PlayerStats.Instance.AllStatUIUpdate();
    }
}
