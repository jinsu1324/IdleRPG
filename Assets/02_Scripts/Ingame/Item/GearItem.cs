using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장비 아이템
/// </summary>
public class GearItem : Item, IEnhanceableItem
{
    public GearDataSO GearDataSO { get; private set; }  // 장비 데이터
    public string AttackAnimType { get; private set; }  // 공격 애니메이션 타입
    public GameObject Prefab { get; private set; }      // 아이템 프리팹
    public Dictionary<StatType, int> AbilityDict { get; private set; }  // 아이템이 제공하는 어빌리티들 딕셔너리

    public int Level { get; private set; }
    public int EnhanceableCount { get; private set; }

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
        Icon = gearDataSO.Icon;
        AttackAnimType = gearDataSO.AttackAnimType;
        Prefab = gearDataSO.Prefab;
        Level = level;
        Count = 1;
        EnhanceableCount = 10;
        AbilityDict = new Dictionary<StatType, int>(gearDataSO.GetAbilityDict_ByLevel(level));
    }

    /// <summary>
    /// 아이템 레벨업
    /// </summary>
    public void ItemLevelUp()
    {
        Level++;    // 레벨업
        AbilityDict = new Dictionary<StatType, int>(GearDataSO.GetAbilityDict_ByLevel(Level));  // 레벨에 맞는 새로운 스탯들 적용
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
    /// 어빌리티 딕셔너리 가져오기
    /// </summary>
    public Dictionary<StatType, int> GetAbilityDict() => AbilityDict;
}
