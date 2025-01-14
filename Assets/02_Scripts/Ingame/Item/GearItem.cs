using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ������
/// </summary>
public class GearItem : Item, IEnhanceableItem
{
    public GearDataSO GearDataSO { get; private set; }                      // ��� ������
    public Dictionary<StatType, int> AttributeDict { get; private set; }    // ������ �´� �Ӽ��� ��ųʸ�
    public string AttackAnimType { get; private set; }                      // ���� �ִϸ��̼� Ÿ��
    public GameObject Prefab { get; private set; }                          // ������ ������
    public int Level { get; private set; }                                  // ����
    public int EnhanceableCount { get; private set; }                       // ��ȭ ������ ����

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
        Count = 1;
        Icon = gearDataSO.Icon;
        AttributeDict = new Dictionary<StatType, int>(gearDataSO.GetAttributeDict_ByLevel(level));
        AttackAnimType = gearDataSO.AttackAnimType;
        Prefab = gearDataSO.Prefab;
        Level = level;
        EnhanceableCount = 10;
    }

    /// <summary>
    /// ������ ������
    /// </summary>
    public void ItemLevelUp()
    {
        // ������
        Level++;    

        // �Ӽ��� ������ �°� �ֽ�ȭ
        AttributeDict = new Dictionary<StatType, int>(GearDataSO.GetAttributeDict_ByLevel(Level));  
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
    /// ���� �� �����ۿ� �ִ� �Ӽ��� ��ųʸ� ��������
    /// </summary>
    public Dictionary<StatType, int> GetAttributeDict() => AttributeDict;
}
