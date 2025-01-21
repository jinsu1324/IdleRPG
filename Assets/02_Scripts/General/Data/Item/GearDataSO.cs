using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공격 애니메이션 타입
/// </summary>
public enum AttackAnimType
{
    Hand = 0,
    Melee = 1,
    Magic = 2,
}

/// <summary>
/// 장비속성
/// </summary>
[System.Serializable]
public class GearStat
{
    public string Type;
    public string Value;
}

/// <summary>
/// 레벨별 장비속성들
/// </summary>
[System.Serializable]
public class LevelGearStats
{
    public int Level;
    public List<GearStat> GearStatList;
}

/// <summary>
/// 장비 스크립터블 오브젝트
/// </summary>
[System.Serializable]
public class GearDataSO : ItemDataSO
{
    public string AttackAnimType;
    public List<LevelGearStats> LevelGearStatsList;

    /// <summary>
    /// 레벨에 맞는 장비속성들 가져오기
    /// </summary>
    public Dictionary<StatType, float> GetGearStats(int level)
    {
        Dictionary<StatType, float> gearStatDict = new Dictionary<StatType, float>();

        LevelGearStats levelGearStats = LevelGearStatsList.Find(x => x.Level == level);
        if (levelGearStats == null)
            return null;

        // 해당 레벨의 장비속성들을 딕셔너리 형태로 저장 후 반환
        foreach (GearStat gearStat in levelGearStats.GearStatList)
        {
            StatType type = (StatType)Enum.Parse(typeof(StatType), gearStat.Type);
            float value = float.Parse(gearStat.Value);

            gearStatDict.Add(type, value);
        }

        return gearStatDict;
    }
}



