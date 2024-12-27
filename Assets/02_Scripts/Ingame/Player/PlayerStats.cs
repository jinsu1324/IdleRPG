using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public struct PlayerStatArgs
{
    public int TotalPower;          // ���� ������
    public int BeforeTotalPower;    // ���� ���� ������
    public int AttackPower;         // ���ݷ�
    public int AttackSpeed;         // ���ݼӵ�
    public int MaxHp;               // �ִ� ü��
    public int CriticalRate;        // ũ��Ƽ�� Ȯ��
    public int CriticalMultiple;    // ũ��Ƽ�� ����
}

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
public class PlayerStats : SingletonBase<PlayerStats>
{
    // �÷��̾� ���� ����Ǿ��� �� �̺�Ʈ
    public static event Action<PlayerStatArgs> OnPlayerStatChanged; 

    // ���Ⱥ��� StatModifier ����Ʈ�� ����
    private Dictionary<StatType, List<StatModifier>> _statModifierDict = new Dictionary<StatType, List<StatModifier>>();    

    /// <summary>
    /// ���� ������̾� ��ųʸ��� �ʱ�ȭ
    /// </summary>
    public void InitStatModifierDict()
    {
        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            _statModifierDict[statType] = new List<StatModifier>(); // ��� StatType�� ���缭 �� StatModifier ����Ʈ�� Key, Value�� �Ҵ�
    }

    /// <summary>
    /// ���� ������Ʈ (�� ����)
    /// </summary>
    public void UpdateModifier(Dictionary<StatType, int> statDict, object source)
    {
        // ���� ������ ���
        float beforeTotalPower = (int)Mathf.Floor(GetAllFinalStat());

        // ���ȵ� ���� �߰�
        foreach (var kvp in statDict)
        {
            StatType statType = kvp.Key;
            int value = kvp.Value;

            StatModifier modifier = _statModifierDict[statType].Find(modifier => modifier.Source == source);

            if (modifier != null)
                modifier.Value = value; // �̹� �����ϸ� �� �� ����
            else
                AddModifier(statType, value, source);  // ���� StatModifier �� ������ ���� �߰�
        }
        
        // ���Ⱥ��� �̺�Ʈ ����
        OnPlayerStatChanged?.Invoke(CalculateStats(beforeTotalPower));
    }

    /// <summary>
    /// ���� �߰�
    /// </summary>
    public void AddModifier(StatType statType, float value, object source)
    {
        _statModifierDict[statType].Add(new StatModifier(value, source));   // statType Ű���� StatModifier List���ٰ� �߰�
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void RemoveModifier(Dictionary<StatType, int> statDict, object source)
    {
        // ���� ������ ���
        float beforeTotalPower = (int)Mathf.Floor(GetAllFinalStat());

        // ���ȵ� ���� ����
        foreach (var kvp in statDict)
        {
            StatType statType = kvp.Key;

            _statModifierDict[statType].RemoveAll(modifier => modifier.Source == source); // �� Source�� �ش��ϴ� Modifier ����
        }
        
        // ���Ⱥ��� �̺�Ʈ ����
        OnPlayerStatChanged?.Invoke(CalculateStats(beforeTotalPower));
    }

    /// <summary>
    /// ����Ÿ�� Key �ӿ� �ִ� ��� modifier value���� ���ؼ� �������� ����
    /// </summary>
    public float GetFinalStat(StatType statType)
    {
        return _statModifierDict[statType].Sum(modifier => modifier.Value); // statType Ű���� StatModifier List�� ��� Value������ ���ؼ� ��ȯ
    }

    /// <summary>
    /// ��ü ����� ���� ���� (�� ������ ����)
    /// </summary>
    public float GetAllFinalStat()
    {
        float allFinalStatValues = 0;
        StatType[] allStatType = (StatType[])Enum.GetValues(typeof(StatType));

        foreach (StatType statType in allStatType)
        {
            allFinalStatValues += GetFinalStat(statType);
        }

        return allFinalStatValues;
    }

    /// <summary>
    /// ���� ��� �ؼ� PlayerStatArgs �� ����
    /// </summary>
    public PlayerStatArgs CalculateStats(float beforeTotalPower)
    {
        PlayerStatArgs args = new PlayerStatArgs
        {
            BeforeTotalPower = (int)beforeTotalPower,
            TotalPower = (int)Mathf.Floor(GetAllFinalStat()),
            AttackPower = (int)GetFinalStat(StatType.AttackPower),
            AttackSpeed = (int)GetFinalStat(StatType.AttackSpeed),
            MaxHp = (int)GetFinalStat(StatType.MaxHp),
            CriticalRate = (int)GetFinalStat(StatType.CriticalRate),
            CriticalMultiple = (int)GetFinalStat(StatType.CriticalMultiple)
        };

        return args;
    }
}
