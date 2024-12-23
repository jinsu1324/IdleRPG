using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public enum StatType
{
    AttackPower,
    AttackSpeed,
    MaxHp,
    CriticalRate,
    CriticalMultiple
}

public class PlayerStats : SingletonBase<PlayerStats>
{
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
    /// ���� �߰�
    /// </summary>
    public void AddModifier(StatType statType, float value, object source)
    {
        _statModifierDict[statType].Add(new StatModifier(value, source));   // statType Ű���� StatModifier List���ٰ� �߰�
    }

    /// <summary>
    /// ���� ������Ʈ (�� ����)
    /// </summary>
    public void UpdateModifier(StatType statType, float newVelue, object source)
    {
        StatModifier modifier = _statModifierDict[statType].Find(modifier => modifier.Source == source);
        
        if (modifier != null)
            modifier.Value = newVelue; // �̹� �����ϸ� �� �� ����
        else
            AddModifier(statType, newVelue, source);  // ���� StatModifier �� ������ ���� �߰�
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void RemoveModifier(StatType statType, object source)
    {
        _statModifierDict[statType].RemoveAll(modifier => modifier.Source == source); // �� Source�� �ش��ϴ� Modifier ����
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






    //--------------------------------------------------------------
    public TextMeshProUGUI _attackPowerText;
    public TextMeshProUGUI _attackSpeedText;
    public TextMeshProUGUI _maxHpText;
    public TextMeshProUGUI _criticalRateText;
    public TextMeshProUGUI _criticalMultipleText;

    public void AllStatUIUpdate()
    {
        _attackPowerText.text = "AttackPower : " + _statModifierDict[StatType.AttackPower].Sum(modifier => modifier.Value).ToString();
        _attackSpeedText.text = "AttackSpeed : " + _statModifierDict[StatType.AttackSpeed].Sum(modifier => modifier.Value).ToString();
        _maxHpText.text = "MaxHp : " + _statModifierDict[StatType.MaxHp].Sum(modifier => modifier.Value).ToString();
        _criticalRateText.text = "CriticalRate : " + _statModifierDict[StatType.CriticalRate].Sum(modifier => modifier.Value).ToString();
        _criticalMultipleText.text = "CriticalMultiple : " + _statModifierDict[StatType.CriticalMultiple].Sum(modifier => modifier.Value).ToString();
    }
}
