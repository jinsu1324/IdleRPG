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
    [SerializeField] private EquipSlot_Gear[] _equipSlotGearArr;     // ���� ���Ե�

    [Title("���� �ؽ�Ʈ", bold : false)]
    [SerializeField] private TextMeshProUGUI _attackPowerText;      // ���ݷ� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _attackSpeedText;      // ���ݼӵ� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _maxHpText;            // �ִ�ü�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _criticalRateText;     // ġ��Ÿ Ȯ�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _criticalMultipleText; // ġ��Ÿ ���� �ؽ�Ʈ

    [Title("��� �κ��丮 �˾�", bold: false)]
    [SerializeField] private GearInvenPopup _gearInvenPopup;        // ��� �κ��丮 �˾�

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSlot_Gear.OnClickGearInvenButton += _gearInvenPopup.Show;  // ����κ���ư �������� -> ����κ��˾� ����
        PlayerStats.OnPlayerStatChanged += Update_StatTexts;    // �÷��̾� ������ �ٲ�� -> �����ؽ�Ʈ ������Ʈ
        EquipGearManager.OnEquipGear += _gearInvenPopup.Hide; // ��� �����Ҷ� -> ����κ��˾� �ݱ�
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipSlot_Gear.OnClickGearInvenButton -= _gearInvenPopup.Show;
        PlayerStats.OnPlayerStatChanged -= Update_StatTexts;
        EquipGearManager.OnEquipGear -= _gearInvenPopup.Hide;
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public override void Show()
    {
        PlayerStatArgs args = PlayerStats.GetCurrentPlayerStatArgs();
        Update_StatTexts(args); // ���� �ؽ�Ʈ ������Ʈ
        
        Init_GearEquipSlots(); // ����������� ������Ʈ

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
    /// ��񽽷Ե� �ʱ�ȭ
    /// </summary>
    private void Init_GearEquipSlots()
    {
        foreach (EquipSlot_Gear equipSlotGear in _equipSlotGearArr)
            equipSlotGear.Init();
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
}
