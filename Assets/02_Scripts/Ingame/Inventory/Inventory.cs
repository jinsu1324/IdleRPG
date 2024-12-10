using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

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

    private InventorySlot _selectSlot = null;   // ���õ� ���� 


    public void SelectSlot(InventorySlot newSelectSlot)
    {
        // �̹� Ȱ��ȭ�� ������ �ٽ� Ŭ������ ��� ����
        if (_selectSlot == newSelectSlot)
            return; 

        // ���� Ȱ��ȭ�� ������ �ִٸ�, �� ������ ���̶���Ʈ OFF
        if (_selectSlot != null)
            _selectSlot.HigilightIconOFF();

        // �� �������� ��ü �� ���̶���Ʈ ON
        _selectSlot = newSelectSlot;
        _selectSlot.HigilightIconON();

        // �г� �ѱ� �� �ʱ�ȭ
        _selectItemInfoPanel.Init(_selectSlot);
    }



    // sdfsdf 
    public bool IsEquipped(Equipment item)
    {
        return _equippedItem == item;
    }

    private InventorySlot _equippedSlot = null; // ���� ������ ����




    private List<Equipment> _equipmentList = new List<Equipment>();

    private Equipment _equippedItem;

    [SerializeField] private List<InventorySlot> _inventorySlotList;

    [SerializeField] private Image _equippedItemIcon;

    [SerializeField] private SelectItemInfoPanel _selectItemInfoPanel;


    public void Equip(Equipment item)
    {
        if (_equippedItem != null) // ������ �������� �����ϸ�
        {
            if (_equippedItem == item)
            {
                Debug.Log("�̹� �������̴ϱ� �ƹ��͵� ���Ұ�!");
                return;
            }

            UnEquip(_equippedItem); // �����ϰ�
        }

        _equippedItem = item; // ����!
        Debug.Log($"{item.Name}�� �����߽��ϴ�.");

        // ���� ������ ������ ����
        if (_equippedSlot != null)
        {
            _equippedSlot.EqiupIconOFF();
        }
        // ���ο� ������ ������ �ѱ�
        _equippedSlot = FindSlotByItem(item); // �����ۿ� �ش��ϴ� ���� ã��
        if (_equippedSlot != null)
        {
            _equippedSlot.EquipIconON();
        }

        _equippedItemIcon.sprite = _equippedItem.Icon;  
    }

    public void UnEquip(Equipment item)
    {
        if (_equippedItem == item) // �Ϸ��� �������� ������ �������̸�
        {
            _equippedItem = null;   // ����
            Debug.Log($"{item.Name}�� �����߽��ϴ�.");

            _equippedItemIcon.sprite = null;

            if (_equippedSlot != null)
            {
                _equippedSlot.EqiupIconOFF(); // ���� ������ ����
                _equippedSlot = null; // ���� ���� �ʱ�ȭ
            }
        }
    }


    private InventorySlot FindSlotByItem(Equipment item)
    {
        foreach (var slot in _inventorySlotList)
        {
            if (slot.CurrentSlotItem == item)
            {
                return slot; // �ش� �������� ���� ���� ��ȯ
            }
        }
        return null; // �������� ���� ������ ������ null
    }






    public void AddRandomEquipment()
    {
        EquipmentDataSO equipmentDataSO 
            = DataManager.Instance.GetEquipmentDataSOByID(GetRandomEquipmentID().ToString());

        Equipment equipment = new Equipment(equipmentDataSO);

        if (AddItem(equipment))
        {
            Debug.Log($"{equipment.Name} ---- ȹ��!");
        }
        else
        {
            Debug.Log("�߰��� �󽽷� ����!");

        }

    }

    public bool AddItem(Equipment equipment)
    {
        foreach (InventorySlot inventorySlot in _inventorySlotList)
        {
            if (inventorySlot.IsSlotEmpty)
            {
                inventorySlot.AddItem(equipment);

                _equipmentList.Add(equipment);

                return true;
            }
        }

        return false;
    }







    public void RemoveRandomItem()
    {
        Equipment havEquipment = null;

        if (_equipmentList.Count > 0)
        {
            Random random = new Random(); // Random ��ü ����
            int randomIndex = random.Next(0, _equipmentList.Count); // 0���� ����Ʈ ����-1 ������ ���� �ε���
            havEquipment = _equipmentList[randomIndex];
        }
        else
        {
            havEquipment = null;
            Debug.Log("���� �������� �����ϴ�!");
            return;
        }


        if (RemoveItem(havEquipment))
        {
            Debug.Log($"{havEquipment.Name} ---- ����!");
        }
        else
        {
            Debug.Log($"{havEquipment.Name} ������ ���� ����");
        }
    }

    public bool RemoveItem(Equipment equipment)
    {
        foreach (InventorySlot inventorySlot in _inventorySlotList)
        {
            if (inventorySlot.CurrentSlotItem == equipment)
            {
                inventorySlot.ClearItem();
                _equipmentList.Remove(equipment);
                return true;
            }
        }

        return false;
    }






    public EquipmentID GetRandomEquipmentID()
    {
        // Enum ������ �迭�� ������
        Array values = Enum.GetValues(typeof(EquipmentID));
        // ���� �ε��� ����
        Random random = new Random();
        int randomIndex = random.Next(values.Length);
        // ���� �� ��ȯ
        return (EquipmentID)values.GetValue(randomIndex);
    }

}
