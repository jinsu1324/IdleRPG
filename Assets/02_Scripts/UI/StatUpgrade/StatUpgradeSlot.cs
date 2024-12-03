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
    private Action _onUpdateTotalCombatPowerText;                   // 통합 전투력 텍스트 업데이트 함수를 저장할 대리자


    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(StatID id, Action updateTotalCombatPowerText)
    {
        _statID = id;

        _onUpdateTotalCombatPowerText = updateTotalCombatPowerText; // 통합 전투력 텍스트 업데이트 함수 대리자에 저장

        _statUpgradeButton.Init(UpdateUI, _statID);   // 업그레이드 버튼 초기화

        UpdateUI();

    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UpdateUI()
    {
        // 이 슬롯의 스탯ID에 맞게 스탯 가져오기
        StatComponent stat = StatManager.Instance.GetStat(_statID);

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

        _onUpdateTotalCombatPowerText?.Invoke();    // 총합 전투력 텍스트 업데이트
    }

    /// <summary>
    /// 업그레이드 버튼 클릭시 호출될 함수
    /// </summary>
    public void OnUpgradeButtonClicked()
    {
        // 레벨업 가능하면 레벨업 로직실행하고 true 반환 (TryLevelUpStatByID 내부에 구현되어있음)
        if (StatManager.Instance.TryLevelUpStatByID(_statID))
        {
            // UI 업데이트
            UpdateUI();

            // 토스트 메시지 호출
            ToastManager.Instance.ShowToastCombatPower();
        }
        else
        {
            Debug.Log("이 스탯을 업그레이드 할 골드가 충분하지 않습니다!");
        }
    }
}
