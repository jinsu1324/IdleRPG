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

    private InventorySlot _selectSlot = null;   // 선택된 슬롯 


    public void SelectSlot(InventorySlot newSelectSlot)
    {
        // 이미 활성화된 슬롯을 다시 클릭했을 경우 무시
        if (_selectSlot == newSelectSlot)
            return; 

        // 이전 활성화된 슬롯이 있다면, 그 슬롯의 하이라이트 OFF
        if (_selectSlot != null)
            _selectSlot.HigilightIconOFF();

        // 새 슬롯으로 교체 후 하이라이트 ON
        _selectSlot = newSelectSlot;
        _selectSlot.HigilightIconON();

        // 패널 켜기 및 초기화
        _selectItemInfoPanel.Init(_selectSlot);
    }



    // sdfsdf 
    public bool IsEquipped(Equipment item)
    {
        return _equippedItem == item;
    }

    private InventorySlot _equippedSlot = null; // 현재 장착된 슬롯




    private List<Equipment> _equipmentList = new List<Equipment>();

    private Equipment _equippedItem;

    [SerializeField] private List<InventorySlot> _inventorySlotList;

    [SerializeField] private Image _equippedItemIcon;

    [SerializeField] private SelectItemInfoPanel _selectItemInfoPanel;


    public void Equip(Equipment item)
    {
        if (_equippedItem != null) // 장착된 아이템이 존재하면
        {
            if (_equippedItem == item)
            {
                Debug.Log("이미 착용중이니까 아무것도 안할게!");
                return;
            }

            UnEquip(_equippedItem); // 해제하고
        }

        _equippedItem = item; // 장착!
        Debug.Log($"{item.Name}를 장착했습니다.");

        // 이전 슬롯의 아이콘 끄기
        if (_equippedSlot != null)
        {
            _equippedSlot.EqiupIconOFF();
        }
        // 새로운 슬롯의 아이콘 켜기
        _equippedSlot = FindSlotByItem(item); // 아이템에 해당하는 슬롯 찾기
        if (_equippedSlot != null)
        {
            _equippedSlot.EquipIconON();
        }

        _equippedItemIcon.sprite = _equippedItem.Icon;  
    }

    public void UnEquip(Equipment item)
    {
        if (_equippedItem == item) // 하려는 아이템이 장착된 아이템이면
        {
            _equippedItem = null;   // 해제
            Debug.Log($"{item.Name}를 해제했습니다.");

            _equippedItemIcon.sprite = null;

            if (_equippedSlot != null)
            {
                _equippedSlot.EqiupIconOFF(); // 장착 아이콘 끄기
                _equippedSlot = null; // 현재 슬롯 초기화
            }
        }
    }


    private InventorySlot FindSlotByItem(Equipment item)
    {
        foreach (var slot in _inventorySlotList)
        {
            if (slot.CurrentSlotItem == item)
            {
                return slot; // 해당 아이템을 가진 슬롯 반환
            }
        }
        return null; // 아이템을 가진 슬롯이 없으면 null
    }






    public void AddRandomEquipment()
    {
        EquipmentDataSO equipmentDataSO 
            = DataManager.Instance.GetEquipmentDataSOByID(GetRandomEquipmentID().ToString());

        Equipment equipment = new Equipment(equipmentDataSO);

        if (AddItem(equipment))
        {
            Debug.Log($"{equipment.Name} ---- 획득!");
        }
        else
        {
            Debug.Log("추가할 빈슬롯 없음!");

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
            Random random = new Random(); // Random 객체 생성
            int randomIndex = random.Next(0, _equipmentList.Count); // 0부터 리스트 개수-1 사이의 랜덤 인덱스
            havEquipment = _equipmentList[randomIndex];
        }
        else
        {
            havEquipment = null;
            Debug.Log("가진 아이템이 없습니다!");
            return;
        }


        if (RemoveItem(havEquipment))
        {
            Debug.Log($"{havEquipment.Name} ---- 제거!");
        }
        else
        {
            Debug.Log($"{havEquipment.Name} 가지고 있지 않음");
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
        // Enum 값들을 배열로 가져옴
        Array values = Enum.GetValues(typeof(EquipmentID));
        // 랜덤 인덱스 선택
        Random random = new Random();
        int randomIndex = random.Next(values.Length);
        // 랜덤 값 반환
        return (EquipmentID)values.GetValue(randomIndex);
    }

}
