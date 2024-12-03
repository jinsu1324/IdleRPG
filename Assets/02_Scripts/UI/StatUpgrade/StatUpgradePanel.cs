using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class StatUpgradePanel : MonoBehaviour
{
    [SerializeField] private StatUpgradeSlot _statUpgradeSlotPrefab;      // ������ ���� ���׷��̵� ���� ������
    [SerializeField] private RectTransform _slotParent;                   // ���Ե� ������ �θ�
    [SerializeField] private TextMeshProUGUI _totalCombatPowerText;       // ���� ������ �ؽ�Ʈ

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
        foreach (StatComponent stat in StatManager.Instance.GetAllStats())
        {
            StatUpgradeSlot statUpgradeSlot = Instantiate(_statUpgradeSlotPrefab, _slotParent);
            statUpgradeSlot.Init(stat.StatID, Update_TotalCombatPowerText);
        }
    }

    /// <summary>
    /// ���� ������ �ؽ�Ʈ ������Ʈ
    /// </summary>
    private void Update_TotalCombatPowerText()
    {
        _totalCombatPowerText.text = AlphabetNumConverter.Convert(StatManager.Instance.GetTotalCombatPower());
    }
}
