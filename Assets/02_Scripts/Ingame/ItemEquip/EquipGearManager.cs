using System;
using System.Collections;
using System.Collections.Generic;
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
/// ������ ��� ����
/// </summary>
public class EquipGearManager
{
    // ���� ��� ����Ǿ��� �� �̺�Ʈ
    public static event Action<OnEquipGearChangedArgs> OnEquipGearChanged;

    // ������ ��� ��ųʸ�
    private static Dictionary<ItemType, Gear> _equipGearDict = new Dictionary<ItemType, Gear>
    { 
        { ItemType.Weapon, null},
        { ItemType.Armor, null},
        { ItemType.Helmet, null}
    };

    /// <summary>
    /// ��� ����
    /// </summary>
    public static void EquipGear(Gear gear)
    {
        // �̹� ������ �������̸� �׳� ����
        if (IsEquippedGear(gear))
            return;

        // �̹� ���Կ� �ٸ� ������ �������� ������ ���� ��������
        if (_equipGearDict.TryGetValue(gear.ItemType, out Gear equippedItem))
        {
            if (equippedItem != null)
                UnEquipGear(equippedItem);
        }

        // ����
        _equipGearDict[gear.ItemType] = gear;

        // ������ ��� ����Ǿ��� �� �̺�Ʈ ����
        OnEquipGearChangedArgs args = new OnEquipGearChangedArgs() 
        { 
            ItemType = gear.ItemType, 
            Prefab = gear.Prefab, 
            AttackAnimType = gear.AttackAnimType, 
            IsTryEquip = true 
        };
        OnEquipGearChanged?.Invoke(args);

        // �÷��̾� ���ȿ� ������ ���ȵ� ���� �߰�
        PlayerStats.UpdateStatModifier(gear.GetAbilityDict(), gear);

    }

    /// <summary>
    /// ��� ���� ����
    /// </summary>
    public static void UnEquipGear(Gear gear)
    {
        // ������ ��� ������ �׳� ����
        if (IsEquippedGear(gear) == false)
        {
            Debug.Log("������ ��� �����ϴ�.");
            return;
        }

        // ��������
        _equipGearDict[gear.ItemType] = null;
        
        // ������ ��� ����Ǿ��� �� �̺�Ʈ ����
        OnEquipGearChangedArgs args = new OnEquipGearChangedArgs() 
        { 
            ItemType = gear.ItemType, 
            Prefab = null, 
            AttackAnimType = AttackAnimType.Hand.ToString(), 
            IsTryEquip = false 
        };
        OnEquipGearChanged?.Invoke(args);

        // �÷��̾� ���ȿ� ������ ���ȵ� ���� ����
        PlayerStats.RemoveStatModifier(gear.GetAbilityDict(), gear);
    }

    /// <summary>
    /// ������ �������?
    /// </summary>
    public static bool IsEquippedGear(Gear gear)
    {
        return _equipGearDict.ContainsKey(gear.ItemType) && _equipGearDict[gear.ItemType] == gear;
    }

    /// <summary>
    /// ������ ��� ��������
    /// </summary>
    public static Gear GetEquipGear(ItemType itemType)
    {
        if (_equipGearDict.TryGetValue(itemType, out Gear equipItem))
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
