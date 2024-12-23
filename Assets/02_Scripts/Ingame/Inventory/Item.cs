using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private readonly ItemDataSO _baseData;                      // 아이템 데이터
    public string ID { get; private set; }                      // ID
    public ItemType ItemType { get; private set; }              // 아이템이 속한 아이템 타입
    public string Name { get; private set; }                    // 이름
    public string Grade { get; private set; }                   // 등급
    public Sprite Icon { get; private set; }                    // 아이콘
    public int Level { get; private set; }                      // 레벨
    public int Count { get; private set; }                      // 갯수
    public int EnhanceableCount { get; private set; }           // 강화 가능한 갯수

    private Dictionary<StatType, int> _statDict;                // 아이템이 제공하는 스탯들 딕셔너리 (레벨에 맞게)

    /// <summary>
    /// 생성자
    /// </summary>
    public Item(ItemDataSO baseData, int level)
    {
        _baseData = baseData;
        ID = baseData.ID;
        ItemType = (ItemType)Enum.Parse(typeof(ItemType), baseData.ItemType) ;
        Name = baseData.Name;
        Grade = baseData.Grade;
        Icon = baseData.Icon;
        Level = level;
        Count = 1;
        EnhanceableCount = 10;
        _statDict = new Dictionary<StatType, int>(_baseData.GetStatDictByLevel(level));
    }

    /// <summary>
    /// 스탯들 딕셔너리 가져오기
    /// </summary>
    public Dictionary<StatType, int> GetStatDict() => _statDict;

    /// <summary>
    /// 아이템 갯수 추가
    /// </summary>
    public void AddCount() => Count++;

    /// <summary>
    /// 아이템 갯수 강화갯수만큼 소비
    /// </summary>
    public void RemoveCountByEnhance() => Count -= EnhanceableCount;

    /// <summary>
    /// 아이템 레벨업
    /// </summary>
    public void ItemLevelUp()
    {
        Level++;    // 레벨업
        _statDict = new Dictionary<StatType, int>(_baseData.GetStatDictByLevel(Level));  // 레벨에 맞는 새로운 스탯들 적용
    }

    /// <summary>
    /// 강화 가능한지?
    /// </summary>
    public bool IsEnhanceable() => Count >= EnhanceableCount;
}
