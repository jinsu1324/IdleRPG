using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonBase<ItemManager>
{
    [SerializeField] private List<ItemDataSO> _itemDataSOList;  // ������ �����͵� ����Ʈ
    private static Dictionary<string, ItemDataSO> _itemDataSODict; // �����۵����� ��ũ���ͺ� ��ųʸ�

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_ItemDataSODict();
    }

    /// <summary>
    /// ������ ������ ��ųʸ� ����
    /// </summary>
    private void Set_ItemDataSODict()
    {
        _itemDataSODict = new Dictionary<string, ItemDataSO>();

        foreach (ItemDataSO itemDataSO in _itemDataSOList)
        {
            if (_itemDataSODict.ContainsKey(itemDataSO.ID) == false)
                _itemDataSODict[itemDataSO.ID] = itemDataSO;
        }
    }

    /// <summary>
    /// Ư�� ������Ÿ�Կ� �´� ��� ItemDataSO ��������
    /// </summary>
    public static List<ItemDataSO> GetItemDataSOList_ByType(ItemType itemType)
    {
        List<ItemDataSO> result = new List<ItemDataSO>();

        foreach (ItemDataSO itemDataSO in _itemDataSODict.Values)
        {
            if (itemDataSO.ItemType == itemType.ToString())
                result.Add(itemDataSO);
        }

        return result;
    }

    /// <summary>
    /// ID ���´� ItemDataSO ��������
    /// </summary>
    public static ItemDataSO GetItemDataSO(string id)
    {
        if (_itemDataSODict.TryGetValue(id, out ItemDataSO itemDataSO))
            return itemDataSO;

        Debug.Log($"{id} �� �´� ItemDataSO�� ã�� �� �����ϴ�.");
        return null;
    }

    /// <summary>
    /// �ش� �������� ��ȭ��������?
    /// </summary>
    public static bool CanEnhance(Item item)
    {
        ItemDataSO itemDataSO = GetItemDataSO(item.ID);
        return item.Count >= itemDataSO.GetEnhanceCount(item.Level);
    }
}
