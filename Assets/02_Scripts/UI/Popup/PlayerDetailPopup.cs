using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어 스탯, 착용중인 장비 상세정보 팝업
/// </summary>
public class PlayerDetailPopup : PopupBase
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

    [Title("인벤토리 팝업", bold: false)]
    [SerializeField] private InvenPopup _invenPopup;                // 인벤토리 팝업



    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // 버튼들 누르면 아이템 타입에 맞는 팝업 켜지게
        _weaponInvenButton.onClick.AddListener(() => _invenPopup.Show(ItemType.Weapon));
        _armorInvenButton.onClick.AddListener(() => _invenPopup.Show(ItemType.Armor));
    }

    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public override void Show()
    {
        UIUpdate();

        SelectItemInfo.OnItemInfoChanged += UIUpdate;   // 

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 팝업 끄기
    /// </summary>
    public override void Hide()
    {
        SelectItemInfo.OnItemInfoChanged -= UIUpdate;   //


        gameObject.SetActive(false);
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UIUpdate()
    {
        _attackPowerText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.AttackPower)}";
        _attackSpeedText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.AttackSpeed)}";
        _maxHpText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.MaxHp)}";
        _criticalRateText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.CriticalRate)}";
        _criticalMultipleText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.CriticalMultiple)}";


        foreach (var kvp in _equipSlotDict)
        {
            ItemType itemType = kvp.Key;
            EquipSlot equipSlot = kvp.Value;    

            Item equipItem = EquipItemManager.GetEquipItem(itemType);
            
            if (equipItem != null)
                equipSlot.Show_EquipSlotInfo(equipItem);
            else
                equipSlot.OFF_EquipSlotInfo();
        }
    }
}
