using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// ������ ȹ�� ���
/// </summary>
public class ItemDropMachine : MonoBehaviour
{
    public static event Action<List<Item>> OnDroppedItem;   // ������ ��� �Ϸ�Ǿ����� �̺�Ʈ

    [Title("��� ����", bold: false)]
    [SerializeField] 
    [Range(1, 10)] 
    private int _maxDropCount;

    [Title("����� ������ Ÿ��", bold: false)]
    [SerializeField] 
    private ItemType _itemType;

    /// <summary>
    /// ������ ��� (��ư����)
    /// </summary>
    public void OnClickDropItem()
    {
        List<Item> dropItemList = new List<Item>(); 

        for (int i = 0; i < _maxDropCount; i++)
        {
            Item item = CreateItem(); 
            ItemInven.AddItem(item);
            dropItemList.Add(item);
        }

        OnDroppedItem?.Invoke(dropItemList);
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    private Item CreateItem()
    {
        // �ش� ������Ÿ���� ��� ������ ��ũ���ͺ� ������Ʈ�� �߿��� 1���� ����
        List<ItemDataSO> itemDataSOList = ItemDataManager.GetItemDataSOList_ByType(_itemType);
        ItemDataSO itemDataSO = itemDataSOList[RandomIndex(itemDataSOList.Count)];

        Item item = new Item(itemDataSO.ID, itemDataSO.ItemType, 1, 1);
        return item;
    }

    /// <summary>
    /// 0-maxCount ���� ������ ���� ��ȯ
    /// </summary>
    private int RandomIndex(int maxCount)
    {
        int index = Random.Range(0, maxCount);
        return index;
    }
}
