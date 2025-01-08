using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��� �˾� (���罺��, �������� ���)
/// </summary>
public class GearPopup : BottomTabPopupBase
{
    [Title("���� ���Ե�", bold: false)]
    [SerializeField] private EquipSlotGear[] _equipSlotGearArr;     // ���� ���Ե�

    [Title("���� �ؽ�Ʈ", bold : false)]
    [SerializeField] private TextMeshProUGUI _attackPowerText;      // ���ݷ� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _attackSpeedText;      // ���ݼӵ� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _maxHpText;            // �ִ�ü�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _criticalRateText;     // ġ��Ÿ Ȯ�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _criticalMultipleText; // ġ��Ÿ ���� �ؽ�Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += Update_StatTexts;    // �÷��̾� ������ �ٲ��, �����ؽ�Ʈ ������Ʈ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= Update_StatTexts;
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public override void Show()
    {
        PlayerStatArgs args = PlayerStats.GetCurrentPlayerStatArgs(0);
        Update_StatTexts(args); // ���� �ؽ�Ʈ ������Ʈ
        
        Update_EquipSlots(); // ���� ���� ������Ʈ

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �����ؽ�Ʈ ������Ʈ
    /// </summary>
    private void Update_StatTexts(PlayerStatArgs args)
    {
        _attackPowerText.text = $"{args.AttackPower}";
        _attackSpeedText.text = $"{args.AttackSpeed}";
        _maxHpText.text = $"{args.MaxHp}";
        _criticalRateText.text = $"{args.CriticalRate}";
        _criticalMultipleText.text = $"{args.CriticalMultiple}";
    }

    /// <summary>
    /// ������ ��� ���� ��񽽷� ������Ʈ
    /// </summary>
    private void Update_EquipSlots()
    {
        foreach (EquipSlotGear equipSlotGear in _equipSlotGearArr)
            equipSlotGear.UpdateSlot();
    }
}
