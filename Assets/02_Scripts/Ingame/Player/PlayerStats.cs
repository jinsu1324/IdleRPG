using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// �÷��̾� ���� ������Ʈ�� �ʿ��� �͵� ����ü
/// </summary>
public struct PlayerStatUpdateArgs
{
    public Dictionary<StatType, float> DetailStatDict;  // �Ӽ��� ��ųʸ�
    public string SourceID;                             // ��ó ID
}

/// <summary>
/// �÷��̾� ���� Args
/// </summary>
public struct PlayerStatArgs
{
    public float TotalPower;          // ���� ������
    public float BeforeTotalPower;    // ���� ���� ������
    public float AttackPower;         // ���ݷ�
    public float AttackSpeed;         // ���ݼӵ�
    public float MaxHp;               // �ִ� ü��
    public float CriticalRate;        // ũ��Ƽ�� Ȯ��
    public float CriticalMultiple;    // ũ��Ƽ�� ����
}

/// <summary>
/// �÷��̾� ���� Ÿ��
/// </summary>
public enum StatType
{
    AttackPower,
    AttackSpeed,
    MaxHp,
    CriticalRate,
    CriticalMultiple
}

/// <summary>
/// �÷��̾� ����
/// </summary>
public class PlayerStats
{
    public static event Action<PlayerStatArgs> OnPlayerStatChanged; // �÷��̾� ���� ����Ǿ��� �� �̺�Ʈ
    private static Dictionary<StatType, List<StatModifier>> _statModifierDict; // ���Ⱥ��� StatModifier ����Ʈ�� ����
    public static float BeforeTotalPower { get; private set; } // ����������

    /// <summary>
    /// ���� ������ (Ŭ������ ó�� ������ �� �� ���� ȣ��)
    /// </summary>
    static PlayerStats()
    {
        _statModifierDict = new Dictionary<StatType, List<StatModifier>>()
        {
            { StatType.AttackPower, new List<StatModifier>()},
            { StatType.AttackSpeed, new List<StatModifier>()},
            { StatType.MaxHp, new List<StatModifier>()},
            { StatType.CriticalRate, new List<StatModifier>()},
            { StatType.CriticalMultiple, new List<StatModifier>()},
        };
    }

    /// <summary>
    /// ���� ������̾� ������Ʈ
    /// </summary>
    public static void UpdateStatModifier(PlayerStatUpdateArgs args)
    {
        Dictionary<StatType, float> attributeDict = args.DetailStatDict; // �Ӽ��� ��ųʸ�
        string sourceID = args.SourceID;    // ��ó

        // ���� ������ ���
        BeforeTotalPower = GetAllStatValue();

        // ������ ���ȵ� ���� �÷��̾� ���ȿ� �߰�
        foreach (var kvp in attributeDict)
        {
            StatType statType = kvp.Key;
            float value = kvp.Value;

            // ��ųʸ��� �ҽ��� �����ϴ��� Ȯ��
            StatModifier foundStatModifier = FindSource_In_StatModifierDict(statType, sourceID);

            // �̹� �����ϸ� �� �� ����, ������ ���� �߰�
            if (foundStatModifier != null)
                foundStatModifier.Value = value; 
            else
                AddStatModifier(statType, value, sourceID);
        }

        // ���Ⱥ��� �̺�Ʈ ����
        OnPlayerStatChanged?.Invoke(GetCurrentPlayerStatArgs());
    }

    /// <summary>
    /// ���� ������̾� �߰�
    /// </summary>
    public static void AddStatModifier(StatType statType, float value, string sourceID)
    {
        _statModifierDict[statType].Add(new StatModifier(value, sourceID));   
    }

    /// <summary>
    /// ���� ������̾� ����
    /// </summary>
    public static void RemoveStatModifier(PlayerStatUpdateArgs args)
    {
        Dictionary<StatType, float> attributeDict = args.DetailStatDict; // �Ӽ��� ��ųʸ�
        string sourceID = args.SourceID;    // ��ó

        // ���� ������ ���
        BeforeTotalPower = GetAllStatValue();

        // �ش� ������(source)�� ���ȵ���, �÷��̾� ���ȿ��� ����
        foreach (var kvp in attributeDict)
        {
            StatType statType = kvp.Key;

            _statModifierDict[statType].RemoveAll(modifier => modifier.SourceID == sourceID);
        }
        
        // ���Ⱥ��� �̺�Ʈ ����
        OnPlayerStatChanged?.Invoke(GetCurrentPlayerStatArgs());
    }

    /// <summary>
    /// ���� �� ��������
    /// </summary>
    public static float GetStatValue(StatType statType)
    {
        float resultStatValue = _statModifierDict[statType].Sum(modifier => modifier.Value);
        return resultStatValue; 
    }

    /// <summary>
    /// ��ü ���� �� �������� (�� ������ ����)
    /// </summary>
    public static float GetAllStatValue()
    {
        float resultStatValue = 0;
        StatType[] AllStatTypes = (StatType[])Enum.GetValues(typeof(StatType));

        foreach (StatType statType in AllStatTypes)
        {
            // ���Ⱥ��� ������ �ٸ��� ���
            float calculatePower = 0.0f;
            switch (statType)
            {
                case StatType.AttackPower:
                    calculatePower = GetStatValue(statType) * 100f;
                    break;
                case StatType.AttackSpeed:
                    calculatePower = GetStatValue(statType) * 1000f;
                    break;
                case StatType.MaxHp:
                    calculatePower = GetStatValue(statType) * 10f;
                    break;
                case StatType.CriticalRate:
                    calculatePower = GetStatValue(statType) * 10000f;
                    break;
                case StatType.CriticalMultiple:
                    calculatePower = GetStatValue(statType) * 10000f;
                    break;
            }

            resultStatValue += calculatePower;
        }

        return resultStatValue;
    }

    /// <summary>
    /// ���� ���ȵ� ����ؼ� PlayerStatArgs �� ����
    /// </summary>
    public static PlayerStatArgs GetCurrentPlayerStatArgs()
    {
        PlayerStatArgs args = new PlayerStatArgs
        {
            BeforeTotalPower = BeforeTotalPower,
            TotalPower = GetAllStatValue(),
            AttackPower = GetStatValue(StatType.AttackPower),
            AttackSpeed = GetStatValue(StatType.AttackSpeed),
            MaxHp = GetStatValue(StatType.MaxHp),
            CriticalRate = GetStatValue(StatType.CriticalRate),
            CriticalMultiple = GetStatValue(StatType.CriticalMultiple)
        };

        return args;
    }

    /// <summary>
    /// ��ųʸ��� �ҽ�ID�� �����ϴ��� Ȯ��
    /// </summary>
    private static StatModifier FindSource_In_StatModifierDict(StatType statType, string sourceID)
    {
        StatModifier statModifier = _statModifierDict[statType].Find(modifier => modifier.SourceID == sourceID);

        if (statModifier != null)
            return statModifier;
        else
            return null;
    }
}
