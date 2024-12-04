using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgradeSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statNameText;         // ���� �̸� �ؽ�Ʈ 
    [SerializeField] private TextMeshProUGUI _levelText;            // ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _valueText;            // ���� �� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _costText;             // ���׷��̵� ��� �ؽ�Ʈ
    [SerializeField] private Image _statIcon;                       // ���� ������
    [SerializeField] private StatUpgradeButton _statUpgradeButton;  // ���� ���׷��̵� ��ư
    private StatID _statID;                                         // ���� ID

    /// <summary>
    /// Start
    /// </summary>
    private void OnEnable()
    {
        PlayerManager.Instance.OnStatChanged += UpdateSlotUI;
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(StatID id)
    {
        _statID = id;

        _statUpgradeButton.Init(_statID);   // ���׷��̵� ��ư �ʱ�ȭ

        UpdateSlotUI(null);
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UpdateSlotUI(OnStatChangedArgs? args)
    {
        // �� ������ ����ID�� �°� ���� ��������
        Stat stat = PlayerManager.Instance.GetStat(_statID);

        // UI ��ҵ� ������Ʈ
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
