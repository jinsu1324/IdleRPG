using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 가지고 있는 아이템 인벤토리
/// </summary>
public class ItemInven : ISavable
{
    public string Key => nameof(ItemInven);         // 데이터 저장에 사용될 고유 키
    
    public static event Action<Item> OnAddItem;     // 아이템 추가되었을 때 이벤트

    [SaveField] // 가지고 있는 아이템 인벤토리 딕셔너리 
    public static Dictionary<ItemType, List<Item>> _itemInvenDict = new Dictionary<ItemType, List<Item>>();

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
        CheckAnd_SetDict(item.ItemType);

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
        CheckAnd_SetDict(itemType);

        if (_itemInvenDict.TryGetValue(itemType, out List<Item> itemInven))
        {
            if (itemInven.Any(item => ItemEnhanceManager.CanEnhance(item)))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    /// <summary>
    /// 아이템 타입에 맞는 아이템 인벤토리 가져오기
    /// </summary>
    public static List<Item> GetItemInven(ItemType itemType)
    {
        CheckAnd_SetDict(itemType);

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

    /// <summary>
    /// 딕셔너리에 키 체크해보고 없으면 딕셔너리 만들기
    /// </summary>
    private static void CheckAnd_SetDict(ItemType itemType)
    {
        if (_itemInvenDict.ContainsKey(itemType) == false)
            _itemInvenDict[itemType] = new List<Item>();
    }
}
