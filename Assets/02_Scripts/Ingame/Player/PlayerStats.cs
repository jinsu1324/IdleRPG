using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public struct PlayerStatArgs
{
    public int TotalPower;          // 총합 전투력
    public int BeforeTotalPower;    // 이전 총합 전투력
    public int AttackPower;         // 공격력
    public int AttackSpeed;         // 공격속도
    public int MaxHp;               // 최대 체력
    public int CriticalRate;        // 크리티컬 확률
    public int CriticalMultiple;    // 크리티컬 배율
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
/// 플레이어 스탯
/// </summary>
public class PlayerStats : SingletonBase<PlayerStats>
{
    // 플레이어 스탯 변경되었을 때 이벤트
    public static event Action<PlayerStatArgs> OnPlayerStatChanged; 

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
    /// 스탯 업데이트 (값 갱신)
    /// </summary>
    public void UpdateModifier(Dictionary<StatType, int> statDict, object source)
    {
        // 이전 전투력 계산
        float beforeTotalPower = (int)Mathf.Floor(GetAllFinalStat());

        // 스탯들 전부 추가
        foreach (var kvp in statDict)
        {
            StatType statType = kvp.Key;
            int value = kvp.Value;

            StatModifier modifier = _statModifierDict[statType].Find(modifier => modifier.Source == source);

            if (modifier != null)
                modifier.Value = value; // 이미 존재하면 그 값 갱신
            else
                AddModifier(statType, value, source);  // 기존 StatModifier 가 없으면 새로 추가
        }
        
        // 스탯변경 이벤트 실행
        OnPlayerStatChanged?.Invoke(CalculateStats(beforeTotalPower));
    }

    /// <summary>
    /// 스탯 추가
    /// </summary>
    public void AddModifier(StatType statType, float value, object source)
    {
        _statModifierDict[statType].Add(new StatModifier(value, source));   // statType 키값의 StatModifier List에다가 추가
    }

    /// <summary>
    /// 스탯제거
    /// </summary>
    public void RemoveModifier(Dictionary<StatType, int> statDict, object source)
    {
        // 이전 전투력 계산
        float beforeTotalPower = (int)Mathf.Floor(GetAllFinalStat());

        // 스탯들 전부 제거
        foreach (var kvp in statDict)
        {
            StatType statType = kvp.Key;

            _statModifierDict[statType].RemoveAll(modifier => modifier.Source == source); // 이 Source에 해당하는 Modifier 제거
        }
        
        // 스탯변경 이벤트 실행
        OnPlayerStatChanged?.Invoke(CalculateStats(beforeTotalPower));
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

    /// <summary>
    /// 스탯 계산 해서 PlayerStatArgs 로 리턴
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
