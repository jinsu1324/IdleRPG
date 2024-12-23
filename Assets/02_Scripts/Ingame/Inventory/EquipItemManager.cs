using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItemManager
{
    // ������ ������ ��ųʸ�
    private static Dictionary<ItemType, Item> _equipItemDict = new Dictionary<ItemType, Item>
    { 
        { ItemType.Weapon, null},
        { ItemType.Armor, null}
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


        // �÷��̾� ���ȿ� ������ ���ȵ� ���� �߰�
        foreach (var kvp in item.GetStatDict())
            PlayerStats.Instance.UpdateModifier(kvp.Key, kvp.Value, item);


        // ���� �����ִ� UI ������Ʈ
        PlayerStats.Instance.AllStatUIUpdate();
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

        // �÷��̾� ���ȿ� ������ ���ȵ� ���� ����
        foreach (var kvp in item.GetStatDict())
            PlayerStats.Instance.RemoveModifier(kvp.Key, item);

        // ���� �����ִ� UI ������Ʈ
        PlayerStats.Instance.AllStatUIUpdate();
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
