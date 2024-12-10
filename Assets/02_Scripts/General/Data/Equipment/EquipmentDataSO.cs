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
    public List<UpgradeInfo> UpgradeInfoList;       // ���׷��̵� ������ ����Ʈ
}

/// <summary>
/// ���׷��̵� ����
/// </summary>
[System.Serializable]
public class UpgradeInfo
{
    public string Level;                            // ����
    public List<EquipmentStat> EquipmentStatList;   // ������ �ִ� ���ȵ� ����Ʈ
}

/// <summary>
/// �����
/// </summary>
[System.Serializable]
public class EquipmentStat
{
    public string StatKey;
    public string StatValue;
}