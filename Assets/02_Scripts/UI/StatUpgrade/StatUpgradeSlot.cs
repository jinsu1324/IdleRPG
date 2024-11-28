using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgradeSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statNameText;     // ���� �̸� �ؽ�Ʈ 
    [SerializeField] private TextMeshProUGUI _levelText;        // ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _valueText;        // ���� �� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _costText;         // ���׷��̵� ��� �ؽ�Ʈ
    [SerializeField] private Button _upgradeButton;             // ���׷��̵� ��ư
    private string _statID;                                     // ���� ID
    private PlayerManager _playerManager;                       // �÷��̾� �Ŵ���

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Initialize(string id, PlayerManager playerManager)
    {
        _statID = id;
        _playerManager = playerManager;

        UpdateUI();
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UpdateUI()
    {
        // �� ������ ����ID�� �°� ���� ��������
        Stat stat = _playerManager.GetStatByID(_statID);

        // UI ��ҵ� ������Ʈ
        if (stat != null)
        {
            _statNameText.text = stat.Name;
            _levelText.text = $"Lv.{stat.Level}";
            _valueText.text = $"{stat.Value}";
            _costText.text = $"Cost {stat.Cost}";
            //_upgradeButton.interactable = _playerManager.CanAffordStat(stat.Cost);
        }
    }

    /// <summary>
    /// ���׷��̵� ��ư Ŭ���� ȣ��� �Լ�
    /// </summary>
    public void OnUpgradeButtonClicked()
    {
        // ������ �����ϸ� ������ ���������ϰ� true ��ȯ (TryLevelUpStatByID ���ο� �����Ǿ�����)
        if (_playerManager.TryLevelUpStatByID(_statID))
        {
            // UI ������Ʈ
            UpdateUI();
        }
        else
        {
            Debug.Log("�� ������ ���׷��̵� �� ��尡 ������� �ʽ��ϴ�!");
        }
    }
}
