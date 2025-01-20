using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �ִϸ��̼� Ÿ��
/// </summary>
public enum AttackAnimType
{
    Hand = 0,
    Melee = 1,
    Magic = 2,
}

/// <summary>
/// ���Ӽ�
/// </summary>
[System.Serializable]
public class GearStat
{
    public string Type;
    public string Value;
}

/// <summary>
/// ������ ���Ӽ���
/// </summary>
[System.Serializable]
public class LevelGearStats
{
    public int Level;
    public List<GearStat> GearStatList;
}

/// <summary>
/// ��� ��ũ���ͺ� ������Ʈ
/// </summary>
[System.Serializable]
public class GearDataSO : ItemDataSO
{
    public string AttackAnimType;
    public List<LevelGearStats> LevelGearStatsList;

    /// <summary>
    /// ������ �´� ���Ӽ��� ��������
    /// </summary>
    public Dictionary<StatType, float> GetGearStats(int level)
    {
        Dictionary<StatType, float> gearStatDict = new Dictionary<StatType, float>();

        LevelGearStats levelGearStats = LevelGearStatsList.Find(x => x.Level == level);
        if (levelGearStats == null)
            return null;

        // �ش� ������ ���Ӽ����� ��ųʸ� ���·� ���� �� ��ȯ
        foreach (GearStat gearStat in levelGearStats.GearStatList)
        {
            StatType type = (StatType)Enum.Parse(typeof(StatType), gearStat.Type);
            float value = float.Parse(gearStat.Value);

            gearStatDict.Add(type, value);
        }

        return gearStatDict;
    }
}



