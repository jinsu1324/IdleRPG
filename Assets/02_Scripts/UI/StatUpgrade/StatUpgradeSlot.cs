using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgradeSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statNameText;     // 스탯 이름 텍스트 
    [SerializeField] private TextMeshProUGUI _levelText;        // 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _valueText;        // 실제 값 텍스트
    [SerializeField] private TextMeshProUGUI _costText;         // 업그레이드 비용 텍스트
    [SerializeField] private Image _statIcon;                   // 스탯 아이콘
    [SerializeField] private Button _upgradeButton;             // 업그레이드 버튼
    private string _statID;                                     // 스탯 ID
    private PlayerManager _playerManager;                       // 플레이어 매니저

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize(string id)
    {
        _statID = id;
        _playerManager = PlayerManager.Instance;

        UpdateUI();
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UpdateUI()
    {
        // 이 슬롯의 스탯ID에 맞게 스탯 가져오기
        Stat stat = _playerManager.GetStatByID(_statID);

        // UI 요소들 업데이트
        if (stat != null)
        {
            _statNameText.text = stat.Name;
            _levelText.text = $"Lv.{stat.Level}";
            _valueText.text = $"{stat.Value}";
            _costText.text = $"Cost {stat.Cost}";
            _statIcon.sprite = ResourceManager.Instance.GetIcon(stat.ID);
            //_upgradeButton.interactable = _playerManager.CanAffordStat(stat.Cost);
        }
    }

    /// <summary>
    /// 업그레이드 버튼 클릭시 호출될 함수
    /// </summary>
    public void OnUpgradeButtonClicked()
    {
        // 레벨업 가능하면 레벨업 로직실행하고 true 반환 (TryLevelUpStatByID 내부에 구현되어있음)
        if (_playerManager.TryLevelUpStatByID(_statID))
        {
            // UI 업데이트
            UpdateUI();
        }
        else
        {
            Debug.Log("이 스탯을 업그레이드 할 골드가 충분하지 않습니다!");
        }
    }
}
