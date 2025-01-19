using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;











/// <summary>
/// ������ �ִ� ������ �κ��丮
/// </summary>
[System.Serializable]
public class ItemInven : ISavable
{
    public static event Action<Item> OnAddItem;                                     // ������ �߰��Ǿ��� �� �̺�Ʈ
    [SaveField] public static Dictionary<ItemType, List<Item>> _itemInvenDict;      // ������ �ִ� ������ �κ��丮 ��ųʸ� 

    public string Key => nameof(ItemInven);
    public void NotifyLoaded()
    {
        throw new NotImplementedException();
    }





    /// <summary>
    /// ���� ������ (Ŭ������ ó�� ������ �� �� ���� ȣ��)
    /// </summary>
    static ItemInven()
    {
        _itemInvenDict = new Dictionary<ItemType, List<Item>>()
        {
            { ItemType.Weapon, new List<Item>() },
            { ItemType.Armor, new List<Item>() },
            { ItemType.Helmet, new List<Item>() },
            { ItemType.Skill, new List<Item>() }
        };
    }

    /// <summary>
    /// �κ��丮�� ������ �߰�
    /// </summary>
    public static void AddItem(Item item)
    {
        Item existItem = HasItemInInven(item); 
        
        if (existItem != null) 
        {
            // ������ �ִ� �������̸� ������ �߰�
            existItem.AddCount();

            // �������߰� �̺�Ʈ ��Ƽ
            OnAddItem?.Invoke(existItem); 
        }
        else
        {
            // �ƴϸ� ������ �߰�
            _itemInvenDict[item.ItemType].Add(item);

            // �������߰� �̺�Ʈ ��Ƽ
            OnAddItem?.Invoke(item); 
        }

        
    }

    /// <summary>
    /// ������ �ִ� ���������� Ȯ�� �� ��ȯ
    /// </summary>
    public static Item HasItemInInven(Item item)
    {
        Item existItem = _itemInvenDict[item.ItemType].Find(x => x.ID == item.ID);

        if (existItem != null)
        {
            return existItem;
        }
        else
        {
            Debug.Log($"{item.Name} �� ó�� ȹ���ϴ°ſ���.");
            return null;
        }
    }

    /// <summary>
    /// ������ Ÿ�Կ� �´� ������ �κ��丮 ��������
    /// </summary>
    public static List<Item> GetItemInven(ItemType itemType)
    {
        if (_itemInvenDict.TryGetValue(itemType, out List<Item> itemInven))
        {
            return itemInven;
        }
        else
        {
            Debug.Log($"{itemType} Ÿ�� ������ �κ��丮�� �����ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ������ Ÿ�Ժ� �κ��丮�� ��ȭ ������ �������� �ִ���?
    /// </summary>
    public static bool HasEnhanceableItem(ItemType itemType)
    {
        if (_itemInvenDict[itemType].
            Where(item => item is IEnhanceableItem).    // �κ��丮���� IEnhanceableItem �� ��� ã��
            Cast<IEnhanceableItem>().                   // ����Ҹ� IEnhanceableItem �� ��ȯ
            Any(enhanceableItem => enhanceableItem.CanEnhance()))   // ��� IEnhanceableItem �߿� CanEnhance() �� ��Ұ� �ִ��� Ȯ��
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

    /// <summary>
    /// �� �������� ��ȭ��������?
    /// </summary>
    public static bool CanEnhanceable_ThisItem(Item item)
    {
        Item existItem = HasItemInInven(item);

        if (existItem == null)
            return false;

        if (existItem is IEnhanceableItem enhanceableItem)
            return enhanceableItem.CanEnhance(); 

        return false;
    }

    
}
