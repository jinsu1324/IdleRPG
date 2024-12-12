using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private readonly ItemDataSO _baseData;                      // 아이템 데이터
    public string ID { get; private set; }                      // ID
    public string Name { get; private set; }                    // 이름
    public Sprite Icon { get; private set; }                    // 아이콘

    private Dictionary<StatType, int> _statDictionaryByLevel;   // 아이템이 제공하는 스탯들 딕셔너리 (레벨에 맞게)

    public int Level { get; private set; }  // 레벨
    public int Count { get; private set; }  // 갯수
    public int EnhanceableCount { get; private set; }   // 강화 가능한 갯수

    public ItemType ItemType { get; private set; }  // 아이템이 속한 아이템 타입

    /// <summary>
    /// 생성자
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

        // 생성된 아이템의 level에 맞는 스탯들을 딕셔너리로 가져오기
        _statDictionaryByLevel = new Dictionary<StatType, int>(_baseData.GetStatDictionaryByLevel(level));
    }

    /// <summary>
    /// 장비가 제공하는 스탯들 딕셔너리 리턴 (레벨에 맞게)
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
        Debug.Log("강화버튼 눌렸습니다! 강화합니다!");

        Count -= EnhanceableCount; // 갯수 소비

        Level++;    // 레벨 증가

        _statDictionaryByLevel = new Dictionary<StatType, int>(_baseData.GetStatDictionaryByLevel(Level));  // 레벨에 맞는 새로운 스탯들 적용

        //ItemSlotFinder.FindSlot_ContainItem(this, ItemSlotManager.Instance.GetItemSlotList()).UpdateItemInfoUI();
        ItemSlotFinder.FindSlot_ContainItem(this, InventoryManager.Instance.GetItemSlotManager(ItemType).GetItemSlotList()).UpdateItemInfoUI();
    }
}
