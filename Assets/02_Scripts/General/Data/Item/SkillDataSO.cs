using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų �Ӽ� Ÿ��
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
/// ��ų�Ӽ�
/// </summary>
[System.Serializable]
public class SkillStat
{
    public string Type;
    public string Value;
}

/// <summary>
/// ������ ��ų�Ӽ���
/// </summary>
[System.Serializable]
public class LevelSkillStats
{
    public int Level;
    public List<SkillStat> SkillStatList;
}

/// <summary>
/// ��ų ��ũ���ͺ� ������Ʈ
/// </summary>
[System.Serializable]
public class SkillDataSO : ItemDataSO
{
    public float CoolTime;
    public List<LevelSkillStats> LevelSkillStatsList;

    /// <summary>
    /// ������ �´� ��ų�Ӽ��� ��������
    /// </summary>
    public Dictionary<SkillStatType, float> GetSkillStats(int level)
    {
        Dictionary<SkillStatType, float> skillStatDict = new Dictionary<SkillStatType, float>();

        LevelSkillStats levelSkillStats = LevelSkillStatsList.Find(x => x.Level == level);
        if (levelSkillStats == null)
            return null;

        // �ش� ������ ��ų�Ӽ����� ��ųʸ� ���·� ���� �� ��ȯ
        foreach (SkillStat skillStat in levelSkillStats.SkillStatList)
        {
            SkillStatType type = (SkillStatType)Enum.Parse(typeof(SkillStatType), skillStat.Type);
            float value = float.Parse(skillStat.Value);

            skillStatDict.Add(type, value);
        }

        return skillStatDict;
    }
}
