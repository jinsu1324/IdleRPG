using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// ������ ȹ�� ���
/// </summary>
public class ItemDropMachine : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;    // ȹ���� ������ Ÿ��

    /// <summary>
    /// ���� ������ ȹ�� ��ư
    /// </summary>
    public void AcquireRandomItemButton()
    {
        Item item = RandomPickItem(); // ������ �������� ��������
        int dropCount = RandomPickDropCount(5); // ȹ�� �� �������� ��������
        
        for (int i = 0; i < dropCount; i++) // ȹ�� �� ��ŭ �ݺ� ȹ��
            AcquireItem(item);

        Debug.Log("------------------------------");
        ItemInven.CheckCurrentItemInven(item);  // �ϴ� ����׷� üũ��
    }

    /// <summary>
    /// ������ ȹ��
    /// </summary>
    private void AcquireItem(Item item)
    {
        // ������ �ִ� ���������� üũ
        Item existItem = ItemInven.HasItemInInven(item); 

        // �̹� ������, �� �������� ������ �߰�
        if (existItem != null)
        {
            existItem.AddCount();
            return;
        }
        
        // ������, ���� �������� �߰�
        ItemInven.AddItem(item);
        return;
    }

    /// <summary>
    /// ��� �������� ��
    /// </summary>
    private Item RandomPickItem()
    {
        // ���� ������Ÿ���� �����۵����ʹ� ��� ��������
        List<ItemDataSO> typeItemDataSOList =  DataManager.Instance.GetAllItemDataSOByItemType(_itemType);  

        // �� �����۵����͵� �߿��� �ϳ� ���� ��
        ItemDataSO itemDataSO = typeItemDataSOList[RandomPickItemIndex(typeItemDataSOList.Count)];

        // �����ȵ� �����ͷ� ������ �����
        Item item = new Item(itemDataSO, 1);

        return item;
    }

    /// <summary>
    /// ������ ���� �� �������� ��
    /// </summary>
    private int RandomPickItemIndex(int maxCount)
    {
        int index = Random.Range(0, maxCount);
        return index;
    }

    /// <summary>
    /// ȹ�� ���� �������� ��
    /// </summary>
    private int RandomPickDropCount(int maxCount)
    {
        int dropCount = Random.Range(1, 5 + 1);
        return dropCount;
    }
}
