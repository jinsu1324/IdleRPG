using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장비 팝업 (현재스탯, 착용중인 장비)
/// </summary>
public class GearPopup : BottomTabPopupBase
{
    [Title("장착 슬롯들", bold: false)]
    [SerializeField]
    private Dictionary<ItemType, EquipSlot> _equipSlotDict;         // 장착 슬롯들 딕셔너리

    [Title("스탯 텍스트", bold : false)]
    [SerializeField] private TextMeshProUGUI _attackPowerText;      // 공격력 텍스트
    [SerializeField] private TextMeshProUGUI _attackSpeedText;      // 공격속도 텍스트
    [SerializeField] private TextMeshProUGUI _maxHpText;            // 최대체력 텍스트
    [SerializeField] private TextMeshProUGUI _criticalRateText;     // 치명타 확률 텍스트
    [SerializeField] private TextMeshProUGUI _criticalMultipleText; // 치명타 배율 텍스트

    [Title("인벤토리 버튼", bold : false)]
    [SerializeField] private Button _weaponInvenButton;             // 무기 인벤토리 버튼
    [SerializeField] private Button _armorInvenButton;              // 갑옷 인벤토리 버튼
    [SerializeField] private Button _helmetInvenButton;             // 헬멧 인벤토리 버튼

    [Title("장비 인벤토리 팝업", bold: false)]
    [SerializeField] private GearInvenPopup _gearInvenPopup;        // 장비 인벤토리 팝업

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGearChanged += Update_EquipSlots; // 장착 장비가 바뀌면, 장비팝업 장착슬롯 업데이트
        PlayerStats.OnPlayerStatChanged += Update_StatTexts;    // 플레이어 스탯이 바뀌면, 스탯텍스트 업데이트

        _weaponInvenButton.onClick.AddListener(() => _gearInvenPopup.Show(ItemType.Weapon)); // 무기 인벤토리 버튼 할당
        _armorInvenButton.onClick.AddListener(() => _gearInvenPopup.Show(ItemType.Armor));   // 갑옷 인벤토리 버튼 할당
        _helmetInvenButton.onClick.AddListener(() => _gearInvenPopup.Show(ItemType.Helmet)); // 헬멧 인벤토리 버튼 할당
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipGearManager.OnEquipGearChanged -= Update_EquipSlots;
        PlayerStats.OnPlayerStatChanged -= Update_StatTexts;

        _weaponInvenButton.onClick.RemoveAllListeners();
        _armorInvenButton.onClick.RemoveAllListeners();
        _helmetInvenButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public override void Show()
    {
        PlayerStatArgs args = PlayerStats.GetCurrentPlayerStatArgs(0);
        Update_StatTexts(args);

        OnEquipGearChangedArgs equipArgs = new OnEquipGearChangedArgs();
        Update_EquipSlots(equipArgs);

        _gearInvenPopup.Hide();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 팝업 끄기
    /// </summary>
    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 스탯텍스트 업데이트
    /// </summary>
    private void Update_StatTexts(PlayerStatArgs args)
    {
        _attackPowerText.text = $"{args.AttackPower}";
        _attackSpeedText.text = $"{args.AttackSpeed}";
        _maxHpText.text = $"{args.MaxHp}";
        _criticalRateText.text = $"{args.CriticalRate}";
        _criticalMultipleText.text = $"{args.CriticalMultiple}";
    }

    /// <summary>
    /// 장착한 장비에 따라 장비슬롯 업데이트
    /// </summary>
    private void Update_EquipSlots(OnEquipGearChangedArgs args)
    {
        foreach (var kvp in _equipSlotDict)
        {
            ItemType itemType = kvp.Key;
            EquipSlot equipSlot = kvp.Value;    

            IItem equipItem = EquipGearManager.GetEquipGear(itemType);
            
            if (equipItem != null)
                equipSlot.ShowInfoGO(equipItem);
            else
                equipSlot.ShowEmptyGO();
        }
    }
}
