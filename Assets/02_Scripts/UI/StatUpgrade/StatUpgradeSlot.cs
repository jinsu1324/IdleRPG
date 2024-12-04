using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgradeSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statNameText;         // 스탯 이름 텍스트 
    [SerializeField] private TextMeshProUGUI _levelText;            // 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _valueText;            // 실제 값 텍스트
    [SerializeField] private TextMeshProUGUI _costText;             // 업그레이드 비용 텍스트
    [SerializeField] private Image _statIcon;                       // 스탯 아이콘
    [SerializeField] private StatUpgradeButton _statUpgradeButton;  // 스탯 업그레이드 버튼
    private StatID _statID;                                         // 스탯 ID

    /// <summary>
    /// Start
    /// </summary>
    private void OnEnable()
    {
        PlayerManager.Instance.OnStatChanged += UpdateSlotUI;
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(StatID id)
    {
        _statID = id;

        _statUpgradeButton.Init(_statID);   // 업그레이드 버튼 초기화

        UpdateSlotUI(null);
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UpdateSlotUI(OnStatChangedArgs? args)
    {
        // 이 슬롯의 스탯ID에 맞게 스탯 가져오기
        Stat stat = PlayerManager.Instance.GetStat(_statID);

        // UI 요소들 업데이트
        if (stat != null)
        {
            _statNameText.text = stat.Name;
            _levelText.text = $"Lv.{stat.Level}";
            _valueText.text = AlphabetNumConverter.Convert(stat.Value);
            _costText.text = AlphabetNumConverter.Convert(stat.Cost);
            _statIcon.sprite = ResourceManager.Instance.GetIcon(stat.StatID.ToString());
            //_upgradeButton.interactable = _playerManager.CanAffordStat(stat.Cost);
        }
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerManager.Instance.OnStatChanged -= UpdateSlotUI;
    }
}
