using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 가지고 있는 아이템 인벤토리
/// </summary>
public class ItemInven
{
    // 가지고 있는 아이템이 변경되었을 때 이벤트
    public static event Action OnItemInvenChanged;

    // 가지고 있는 아이템 인벤토리 딕셔너리 
    private static Dictionary<ItemType, List<IItem>> _itemInvenDict = new Dictionary<ItemType, List<IItem>>();   

    /// <summary>
    /// 인벤토리에 아이템 추가
    /// </summary>
    public static void AddItem(IItem item)
    {
        TrySet_ItemInvenDict(item);
        
        IItem existItem = HasItemInInven(item); // 가지고 있는 아이템이면 갯수만 추가
        if (existItem != null)
        {
            existItem.AddCount();
        }
        else
        {
            _itemInvenDict[item.ItemType].Add(item); // 아니면 아이템 추가
        }

        OnItemInvenChanged?.Invoke(); // 가지고 있는 아이템이 변경되었을 때 이벤트 호출
    }

    /// <summary>
    /// 아이템 강화
    /// </summary>
    public static void Enhance(IItem item)
    {
        item.RemoveCountByEnhance();    // 아이템 갯수 감소
        item.ItemLevelUp();             // 아이템 레벨업

        // 장비일때만
        if (item is Gear gear)
        {
            // 해당 아이템이 장착되어 있을때만
            if (EquipGearManager.IsEquippedGear(gear))
            {
                // 플레이어 스탯에 아이템 스탯들 전부 추가
                PlayerStats.UpdateStatModifier(gear.GetAbilityDict(), item);
            }
        }

        OnItemInvenChanged?.Invoke(); // 가지고 있는 아이템이 변경되었을 때 이벤트 호출
    }


    /// <summary>
    /// 가지고 있는 아이템인지 확인 후 반환
    /// </summary>
    public static IItem HasItemInInven(IItem item)
    {
        TrySet_ItemInvenDict(item);

        IItem existItem = _itemInvenDict[item.ItemType].Find(x => x.ID == item.ID);

        if (existItem != null)
        {
            return existItem;
        }
        else
        {
            Debug.Log($"{item.ID} 아이템을 가지고 있지 않습니다.");
            return null;
        }
    }

    /// <summary>
    /// 딕셔너리 없으면 새로 만듦
    /// </summary>
    private static void TrySet_ItemInvenDict(IItem item)
    {
        if (_itemInvenDict.ContainsKey(item.ItemType) == false)
            _itemInvenDict[item.ItemType] = new List<IItem>();
    }

    /// <summary>
    /// 아이템 타입에 맞는 아이템 인벤토리 가져오기
    /// </summary>
    public static List<IItem> GetItemInvenByItemType(ItemType itemType)
    {
        if (_itemInvenDict.TryGetValue(itemType, out List<IItem> itemInven))
        {
            return itemInven;
        }
        else
        {
            Debug.Log("아직 아이템 인벤이 없어요.");
            return null;
        }
    }

    /// <summary>
    /// 아이템 타입별 인벤토리에 강화 가능한 아이템이 있는지?
    /// </summary>
    public static bool HasEnhanceableItem(ItemType itemType)
    {
        if (_itemInvenDict.ContainsKey(itemType) == false)
        {
            Debug.Log($"아직 {itemType} 타입의 인벤토리 자체가 없어요.");
            return false;
        }

        if (_itemInvenDict[itemType].Exists(item => item.IsEnhanceable()))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 인벤토리에 강화 가능한 장비가 있는지?
    /// </summary>
    public static bool HasEnhanceableGear()
    {
        bool isEnhanceable_Weapon = HasEnhanceableItem(ItemType.Weapon);
        bool isEnhanceable_Armor = HasEnhanceableItem(ItemType.Armor);
        bool isEnhanceable_Helmet = HasEnhanceableItem(ItemType.Helmet);

        // 장비 확인해서 한개라도 true면 true반환
        if (isEnhanceable_Weapon || isEnhanceable_Armor || isEnhanceable_Helmet)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 인벤토리에 강화 가능한 스킬이 있는지?
    /// </summary>
    public static bool HasEnhanceableSkill()
    {
        bool isEnhanceable_Skill = HasEnhanceableItem(ItemType.Skill);

        if (isEnhanceable_Skill)
            return true;
        else
            return false;
    }
}
