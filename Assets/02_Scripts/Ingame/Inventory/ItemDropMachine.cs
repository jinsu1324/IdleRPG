using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// ���� �������̳� ����û���� ���� ��踦 ����ٰ� �����غ���.
// ��� �߰������� �� ��ü�� ������ ���� �ʿ䰡 ����.
// �������ִ� ��δ� �ʿ��� ��� �߰�������
// ��� ��Ӹӽ��� ������. �� ����Ұ� �ƴ϶����̾�
// ����Ұ� ����� �����Ұ� �ʿ���
// �� ����Ұ� �߻����� ���� �ƴ϶��, ��𼭶� �ݵ�� ������ �������

/// <summary>
/// ������ ȹ�� ���
/// </summary>
public class ItemDropMachine : MonoBehaviour
{
    [Title("ȹ���� �������� ������ �κ��丮", Bold = false)]
    [SerializeField] private ItemInventory _itemInventory;    // ������ �κ��丮

    [SerializeField] private ItemType _itemType;    // �κ��丮�� ������ Ÿ��

    /// <summary>
    /// ���� ������ ȹ�� ��ư
    /// </summary>
    public void AcquireRandomItemButton()
    {
        Item item = RandomPickItem(); // ������ �������� ��������
        int dropCount = RandomPickDropCount(5); // ȹ�� �� �������� ��������
        
        for (int i = 0; i < dropCount; i++) // ȹ�� �� ��ŭ �ݺ� ȹ��
            AcquireItem(item);
    }

    /// <summary>
    /// ������ ȹ��
    /// </summary>
    private bool AcquireItem(Item item)
    {
        // ������ �ִ� ���������� üũ
        Item existItem = _itemInventory.HasItemInInventory(item); 

        // �̹� ������, �� �������� ������ �߰�������
        if (existItem != null)
        {
            // ���� �߰�
            existItem.AddCount();

            // �ش� �������� ������ ã�� UI ����
            ItemSlot itemSlot = ItemSlotFinder.FindSlot_ContainItem(existItem, InventoryManager.Instance.GetItemSlotManager(existItem.ItemType).GetItemSlotList());
            itemSlot.UpdateItemInfoUI();

            

            return true;
        }

        // ������, ���� �������� ȹ��������
        else
        {
            ItemSlot emptySlot = ItemSlotFinder.FindSlot_Empty(InventoryManager.Instance.GetItemSlotManager(item.ItemType).GetItemSlotList());

            if (emptySlot != null)
            {
                emptySlot.AddItem(item);
                _itemInventory.AddInventory(item);

                return true;
            }
        }

        Debug.Log("�������� �߰����� ���߽��ϴ�.");
        return false; // �� ��͵� �������� ����
    }

    /// <summary>
    /// ��� �������� ��
    /// </summary>
    private Item RandomPickItem()
    {
        List<ItemDataSO> typeItemDataSOList =  DataManager.Instance.GetAllItemDataSOByItemType(_itemType);

        ItemDataSO itemDataSO = typeItemDataSOList[RandomPickItemIndex(typeItemDataSOList.Count)];

        Item equipment = new Item(itemDataSO, 1); // ������ Ÿ�Կ� �´� ������ ��� �ϳ� ����

        return equipment;
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

    /// <summary>
    /// ���� EquipmentID ��ȯ
    /// </summary>
    private ItemID GetRandomItemID()
    {
        // Enum ������ �迭�� ������
        Array values = Enum.GetValues(typeof(ItemID));

        // ���� �ε��� ����
        int randomIndex = Random.Range(0, values.Length);

        // ���� EquipmentID ��ȯ
        return (ItemID)values.GetValue(randomIndex);
    }
}
