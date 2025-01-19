using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장비 아이템
/// </summary>
public class GearItem : Item, IEnhanceableItem
{
    public int Level { get; private set; }                                               // 레벨
    [JsonIgnore] public GearDataSO GearDataSO { get; private set; }                      // 장비 데이터
    [JsonIgnore] public Dictionary<StatType, float> AttributeDict { get; private set; }  // 레벨에 맞는 속성들 딕셔너리
    [JsonIgnore] public string AttackAnimType { get; private set; }                      // 공격 애니메이션 타입
    [JsonIgnore] public GameObject Prefab { get; private set; }                          // 아이템 프리팹
    [JsonIgnore] public int EnhanceableCount { get; private set; }                       // 강화 가능한 갯수

    /// <summary>
    /// 초기화
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
    /// 아이템 레벨업
    /// </summary>
    public void ItemLevelUp()
    {
        // 레벨업
        Level++;    

        // 속성들 레벨에 맞게 최신화
        AttributeDict = new Dictionary<StatType, float>(GearDataSO.GetAttributeDict_ByLevel(Level));  
    }

    /// <summary>
    /// 아이템 갯수를 강화 갯수만큼 소비
    /// </summary>
    public void RemoveCountByEnhance() => Count -= EnhanceableCount;

    /// <summary>
    /// 강화 가능한지?
    /// </summary>
    public bool CanEnhance() => Count >= EnhanceableCount;

    /// <summary>
    /// 현재 이 아이템에 있는 속성들 딕셔너리 가져오기
    /// </summary>
    public Dictionary<StatType, float> GetAttributeDict() => AttributeDict;
}
