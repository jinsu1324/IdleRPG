using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInven
{
    private static Dictionary<ItemType, List<Item>> _itemInvenDict = new Dictionary<ItemType, List<Item>>();  // 가지고 있는 아이템 인벤토리 딕셔너리  

    /// <summary>
    /// 인벤토리에 아이템 추가
    /// </summary>
    public static void AddItem(Item item)
    {
        CheckAndMakeItemInvenDict(item);

        _itemInvenDict[item.ItemType].Add(item);    // 아이템 추가
    }

    /// <summary>
    /// 가지고 있는 아이템인지 확인 후 반환
    /// </summary>
    public static Item HasItemInInven(Item item)
    {
        CheckAndMakeItemInvenDict(item);

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
    private static void CheckAndMakeItemInvenDict(Item item)
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





    // 임시 디버그 가지고 있는 아이템들 체크용!
    public static void CheckCurrentItemInven(Item item)
    {
        for (int i = 0; i < _itemInvenDict[item.ItemType].Count; i++)
        {
            Debug.Log($"{item.ItemType} 인벤토리의 아이템 : {_itemInvenDict[item.ItemType][i].Name} {_itemInvenDict[item.ItemType][i].Count}");
        }
    }
}
