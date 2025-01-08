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
    [SerializeField] private EquipSlotGear[] _equipSlotGearArr;     // 장착 슬롯들

    [Title("스탯 텍스트", bold : false)]
    [SerializeField] private TextMeshProUGUI _attackPowerText;      // 공격력 텍스트
    [SerializeField] private TextMeshProUGUI _attackSpeedText;      // 공격속도 텍스트
    [SerializeField] private TextMeshProUGUI _maxHpText;            // 최대체력 텍스트
    [SerializeField] private TextMeshProUGUI _criticalRateText;     // 치명타 확률 텍스트
    [SerializeField] private TextMeshProUGUI _criticalMultipleText; // 치명타 배율 텍스트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += Update_StatTexts;    // 플레이어 스탯이 바뀌면, 스탯텍스트 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= Update_StatTexts;
    }

    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public override void Show()
    {
        PlayerStatArgs args = PlayerStats.GetCurrentPlayerStatArgs(0);
        Update_StatTexts(args); // 스탯 텍스트 업데이트
        
        Update_EquipSlots(); // 장착 슬롯 업데이트

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
    private void Update_EquipSlots()
    {
        foreach (EquipSlotGear equipSlotGear in _equipSlotGearArr)
            equipSlotGear.UpdateSlot();
    }
}
