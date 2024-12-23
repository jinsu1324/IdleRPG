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
    // 스탯별로 StatModifier 리스트를 관리
    private Dictionary<StatType, List<StatModifier>> _statModifierDict = new Dictionary<StatType, List<StatModifier>>();    

    /// <summary>
    /// 스탯 모디파이어 딕셔너리를 초기화
    /// </summary>
    public void InitStatModifierDict()
    {
        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            _statModifierDict[statType] = new List<StatModifier>(); // 모든 StatType에 맞춰서 빈 StatModifier 리스트를 Key, Value로 할당
    }

    /// <summary>
    /// 스탯 추가
    /// </summary>
    public void AddModifier(StatType statType, float value, object source)
    {
        _statModifierDict[statType].Add(new StatModifier(value, source));   // statType 키값의 StatModifier List에다가 추가
    }

    /// <summary>
    /// 스탯 업데이트 (값 갱신)
    /// </summary>
    public void UpdateModifier(StatType statType, float newVelue, object source)
    {
        StatModifier modifier = _statModifierDict[statType].Find(modifier => modifier.Source == source);
        
        if (modifier != null)
            modifier.Value = newVelue; // 이미 존재하면 그 값 갱신
        else
            AddModifier(statType, newVelue, source);  // 기존 StatModifier 가 없으면 새로 추가
    }

    /// <summary>
    /// 스탯제거
    /// </summary>
    public void RemoveModifier(StatType statType, object source)
    {
        _statModifierDict[statType].RemoveAll(modifier => modifier.Source == source); // 이 Source에 해당하는 Modifier 제거
    }

    /// <summary>
    /// 스탯타입 Key 속에 있는 모든 modifier value들을 더해서 최종값을 리턴
    /// </summary>
    public float GetFinalStat(StatType statType)
    {
        return _statModifierDict[statType].Sum(modifier => modifier.Value); // statType 키값의 StatModifier List의 모든 Value값들을 더해서 반환
    }

    /// <summary>
    /// 전체 밸류들 총합 리턴 (총 전투력 개념)
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
