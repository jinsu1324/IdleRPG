using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private readonly GearDataSO _baseData;                      // ������ ������
    public string ID { get; private set; }                      // ID
    public ItemType ItemType { get; private set; }              // �������� ���� ������ Ÿ��
    public string Name { get; private set; }                    // �̸�
    public string Grade { get; private set; }                   // ���
    public string AttackAnimType { get; private set; }          // ���� �ִϸ��̼� Ÿ��
    public GameObject Prefab { get; private set; }              // ������ ������
    public Sprite Icon { get; private set; }                    // ������
    public int Level { get; private set; }                      // ����
    public int Count { get; private set; }                      // ����
    public int EnhanceableCount { get; private set; }           // ��ȭ ������ ����

    private Dictionary<StatType, int> _statDict;                // �������� �����ϴ� ���ȵ� ��ųʸ� (������ �°�)

    /// <summary>
    /// ������
    /// </summary>
    public Item(GearDataSO baseData, int level)
    {
        _baseData = baseData;
        ID = baseData.ID;
        ItemType = (ItemType)Enum.Parse(typeof(ItemType), baseData.ItemType) ;
        Name = baseData.Name;
        Grade = baseData.Grade;
        AttackAnimType = baseData.AttackAnimType;
        Prefab = baseData.Prefab;
        Icon = baseData.Icon;
        Level = level;
        Count = 1;
        EnhanceableCount = 10;
        _statDict = new Dictionary<StatType, int>(_baseData.GetAbilityDict_ByLevel(level));
    }

    /// <summary>
    /// ���ȵ� ��ųʸ� ��������
    /// </summary>
    public Dictionary<StatType, int> GetStatDict() => _statDict;

    /// <summary>
    /// ������ ���� �߰�
    /// </summary>
    public void AddCount() => Count++;

    /// <summary>
    /// ������ ���� ��ȭ������ŭ �Һ�
    /// </summary>
    public void RemoveCountByEnhance() => Count -= EnhanceableCount;

    /// <summary>
    /// ������ ������
    /// </summary>
    public void ItemLevelUp()
    {
        Level++;    // ������
        _statDict = new Dictionary<StatType, int>(_baseData.GetAbilityDict_ByLevel(Level));  // ������ �´� ���ο� ���ȵ� ����
    }

    /// <summary>
    /// ��ȭ ��������?
    /// </summary>
    public bool IsEnhanceable() => Count >= EnhanceableCount;
}
