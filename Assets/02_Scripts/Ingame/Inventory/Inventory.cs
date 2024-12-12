using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    ////HaveItemContainer
    ////------------------------------------------------------------------------------------------------------------------------------------------------------
    //private List<Item> _haveItemList = new List<Item>();      // ������ �ִ� ������ ����Ʈ
    ////------------------------------------------------------------------------------------------------------------------------------------------------------


    ////ItemSlotContainer
    ////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
    //[SerializeField]
    //private List<ItemSlot> _inventorySlotList;                     // �κ��丮 ���� ����Ʈ
    //private ItemSlot _selectedSlot;                                // ���õ� ����
    //[SerializeField]
    //private SelectItemInfoPanel _selectedItemInfoPanel;                 // ���õ� ������ ���� �г�


    ///// <summary>
    ///// ���õ� ���� ���̶�����
    ///// </summary>
    //public void HighlightingSelectdSlot(ItemSlot newSelectedSlot)
    //{
    //    // �̹� ���õ� ������ �ٽ� Ŭ������ ��� ����
    //    if (_selectedSlot == newSelectedSlot)
    //        return;

    //    // ������ ���õ� ������ �ִٸ�, �� ������ ���̶���Ʈ OFF
    //    if (_selectedSlot != null)
    //        _selectedSlot.SelectFrameOFF();

    //    // �� �������� ��ü �� ���̶���Ʈ ON
    //    _selectedSlot = newSelectedSlot;
    //    _selectedSlot.SelectFrameON();

    //    // ���õ� ������ ���� �г� �ѱ�
    //    SelectedItemInfoPanel_ON();
    //}

    ///// <summary>
    ///// ���õ� ������ ���� �г� �ѱ�
    ///// </summary>
    //public void SelectedItemInfoPanel_ON()
    //{
    //    _selectedItemInfoPanel.OpenAndInit(_selectedSlot);
    //}
    ////----------------------------------------------------------------------------------------------------------------------------------------------------------------









    [SerializeField]
    private List<Image> _equippedItemIconList;                          // ������ ������ ������ ����Ʈ (�ִ� 3��)

    private List<Item> _equippedItemList = new List<Item>();            // ������ ������ ����Ʈ
    private int _maxEquipableItemCount = 3;                             // �ִ� ���� ������ ������ ����

    











    /// <summary>
    /// ����
    /// </summary>
    public void Equip(Item item)
    {
        // �̹� ������ ��������, �ִ� ���������� �ʰ������� �׳� ����
        if (_equippedItemList.Count >= _maxEquipableItemCount)
        {
            Debug.Log("���� ������ �ִ� ������ ������ �ʰ��߽��ϴ�!");
            return;
        }

        // �̹� ������ �������̸� �׳� ����
        if (IsEquipped(item))
        {
            Debug.Log("�̹� ������ �������Դϴ�!");
            return;
        }

        // ����
        _equippedItemList.Add(item);

        // �÷��̾� ���ȿ� ������ ���ȵ� �߰�
        AddItemStatsToPlayer(item.GetStatDictionaryByLevel(), item);

        // ���Կ� ���� ������ ON
        //FindSlotByItem(item).EquippedIconON();
        ItemSlotFinder.FindSlot_ContainItem(item, ItemSlotContainer.Instance.GetItemSlotList()).EquippedIconON();


        // ���� ������ ����Ʈ UI ������Ʈ
        Update_EquippedItemIconListUI();

        // ���� �����ִ� UI ������Ʈ
        PlayerStats.Instance.AllStatUIUpdate(); 
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void UnEquip(Item item)
    {
        // �����Ϸ��� �������� �������� �ʾ����� �׳� ����
        if (_equippedItemList.Contains(item) == false)
        {
            Debug.Log("�����Ϸ��� �������� �������� �ʾҽ��ϴ�!");
            return;
        }

        // ����
        _equippedItemList.Remove(item);

        // �÷��̾� ���ȿ��� ������ ���ȵ� ����
        RemoveItemStatsToPlayer(item.GetStatDictionaryByLevel(), item);

        // ���Կ� ���� ������ OFF
        //FindSlotByItem(item).EqiuppedIconOFF();
        ItemSlotFinder.FindSlot_ContainItem(item, ItemSlotContainer.Instance.GetItemSlotList()).EquippedIconOFF();

        // ���� ������ ����Ʈ UI ������Ʈ
        Update_EquippedItemIconListUI();

        // ���� �����ִ� UI ������Ʈ
        PlayerStats.Instance.AllStatUIUpdate();
    }

    /// <summary>
    /// �÷��̾� ���ȿ� ������ ���ȵ� �߰�
    /// </summary>
    private void AddItemStatsToPlayer(Dictionary<StatType, int> statDict, Item selfItem)
    {
        foreach (var kvp in statDict)
            PlayerStats.Instance.AddModifier(kvp.Key, kvp.Value, selfItem);
    }

    /// <summary>
    /// �÷��̾� ���ȿ��� ������ ���ȵ� ����
    /// </summary>
    private void RemoveItemStatsToPlayer(Dictionary<StatType, int> statDict, Item selfItem)
    {
        foreach (var kvp in statDict)
            PlayerStats.Instance.RemoveModifier(kvp.Key, selfItem);
    }




    ////ItemSlotFinder
    ////----------------------------------------------------------------------------------------------------------------------------------------------------------------
    ///// <summary>
    ///// �ش� �������� ����ִ� ���� ã��
    ///// </summary>
    //public ItemSlot FindSlotByItem(Item item)
    //{
    //    foreach (ItemSlot slot in _inventorySlotList)
    //    {
    //        if (slot.CurrentItem == item)
    //            return slot; 
    //    }

    //    return null;
    //}
    ////----------------------------------------------------------------------------------------------------------------------------------------------------------------






    /// <summary>
    /// ������ ������ ������ ����Ʈ UI ������Ʈ
    /// </summary>
    private void Update_EquippedItemIconListUI()
    {
        for (int i = 0; i < _equippedItemIconList.Count; i++)   // 3����ŭ �ݺ� (������ ���� �ִ� ����)
        {
            if (i < _equippedItemList.Count) // ���� ������ ���������� ������ ǥ��
            {
                _equippedItemIconList[i].sprite = _equippedItemList[i].Icon;
                _equippedItemIconList[i].gameObject.SetActive(true);
            }
            else // �������� ��Ȱ��ȭ
            {
                _equippedItemIconList[i].sprite = null;
                _equippedItemIconList[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// �ش� �������� ������ ���������� Ȯ��
    /// </summary>
    public bool IsEquipped(Item item)
    {
        return _equippedItemList.Contains(item);
    }

    








    ////ItemDropMachine
    ////----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    ///// <summary>
    ///// ���� ������ �߰� ��ư
    ///// </summary>
    //public void OnClickAddRandomItem()
    //{
    //    // ������ ������� ��������
    //    ItemDataSO equipmentDataSO 
    //        = DataManager.Instance.GetItemDataSOByID(GetRandomEquipmentID().ToString());


    //    // ���� ��� �ϳ� ����
    //    Item equipment = new Item(equipmentDataSO, 1);

    //    int obtainsCount = Random.Range(1, 5); // 1, 2, 3, 4

    //    for (int i = 0; i < obtainsCount; i++)
    //    {
    //        // �߰�
    //        if (AddItem(equipment))
    //            Debug.Log($"{equipment.Name} ---- ȹ��! {i+1}");
    //        else
    //            Debug.Log("�߰� ����!");
    //    }
    //}
    
    ///// <summary>
    ///// ������ �߰�
    ///// </summary>
    //public bool AddItem(Item item)
    //{
    //    Item existEquipment = _haveItemList.Find(x => x.ID == item.ID);    // �̹� �ִ��� üũ

    //    // �̹� ������, �� �������� ������ �߰�������
    //    if (existEquipment != null)
    //    {
    //        // ���� �߰�
    //        existEquipment.AddCount();

    //        // �ش� �������� ������ ã�� UI ����
    //        ItemSlot slot = FindSlotByItem(existEquipment);
    //        slot.UpdateItemInfoUI();

    //        return true;
    //    }

    //    // ������, ���� �������� ȹ��������
    //    else
    //    {
    //        // ����ִ� ������ ã�Ƽ�, ���Կ� ������ �߰� + ���� �����۸���Ʈ���� �߰�
    //        foreach (ItemSlot slot in _inventorySlotList)
    //        {
    //            if (slot.IsSlotEmpty)
    //            {
    //                slot.AddItem(item);
    //                _haveItemList.Add(item);

    //                return true;
    //            }
    //        }
    //    }

       
    //    // �� ��͵� �������� ���о�
    //    return false;
    //}

    ///// <summary>
    ///// ���� ������ ���� ��ư
    ///// </summary>
    //public void OnClickRemoveRandomItem()
    //{
    //    // ���� ������ �ƹ��͵� ������ �׳� ����
    //    if (_haveItemList.Count <= 0)
    //        return;

    //    // ���� �����۵� �߿� �ƹ��ų� ������
    //    int randomIndex = Random.Range(0, _haveItemList.Count);
    //    Item randomPickedEquipment = _haveItemList[randomIndex];

    //    // ����
    //    if (RemoveItem(randomPickedEquipment))
    //        Debug.Log($"{randomPickedEquipment.Name} ---- ����!");
    //    else
    //        Debug.Log($"���� ����!");
    //}

    ///// <summary>
    ///// ������ ����
    ///// </summary>
    //public bool RemoveItem(Item item)
    //{
    //    // �ش� �������� �ִ� ������ ã�Ƽ� ������ ���� + ���� �����ۿ����� ����
    //    foreach (ItemSlot slot in _inventorySlotList)
    //    {
    //        if (slot.CurrentItem == item)
    //        {
    //            slot.ClearItem();
    //            _haveItemList.Remove(item);

    //            return true;
    //        }
    //    }

    //    return false; // ����
    //}

    ///// <summary>
    ///// ���� EquipmentID ��ȯ
    ///// </summary>
    //public ItemID GetRandomEquipmentID()
    //{
    //    // Enum ������ �迭�� ������
    //    Array values = Enum.GetValues(typeof(ItemID));

    //    // ���� �ε��� ����
    //    int randomIndex = Random.Range(0, values.Length);

    //    // ���� EquipmentID ��ȯ
    //    return (ItemID)values.GetValue(randomIndex);
    //}
    ////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



}
