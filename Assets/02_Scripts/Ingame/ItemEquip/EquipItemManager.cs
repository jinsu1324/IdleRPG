using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct OnEquipItemChangedArgs
{
    public ItemType ItemType;
    public GameObject Prefab;
    public string AttackAnimType;
}

/// <summary>
/// ������ ������ ����
/// </summary>
public class EquipItemManager
{
    // ���� �������� ����Ǿ��� �� �̺�Ʈ
    public static event Action<OnEquipItemChangedArgs, bool> OnEquipItemChanged;      

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

        // �̹� ���Կ� �ٸ� ������ �������� ������ ���� ��������
        if (_equipItemDict.TryGetValue(item.ItemType, out Item equippedItem))
        {
            if (equippedItem != null)
                UnEquip(equippedItem);
        }

        // ����
        _equipItemDict[item.ItemType] = item;

        // ���� �������� ����Ǿ��� �� �̺�Ʈ ����
        OnEquipItemChangedArgs args = new OnEquipItemChangedArgs() { ItemType = item.ItemType, Prefab = item.Prefab, AttackAnimType = item.AttackAnimType };
        OnEquipItemChanged?.Invoke(args, true);

        // �÷��̾� ���ȿ� ������ ���ȵ� ���� �߰�
        PlayerStats.UpdateStatModifier(item.GetStatDict(), item);
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

        // ���� �������� ����Ǿ��� �� �̺�Ʈ ����
        OnEquipItemChangedArgs args = new OnEquipItemChangedArgs() { ItemType = item.ItemType, Prefab = null, AttackAnimType = AttackAnimType.Hand.ToString() };
        OnEquipItemChanged?.Invoke(args, false);

        // �÷��̾� ���ȿ� ������ ���ȵ� ���� ����
        PlayerStats.RemoveStatModifier(item.GetStatDict(), item);
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
