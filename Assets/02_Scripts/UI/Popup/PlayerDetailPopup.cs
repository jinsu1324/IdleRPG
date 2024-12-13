using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDetailPopup : PopupBase
{
    [SerializeField] private TextMeshProUGUI _attackPowerText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;
    [SerializeField] private TextMeshProUGUI _maxHpText;
    [SerializeField] private TextMeshProUGUI _criticalRateText;
    [SerializeField] private TextMeshProUGUI _criticalMultipleText;

    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Show()
    {
        UIUpdate();
        gameObject.SetActive(true);
    }

    private void UIUpdate()
    {
        _attackPowerText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.AttackPower)}";
        _attackSpeedText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.AttackSpeed)}";
        _maxHpText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.MaxHp)}";
        _criticalRateText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.CriticalRate)}";
        _criticalMultipleText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.CriticalMultiple)}";
    }
}
