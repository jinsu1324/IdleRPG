using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private StatType _statType;                    // ���׷��̵��� ����Ÿ��
    [SerializeField] private TextMeshProUGUI _upgradeNameText;      // ���׷��̵� �̸� �ؽ�Ʈ 
    [SerializeField] private TextMeshProUGUI _levelText;            // ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _valueText;            // ���� �� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _costText;             // ���׷��̵� ��� �ؽ�Ʈ
    [SerializeField] private Image _upgradeIcon;                    // ���׷��̵� ������
    [SerializeField] private UpgradeButton _UpgradeButton;          // ���׷��̵� ��ư
    [SerializeField] private GameObject _dimd;                      // ���׷��̵� ��ư ����
    private int _upgradeCost;                                       // ���׷��̵� ���

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        Upgrade.OnUpgradeChanged += UpdateUpgradeSlotUI;   // ���׷��̵� ����Ǿ��� �� -> ���׷��̵� ���� UI ������Ʈ 
        GoldManager.OnGoldChange += UpdateDimd; // ��� �ٲ������ -> ���� ������Ʈ
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
    /// ���� UI ������Ʈ
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

        // ũ��Ƽ�� ������ �ۼ�Ƽ���� ǥ��
        if (_statType.ToString() == StatType.CriticalRate.ToString() || _statType.ToString() == StatType.CriticalMultiple.ToString())
            _valueText.text = NumberConverter.ConvertPercentage(upgrade.Value);
        // ���ݼӵ��� �Ҽ��� ���ؼ� ǥ��
        else if (_statType.ToString() == StatType.AttackSpeed.ToString())
            _valueText.text = NumberConverter.ConvertFixedDecimals(upgrade.Value);
        // �������� ���ĺ����� ǥ��
        else
            _valueText.text = NumberConverter.ConvertAlphabet(upgrade.Value);

        UpdateDimd();
    }

    /// <summary>
    /// ���� ������Ʈ
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
    /// ����Ÿ�� ��������
    /// </summary>
    public StatType GetStatType() => _statType;
}
