using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// 스킬 속성 타입
/// </summary>
public enum SkillStatType
{
    AttackPercentage,
    Range,
    AddAttackSpeed,
    AddAttackPower,
    Duration,
    ProjectileCount,
    ProjectileSpeed
}

/// <summary>
/// 스킬속성
/// </summary>
[System.Serializable]
public class SkillStat
{
    public string Type;
    public string Value;
}

/// <summary>
/// 레벨별 스킬속성들
/// </summary>
[System.Serializable]
public class LevelSkillStats
{
    public int Level;
    public List<SkillStat> SkillStatList;
}

/// <summary>
/// 스킬 스크립터블 오브젝트
/// </summary>
[System.Serializable]
public class SkillDataSO : ItemDataSO
{
    public float CoolTime;
    public List<LevelSkillStats> LevelSkillStatsList;

    /// <summary>
    /// 레벨에 맞는 스킬속성들 가져오기
    /// </summary>
    public Dictionary<SkillStatType, float> GetSkillStats(int level)
    {
        Dictionary<SkillStatType, float> skillStatDict = new Dictionary<SkillStatType, float>();

        LevelSkillStats levelSkillStats = LevelSkillStatsList.Find(x => x.Level == level);
        if (levelSkillStats == null)
            return null;

        // 해당 레벨의 스킬속성들을 딕셔너리 형태로 저장 후 반환
        foreach (SkillStat skillStat in levelSkillStats.SkillStatList)
        {
            SkillStatType type = (SkillStatType)Enum.Parse(typeof(SkillStatType), skillStat.Type);
            float value = float.Parse(skillStat.Value);

            skillStatDict.Add(type, value);
        }

        return skillStatDict;
    }

    /// <summary>
    /// 다이나믹 텍스트 가져오기
    /// </summary>
    public string GetDynamicDesc(int level)
    {
        Dictionary<SkillStatType, float> skillStatDict = GetSkillStats(level);

        string pattern = @"\[(.*?)\]";

        string resultDesc = Regex.Replace(Desc, pattern, match =>
        {
            SkillStatType skillStatType = (SkillStatType)Enum.Parse(typeof(SkillStatType), match.Groups[1].Value);

            if (skillStatDict.TryGetValue(skillStatType, out float value))
            {
                return skillStatType == SkillStatType.AttackPercentage
                    ? NumberConverter.ConvertPercentage(value) : value.ToString();
            }

            return match.Value;
        });

        return resultDesc;

    }
}
