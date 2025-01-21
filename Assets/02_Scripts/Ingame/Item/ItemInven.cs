using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 가지고 있는 아이템 인벤토리
/// </summary>
public static class ItemInven
{
    public static event Action<Item> OnAddItem;                         // 아이템 추가되었을 때 이벤트
    public static Dictionary<ItemType, List<Item>> _itemInvenDict;      // 가지고 있는 아이템 인벤토리 딕셔너리 

    /// <summary>
    /// 정적 생성자 (클래스가 처음 참조될 때 한 번만 호출)
    /// </summary>
    static ItemInven()
    {
        _itemInvenDict = new Dictionary<ItemType, List<Item>>()
        {
            { ItemType.Weapon, new List<Item>() },
            { ItemType.Armor, new List<Item>() },
            { ItemType.Helmet, new List<Item>() },
            { ItemType.Skill, new List<Item>() }
        };
    }

    /// <summary>
    /// 인벤토리에 아이템 추가
    /// </summary>
    public static void AddItem(Item item)
    {
        Item hasItem = HasItem(item); 
        
        if (hasItem != null) 
        {
            int addCount = 1;
            hasItem.AddCount(addCount); // 가지고 있는 아이템이면 갯수만 추가
            OnAddItem?.Invoke(hasItem); 
        }
        else
        {
            _itemInvenDict[item.ItemType].Add(item);
            OnAddItem?.Invoke(item); 
        }
    }

    /// <summary>
    /// 가지고 있는 아이템인지 확인 후 반환
    /// </summary>
    public static Item HasItem(Item item)
    {
        Item foundItem = _itemInvenDict[item.ItemType].Find(x => x.ID == item.ID);

        if (foundItem != null)
            return foundItem;
        else
        {
            Debug.Log($"{item.ID} 은(는) 처음 획득하는거에요.");
            return null;
        }
    }

    /// <summary>
    /// 인벤토리에 강화 가능한 아이템이 있는지?
    /// </summary>
    public static bool HasEnhanceableItem(ItemType itemType)
    {
        if (_itemInvenDict[itemType].Any(item => ItemEnhanceManager.CanEnhance(item)))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 아이템 타입에 맞는 아이템 인벤토리 가져오기
    /// </summary>
    public static List<Item> GetItemInven(ItemType itemType)
    {
        if (_itemInvenDict.TryGetValue(itemType, out List<Item> itemInven))
            return itemInven;
        else
        {
            Debug.Log($"{itemType} 타입 아이템 인벤토리가 없습니다.");
            return null;
        }
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
