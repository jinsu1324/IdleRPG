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
    private static Dictionary<ItemType, List<IItem>> _itemInvenDict = new Dictionary<ItemType, List<IItem>>();   

    /// <summary>
    /// �κ��丮�� ������ �߰�
    /// </summary>
    public static void AddItem(IItem item)
    {
        TrySet_ItemInvenDict(item);
        
        IItem existItem = HasItemInInven(item); // ������ �ִ� �������̸� ������ �߰�
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
    /// ������ ��ȭ
    /// </summary>
    public static void Enhance(IItem item)
    {
        item.RemoveCountByEnhance();    // ������ ���� ����
        item.ItemLevelUp();             // ������ ������

        // ����϶���
        if (item is Gear gear)
        {
            // �ش� �������� �����Ǿ� ��������
            if (EquipGearManager.IsEquippedGear(gear))
            {
                // �÷��̾� ���ȿ� ������ ���ȵ� ���� �߰�
                PlayerStats.UpdateStatModifier(gear.GetAbilityDict(), item);
            }
        }

        OnItemInvenChanged?.Invoke(); // ������ �ִ� �������� ����Ǿ��� �� �̺�Ʈ ȣ��
    }


    /// <summary>
    /// ������ �ִ� ���������� Ȯ�� �� ��ȯ
    /// </summary>
    public static IItem HasItemInInven(IItem item)
    {
        TrySet_ItemInvenDict(item);

        IItem existItem = _itemInvenDict[item.ItemType].Find(x => x.ID == item.ID);

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
    private static void TrySet_ItemInvenDict(IItem item)
    {
        if (_itemInvenDict.ContainsKey(item.ItemType) == false)
            _itemInvenDict[item.ItemType] = new List<IItem>();
    }

    /// <summary>
    /// ������ Ÿ�Կ� �´� ������ �κ��丮 ��������
    /// </summary>
    public static List<IItem> GetItemInvenByItemType(ItemType itemType)
    {
        if (_itemInvenDict.TryGetValue(itemType, out List<IItem> itemInven))
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
    /// ������ Ÿ�Ժ� �κ��丮�� ��ȭ ������ �������� �ִ���?
    /// </summary>
    public static bool HasEnhanceableItem(ItemType itemType)
    {
        if (_itemInvenDict.ContainsKey(itemType) == false)
        {
            Debug.Log($"���� {itemType} Ÿ���� �κ��丮 ��ü�� �����.");
            return false;
        }

        if (_itemInvenDict[itemType].Exists(item => item.IsEnhanceable()))
            return true;
        else
            return false;
    }

    /// <summary>
    /// �κ��丮�� ��ȭ ������ ��� �ִ���?
    /// </summary>
    public static bool HasEnhanceableGear()
    {
        bool isEnhanceable_Weapon = HasEnhanceableItem(ItemType.Weapon);
        bool isEnhanceable_Armor = HasEnhanceableItem(ItemType.Armor);
        bool isEnhanceable_Helmet = HasEnhanceableItem(ItemType.Helmet);

        // ��� Ȯ���ؼ� �Ѱ��� true�� true��ȯ
        if (isEnhanceable_Weapon || isEnhanceable_Armor || isEnhanceable_Helmet)
            return true;
        else
            return false;
    }

    /// <summary>
    /// �κ��丮�� ��ȭ ������ ��ų�� �ִ���?
    /// </summary>
    public static bool HasEnhanceableSkill()
    {
        bool isEnhanceable_Skill = HasEnhanceableItem(ItemType.Skill);

        if (isEnhanceable_Skill)
            return true;
        else
            return false;
    }
}
