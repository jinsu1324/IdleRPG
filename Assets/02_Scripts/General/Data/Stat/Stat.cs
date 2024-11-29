using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatID
{
    AttackPower,
    AttackDelay,
    MaxHp,
    Critical
}

[System.Serializable]
public class Stat : BaseData
{
    public string Name;          // 스탯 이름
    public int Level;            // 레벨
    public int Value;            // 실제 스탯 값
    public int ValueIncrease;    // 레벨 오르면 증가하는 값
    public int Cost;             // 업그레이드 비용
    public int CostIncrease;     // 레벨 오르면 증가하는 업그레이드 비용

    /// <summary>
    /// 레벨업
    /// </summary>
    public void LevelUp()
    {
        Level++;
        Value += ValueIncrease;
        Cost += CostIncrease;
    }
}
