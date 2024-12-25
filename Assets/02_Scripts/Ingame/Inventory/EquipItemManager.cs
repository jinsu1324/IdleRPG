using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItemManager
{
    public static event Action<ItemType, Sprite> OnEquipItemChanged;  // ������ �������� ����Ǿ��� �� �̺�Ʈ

    // ������ ������ ��ųʸ�
    private static Dictionary<ItemType, Item> _equipItemDict = new Dictionary<ItemType, Item>
    { 
        { ItemType.Weapon, null},
        { ItemType.Armor, null},
        { ItemType.Helmet, null}
    };

    /// <summary>
    /// ����
    /// </summary>
    public static void Equip(Item item)
    {
        // �̹� ������ �������̸� �׳� ����
        if (IsEquipped(item))
        {
            Debug.Log("�̹� ������ �������Դϴ�!");
            return;
        }

        // ����
        _equipItemDict[item.ItemType] = item;
        Debug.Log($"{item.ItemType} �� {item.Name} �����Ϸ�!");

        // ������ �������� ����Ǿ��� �� �̺�Ʈ ����
        OnEquipItemChanged?.Invoke(item.ItemType, item.ItemSprite);

        // �÷��̾� ���ȿ� ������ ���ȵ� ���� �߰�
        PlayerStats.Instance.UpdateModifier(item.GetStatDict(), item);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public static void UnEquip(Item item)
    {
        // ������ �������� ������ �׳� ����
        if (IsEquipped(item) == false)
        {
            Debug.Log("������ �������� �����ϴ�.");
            return;
        }

        // ��������
        _equipItemDict[item.ItemType] = null;
        Debug.Log($"{item.ItemType} ���� {item.Name} ��������!");

        // ������ �������� ����Ǿ��� �� �̺�Ʈ ����
        OnEquipItemChanged?.Invoke(item.ItemType, null);

        // �÷��̾� ���ȿ� ������ ���ȵ� ���� ����
        PlayerStats.Instance.RemoveModifier(item.GetStatDict(), item);
    }

    /// <summary>
    /// ������ ����������?
    /// </summary>
    public static bool IsEquipped(Item item)
    {
        return _equipItemDict.ContainsKey(item.ItemType) && _equipItemDict[item.ItemType] == item;
    }

    /// <summary>
    /// ������ ������ ��������
    /// </summary>
    public static Item GetEquipItem(ItemType itemType)
    {
        if (_equipItemDict.TryGetValue(itemType, out Item equipItem))
        {
            return equipItem;
        }
        else
        {
            Debug.Log($"{itemType} �� ������ �������� �����ϴ�.");
            return null;
        }
    }
}
