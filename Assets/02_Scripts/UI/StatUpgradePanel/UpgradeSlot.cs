using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _upgradeNameText;      // 업그레이드 이름 텍스트 
    [SerializeField] private TextMeshProUGUI _levelText;            // 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _valueText;            // 실제 값 텍스트
    [SerializeField] private TextMeshProUGUI _costText;             // 업그레이드 비용 텍스트
    [SerializeField] private Image _upgradeIcon;                    // 업그레이드 아이콘
    [SerializeField] private UpgradeButton _UpgradeButton;          // 업그레이드 버튼
    private string _id;                                             // 업그레이드 ID

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        UpgradeManager.OnUpgradeChanged += UpdateUpgradeSlotUI;   // 업그레이드가 변경될 때, 업그레이드 슬롯 UI 업데이트 
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(string id)
    {
        _id = id;

        _UpgradeButton.Init(_id);   // 업그레이드 버튼 초기화

        UpdateUpgradeSlotUI();
    }

    /// <summary>
    /// 슬롯 UI 업데이트
    /// </summary>
    private void UpdateUpgradeSlotUI()
    {
        // 이 슬롯의 업그레이드 ID에 맞게 스탯 가져오기
        Upgrade stat = UpgradeManager.GetUpgrade(_id);

        // UI 요소들 업데이트
        if (stat != null)
        {
            _upgradeNameText.text = stat.Name;
            _levelText.text = $"Lv.{stat.Level}";
            _valueText.text = AlphabetNumConverter.Convert(stat.Value);
            _costText.text = AlphabetNumConverter.Convert(stat.Cost);
            _upgradeIcon.sprite = ResourceManager.Instance.GetIcon(stat.ID.ToString());
            //_upgradeButton.interactable = _playerManager.CanAffordStat(stat.Cost);
        }
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        UpgradeManager.OnUpgradeChanged -= UpdateUpgradeSlotUI;
    }
}
