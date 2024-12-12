using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۽��Ե��� ������, Ư�� ���ǿ� �´� �����۽����� ã���ִ� �ӽ�
/// </summary>
public class ItemSlotFinder
{
    /// <summary>
    /// Ư�� �������� ����ִ� ���� ã��
    /// </summary>
    public static ItemSlot FindSlot_ContainItem(Item item, List<ItemSlot> itemSlotList)
    {
        foreach (ItemSlot slot in itemSlotList)
        {
            if (slot.CurrentItem == item)
                return slot;
        }

        Debug.Log($"{item.ID} �� ����ִ� ������ ã�� �� �����ϴ�!");
        return null;
    }

    /// <summary>
    /// ����ִ� ���� ã��
    /// </summary>
    public static ItemSlot FindSlot_Empty(List<ItemSlot> itemSlotList)
    {
        foreach (ItemSlot slot in itemSlotList)
        {
            if (slot.IsSlotEmpty)
                return slot;
        }

        Debug.Log("����ִ� ������ ã�� �� �����ϴ�!");
        return null;
    }
}
