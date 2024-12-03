using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : SerializedMonoBehaviour
{
    [SerializeField] public StatID StatID { get; set; } // 스탯 ID
    public string Name { get; private set; }            // 이름 
    public int Level { get; private set; }              // 레벨
    public int Value { get; private set; }              // 실제 스탯 값
    public int ValueIncrease { get; private set; }      // 레벨 오르면 증가하는 값
    public int Cost { get; private set; }               // 업그레이드 비용
    public int CostIncrease { get; private set; }       // 레벨 오르면 증가하는 업그레이드 비용


    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(string name, int level, int value, int valueIncrease, int cost, int costIncrease)
    {
        Name = name;
        Level = level;
        Value = value;
        ValueIncrease = valueIncrease;
        Cost = cost;
        CostIncrease = costIncrease;
    }

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
