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
    public List<UpgradeInfo> UpgradeInfoList;   // ���׷��̵� ������ ����Ʈ


    /// <summary>
    /// ���ϴ� ������ �´� ���ȵ� ��ųʸ��� ��������
    /// </summary>
    public Dictionary<StatType, int> GetStatDictByLevel(int level)
    {
        // �ߺ��� �����͸� get���� �ʰ� �� ��ųʸ� ����
        Dictionary<StatType, int> statDict = new Dictionary<StatType, int>();

        // level�� �´� upgradeInfo ã��
        UpgradeInfo upgradeInfo = UpgradeInfoList.Find(upgradeInfo => upgradeInfo.Level == level.ToString());

        // ������ �׳� ����
        if (upgradeInfo == null)
        {
            Debug.Log($"{level}�� �ش��ϴ� upgradeInfo�� ã�� ���߽��ϴ�.");
            return statDict;
        }

        // �ش� ������ equipmentStat���� ��ųʸ��� �ֱ�
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
/// ���׷��̵� ����
/// </summary>
[System.Serializable]
public class UpgradeInfo
{
    public string Level;                  // ����
    public List<ItemStat> ItemStatList;   // ������ �ִ� ���ȵ� ����Ʈ
}

/// <summary>
/// ������ ����
/// </summary>
[System.Serializable]
public class ItemStat
{
    public string StatType;
    public string StatValue;
}