using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private readonly ItemDataSO _baseData;                      // ������ ������
    public string ID { get; private set; }                      // ID
    public string Name { get; private set; }                    // �̸�
    public Sprite Icon { get; private set; }                    // ������

    private Dictionary<StatType, int> _statDictionaryByLevel;   // �������� �����ϴ� ���ȵ� ��ųʸ� (������ �°�)

    public int Level { get; private set; }  // ����
    public int Count { get; private set; }  // ����
    public int EnhanceableCount { get; private set; }   // ��ȭ ������ ����

    public ItemType ItemType { get; private set; }  // �������� ���� ������ Ÿ��

    /// <summary>
    /// ������
    /// </summary>
    public Item(ItemDataSO baseData, int level)
    {
        _baseData = baseData;
        ID = _baseData.ID;
        Name = _baseData.Name;
        Icon = _baseData.Icon;
        Level = level;
        ItemType = (ItemType)Enum.Parse(typeof(ItemType), _baseData.ItemType) ;

        Count = 1;
        EnhanceableCount = 10;

        // ������ �������� level�� �´� ���ȵ��� ��ųʸ��� ��������
        _statDictionaryByLevel = new Dictionary<StatType, int>(_baseData.GetStatDictionaryByLevel(level));
    }

    /// <summary>
    /// ��� �����ϴ� ���ȵ� ��ųʸ� ���� (������ �°�)
    /// </summary>
    public Dictionary<StatType, int> GetStatDictionaryByLevel() => _statDictionaryByLevel;



    public void AddCount()
    {
        Count++;
    }

    public bool IsEnhanceable()
    {
        return Count >= EnhanceableCount;
    }

    public void Enhance()
    {
        Debug.Log("��ȭ��ư ���Ƚ��ϴ�! ��ȭ�մϴ�!");

        Count -= EnhanceableCount; // ���� �Һ�

        Level++;    // ���� ����

        _statDictionaryByLevel = new Dictionary<StatType, int>(_baseData.GetStatDictionaryByLevel(Level));  // ������ �´� ���ο� ���ȵ� ����

        //ItemSlotFinder.FindSlot_ContainItem(this, ItemSlotManager.Instance.GetItemSlotList()).UpdateItemInfoUI();
        ItemSlotFinder.FindSlot_ContainItem(this, InventoryManager.Instance.GetItemSlotManager(ItemType).GetItemSlotList()).UpdateItemInfoUI();
    }
}
