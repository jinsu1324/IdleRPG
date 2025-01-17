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
    public Dictionary<StatType, float> AttributeDict;   // �Ӽ��� ��ųʸ�
    public object Source;                               // ��ó
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
    public static float BeforeCombatPower { get; private set; } // ����������

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
        Dictionary<StatType, float> attributeDict = args.AttributeDict; // �Ӽ��� ��ųʸ�
        object source = args.Source;    // ��ó

        // ���� ������ ���
        BeforeCombatPower = GetAllStatValue();

        // ������ ���ȵ� ���� �÷��̾� ���ȿ� �߰�
        foreach (var kvp in attributeDict)
        {
            StatType statType = kvp.Key;
            float value = kvp.Value;

            // ��ųʸ��� �ҽ��� �����ϴ��� Ȯ��
            StatModifier foundStatModifier = FindSource_In_StatModifierDict(statType, source);

            // �̹� �����ϸ� �� �� ����, ������ ���� �߰�
            if (foundStatModifier != null)
                foundStatModifier.Value = value; 
            else
                AddStatModifier(statType, value, source);
        }

        // ���Ⱥ��� �̺�Ʈ ����
        OnPlayerStatChanged?.Invoke(GetCurrentPlayerStatArgs());
    }

    /// <summary>
    /// ���� ������̾� �߰�
    /// </summary>
    public static void AddStatModifier(StatType statType, float value, object source)
    {
        _statModifierDict[statType].Add(new StatModifier(value, source));   
    }

    /// <summary>
    /// ���� ������̾� ����
    /// </summary>
    public static void RemoveStatModifier(PlayerStatUpdateArgs args)
    {
        Dictionary<StatType, float> attributeDict = args.AttributeDict; // �Ӽ��� ��ųʸ�
        object source = args.Source;    // ��ó

        // ���� ������ ���
        BeforeCombatPower = GetAllStatValue();

        // �ش� ������(source)�� ���ȵ���, �÷��̾� ���ȿ��� ����
        foreach (var kvp in attributeDict)
        {
            StatType statType = kvp.Key;

            _statModifierDict[statType].RemoveAll(modifier => modifier.Source == source);
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
            resultStatValue += GetStatValue(statType);

        return resultStatValue;
    }

    /// <summary>
    /// ���� ���ȵ� ����ؼ� PlayerStatArgs �� ����
    /// </summary>
    public static PlayerStatArgs GetCurrentPlayerStatArgs()
    {
        PlayerStatArgs args = new PlayerStatArgs
        {
            BeforeTotalPower = BeforeCombatPower,
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
    /// ��ųʸ��� �ҽ��� �����ϴ��� Ȯ��
    /// </summary>
    private static StatModifier FindSource_In_StatModifierDict(StatType statType, object source)
    {
        StatModifier statModifier = _statModifierDict[statType].Find(modifier => modifier.Source == source);

        if (statModifier != null)
            return statModifier;
        else
            return null;
    }
}
