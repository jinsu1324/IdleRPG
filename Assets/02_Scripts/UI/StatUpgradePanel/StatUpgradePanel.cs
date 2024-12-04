using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatUpgradePanel : MonoBehaviour
{
    [SerializeField] private StatUpgradeSlot _statUpgradeSlotPrefab;      // ������ ���� ���׷��̵� ���� ������
    [SerializeField] private RectTransform _slotParent;                   // ���Ե� ������ �θ�
    [SerializeField] private TextMeshProUGUI _totalCombatPowerText;       // ���� ������ �ؽ�Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStatContainer.OnStatChanged += UpdateTotalPowerTextUI;    // ������ ����� ��, ���� ������ �ؽ�ƮUI ������Ʈ 
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        SpawnSlots();
    }

    /// <summary>
    /// ���Ե� ���� + �ʱ�ȭ
    /// </summary>
    private void SpawnSlots()
    {
        // �÷��̾� ���� ������ŭ �ݺ�
        foreach (Stat stat in PlayerStatContainer.Instance.GetAllStats())
        {
            StatUpgradeSlot statUpgradeSlot = Instantiate(_statUpgradeSlotPrefab, _slotParent);
            statUpgradeSlot.Init(stat.StatID);
        }

        OnStatChangedArgs args = new OnStatChangedArgs() { TotalPower = PlayerStatContainer.Instance.GetTotalPower() };
        UpdateTotalPowerTextUI(args);
    }

    /// <summary>
    /// ���� ������ �ؽ�Ʈ ������Ʈ
    /// </summary>
    private void UpdateTotalPowerTextUI(OnStatChangedArgs? args)
    {
        _totalCombatPowerText.text = AlphabetNumConverter.Convert(args?.TotalPower ?? 0);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStatContainer.OnStatChanged -= UpdateTotalPowerTextUI;
    }
}
