using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// 플레이어 스탯 Args
/// </summary>
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

/// <summary>
/// 플레이어 스탯 타입
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
/// 플레이어 스탯
/// </summary>
public class PlayerStats
{
    public static event Action<PlayerStatArgs> OnPlayerStatChanged; // 플레이어 스탯 변경되었을 때 이벤트
    private static Dictionary<StatType, List<StatModifier>> _statModifierDict; // 스탯별로 StatModifier 리스트를 관리
    public static int BeforeCombatPower { get; private set; }

    /// <summary>
    /// 정적 생성자 (클래스가 처음 참조될 때 한 번만 호출)
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
    /// 스탯 모디파이어 업데이트
    /// </summary>
    public static void UpdateStatModifier(Dictionary<StatType, int> itemStatDict, object source)
    {
        // 이전 전투력 계산
        BeforeCombatPower = GetAllStatValue();

        // 아이템 스탯들 전부 플레이어 스탯에 추가
        foreach (var kvp in itemStatDict)
        {
            StatType statType = kvp.Key;
            int value = kvp.Value;

            // 딕셔너리에 소스가 존재하는지 확인
            StatModifier foundStatModifier = FindSource_In_StatModifierDict(statType, source);

            // 이미 존재하면 그 값 갱신, 없으면 새로 추가
            if (foundStatModifier != null)
                foundStatModifier.Value = value; 
            else
                AddStatModifier(statType, value, source);
        }
        
        // 스탯변경 이벤트 실행
        OnPlayerStatChanged?.Invoke(GetCurrentPlayerStatArgs());
    }

    /// <summary>
    /// 스탯 모디파이어 추가
    /// </summary>
    public static void AddStatModifier(StatType statType, float value, object source)
    {
        _statModifierDict[statType].Add(new StatModifier(value, source));   
    }

    /// <summary>
    /// 스탯 모디파이어 제거
    /// </summary>
    public static void RemoveStatModifier(Dictionary<StatType, int> itemStatDict, object source)
    {
        // 이전 전투력 계산
        BeforeCombatPower = GetAllStatValue();

        // 해당 아이템(source)의 스탯들을, 플레이어 스탯에서 제거
        foreach (var kvp in itemStatDict)
        {
            StatType statType = kvp.Key;

            _statModifierDict[statType].RemoveAll(modifier => modifier.Source == source);
        }
        
        // 스탯변경 이벤트 실행
        OnPlayerStatChanged?.Invoke(GetCurrentPlayerStatArgs());
    }

    /// <summary>
    /// 스탯 값 가져오기
    /// </summary>
    public static int GetStatValue(StatType statType)
    {
        float resultStatValue = _statModifierDict[statType].Sum(modifier => modifier.Value);
        return (int)Mathf.Floor(resultStatValue); 
    }

    /// <summary>
    /// 전체 스탯 값 가져오기 (총 전투력 개념)
    /// </summary>
    public static int GetAllStatValue()
    {
        float resultStatValue = 0;
        StatType[] AllStatTypes = (StatType[])Enum.GetValues(typeof(StatType));

        foreach (StatType statType in AllStatTypes)
            resultStatValue += GetStatValue(statType);

        return (int)Mathf.Floor(resultStatValue);
    }

    /// <summary>
    /// 현재 스탯들 계산해서 PlayerStatArgs 로 리턴
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
    /// 딕셔너리에 소스가 존재하는지 확인
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
