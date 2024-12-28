using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어 상세정보 팝업 (현재스탯, 착용중인 장비)
/// </summary>
public class PlayerDetailPopup : TabPopupBase
{
    [Title("장착 슬롯들", bold: false)]
    [SerializeField]
    private Dictionary<ItemType, EquipSlot> _equipSlotDict = new Dictionary<ItemType, EquipSlot>(); // 장착 슬롯들 딕셔너리

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

    [Title("인벤토리 팝업", bold: false)]
    [SerializeField] private InvenPopup _invenPopup;                // 인벤토리 팝업

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _weaponInvenButton.onClick.AddListener(() => _invenPopup.Show(ItemType.Weapon)); // 버튼 누르면 타입에 맞는 인벤토리 켜지게
        _armorInvenButton.onClick.AddListener(() => _invenPopup.Show(ItemType.Armor));
        _helmetInvenButton.onClick.AddListener(() => _invenPopup.Show(ItemType.Helmet));
    }

    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public override void Show()
    {
        SelectItemInfoUI.OnItemStatueChanged += Update_EquipSlots;   // 아이템 상태가 바뀌면, 캐릭터 정보팝업 UI 업데이트
        PlayerStats.OnPlayerStatChanged += Update_StatTexts;    // 플레이어 스탯이 바뀌면, 스탯텍스트 업데이트


        PlayerStatArgs args = PlayerStats.Instance.CalculateStats(0);
        Update_StatTexts(args);
        
        Update_EquipSlots();
        
        gameObject.SetActive(true);
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
    /// 장착슬롯 업데이트
    /// </summary>
    private void Update_EquipSlots()
    {
        // 장착한 장비에 따라 장비슬롯 업데이트
        foreach (var kvp in _equipSlotDict)
        {
            ItemType itemType = kvp.Key;
            EquipSlot equipSlot = kvp.Value;    

            Item equipItem = EquipItemManager.GetEquipItem(itemType);
            
            if (equipItem != null)
                equipSlot.Show(equipItem, itemType);
            else
                equipSlot.Hide(itemType);
        }
    }

    /// <summary>
    /// 팝업 끄기
    /// </summary>
    public override void Hide()
    {
        SelectItemInfoUI.OnItemStatueChanged -= Update_EquipSlots;
        PlayerStats.OnPlayerStatChanged -= Update_StatTexts;
        gameObject.SetActive(false);
    }
}
