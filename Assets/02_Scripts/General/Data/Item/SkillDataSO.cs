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
    AddAttackSpeed,
    AddAttackPower,
    Duration,
    ProjectileCount,
    ProjectileSpeed
}

/// <summary>
/// ��ų ��ũ���ͺ� ������Ʈ (+��ų������)
/// </summary>
[System.Serializable]
public class SkillDataSO : ItemDataSO
{
    public GameObject SkillPrefab;   // ��ų ������

    /// <summary>
    /// ������ �´� �Ӽ����� ��ųʸ��� ��������
    /// </summary>
    public Dictionary<SkillAttributeType, float> GetAttributeDict_ByLevel(int level)
    {
        // �ߺ��� �����͸� get���� �ʰ� �� ��ųʸ� ����
        Dictionary<SkillAttributeType, float> attributeDict = new Dictionary<SkillAttributeType, float>();

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
            float value = float.Parse(attribute.Value);

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
