using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReddotComponent))]
public class EquipSlot_Gear : EquipSlot
{
    public static event Action<ItemType> OnClickGearInvenButton;    // 장비인벤버튼 눌렀을 때 이벤트

    [SerializeField] private Button _gearInvenButton;               // 장비인벤버튼
    [SerializeField] private ReddotComponent _reddotComponent;      // 레드닷 컴포넌트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGear += TryUpdate_EquipSlotGear; // 장비 장착할때 -> 장비장착슬롯 업데이트
        EquipGearManager.OnUnEquipGear += TryUpdate_EquipSlotGear; // 장비 해제할때 -> 장비장착슬롯 업데이트
        ItemEnhanceManager.OnItemEnhance += TryUpdate_EquipSlotGear; // 아이템 강화할때 -> 장비장착슬롯 업데이트

        _gearInvenButton.onClick.AddListener(Notify_OnClickGearInvenButton); // 장비인벤버튼 누르면 -> 이벤트 노티
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipGearManager.OnEquipGear -= TryUpdate_EquipSlotGear;
        EquipGearManager.OnUnEquipGear -= TryUpdate_EquipSlotGear;
        ItemEnhanceManager.OnItemEnhance -= TryUpdate_EquipSlotGear;

        _gearInvenButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        Update_EquipSlotGear();
    }

    /// <summary>
    /// 장비장착슬롯 업데이트
    /// </summary>
    private void TryUpdate_EquipSlotGear(Item item)
    {
        // 이 슬롯하고 아이템타입이 다르면 무시
        if (_slotItemType != item.ItemType) 
            return;

        Update_EquipSlotGear();
        
    }

    /// <summary>
    /// 실제 장비장착슬롯 업데이트 로직
    /// </summary>
    private void Update_EquipSlotGear()
    {
        // 장착한 장비 가져오기
        Item equipGear = EquipGearManager.GetEquippedItem(_slotItemType);

        // 장착한 장비 있으면 슬롯 보여주고(+업데이트), 없으면 비우기
        if (equipGear != null)
            UpdateSlot(equipGear);
        else
            EmptySlot();

        // 레드닷 업데이트
        Update_ReddotComponent();
    }

    /// <summary>
    /// 장비인벤버튼 클릭 이벤트 노티
    /// </summary>
    private void Notify_OnClickGearInvenButton()
    {
        OnClickGearInvenButton?.Invoke(_slotItemType);
    }

    /// <summary>
    /// 레드닷 컴포넌트 업데이트 (인벤토리에 강화가능한 아이템이 있는지?)
    /// </summary>
    public void Update_ReddotComponent()
    {
        _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_slotItemType));
    }
}
