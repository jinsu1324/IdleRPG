using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatID
{
    Attack,
    AttackDelay,
    MaxHp,
    Critical
}

[System.Serializable]
public class StatData : BaseData
{
    public string Name;
    public int Level;
    public int Value;
    public int ValueIncrease;
    public int Cost;
    public int CostIncrease;

    public void LevelUp()
    {
        Level++;
        Value += ValueIncrease;
        Cost += CostIncrease;
    }
}
