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
    [SerializeField] private EquipSlot_Gear[] _equipSlotGearArr;     // 장착 슬롯들

    [Title("스탯 텍스트", bold : false)]
    [SerializeField] private TextMeshProUGUI _attackPowerText;      // 공격력 텍스트
    [SerializeField] private TextMeshProUGUI _attackSpeedText;      // 공격속도 텍스트
    [SerializeField] private TextMeshProUGUI _maxHpText;            // 최대체력 텍스트
    [SerializeField] private TextMeshProUGUI _criticalRateText;     // 치명타 확률 텍스트
    [SerializeField] private TextMeshProUGUI _criticalMultipleText; // 치명타 배율 텍스트

    [Title("장비 인벤토리 팝업", bold: false)]
    [SerializeField] private GearInvenPopup _gearInvenPopup;        // 장비 인벤토리 팝업

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSlot_Gear.OnClickGearInvenButton += _gearInvenPopup.Show;  // 장비인벤버튼 눌렀을때 -> 장비인벤팝업 열기
        PlayerStats.OnPlayerStatChanged += Update_StatTexts;    // 플레이어 스탯이 바뀌면 -> 스탯텍스트 업데이트
        EquipGearManager.OnEquipGear += _gearInvenPopup.Hide; // 장비 장착할때 -> 장비인벤팝업 닫기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipSlot_Gear.OnClickGearInvenButton -= _gearInvenPopup.Show;
        PlayerStats.OnPlayerStatChanged -= Update_StatTexts;
        EquipGearManager.OnEquipGear -= _gearInvenPopup.Hide;
    }

    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public override void Show()
    {
        PlayerStatArgs args = PlayerStats.GetCurrentPlayerStatArgs();
        Update_StatTexts(args); // 스탯 텍스트 업데이트
        
        Init_GearEquipSlots(); // 장비장착슬롯 업데이트

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
    /// 장비슬롯들 초기화
    /// </summary>
    private void Init_GearEquipSlots()
    {
        foreach (EquipSlot_Gear equipSlotGear in _equipSlotGearArr)
            equipSlotGear.Init();
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
}
