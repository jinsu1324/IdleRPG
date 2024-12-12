using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveItemContainer
{
    private static List<Item> _haveItemList = new List<Item>();      // ������ �ִ� ������ ����Ʈ

    /// <summary>
    /// ������ �ִ� ���������� üũ
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
            Debug.Log($"{item.ID} �������� ������ ���� �ʽ��ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ������ �ִ� ������ ����Ʈ�� �߰�
    /// </summary>
    public static void AddHaveItemList(Item item)
    {
        _haveItemList.Add(item);
    }
}
