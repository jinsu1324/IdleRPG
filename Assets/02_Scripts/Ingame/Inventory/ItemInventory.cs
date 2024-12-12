using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private List<Item> _itemList = new List<Item>();      // 가지고 있는 아이템 리스트

    /// <summary>
    /// 가지고 있는 아이템인지
    /// </summary>
    public Item HasItemInInventory(Item item)
    {
        Item existItem = _itemList.Find(x => x.ID == item.ID);

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
    public void AddInventory(Item item)
    {
        _itemList.Add(item);
    }
}
