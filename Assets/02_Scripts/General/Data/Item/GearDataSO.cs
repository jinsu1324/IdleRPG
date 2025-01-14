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
    public string AttackAnimType;   // ���� �ִϸ��̼� Ÿ��
    public GameObject Prefab;       // ������ ������

    /// <summary>
    /// ������ �´� �Ӽ����� ��ųʸ��� ��������
    /// </summary>
    public Dictionary<StatType, int> GetAttributeDict_ByLevel(int level)
    {
        // �ߺ��� �����͸� get���� �ʰ� �� ��ųʸ� ����
        Dictionary<StatType, int> attributeDict = new Dictionary<StatType, int>();

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
            StatType statType = (StatType)Enum.Parse(typeof(StatType), attribute.Type);
            int value = int.Parse(attribute.Value);

            attributeDict.Add(statType, value);
        }

        return attributeDict;
    }
}
