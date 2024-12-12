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
    // ���� �������̳� ����û���� ���� ��踦 ����ٰ� �����غ���.
    // ��� �߰������� �� ��ü�� ������ ���� �ʿ䰡 ����.
    // �������ִ� ��δ� �ʿ��� ��� �߰�������
    // ��� ��Ӹӽ��� ������. �� ����Ұ� �ƴ϶����̾�
    // ����Ұ� ����� �����Ұ� �ʿ���
    // �� ����Ұ� �߻����� ���� �ƴ϶��, ��𼭶� �ݵ�� ������ �������


    //[Title("ȹ���� �������� ������ ���� �����̳�", Bold = false)]
    //[SerializeField] private ItemSlotContainer _itemSlotContainer;    // �����۽��Ե��� ����ִ� �����̳�


    /// <summary>
    /// ���� ������ ȹ�� ��ư
    /// </summary>
    public void AcquireRandomItemButton()
    {
        Item item = RandomPickItem(); // ������ �������� ��������
        int dropCount = RandomPickDropCount(); // ȹ�� �� �������� ��������
        
        for (int i = 0; i < dropCount; i++) // ȹ�� �� ��ŭ �ݺ� ȹ��
            AcquireItem(item);
    }

    /// <summary>
    /// ������ ȹ��
    /// </summary>
    public bool AcquireItem(Item item)
    {
        
        // ������ �ִ� ���������� üũ
        Item existItem = HaveItemContainer.HaveItemCheck(item); 

        // �̹� ������, �� �������� ������ �߰�������
        if (existItem != null)
        {
            // ���� �߰�
            existItem.AddCount();

            // �ش� �������� ������ ã�� UI ����
            ItemSlot itemSlot = ItemSlotFinder.FindSlot_ContainItem(existItem, ItemSlotContainer.Instance.GetItemSlotList());
            itemSlot.UpdateItemInfoUI();

            return true;
        }

        // ������, ���� �������� ȹ��������
        else
        {
            ItemSlot emptySlot = ItemSlotFinder.FindSlot_Empty(ItemSlotContainer.Instance.GetItemSlotList());

            if (emptySlot != null)
            {
                emptySlot.AddItem(item);
                HaveItemContainer.AddHaveItemList(item);

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
        ItemDataSO itemDataSO = DataManager.Instance.GetItemDataSOByID(GetRandomItemID().ToString()); // ������ ������� ��������
        Item equipment = new Item(itemDataSO, 1); // ���� ��� �ϳ� ����

        return equipment;
    }

    /// <summary>
    /// ȹ�� ���� �������� ��
    /// </summary>
    private int RandomPickDropCount()
    {
        int dropCount = Random.Range(1, 5); // ���� ȹ�� ���� 1, 2, 3, 4

        return dropCount;
    }

    /// <summary>
    /// ���� EquipmentID ��ȯ
    /// </summary>
    public ItemID GetRandomItemID()
    {
        // Enum ������ �迭�� ������
        Array values = Enum.GetValues(typeof(ItemID));

        // ���� �ε��� ����
        int randomIndex = Random.Range(0, values.Length);

        // ���� EquipmentID ��ȯ
        return (ItemID)values.GetValue(randomIndex);
    }
}
