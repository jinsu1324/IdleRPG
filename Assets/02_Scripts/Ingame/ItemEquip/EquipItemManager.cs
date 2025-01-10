using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public struct OnEquipGearChangedArgs
{
    public ItemType ItemType;
    public GameObject Prefab;
    public string AttackAnimType;
    public bool IsTryEquip;
}

/// <summary>
/// ������ ������ ����
/// </summary>
public class EquipItemManager
{
    public static event Action<OnEquipGearChangedArgs> OnEquipGearChanged;  // ���� �������� ����Ǿ��� �� �̺�Ʈ
    
    private static Dictionary<ItemType, Item[]> _equipItemDict;             // ������ ������ ��ųʸ�

    private static int _weaponMaxCount = 1;                                 // ���� �ִ� �������� ����
    private static int _armorMaxCount = 1;                                  // ���� �ִ� �������� ����
    private static int _HelmetMaxCount = 1;                                 // ��� �ִ� �������� ����
    private static int _SkillMaxCount = 3;                                  // ��ų �ִ� �������� ����

    /// <summary>
    /// ���� ������ (Ŭ������ ó�� ������ �� �� ���� ȣ��)
    /// </summary>
    static EquipItemManager()
    {
        _equipItemDict = new Dictionary<ItemType, Item[]>
        {
            { ItemType.Weapon, new Item[_weaponMaxCount]},
            { ItemType.Armor, new Item[_armorMaxCount]},
            { ItemType.Helmet, new Item[_HelmetMaxCount]},
            { ItemType.Skill, new Item[_SkillMaxCount]}
        };
    }

    /// <summary>
    /// ��� ����
    /// </summary>
    public static void Equip(Item item, int slotIndex)
    {
        // ���� �������� �������̸� �׳� ����
        if (IsEquipped(item))
            return;

        // �ٸ� �������� �����Ǿ� �ִٸ�, ���� ���� ����
        Item oldItem = GetEquippedItem(item.ItemType, slotIndex);
        if (oldItem != null)
            UnEquip(oldItem);

        // ���ο� ��ų ����
        _equipItemDict[item.ItemType][slotIndex] = item;

        Debug.Log($"������ ��������! {GetEquippedItem(item.ItemType, slotIndex).Name}");
    }

    /// <summary>
    /// ��� ���� ����
    /// </summary>
    public static void UnEquip(Item item)
    {
        // �ش� �������� ��� �ε����� �����Ǿ��ִ��� ã��
        int equippedIndex = FindEquippedSlotIndex(item);

        // �ε����� ã���� �����ٸ�(-1) �׳ɸ���
        if (IsIndexFound(equippedIndex) == false)
        {
            Debug.Log($"{item.Name} �������� �������� �ȿ� ��� ���������� �Ұ����մϴ�.");
            return;
        }

        // ��������
        Debug.Log($"������ ��������! {_equipItemDict[item.ItemType][equippedIndex].Name}");
        _equipItemDict[item.ItemType][equippedIndex] = null;


    }

    /// <summary>
    /// �ش� ���Կ� �����Ǿ��ִ� ������ ��������
    /// </summary>
    public static Item GetEquippedItem(ItemType itemType, int slotIndex)
    {
        return _equipItemDict[itemType][slotIndex];
    }

    /// <summary>
    /// ������ ����������?
    /// </summary>
    public static bool IsEquipped(Item item)
    {
        return _equipItemDict[item.ItemType].Contains(item);
    }

    /// <summary>
    /// �ش� �������� ��� ���Կ� �����Ǿ��ִ��� ã��
    /// </summary>
    private static int FindEquippedSlotIndex(Item item)
    {
        return Array.FindIndex(_equipItemDict[item.ItemType], x => x == item);
    }

    /// <summary>
    /// �ε����� ã�� �� �־�����?
    /// </summary>
    private static bool IsIndexFound(int equippedIndex)
    {
        if (equippedIndex >= 0)
            return true;
        else
            return false;
    }





















    /// <summary>
    /// �ִ�� �����ߴ���?
    /// </summary>
    private static bool IsEquipMax(ItemType itemType)
    {
        return GetEmptySlotIndex(itemType) == -1;
    }

    /// <summary>
    /// ����ִ� ���� �ε��� ��������
    /// </summary>
    private static int GetEmptySlotIndex(ItemType itemType)
    {
        return Array.FindIndex(_equipItemDict[itemType], s => s == null);
    }
}
