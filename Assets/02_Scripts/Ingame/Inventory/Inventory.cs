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

    private List<Equipment> _haveItemList = new List<Equipment>();      // 가지고 있는 아이템 리스트

    [SerializeField]
    private List<Image> _equippedItemIconList;                          // 장착한 아이템 아이콘 리스트 (최대 3개)
    private List<Equipment> _equippedItemList = new List<Equipment>();  // 장착된 아이템 리스트
    private int _maxEquipableItemCount = 3;                             // 최대 장착 가능한 아이템 개수

    [SerializeField]
    private List<InventorySlot> _inventorySlotList;                     // 인벤토리 슬롯 리스트
    private InventorySlot _selectedSlot;                                // 선택된 슬롯

    [SerializeField]
    private SelectItemInfoPanel _selectedItemInfoPanel;                 // 선택된 아이템 정보 패널

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
    /// 장착
    /// </summary>
    public void Equip(Equipment item)
    {
        // 이미 장착된 아이템이, 최대 장착갯수를 초과했으면 그냥 리턴
        if (_equippedItemList.Count >= _maxEquipableItemCount)
        {
            Debug.Log("장착 가능한 최대 아이템 개수를 초과했습니다!");
            return;
        }

        // 이미 장착된 아이템이면 그냥 리턴
        if (IsEquipped(item))
        {
            Debug.Log("이미 장착된 아이템입니다!");
            return;
        }

        // 장착
        _equippedItemList.Add(item);

        // 플레이어 스탯에 아이템 스탯들 추가
        AddItemStatsToPlayer(item.GetStatDictionaryByLevel(), item);

        // 슬롯에 장착 아이콘 ON
        FindSlotByItem(item).EquippedIconON();

        // 장착 아이콘 리스트 UI 업데이트
        Update_EquippedItemIconListUI();

        // 스탯 보여주는 UI 업데이트
        PlayerStats.Instance.AllStatUIUpdate(); 
    }

    /// <summary>
    /// 장착 해제
    /// </summary>
    public void UnEquip(Equipment item)
    {
        // 해제하려는 아이템이 장착되지 않았으면 그냥 리턴
        if (_equippedItemList.Contains(item) == false)
        {
            Debug.Log("해제하려는 아이템이 장착되지 않았습니다!");
            return;
        }

        // 해제
        _equippedItemList.Remove(item);

        // 플레이어 스탯에서 아이템 스탯들 제거
        RemoveItemStatsToPlayer(item.GetStatDictionaryByLevel(), item);

        // 슬롯에 장착 아이콘 OFF
        FindSlotByItem(item).EqiuppedIconOFF();

        // 장착 아이콘 리스트 UI 업데이트
        Update_EquippedItemIconListUI();

        // 스탯 보여주는 UI 업데이트
        PlayerStats.Instance.AllStatUIUpdate();
    }

    /// <summary>
    /// 플레이어 스탯에 아이템 스탯들 추가
    /// </summary>
    private void AddItemStatsToPlayer(Dictionary<StatType, int> statDict, Equipment selfItem)
    {
        foreach (var kvp in statDict)
            PlayerStats.Instance.AddModifier(kvp.Key, kvp.Value, selfItem);
    }

    /// <summary>
    /// 플레이어 스탯에서 아이템 스탯들 제거
    /// </summary>
    private void RemoveItemStatsToPlayer(Dictionary<StatType, int> statDict, Equipment selfItem)
    {
        foreach (var kvp in statDict)
            PlayerStats.Instance.RemoveModifier(kvp.Key, selfItem);
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
    /// 장착한 아이템 아이콘 리스트 UI 업데이트
    /// </summary>
    private void Update_EquippedItemIconListUI()
    {
        for (int i = 0; i < _equippedItemIconList.Count; i++)   // 3개만큼 반복 (아이콘 슬롯 최대 갯수)
        {
            if (i < _equippedItemList.Count) // 가진 아이템 갯수까지는 아이콘 표시
            {
                _equippedItemIconList[i].sprite = _equippedItemList[i].Icon;
                _equippedItemIconList[i].gameObject.SetActive(true);
            }
            else // 나머지는 비활성화
            {
                _equippedItemIconList[i].sprite = null;
                _equippedItemIconList[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 해당 아이템이 장착된 아이템인지 확인
    /// </summary>
    public bool IsEquipped(Equipment item)
    {
        return _equippedItemList.Contains(item);
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

        //EquipmentDataSO equipmentDataSO
        //   = DataManager.Instance.GetEquipmentDataSOByID(EquipmentID.Armor_ForestArmor.ToString());


        // 랜덤 장비 하나 생성
        //Equipment equipment = new Equipment(equipmentDataSO);
        Equipment equipment = new Equipment(equipmentDataSO, 1);

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







    //private void Start()
    //{
    //    // PlayerStats 초기화
    //    PlayerStats playerStats = new PlayerStats();

    //    // 장비 생성
    //    Equipment sword = new Equipment("전설의 검", new Dictionary<StatType, float>
    //    {
    //        { StatType.AttackPower, 50 },
    //        { StatType.AttackSpeed, 10 }
    //    });

    //    // 장비 장착
    //    sword.Equip(playerStats);
    //    Debug.Log("장착 후 공격력: " + playerStats.GetFinalStat(StatType.AttackPower)); // +50

    //    // 장비 해제
    //    sword.Unequip(playerStats);
    //    Debug.Log("해제 후 공격력: " + playerStats.GetFinalStat(StatType.AttackPower)); // 0
    //}
}
