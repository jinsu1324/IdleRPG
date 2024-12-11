using System;
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
    public List<UpgradeInfo> UpgradeInfoList;   // ���׷��̵� ������ ����Ʈ


    /// <summary>
    /// ���ϴ� ������ �´� ���ȵ� ��ųʸ��� ��������
    /// </summary>
    public Dictionary<StatType, int> GetStatDictionaryByLevel(int level)
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
        foreach (EquipmentStat equipmentStat in upgradeInfo.EquipmentStatList)
        {
            StatType statType = (StatType)Enum.Parse(typeof(StatType), equipmentStat.StatType);
            int value = int.Parse(equipmentStat.StatValue);

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
    public string Level;                            // ����
    public List<EquipmentStat> EquipmentStatList;   // ������ �ִ� ���ȵ� ����Ʈ
}

/// <summary>
/// �����
/// </summary>
[System.Serializable]
public class EquipmentStat
{
    public string StatType;
    public string StatValue;
}