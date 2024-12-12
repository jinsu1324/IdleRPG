using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템슬롯들을 뒤져서, 특정 조건에 맞는 아이템슬롯을 찾아주는 머신
/// </summary>
public class ItemSlotFinder
{
    /// <summary>
    /// 특정 아이템이 들어있는 슬롯 찾기
    /// </summary>
    public static ItemSlot FindSlot_ContainItem(Item item, List<ItemSlot> itemSlotList)
    {
        foreach (ItemSlot slot in itemSlotList)
        {
            if (slot.CurrentItem == item)
                return slot;
        }

        Debug.Log($"{item.ID} 가 들어있는 슬롯을 찾을 수 없습니다!");
        return null;
    }

    /// <summary>
    /// 비어있는 슬롯 찾기
    /// </summary>
    public static ItemSlot FindSlot_Empty(List<ItemSlot> itemSlotList)
    {
        foreach (ItemSlot slot in itemSlotList)
        {
            if (slot.IsSlotEmpty)
                return slot;
        }

        Debug.Log("비어있는 슬롯을 찾을 수 없습니다!");
        return null;
    }
}
