using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;











/// <summary>
/// 가지고 있는 아이템 인벤토리
/// </summary>
[System.Serializable]
public class ItemInven : ISavable
{
    public static event Action<Item> OnAddItem;                                     // 아이템 추가되었을 때 이벤트
    [SaveField] public static Dictionary<ItemType, List<Item>> _itemInvenDict;      // 가지고 있는 아이템 인벤토리 딕셔너리 

    public string Key => nameof(ItemInven);
    public void NotifyLoaded()
    {
        throw new NotImplementedException();
    }





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
        Item existItem = HasItemInInven(item); 
        
        if (existItem != null) 
        {
            // 가지고 있는 아이템이면 갯수만 추가
            existItem.AddCount();

            // 아이템추가 이벤트 노티
            OnAddItem?.Invoke(existItem); 
        }
        else
        {
            // 아니면 아이템 추가
            _itemInvenDict[item.ItemType].Add(item);

            // 아이템추가 이벤트 노티
            OnAddItem?.Invoke(item); 
        }

        
    }

    /// <summary>
    /// 가지고 있는 아이템인지 확인 후 반환
    /// </summary>
    public static Item HasItemInInven(Item item)
    {
        Item existItem = _itemInvenDict[item.ItemType].Find(x => x.ID == item.ID);

        if (existItem != null)
        {
            return existItem;
        }
        else
        {
            Debug.Log($"{item.Name} 은 처음 획득하는거에요.");
            return null;
        }
    }

    /// <summary>
    /// 아이템 타입에 맞는 아이템 인벤토리 가져오기
    /// </summary>
    public static List<Item> GetItemInven(ItemType itemType)
    {
        if (_itemInvenDict.TryGetValue(itemType, out List<Item> itemInven))
        {
            return itemInven;
        }
        else
        {
            Debug.Log($"{itemType} 타입 아이템 인벤토리가 없습니다.");
            return null;
        }
    }

    /// <summary>
    /// 아이템 타입별 인벤토리에 강화 가능한 아이템이 있는지?
    /// </summary>
    public static bool HasEnhanceableItem(ItemType itemType)
    {
        if (_itemInvenDict[itemType].
            Where(item => item is IEnhanceableItem).    // 인벤토리에서 IEnhanceableItem 를 모두 찾음
            Cast<IEnhanceableItem>().                   // 모든요소를 IEnhanceableItem 로 변환
            Any(enhanceableItem => enhanceableItem.CanEnhance()))   // 모든 IEnhanceableItem 중에 CanEnhance() 인 요소가 있는지 확인
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

    /// <summary>
    /// 이 아이템이 강화가능한지?
    /// </summary>
    public static bool CanEnhanceable_ThisItem(Item item)
    {
        Item existItem = HasItemInInven(item);

        if (existItem == null)
            return false;

        if (existItem is IEnhanceableItem enhanceableItem)
            return enhanceableItem.CanEnhance(); 

        return false;
    }

    
}
