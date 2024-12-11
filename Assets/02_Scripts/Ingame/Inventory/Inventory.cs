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

    [SerializeField] private Image _equippedItemIcon;                       // ������ ������ ������
    [SerializeField] private List<InventorySlot> _inventorySlotList;        // �κ��丮 ���� ����Ʈ
    [SerializeField] private SelectItemInfoPanel _selectedItemInfoPanel;    // ���õ� ������ ���� �г�

    private List<Equipment> _haveItemList = new List<Equipment>();          // ������ �ִ� ������ ����Ʈ 
    private Equipment _equippedItem;                                        // ������ ������
    private InventorySlot _selectedSlot;                                    // ���õ� ����


    /// <summary>
    /// ���õ� ���� ���̶�����
    /// </summary>
    public void HighlightingSelectdSlot(InventorySlot newSelectedSlot)
    {
        // �̹� ���õ� ������ �ٽ� Ŭ������ ��� ����
        if (_selectedSlot == newSelectedSlot)
            return;

        // ������ ���õ� ������ �ִٸ�, �� ������ ���̶���Ʈ OFF
        if (_selectedSlot != null)
            _selectedSlot.SelectFrameOFF();

        // �� �������� ��ü �� ���̶���Ʈ ON
        _selectedSlot = newSelectedSlot;
        _selectedSlot.SelectFrameON();

        // ���õ� ������ ���� �г� �ѱ�
        SelectedItemInfoPanel_ON();
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void Equip(Equipment item)
    {
        // ������ �������� �����ϸ� �� ������ ����
        if (_equippedItem != null) 
        {
            // �������� ��������, �����Ϸ��� �ϴ� �����۰� �����ϴٸ� �ƹ��ϵ� ���� �ʰ� �׳� ����
            if (IsEquipped(item))  
                return;

            // ���� �� ���������� OFF
            UnEquip(_equippedItem);
            FindSlotByItem(_equippedItem).EqiuppedIconOFF();   
        }

        // ���� �� ���������� ON
        _equippedItem = item; 
        FindSlotByItem(_equippedItem).EquippedIconON();

        // ������ ������ �����ܵ� ����
        _equippedItemIcon.sprite = _equippedItem.Icon;  
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void UnEquip(Equipment item)
    {
        // ���� �� �����ܵ鵵 �� ����
        _equippedItem = null;   
        _equippedItemIcon.sprite = null;
        FindSlotByItem(item).EqiuppedIconOFF();
    }

    /// <summary>
    /// �ش� �������� ����ִ� ���� ã��
    /// </summary>
    private InventorySlot FindSlotByItem(Equipment item)
    {
        foreach (InventorySlot slot in _inventorySlotList)
        {
            if (slot.CurrentItem == item)
                return slot;
        }

        return null;
    }

    /// <summary>
    /// �� �������� ������ ����������
    /// </summary>
    public bool IsEquipped(Equipment item)
    {
        return _equippedItem == item;
    }

    /// <summary>
    /// ���õ� ������ ���� �г� �ѱ�
    /// </summary>
    private void SelectedItemInfoPanel_ON()
    {
        _selectedItemInfoPanel.OpenAndInit(_selectedSlot);
    }



    //--------------- �Ʒ����ʹ� �ӽ� ������ ���� ȹ�� ���� �ڵ�


    /// <summary>
    /// ���� ������ �߰� ��ư
    /// </summary>
    public void OnClickAddRandomItem()
    {
        // ������ ������� ��������
        EquipmentDataSO equipmentDataSO 
            = DataManager.Instance.GetEquipmentDataSOByID(GetRandomEquipmentID().ToString());

        // ���� ��� �ϳ� ����
        Equipment equipment = new Equipment(equipmentDataSO);

        // �߰�
        if (AddItem(equipment))
            Debug.Log($"{equipment.Name} ---- ȹ��!");
        else
            Debug.Log("�߰� ����!");

    }
    
    /// <summary>
    /// ������ �߰�
    /// </summary>
    public bool AddItem(Equipment item)
    {
        // ����ִ� ������ ã�Ƽ�, ���Կ� ������ �߰� + ���� �����۸���Ʈ���� �߰�
        foreach (InventorySlot slot in _inventorySlotList)
        {
            if (slot.IsSlotEmpty)
            {
                slot.AddItem(item);
                _haveItemList.Add(item);

                return true;
            }
        }

        return false; // ����
    }

    /// <summary>
    /// ���� ������ ���� ��ư
    /// </summary>
    public void OnClickRemoveRandomItem()
    {
        // ���� ������ �ƹ��͵� ������ �׳� ����
        if (_haveItemList.Count <= 0)
            return;

        // ���� �����۵� �߿� �ƹ��ų� ������
        int randomIndex = Random.Range(0, _haveItemList.Count);
        Equipment randomPickedEquipment = _haveItemList[randomIndex];

        // ����
        if (RemoveItem(randomPickedEquipment))
            Debug.Log($"{randomPickedEquipment.Name} ---- ����!");
        else
            Debug.Log($"���� ����!");
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public bool RemoveItem(Equipment item)
    {
        // �ش� �������� �ִ� ������ ã�Ƽ� ������ ���� + ���� �����ۿ����� ����
        foreach (InventorySlot slot in _inventorySlotList)
        {
            if (slot.CurrentItem == item)
            {
                slot.ClearItem();
                _haveItemList.Remove(item);

                return true;
            }
        }

        return false; // ����
    }

    /// <summary>
    /// ���� EquipmentID ��ȯ
    /// </summary>
    public EquipmentID GetRandomEquipmentID()
    {
        // Enum ������ �迭�� ������
        Array values = Enum.GetValues(typeof(EquipmentID));

        // ���� �ε��� ����
        int randomIndex = Random.Range(0, values.Length);

        // ���� EquipmentID ��ȯ
        return (EquipmentID)values.GetValue(randomIndex);
    }
}
