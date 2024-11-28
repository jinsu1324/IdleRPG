using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgradeButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statNameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Button _upgradeButton;

    private string _statID;
    private PlayerManager _playerManager;

    public void Initialize(string id, PlayerManager playerManager)
    {
        _statID = id;
        _playerManager = playerManager;

        UpdateUI();
    }

    public void OnUpgradeButtonClicked()
    {
        if (_playerManager.LevelUpStat(_statID))
        {
            UpdateUI();
        }
        else
        {
            Debug.Log("이 스탯을 업그레이드 할 골드가 충분하지 않습니다!");
        }
    }

    private void UpdateUI()
    {
        var stat = _playerManager.GetStat(_statID);

        if (stat != null)
        {
            _statNameText.text = stat.Name;
            _levelText.text = $"Lv.{stat.Level}";
            _valueText.text = $"{stat.Value}";
            _costText.text = $"Cost {stat.Cost}";
            //_upgradeButton.interactable = _playerManager.CanAffordStat(stat.Cost);
        }
    }
}
