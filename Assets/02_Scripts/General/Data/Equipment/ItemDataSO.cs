using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemGrade
{
    Normal,
    Rare,
    Legendary
}

public enum ItemType
{
    Weapon,
    Armor,
    Helmet
}

public enum ItemID
{
    Weapon_Sword,
    Weapon_Axe,
    Weapon_ForestSword,
    Weapon_MagicStaff,
    Armor_LeatherArmor,
    Armor_SteelArmor,
    Armor_ForestArmor,
    Armor_WizardRobe,
    Helmet_BikingHelmet,
    Helmet_KnightHelmet,
    Helmet_ForestHelmet,
    Helmet_WizardHelmet
}

[System.Serializable]
public class ItemDataSO : ScriptableObject
{
    public string ID;
    public string ItemType;
    public string Name;
    public string Grade;
    public Sprite Icon;
    public List<UpgradeInfo> UpgradeInfoList;   // 업그레이드 정보들 리스트


    /// <summary>
    /// 원하는 레벨에 맞는 스탯들 딕셔너리로 가져오기
    /// </summary>
    public Dictionary<StatType, int> GetStatDictByLevel(int level)
    {
        // 중복된 데이터를 get하지 않게 새 딕셔너리 생성
        Dictionary<StatType, int> statDict = new Dictionary<StatType, int>();

        // level이 맞는 upgradeInfo 찾기
        UpgradeInfo upgradeInfo = UpgradeInfoList.Find(upgradeInfo => upgradeInfo.Level == level.ToString());

        // 없으면 그냥 리턴
        if (upgradeInfo == null)
        {
            Debug.Log($"{level}에 해당하는 upgradeInfo를 찾지 못했습니다.");
            return statDict;
        }

        // 해당 레벨의 equipmentStat들을 딕셔너리에 넣기
        foreach (ItemStat itemStat in upgradeInfo.ItemStatList)
        {
            StatType statType = (StatType)Enum.Parse(typeof(StatType), itemStat.StatType);
            int value = int.Parse(itemStat.StatValue);

            statDict.Add(statType, value);
        }

        return statDict;
    }
}

/// <summary>
/// 업그레이드 정보
/// </summary>
[System.Serializable]
public class UpgradeInfo
{
    public string Level;                  // 레벨
    public List<ItemStat> ItemStatList;   // 가지고 있는 스탯들 리스트
}

/// <summary>
/// 아이템 스탯
/// </summary>
[System.Serializable]
public class ItemStat
{
    public string StatType;
    public string StatValue;
}