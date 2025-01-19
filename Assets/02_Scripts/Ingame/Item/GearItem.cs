using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ������
/// </summary>
public class GearItem : Item, IEnhanceableItem
{
    public int Level { get; private set; }                                               // ����
    [JsonIgnore] public GearDataSO GearDataSO { get; private set; }                      // ��� ������
    [JsonIgnore] public Dictionary<StatType, float> AttributeDict { get; private set; }  // ������ �´� �Ӽ��� ��ųʸ�
    [JsonIgnore] public string AttackAnimType { get; private set; }                      // ���� �ִϸ��̼� Ÿ��
    [JsonIgnore] public GameObject Prefab { get; private set; }                          // ������ ������
    [JsonIgnore] public int EnhanceableCount { get; private set; }                       // ��ȭ ������ ����

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
        AttributeDict = new Dictionary<StatType, float>(gearDataSO.GetAttributeDict_ByLevel(level));
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
        AttributeDict = new Dictionary<StatType, float>(GearDataSO.GetAttributeDict_ByLevel(Level));  
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
    public Dictionary<StatType, float> GetAttributeDict() => AttributeDict;
}
