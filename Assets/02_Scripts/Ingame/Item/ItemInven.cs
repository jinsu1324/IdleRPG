using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ������ �ִ� ������ �κ��丮
/// </summary>
public class ItemInven : ISavable
{
    public string Key => nameof(ItemInven);         // ������ ���忡 ���� ���� Ű
    
    public static event Action<Item> OnAddItem;     // ������ �߰��Ǿ��� �� �̺�Ʈ

    [SaveField] // ������ �ִ� ������ �κ��丮 ��ųʸ� 
    public static Dictionary<ItemType, List<Item>> _itemInvenDict = new Dictionary<ItemType, List<Item>>();

    /// <summary>
    /// �κ��丮�� ������ �߰�
    /// </summary>
    public static void AddItem(Item item)
    {
        Item hasItem = HasItem(item); 
        
        if (hasItem != null) 
        {
            int addCount = 1;
            hasItem.AddCount(addCount); // ������ �ִ� �������̸� ������ �߰�
            OnAddItem?.Invoke(hasItem); 
        }
        else
        {
            _itemInvenDict[item.ItemType].Add(item);
            OnAddItem?.Invoke(item); 
        }
    }

    /// <summary>
    /// ������ �ִ� ���������� Ȯ�� �� ��ȯ
    /// </summary>
    public static Item HasItem(Item item)
    {
        CheckAnd_SetDict(item.ItemType);

        Item foundItem = _itemInvenDict[item.ItemType].Find(x => x.ID == item.ID);

        if (foundItem != null)
            return foundItem;
        else
        {
            Debug.Log($"{item.ID} ��(��) ó�� ȹ���ϴ°ſ���.");
            return null;
        }
    }

    /// <summary>
    /// �κ��丮�� ��ȭ ������ �������� �ִ���?
    /// </summary>
    public static bool HasEnhanceableItem(ItemType itemType)
    {
        CheckAnd_SetDict(itemType);

        if (_itemInvenDict.TryGetValue(itemType, out List<Item> itemInven))
        {
            if (itemInven.Any(item => ItemEnhanceManager.CanEnhance(item)))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    /// <summary>
    /// ������ Ÿ�Կ� �´� ������ �κ��丮 ��������
    /// </summary>
    public static List<Item> GetItemInven(ItemType itemType)
    {
        CheckAnd_SetDict(itemType);

        if (_itemInvenDict.TryGetValue(itemType, out List<Item> itemInven))
            return itemInven;
        else
        {
            Debug.Log($"{itemType} Ÿ�� ������ �κ��丮�� �����ϴ�.");
            return null;
        }
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

    /// <summary>
    /// ��ųʸ��� Ű üũ�غ��� ������ ��ųʸ� �����
    /// </summary>
    private static void CheckAnd_SetDict(ItemType itemType)
    {
        if (_itemInvenDict.ContainsKey(itemType) == false)
            _itemInvenDict[itemType] = new List<Item>();
    }
}
