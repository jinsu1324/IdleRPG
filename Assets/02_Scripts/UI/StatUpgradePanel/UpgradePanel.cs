using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private UpgradeSlot _upgradeSlotPrefab;              // ������ ���׷��̵� ���� ������
    [SerializeField] private RectTransform _slotParent;                   // ���Ե� ������ �θ�
    [SerializeField] private TextMeshProUGUI _totalCombatPowerText;       // ���� ������ �ؽ�Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += UpdateTotalPowerTextUI;    // �÷��̾� ���� ����Ǿ��� ��, ���� ������ �ؽ�ƮUI ������Ʈ 
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= UpdateTotalPowerTextUI;
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
        // �÷��̾� ���׷��̵� ������ŭ �ݺ�
        foreach (Upgrade upgrade in UpgradeManager.GetAllUpgrades())
        {
            UpgradeSlot statUpgradeSlot = Instantiate(_upgradeSlotPrefab, _slotParent);
            statUpgradeSlot.Init(upgrade.ID);
        }

        PlayerStatArgs args = new PlayerStatArgs() { TotalPower = PlayerStats.GetAllStatValue() };
        UpdateTotalPowerTextUI(args);
    }

    /// <summary>
    /// ���� ������ �ؽ�Ʈ ������Ʈ
    /// </summary>
    private void UpdateTotalPowerTextUI(PlayerStatArgs args)
    {
        _totalCombatPowerText.text = NumberConverter.ConvertAlphabet(args.TotalPower);
    }
}
