using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ������ ��� ����
/// </summary>
public class EquipGearManager// : ISavable
{
    public string Key => nameof(EquipGearManager);  // Firebase ������ ����� ���� Ű ����

    public static event Action<Item> OnEquipGear;   // ��� ������ �� �̺�Ʈ
    public static event Action<Item> OnUnEquipGear; // ��� ������ �� �̺�Ʈ

    [SaveField] private static Dictionary<ItemType, Item> _equipGearDict = new Dictionary<ItemType, Item>(); // ������ ��� ��ųʸ�

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
    /// ������ ����������?
    /// </summary>
    public static bool IsEquipped(Item item)
    {
        CheckAnd_SetDict(item.ItemType);
        return _equipGearDict[item.ItemType] == item;
    }

    /// <summary>
    /// ������ ������ ��������
    /// </summary>
    public static Item GetEquippedItem(ItemType itemType)
    {
        CheckAnd_SetDict(itemType);
        return _equipGearDict[itemType];
    }

    /// <summary>
    /// ��ųʸ��� Ű üũ�غ��� ������ ��ųʸ� �����
    /// </summary>
    private static void CheckAnd_SetDict(ItemType itemType)
    {
        if (_equipGearDict.ContainsKey(itemType) == false)
            _equipGearDict[itemType] = null;
    }
}
