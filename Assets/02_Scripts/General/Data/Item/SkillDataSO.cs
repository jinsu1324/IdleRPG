using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų�� �ִ� �Ӽ� Ÿ��
/// </summary>
public enum SkillAttributeType
{
    AttackPercentage,
    Delay,
    Range,
    SplashRadius,
    AddAttackSpeed,
    Duration,
    ProjectileCount
}

/// <summary>
/// ��ų ��ũ���ͺ� ������Ʈ (+��ų������)
/// </summary>
[System.Serializable]
public class SkillDataSO : ItemDataSO
{
    /// <summary>
    /// ������ �´� �Ӽ����� ��ųʸ��� ��������
    /// </summary>
    public Dictionary<SkillAttributeType, int> GetAttributeDict_ByLevel(int level)
    {
        // �ߺ��� �����͸� get���� �ʰ� �� ��ųʸ� ����
        Dictionary<SkillAttributeType, int> attributeDict = new Dictionary<SkillAttributeType, int>();

        // level�� �´� levelAttributes ã��
        LevelAttributes levelAttributes = LevelAttributesList.Find(x => x.Level == level.ToString());

        // ������ �׳� ����
        if (levelAttributes == null)
        {
            Debug.Log($"{level}�� �´� levelAttributes�� ã�� ���߽��ϴ�.");
            return attributeDict;
        }

        // �ش� ������ �����ϴ� �Ӽ�(attribute)���� ��ųʸ��� �ְ� ��ȯ
        foreach (Attribute attribute in levelAttributes.AttributeList)
        {
            SkillAttributeType statType = (SkillAttributeType)Enum.Parse(typeof(SkillAttributeType), attribute.Type);
            int value = int.Parse(attribute.Value);

            attributeDict.Add(statType, value);
        }

        return attributeDict;
    }

    /// <summary>
    /// Ư�� �Ӽ��� ���� ��ȯ
    /// </summary>
    public string GetAttributeValue(SkillAttributeType skillAttributeType, int level)
    {
        // level�� �´� levelAttributes ã��
        LevelAttributes levelAttributes = LevelAttributesList.Find(x => x.Level == level.ToString());

        // �Ӽ��߿��� Ÿ���̸��� ���� �Ӽ��� ã��
        Attribute attribute = levelAttributes.AttributeList.Find(x => x.Type == skillAttributeType.ToString());

        // �� �Ӽ��� ���� ��ȯ
        return attribute.Value;
    }
}
