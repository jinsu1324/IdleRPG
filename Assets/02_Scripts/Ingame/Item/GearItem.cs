using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ������
/// </summary>
public class GearItem : Item, IEnhanceableItem
{
    public GearDataSO GearDataSO { get; private set; }  // ��� ������
    public string AttackAnimType { get; private set; }  // ���� �ִϸ��̼� Ÿ��
    public GameObject Prefab { get; private set; }      // ������ ������
    public Dictionary<StatType, int> AbilityDict { get; private set; }  // �������� �����ϴ� �����Ƽ�� ��ųʸ�

    public int Level { get; private set; }
    public int EnhanceableCount { get; private set; }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(GearDataSO gearDataSO, int level)
    {
        GearDataSO = gearDataSO;
        ID = gearDataSO.ID;
        ItemType = (ItemType)Enum.Parse(typeof(ItemType), gearDataSO.ItemType);
        Name = gearDataSO.Name;
        Grade = gearDataSO.Grade;
        Desc = gearDataSO.Desc;
        Icon = gearDataSO.Icon;
        AttackAnimType = gearDataSO.AttackAnimType;
        Prefab = gearDataSO.Prefab;
        Level = level;
        Count = 1;
        EnhanceableCount = 10;
        AbilityDict = new Dictionary<StatType, int>(gearDataSO.GetAbilityDict_ByLevel(level));
    }

    /// <summary>
    /// ������ ������
    /// </summary>
    public void ItemLevelUp()
    {
        Level++;    // ������
        AbilityDict = new Dictionary<StatType, int>(GearDataSO.GetAbilityDict_ByLevel(Level));  // ������ �´� ���ο� ���ȵ� ����
    }

    /// <summary>
    /// ������ ������ ��ȭ ������ŭ �Һ�
    /// </summary>
    public void RemoveCountByEnhance() => Count -= EnhanceableCount;

    /// <summary>
    /// ��ȭ ��������?
    /// </summary>
    public bool CanEnhance() => Count >= EnhanceableCount;

    /// <summary>
    /// �����Ƽ ��ųʸ� ��������
    /// </summary>
    public Dictionary<StatType, int> GetAbilityDict() => AbilityDict;
}
