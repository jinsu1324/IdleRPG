using System;
using System.Collections;
using System.Collections.Generic;
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
        UpgradeManager.OnUpgradeChanged += UpdateUpgradeSlotUI;   // ���׷��̵尡 ����� ��, ���׷��̵� ���� UI ������Ʈ 
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
        Upgrade stat = UpgradeManager.GetUpgrade(_id);

        // UI ��ҵ� ������Ʈ
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
