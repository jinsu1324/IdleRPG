using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// 플레이어 스탯 업데이트에 필요한 것들 구조체
/// </summary>
public struct PlayerStatUpdateArgs
{
    public Dictionary<StatType, float> AttributeDict;   // 속성들 딕셔너리
    public object Source;                               // 출처
}

/// <summary>
/// 플레이어 스탯 Args
/// </summary>
public struct PlayerStatArgs
{
    public float TotalPower;          // 총합 전투력
    public float BeforeTotalPower;    // 이전 총합 전투력
    public float AttackPower;         // 공격력
    public float AttackSpeed;         // 공격속도
    public float MaxHp;               // 최대 체력
    public float CriticalRate;        // 크리티컬 확률
    public float CriticalMultiple;    // 크리티컬 배율
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
    public static float BeforeCombatPower { get; private set; } // 이전전투력

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
    public static void UpdateStatModifier(PlayerStatUpdateArgs args)
    {
        Dictionary<StatType, float> attributeDict = args.AttributeDict; // 속성들 딕셔너리
        object source = args.Source;    // 출처

        // 이전 전투력 계산
        BeforeCombatPower = GetAllStatValue();

        // 아이템 스탯들 전부 플레이어 스탯에 추가
        foreach (var kvp in attributeDict)
        {
            StatType statType = kvp.Key;
            float value = kvp.Value;

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
    public static void RemoveStatModifier(PlayerStatUpdateArgs args)
    {
        Dictionary<StatType, float> attributeDict = args.AttributeDict; // 속성들 딕셔너리
        object source = args.Source;    // 출처

        // 이전 전투력 계산
        BeforeCombatPower = GetAllStatValue();

        // 해당 아이템(source)의 스탯들을, 플레이어 스탯에서 제거
        foreach (var kvp in attributeDict)
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
    public static float GetStatValue(StatType statType)
    {
        float resultStatValue = _statModifierDict[statType].Sum(modifier => modifier.Value);
        return resultStatValue; 
    }

    /// <summary>
    /// 전체 스탯 값 가져오기 (총 전투력 개념)
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
