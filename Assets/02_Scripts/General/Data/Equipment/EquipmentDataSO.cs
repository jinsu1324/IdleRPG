using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor
}

public enum EquipmentID
{
    Weapon_Sword,
    Weapon_Axe,
    Weapon_ForestSword,
    Weapon_MagicStaff,
    Armor_LeatherArmor,
    Armor_SteelArmor,
    Armor_ForestArmor,
    Armor_WizardRobe
}

[System.Serializable]
public class EquipmentDataSO : ScriptableObject
{
    public string ID;
    public string EquipmentType;
    public string Name;
    public string Grade;
    public Sprite Icon;
    public List<UpgradeInfo> UpgradeInfoList;       // 업그레이드 정보들 리스트
}

/// <summary>
/// 업그레이드 정보
/// </summary>
[System.Serializable]
public class UpgradeInfo
{
    public string Level;                            // 레벨
    public List<EquipmentStat> EquipmentStatList;   // 가지고 있는 스탯들 리스트
}

/// <summary>
/// 장비스탯
/// </summary>
[System.Serializable]
public class EquipmentStat
{
    public string StatKey;
    public string StatValue;
}