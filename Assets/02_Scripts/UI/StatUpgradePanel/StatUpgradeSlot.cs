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
    private string _id;                                             // ���� ID

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStatContainer.OnStatChanged += UpdateStatUpgradeSlotUI;   // ������ ����� ��, ���� ���׷��̵� ���� UI ������Ʈ 
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(string id)
    {
        _id = id;

        _statUpgradeButton.Init(_id);   // ���׷��̵� ��ư �ʱ�ȭ

        OnStatChangedArgs args = new OnStatChangedArgs();
        UpdateStatUpgradeSlotUI(args);
    }

    /// <summary>
    /// ���� UI ������Ʈ
    /// </summary>
    private void UpdateStatUpgradeSlotUI(OnStatChangedArgs args)
    {
        // �� ������ ����ID�� �°� ���� ��������
        Stat stat = PlayerStatContainer.Instance.GetStat(_id);

        //Debug.Log($"{_statID} ������ �����̸� : {stat.Name}");

        // UI ��ҵ� ������Ʈ
        if (stat != null)
        {
            _statNameText.text = stat.Name;
            _levelText.text = $"Lv.{stat.Level}";
            _valueText.text = AlphabetNumConverter.Convert(stat.Value);
            _costText.text = AlphabetNumConverter.Convert(stat.Cost);
            _statIcon.sprite = ResourceManager.Instance.GetIcon(stat.ID.ToString());
            //_upgradeButton.interactable = _playerManager.CanAffordStat(stat.Cost);
        }
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStatContainer.OnStatChanged -= UpdateStatUpgradeSlotUI;
    }
}
