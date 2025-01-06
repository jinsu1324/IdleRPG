using System;
using System.Collections;
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
/// ��� ������ ��ũ���ͺ� ������Ʈ
/// </summary>
[System.Serializable]
public class GearDataSO : ItemDataSO
{
    public string AttackAnimType;
    public GameObject Prefab;

    /// <summary>
    /// ���ϴ� ������ �´� �����Ƽ�� ��ųʸ��� ��������
    /// </summary>
    public Dictionary<StatType, int> GetAbilityDict_ByLevel(int level)
    {
        // �ߺ��� �����͸� get���� �ʰ� �� ��ųʸ� ����
        Dictionary<StatType, int> abilityDict = new Dictionary<StatType, int>();

        // level�� �´� itemLevelInfo ã��
        ItemLevelInfo itemLevelInfo = ItemLevelInfoList.Find(x => x.Level == level.ToString());

        // ������ �׳� ����
        if (itemLevelInfo == null)
        {
            Debug.Log($"{level}�� �ش��ϴ� ItemLevelInfo�� ã�� ���߽��ϴ�.");
            return abilityDict;
        }

        // �ش� ������ itemAbility���� ��ųʸ��� �ֱ�
        foreach (ItemAbility itemAbility in itemLevelInfo.ItemAbilityList)
        {
            StatType statType = (StatType)Enum.Parse(typeof(StatType), itemAbility.AbilityType);
            int value = int.Parse(itemAbility.AbilityValue);

            abilityDict.Add(statType, value);
        }

        return abilityDict;
    }
}
