using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _upgradeNameText;      // ���׷��̵� �̸� �ؽ�Ʈ 
    [SerializeField] private TextMeshProUGUI _levelText;            // ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _valueText;            // ���� �� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _costText;             // ���׷��̵� ��� �ؽ�Ʈ
    [SerializeField] private Image _upgradeIcon;                    // ���׷��̵� ������
    [SerializeField] private UpgradeButton _UpgradeButton;          // ���׷��̵� ��ư
    private string _id;                                             // ���׷��̵� ID

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        UpgradeManager.OnUpgradeLevelUp += UpdateUpgradeSlotUI;   // ���׷��̵� ������ �Ҷ�, ���׷��̵� ���� UI ������Ʈ 
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        UpgradeManager.OnUpgradeLevelUp -= UpdateUpgradeSlotUI;
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(string id)
    {
        _id = id;

        _UpgradeButton.Init(_id);   // ���׷��̵� ��ư �ʱ�ȭ

        UpdateUpgradeSlotUI();
    }

    /// <summary>
    /// ���� UI ������Ʈ
    /// </summary>
    private void UpdateUpgradeSlotUI()
    {
        // �� ������ ���׷��̵� ID�� �°� ���� ��������
        Upgrade upgrade = UpgradeManager.GetUpgrade(_id);

        // UI ��ҵ� ������Ʈ
        if (upgrade != null)
        {
            _upgradeNameText.text = upgrade.Name;
            _levelText.text = $"Lv.{upgrade.Level}";
            _costText.text = $"{upgrade.Cost}";
            _upgradeIcon.sprite = ResourceManager.Instance.GetIcon(upgrade.ID.ToString());

            // ũ��Ƽ�� ������ �ۼ�Ƽ���� ǥ��
            if (_id == StatType.CriticalRate.ToString() || _id == StatType.CriticalMultiple.ToString())
                _valueText.text = NumberConverter.ConvertPercentage(upgrade.Value);
            // ���ݼӵ��� �Ҽ��� ���ؼ� ǥ��
            else if (_id == StatType.AttackSpeed.ToString())
                _valueText.text = NumberConverter.ConvertFixedDecimals(upgrade.Value);
            // �������� ���ĺ����� ǥ��
            else
                _valueText.text = NumberConverter.ConvertAlphabet(upgrade.Value);
        }
    }
}
