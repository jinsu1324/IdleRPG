using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInven
{
    private static Dictionary<ItemType, List<Item>> _itemInvenDict = new Dictionary<ItemType, List<Item>>();  // ������ �ִ� ������ �κ��丮 ��ųʸ�  

    /// <summary>
    /// �κ��丮�� ������ �߰�
    /// </summary>
    public static void AddItem(Item item)
    {
        CheckAndMakeItemInvenDict(item);

        _itemInvenDict[item.ItemType].Add(item);    // ������ �߰�
    }

    /// <summary>
    /// ������ �ִ� ���������� Ȯ�� �� ��ȯ
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
            Debug.Log($"{item.ID} �������� ������ ���� �ʽ��ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ��ųʸ� ������ ���� ����
    /// </summary>
    private static void CheckAndMakeItemInvenDict(Item item)
    {
        if (_itemInvenDict.ContainsKey(item.ItemType) == false)
            _itemInvenDict[item.ItemType] = new List<Item>();
    }

    /// <summary>
    /// ������ Ÿ�Կ� �´� ������ �κ��丮 ��������
    /// </summary>
    public static List<Item> GetItemInvenByItemType(ItemType itemType)
    {
        if (_itemInvenDict.TryGetValue(itemType, out List<Item> itemInven))
        {
            return itemInven;
        }
        else
        {
            Debug.Log("���� ������ �κ��� �����.");
            return null;
        }
    }





    // �ӽ� ����� ������ �ִ� �����۵� üũ��!
    public static void CheckCurrentItemInven(Item item)
    {
        for (int i = 0; i < _itemInvenDict[item.ItemType].Count; i++)
        {
            Debug.Log($"{item.ItemType} �κ��丮�� ������ : {_itemInvenDict[item.ItemType][i].Name} {_itemInvenDict[item.ItemType][i].Count}");
        }
    }
}
