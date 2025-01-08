using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReddotComponent))]
public class EquipSlotGear : EquipSlotBase
{
    [Title("장비 장착슬롯 관련", bold: false)]
    [SerializeField] private GearInvenPopup _gearInvenPopup;    // 장비 인벤토리 팝업
    [SerializeField] private Button _invenOpenButton;           // 인벤토리 오픈 버튼
    [SerializeField] private ReddotComponent _reddotComponent;  // 레드닷 컴포넌트

    /// <summary>
    /// OnEnable
    /// </summary>
    protected override void OnEnable()
    {
        ItemInven.OnItemInvenChanged += UpdateReddotComponent; // 가지고 있는 아이템이 변경되었을 때, 장비장착슬롯 레드닷 업데이트
        EquipGearManager.OnEquipGearChanged += TryUpdate_EquipSlotGear; // 장착한 장비가 변경되었을 때, 장비장착슬롯 업데이트 시도

        _invenOpenButton.onClick.AddListener(ShowInvenPopup);   // 인벤열기 버튼 클릭하면, 인벤 열기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    protected override void OnDisable()
    {
        ItemInven.OnItemInvenChanged -= UpdateReddotComponent;
        EquipGearManager.OnEquipGearChanged -= TryUpdate_EquipSlotGear;

        _invenOpenButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 장비 장착 슬롯 업데이트 시도
    /// </summary>
    private void TryUpdate_EquipSlotGear(OnEquipGearChangedArgs args)
    {
        if (IsSameSlotItemType(args.ItemType) == false) 
            return;

        UpdateSlot();
    }

    /// <summary>
    /// 장착 or 해제중인 아이템의 아이템타입이, 현재 슬롯의 아이템타입과 같은지?
    /// </summary>
    private bool IsSameSlotItemType(ItemType itemType)
    {
        return _slotItemType == itemType;
    }

    /// <summary>
    /// 슬롯 업데이트
    /// </summary>
    public void UpdateSlot()
    {
        // 장착한 장비 가져오기
        Gear gear = EquipGearManager.GetEquipGear(_slotItemType);

        // 장착한 장비 있으면 정보 보여주고, 없으면 빈슬롯 보여주기
        if (gear != null)
            ShowInfo(gear);
        else
            ShowEmpty();

        UpdateReddotComponent(); // 레드닷 업데이트
        HideInvenPopup(); // 인벤팝업 끄기
    }

    /// <summary>
    /// 인벤팝업 열기
    /// </summary>
    private void ShowInvenPopup()
    {
        _gearInvenPopup.Show(_slotItemType);
    }

    /// <summary>
    /// 인벤팝업 끄기
    /// </summary>
    private void HideInvenPopup()
    {
        _gearInvenPopup.Hide();
    } 

    /// <summary>
    /// 레드닷 컴포넌트 업데이트 (인벤토리에 강화가능한 아이템이 있는지?)
    /// </summary>
    public void UpdateReddotComponent()
    {
        _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_slotItemType));
    }
}
