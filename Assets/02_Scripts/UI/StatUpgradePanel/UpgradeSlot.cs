using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private UpgradeID _upgradeID;                  // 업그레이드 ID
    [SerializeField] private TextMeshProUGUI _upgradeNameText;      // 업그레이드 이름 텍스트 
    [SerializeField] private TextMeshProUGUI _levelText;            // 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _valueText;            // 실제 값 텍스트
    [SerializeField] private TextMeshProUGUI _costText;             // 업그레이드 비용 텍스트
    [SerializeField] private Image _upgradeIcon;                    // 업그레이드 아이콘
    [SerializeField] private UpgradeButton _UpgradeButton;          // 업그레이드 버튼

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        UpgradeManager.OnUpgradeLevelUp += UpdateUpgradeSlotUI;   // 업그레이드 레벨업 할때, 업그레이드 슬롯 UI 업데이트 
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        UpgradeManager.OnUpgradeLevelUp -= UpdateUpgradeSlotUI;
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        _UpgradeButton.Init(_upgradeID);
        UpdateUpgradeSlotUI();
    }

    /// <summary>
    /// 슬롯 UI 업데이트
    /// </summary>
    private void UpdateUpgradeSlotUI()
    {
        // 이 슬롯의 업그레이드 ID에 맞게 스탯 가져오기
        Upgrade upgrade = UpgradeManager.GetUpgrade(_upgradeID.ToString());

        // UI 요소들 업데이트
        if (upgrade != null)
        {
            _upgradeNameText.text = upgrade.Name;
            _levelText.text = $"Lv.{upgrade.Level}";
            _costText.text = $"{upgrade.Cost}";
            _upgradeIcon.sprite = ResourceManager.Instance.GetIcon(upgrade.ID.ToString());

            // 크리티컬 관련은 퍼센티지로 표현
            if (_upgradeID.ToString() == StatType.CriticalRate.ToString() || _upgradeID.ToString() == StatType.CriticalMultiple.ToString())
                _valueText.text = NumberConverter.ConvertPercentage(upgrade.Value);
            // 공격속도는 소수점 정해서 표현
            else if (_upgradeID.ToString() == StatType.AttackSpeed.ToString())
                _valueText.text = NumberConverter.ConvertFixedDecimals(upgrade.Value);
            // 나머지는 알파벳으로 표현
            else
                _valueText.text = NumberConverter.ConvertAlphabet(upgrade.Value);
        }
    }
}
