using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų �����Ƽ Ÿ��
/// </summary>
public enum SkillAbilityType
{
    AttackPercentage,
    Delay,
    AddAttackSpeed,
    Range,
}

/// <summary>
/// ��ų ������ ��ũ���ͺ� ������Ʈ
/// </summary>
[System.Serializable]
public class SkillDataSO : ItemDataSO
{
    /// <summary>
    /// ���ϴ� ������ �´� �����Ƽ�� ��ųʸ��� ��������
    /// </summary>
    public Dictionary<SkillAbilityType, int> GetAbilityDict_ByLevel(int level)
    {
        // �ߺ��� �����͸� get���� �ʰ� �� ��ųʸ� ����
        Dictionary<SkillAbilityType, int> abilityDict = new Dictionary<SkillAbilityType, int>();

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
            SkillAbilityType statType = (SkillAbilityType)Enum.Parse(typeof(SkillAbilityType), itemAbility.AbilityType);
            int value = int.Parse(itemAbility.AbilityValue);

            abilityDict.Add(statType, value);
        }

        return abilityDict;
    }
}
