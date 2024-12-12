using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private List<Item> _itemList = new List<Item>();      // ������ �ִ� ������ ����Ʈ

    /// <summary>
    /// ������ �ִ� ����������
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
            Debug.Log($"{item.ID} �������� ������ ���� �ʽ��ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ������ �ִ� ������ ����Ʈ�� �߰�
    /// </summary>
    public void AddInventory(Item item)
    {
        _itemList.Add(item);
    }
}
