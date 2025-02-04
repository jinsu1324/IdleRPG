using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private StatType _statType;                    // 업그레이드할 스탯타입
    [SerializeField] private TextMeshProUGUI _upgradeNameText;      // 업그레이드 이름 텍스트 
    [SerializeField] private TextMeshProUGUI _levelText;            // 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _valueText;            // 실제 값 텍스트
    [SerializeField] private TextMeshProUGUI _costText;             // 업그레이드 비용 텍스트
    [SerializeField] private Image _upgradeIcon;                    // 업그레이드 아이콘
    [SerializeField] private UpgradeButton _UpgradeButton;          // 업그레이드 버튼
    [SerializeField] private GameObject _dimd;                      // 업그레이드 버튼 딤드
    private int _upgradeCost;                                       // 업그레이드 비용

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        Upgrade.OnUpgradeChanged += UpdateUpgradeSlotUI;   // 업그레이드 변경되었을 때 -> 업그레이드 슬롯 UI 업데이트 
        GoldManager.OnGoldChange += UpdateDimd; // 골드 바뀌었을때 -> 딤드 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        Upgrade.OnUpgradeChanged -= UpdateUpgradeSlotUI;
        GoldManager.OnGoldChange -= UpdateDimd;
    }

    /// <summary>
    /// 슬롯 UI 업데이트
    /// </summary>
    private void UpdateUpgradeSlotUI(Upgrade upgrade)
    {
        StatType upgradeStatType = (StatType)Enum.Parse(typeof(StatType), upgrade.UpgradeStatType);
        if (_statType != upgradeStatType)
            return;

        _upgradeNameText.text = upgrade.Name;
        _levelText.text = $"Lv.{upgrade.Level}";
        _upgradeCost = upgrade.Cost;
        _costText.text = NumberConverter.ConvertAlphabet(upgrade.Cost);
        _upgradeIcon.sprite = ResourceManager.Instance.GetIcon(_statType);

        // 크리티컬 관련은 퍼센티지로 표현
        if (_statType.ToString() == StatType.CriticalRate.ToString() || _statType.ToString() == StatType.CriticalMultiple.ToString())
            _valueText.text = NumberConverter.ConvertPercentage(upgrade.Value);
        // 공격속도는 소수점 정해서 표현
        else if (_statType.ToString() == StatType.AttackSpeed.ToString())
            _valueText.text = NumberConverter.ConvertFixedDecimals(upgrade.Value);
        // 나머지는 알파벳으로 표현
        else
            _valueText.text = NumberConverter.ConvertAlphabet(upgrade.Value);

        UpdateDimd();
    }

    /// <summary>
    /// 딤드 업데이트
    /// </summary>
    private void UpdateDimd(int amount = 0)
    {
        bool canUpgrade = GoldManager.HasEnoughGold(_upgradeCost);
        _dimd.SetActive(!canUpgrade);

        if (canUpgrade)
            _costText.color = Color.white;
        else
            _costText.color = Color.red;
    }

    /// <summary>
    /// 스탯타입 가져오기
    /// </summary>
    public StatType GetStatType() => _statType;
}
