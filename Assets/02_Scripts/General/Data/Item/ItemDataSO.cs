using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���
/// </summary>
public enum ItemGrade
{
    Normal,
    Rare,
    Legendary
}

/// <summary>
/// ������ Ÿ��
/// </summary>
public enum ItemType
{
    Weapon,
    Armor,
    Helmet,
    Skill
}

/// <summary>
/// �����۵� �θ� ��ũ���ͺ� ������Ʈ
/// </summary>
[System.Serializable]
public class ItemDataSO : ScriptableObject
{
    public string ID;
    public string ItemType;
    public string Name;
    public string Grade;
    public List<ItemLevelInfo> ItemLevelInfoList;

    /// <summary>
    /// ���ϴ� ������ �´� �����Ƽ�� ��ųʸ��� ��������
    /// </summary>
    public Dictionary<StatType, int> GetAbilityDict_ByLevel(int level)
    {
        // �ߺ��� �����͸� get���� �ʰ� �� ��ųʸ� ����
        Dictionary<StatType, int> statDict = new Dictionary<StatType, int>();

        // level�� �´� itemLevelInfo ã��
        ItemLevelInfo itemLevelInfo = ItemLevelInfoList.Find(x => x.Level == level.ToString());

        // ������ �׳� ����
        if (itemLevelInfo == null)
        {
            Debug.Log($"{level}�� �ش��ϴ� upgradeInfo�� ã�� ���߽��ϴ�.");
            return statDict;
        }

        // �ش� ������ itemAbility���� ��ųʸ��� �ֱ�
        foreach (ItemAbility itemAbility in itemLevelInfo.ItemAbilityList)
        {
            StatType statType = (StatType)Enum.Parse(typeof(StatType), itemAbility.AbilityType);
            int value = int.Parse(itemAbility.AbilityValue);

            statDict.Add(statType, value);
        }

        return statDict;
    }
}

/// <summary>
/// ������ ������ �׿��´� ������(�����Ƽ ��)
/// </summary>
[System.Serializable]
public class ItemLevelInfo
{
    public string Level;                        // ����
    public List<ItemAbility> ItemAbilityList;   // ������ �ִ� �����Ƽ�� ����Ʈ
}

/// <summary>
/// ������ �����Ƽ
/// </summary>
[System.Serializable]
public class ItemAbility
{
    public string AbilityType;
    public string AbilityValue;
}