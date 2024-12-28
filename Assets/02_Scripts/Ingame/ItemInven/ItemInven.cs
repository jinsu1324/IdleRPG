using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �ִ� ������ �κ��丮
/// </summary>
public class ItemInven
{
    // ������ �ִ� �������� ����Ǿ��� �� �̺�Ʈ
    public static event Action OnItemInvenChanged;

    // ������ �ִ� ������ �κ��丮 ��ųʸ� 
    private static Dictionary<ItemType, List<Item>> _itemInvenDict = new Dictionary<ItemType, List<Item>>();   

    /// <summary>
    /// �κ��丮�� ������ �߰�
    /// </summary>
    public static void AddItem(Item item)
    {
        TrySet_ItemInvenDict(item);
        
        Item existItem = HasItemInInven(item); // ������ �ִ� �������̸� ������ �߰�
        if (existItem != null)
        {
            existItem.AddCount();
        }
        else
        {
            _itemInvenDict[item.ItemType].Add(item); // �ƴϸ� ������ �߰�
        }

        OnItemInvenChanged?.Invoke(); // ������ �ִ� �������� ����Ǿ��� �� �̺�Ʈ ȣ��
    }

    /// <summary>
    /// ������ �ִ� ���������� Ȯ�� �� ��ȯ
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
            Debug.Log($"{item.ID} �������� ������ ���� �ʽ��ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ��ųʸ� ������ ���� ����
    /// </summary>
    private static void TrySet_ItemInvenDict(Item item)
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


    /// <summary>
    /// �ش� �κ��丮�� ��ȭ ������ �������� �ִ���?
    /// </summary>
    public static bool HasEnhanceableItem(ItemType itemType)
    {
        if (_itemInvenDict.ContainsKey(itemType) == false)
        {
            Debug.Log("���� ������ �κ��� �����.");
            return false;
        }

        if (_itemInvenDict[itemType].Exists(item => item.IsEnhanceable()))
            return true;
        else
            return false;
    }


    /// <summary>
    /// ��ü �κ��丮�� ��ȭ ������ �������� �ִ���?
    /// </summary>
    public static bool HasEnhanceableItemAllInven()
    {
        // ��ųʸ��� ��� Ÿ��(ItemType)�� �ش� ������ ����Ʈ�� Ž��
        foreach (var kvp in _itemInvenDict)
        {
            // �ش� Ÿ��(ItemType)�� ����Ʈ�� ��ȭ ������ �������� �ϳ��� ������ true ��ȯ
            if (kvp.Value.Exists(item => item.IsEnhanceable()))
            {
                return true;
            }
        }

        // ��� Ÿ���� Ȯ�������� ��ȭ ������ �������� ������ false ��ȯ
        return false;
    }
}
