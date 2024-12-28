using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 가지고 있는 아이템 인벤토리
/// </summary>
public class ItemInven
{
    // 가지고 있는 아이템 인벤토리 딕셔너리 
    private static Dictionary<ItemType, List<Item>> _itemInvenDict = new Dictionary<ItemType, List<Item>>();   

    /// <summary>
    /// 인벤토리에 아이템 추가
    /// </summary>
    public static void AddItem(Item item)
    {
        TrySet_ItemInvenDict(item);

        // 가지고 있는 아이템이면 갯수만 추가
        Item existItem = HasItemInInven(item);
        if (existItem != null)
        {
            existItem.AddCount();
            return;
        }

        // 아이템 추가
        _itemInvenDict[item.ItemType].Add(item);
    }

    /// <summary>
    /// 가지고 있는 아이템인지 확인 후 반환
    /// </summary>
    public static Item HasItemInInven(Item item)
    {
        TrySet_ItemInvenDict(item);

        Item existItem = _itemInvenDict[item.ItemType].Find(x => x.ID == item.ID);

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
    private static void TrySet_ItemInvenDict(Item item)
    {
        if (_itemInvenDict.ContainsKey(item.ItemType) == false)
            _itemInvenDict[item.ItemType] = new List<Item>();
    }

    /// <summary>
    /// 아이템 타입에 맞는 아이템 인벤토리 가져오기
    /// </summary>
    public static List<Item> GetItemInvenByItemType(ItemType itemType)
    {
        if (_itemInvenDict.TryGetValue(itemType, out List<Item> itemInven))
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
    /// 해당 인벤토리에 강화 가능한 아이템이 있는지?
    /// </summary>
    public static bool HasEnhanceableItem(ItemType itemType)
    {
        if (_itemInvenDict.ContainsKey(itemType) == false)
        {
            Debug.Log("아직 아이템 인벤이 없어요.");
            return false;
        }

        Item existItem = _itemInvenDict[itemType].Find(item => item.IsEnhanceable());

        if (existItem != null)
            return true;
        else
            return false;
    }
}
