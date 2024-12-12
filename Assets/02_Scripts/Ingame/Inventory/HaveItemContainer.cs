using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveItemContainer
{
    private static List<Item> _haveItemList = new List<Item>();      // 가지고 있는 아이템 리스트

    /// <summary>
    /// 가지고 있는 아이템인지 체크
    /// </summary>
    public static Item HaveItemCheck(Item item)
    {
        Item existItem = _haveItemList.Find(x => x.ID == item.ID);

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
    /// 가지고 있는 아이템 리스트에 추가
    /// </summary>
    public static void AddHaveItemList(Item item)
    {
        _haveItemList.Add(item);
    }
}
