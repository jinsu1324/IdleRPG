using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ������ ��� ����
/// </summary>
public class EquipGearManager
{
    public static event Action<Item> OnEquipGear;               // ��� ������ �� �̺�Ʈ
    public static event Action<Item> OnUnEquipGear;             // ��� ������ �� �̺�Ʈ
    private static Dictionary<ItemType, Item> _equipGearDict;   // ������ ��� ��ųʸ�

    /// <summary>
    /// ���� ������ (Ŭ������ ó�� ������ �� �� ���� ȣ��)
    /// </summary>
    static EquipGearManager()
    {
        _equipGearDict = new Dictionary<ItemType, Item>();
    }

    /// <summary>
    /// ����
    /// </summary>
    public static void Equip(Item item)
    {
        // �̹� �� �������� �������̸� �׳� ����
        if (IsEquipped(item))
            return;

        // �ٸ� �������� ���� �����Ǿ� �ִٸ�, ���� ����
        Item oldItem = GetEquippedItem(item.ItemType);
        if (oldItem != null)
            UnEquip(oldItem);

        // ���ο� ������ ����
        _equipGearDict[item.ItemType] = item;
        
        // ���� �̺�Ʈ ��Ƽ
        OnEquipGear?.Invoke(item);

        // �÷��̾� ���� ������Ʈ
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);
        if (itemDataSO is GearDataSO gearDataSO)
        {
            PlayerStatUpdateArgs args = new PlayerStatUpdateArgs()
            {
                DetailStatDict = gearDataSO.GetGearStats(item.Level),
                Source = item
            };
            PlayerStats.UpdateStatModifier(args);
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public static void UnEquip(Item item)
    {
        // ����
        _equipGearDict[item.ItemType] = null;

        // ���� �̺�Ʈ ��Ƽ
        OnUnEquipGear?.Invoke(item);

        // �÷��̾� ���� ����
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);
        if (itemDataSO is GearDataSO gearDataSO)
        {
            PlayerStatUpdateArgs args = new PlayerStatUpdateArgs()
            {
                DetailStatDict = gearDataSO.GetGearStats(item.Level),
                Source = item
            };
            PlayerStats.RemoveStatModifier(args);
        }

    }

    /// <summary>
    /// ���� ��ųʸ��� ������ ����
    /// </summary>
    private static void TryCreateDict(ItemType itemType)
    {
        if (_equipGearDict.ContainsKey(itemType) == false)
            _equipGearDict[itemType] = null;
    }

    /// <summary>
    /// ������ ����������?
    /// </summary>
    public static bool IsEquipped(Item item)
    {
        TryCreateDict(item.ItemType);
        return _equipGearDict[item.ItemType] == item;
    }

    /// <summary>
    /// ������ ������ ��������
    /// </summary>
    public static Item GetEquippedItem(ItemType itemType)
    {
        TryCreateDict(itemType);
        return _equipGearDict[itemType];
    }
}
