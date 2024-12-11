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

    [SerializeField] private Image _equippedItemIcon;                       // 장착한 아이템 아이콘
    [SerializeField] private List<InventorySlot> _inventorySlotList;        // 인벤토리 슬롯 리스트
    [SerializeField] private SelectItemInfoPanel _selectedItemInfoPanel;    // 선택된 아이템 정보 패널

    private List<Equipment> _haveItemList = new List<Equipment>();          // 가지고 있는 아이템 리스트 
    private Equipment _equippedItem;                                        // 장착한 아이템
    private InventorySlot _selectedSlot;                                    // 선택된 슬롯


    /// <summary>
    /// 선택된 슬롯 하이라이팅
    /// </summary>
    public void HighlightingSelectdSlot(InventorySlot newSelectedSlot)
    {
        // 이미 선택된 슬롯을 다시 클릭했을 경우 무시
        if (_selectedSlot == newSelectedSlot)
            return;

        // 이전에 선택된 슬롯이 있다면, 그 슬롯의 하이라이트 OFF
        if (_selectedSlot != null)
            _selectedSlot.SelectFrameOFF();

        // 새 슬롯으로 교체 후 하이라이트 ON
        _selectedSlot = newSelectedSlot;
        _selectedSlot.SelectFrameON();

        // 선택된 아이템 정보 패널 켜기
        SelectedItemInfoPanel_ON();
    }

    /// <summary>
    /// 아이템 장착
    /// </summary>
    public void Equip(Equipment item)
    {
        // 장착된 아이템이 존재하면 그 아이템 해제
        if (_equippedItem != null) 
        {
            // 장착중인 아이템이, 장착하려고 하는 아이템과 동일하다면 아무일도 하지 않고 그냥 리턴
            if (IsEquipped(item))  
                return;

            // 해제 및 장착아이콘 OFF
            UnEquip(_equippedItem);
            FindSlotByItem(_equippedItem).EqiuppedIconOFF();   
        }

        // 장착 및 장착아이콘 ON
        _equippedItem = item; 
        FindSlotByItem(_equippedItem).EquippedIconON();

        // 장착된 아이템 아이콘도 설정
        _equippedItemIcon.sprite = _equippedItem.Icon;  
    }

    /// <summary>
    /// 장착 해제
    /// </summary>
    public void UnEquip(Equipment item)
    {
        // 해제 및 아이콘들도 다 제거
        _equippedItem = null;   
        _equippedItemIcon.sprite = null;
        FindSlotByItem(item).EqiuppedIconOFF();
    }

    /// <summary>
    /// 해당 아이템이 들어있는 슬롯 찾기
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
    /// 이 아이템이 장착된 아이템인지
    /// </summary>
    public bool IsEquipped(Equipment item)
    {
        return _equippedItem == item;
    }

    /// <summary>
    /// 선택된 아이템 정보 패널 켜기
    /// </summary>
    private void SelectedItemInfoPanel_ON()
    {
        _selectedItemInfoPanel.OpenAndInit(_selectedSlot);
    }



    //--------------- 아래부터는 임시 아이템 랜덤 획득 제거 코드


    /// <summary>
    /// 랜덤 아이템 추가 버튼
    /// </summary>
    public void OnClickAddRandomItem()
    {
        // 랜덤한 장비데이터 가져오기
        EquipmentDataSO equipmentDataSO 
            = DataManager.Instance.GetEquipmentDataSOByID(GetRandomEquipmentID().ToString());

        // 랜덤 장비 하나 생성
        Equipment equipment = new Equipment(equipmentDataSO);

        // 추가
        if (AddItem(equipment))
            Debug.Log($"{equipment.Name} ---- 획득!");
        else
            Debug.Log("추가 실패!");

    }
    
    /// <summary>
    /// 아이템 추가
    /// </summary>
    public bool AddItem(Equipment item)
    {
        // 비어있는 슬롯을 찾아서, 슬롯에 아이템 추가 + 가진 아이템리스트에도 추가
        foreach (InventorySlot slot in _inventorySlotList)
        {
            if (slot.IsSlotEmpty)
            {
                slot.AddItem(item);
                _haveItemList.Add(item);

                return true;
            }
        }

        return false; // 실패
    }

    /// <summary>
    /// 랜덤 아이템 제거 버튼
    /// </summary>
    public void OnClickRemoveRandomItem()
    {
        // 가진 아이템 아무것도 없으면 그냥 리턴
        if (_haveItemList.Count <= 0)
            return;

        // 가진 아이템들 중에 아무거나 랜덤픽
        int randomIndex = Random.Range(0, _haveItemList.Count);
        Equipment randomPickedEquipment = _haveItemList[randomIndex];

        // 제거
        if (RemoveItem(randomPickedEquipment))
            Debug.Log($"{randomPickedEquipment.Name} ---- 제거!");
        else
            Debug.Log($"제거 실패!");
    }

    /// <summary>
    /// 아이템 제거
    /// </summary>
    public bool RemoveItem(Equipment item)
    {
        // 해당 아이템이 있는 슬롯을 찾아서 아이템 제거 + 가진 아이템에서도 제거
        foreach (InventorySlot slot in _inventorySlotList)
        {
            if (slot.CurrentItem == item)
            {
                slot.ClearItem();
                _haveItemList.Remove(item);

                return true;
            }
        }

        return false; // 실패
    }

    /// <summary>
    /// 랜덤 EquipmentID 반환
    /// </summary>
    public EquipmentID GetRandomEquipmentID()
    {
        // Enum 값들을 배열로 가져옴
        Array values = Enum.GetValues(typeof(EquipmentID));

        // 랜덤 인덱스 선택
        int randomIndex = Random.Range(0, values.Length);

        // 랜덤 EquipmentID 반환
        return (EquipmentID)values.GetValue(randomIndex);
    }
}
