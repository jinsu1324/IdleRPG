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
    public int Value;              // ���� ���׷��̵� ��
    public int ValueIncrease;      // ���� ������ �����ϴ� ��
    public int Cost;               // ���׷��̵� ���
    public int CostIncrease;       // ���� ������ �����ϴ� ���׷��̵� ���

    /// <summary>
    /// �ʱ�ȭ
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
    /// ������
    /// </summary>
    public void LevelUp()
    {
        Level++;
        Value += ValueIncrease;
        Cost += CostIncrease;

        UpdatePlayerStats();
    }

    /// <summary>
    /// PlayerStats�� �� ������Ʈ
    /// </summary>
    private void UpdatePlayerStats()
    {
        StatType statType = (StatType)Enum.Parse(typeof(StatType), ID);
        PlayerStats.Instance.UpdateModifier(statType, Value, this); // ���׷��̵� �ҽ��� �����ϰ� ����



        PlayerStats.Instance.AllStatUIUpdate();
    }
}
