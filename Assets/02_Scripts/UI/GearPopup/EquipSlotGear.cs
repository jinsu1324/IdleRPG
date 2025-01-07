using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReddotComponent))]
public class EquipSlotGear : EquipSlot
{
    [Title("레드닷 컴포넌트 (장비만 업데이트됨)", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;  // 레드닷 컴포넌트

    [Title("인벤토리 열기 버튼", bold: false)]
    [SerializeField] private Button _invenOpenButton;

    [Title("장비 인벤토리 팝업", bold: false)]
    [SerializeField] private GearInvenPopup _gearInvenPopup;        // 장비 인벤토리 팝업

    protected override void OnEnable()
    {
        ItemInven.OnItemInvenChanged += UpdateReddotComponent; // 가지고 있는 아이템이 변경되었을 때, 장착슬롯 레드닷 업데이트
        EquipGearManager.OnEquipGearChanged += Handler;

        _invenOpenButton.onClick.AddListener(() => _gearInvenPopup.Show(_itemType));
    }

    protected override void OnDisable()
    {
        ItemInven.OnItemInvenChanged -= UpdateReddotComponent;
        EquipGearManager.OnEquipGearChanged -= Handler;


        _invenOpenButton.onClick.RemoveAllListeners();
    }

    public override void ShowInfoGO(IItem item)
    {
        base.ShowInfoGO(item);

        
    }

    public override void ShowEmptyGO()
    {
        base.ShowEmptyGO();
    }

    public void UpdateEquipSlotGear()
    {
        Gear gear = EquipGearManager.GetEquipGear(_itemType);

        if (gear != null)
        {
            ShowInfoGO(gear);
            UpdateReddotComponent(); // 레드닷 업데이트
            _gearInvenPopup.Hide(); // 장착되면 팝업 끄기
            return;
        }
        else
        {
            ShowEmptyGO();
            UpdateReddotComponent(); // 레드닷 업데이트
            _gearInvenPopup.Hide();
            return;
        }
    }

    private void Handler(OnEquipGearChangedArgs args)
    {
        if (args.ItemType != _itemType)
            return;

        UpdateEquipSlotGear();
    }



    /// <summary>
    /// 레드닷 컴포넌트 업데이트 (인벤토리에 강화가능한 아이템이 있는지?)
    /// </summary>
    public void UpdateReddotComponent()
    {
        _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_itemType));
    }
}
