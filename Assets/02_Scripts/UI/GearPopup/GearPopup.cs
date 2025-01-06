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
    [SerializeField]
    private Dictionary<ItemType, EquipSlot> _equipSlotDict;         // ���� ���Ե� ��ųʸ�

    [Title("���� �ؽ�Ʈ", bold : false)]
    [SerializeField] private TextMeshProUGUI _attackPowerText;      // ���ݷ� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _attackSpeedText;      // ���ݼӵ� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _maxHpText;            // �ִ�ü�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _criticalRateText;     // ġ��Ÿ Ȯ�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _criticalMultipleText; // ġ��Ÿ ���� �ؽ�Ʈ

    [Title("�κ��丮 ��ư", bold : false)]
    [SerializeField] private Button _weaponInvenButton;             // ���� �κ��丮 ��ư
    [SerializeField] private Button _armorInvenButton;              // ���� �κ��丮 ��ư
    [SerializeField] private Button _helmetInvenButton;             // ��� �κ��丮 ��ư

    [Title("��� �κ��丮 �˾�", bold: false)]
    [SerializeField] private GearInvenPopup _gearInvenPopup;        // ��� �κ��丮 �˾�

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGearChanged += Update_EquipSlots; // ���� ��� �ٲ��, ����˾� �������� ������Ʈ
        PlayerStats.OnPlayerStatChanged += Update_StatTexts;    // �÷��̾� ������ �ٲ��, �����ؽ�Ʈ ������Ʈ

        _weaponInvenButton.onClick.AddListener(() => _gearInvenPopup.Show(ItemType.Weapon)); // ���� �κ��丮 ��ư �Ҵ�
        _armorInvenButton.onClick.AddListener(() => _gearInvenPopup.Show(ItemType.Armor));   // ���� �κ��丮 ��ư �Ҵ�
        _helmetInvenButton.onClick.AddListener(() => _gearInvenPopup.Show(ItemType.Helmet)); // ��� �κ��丮 ��ư �Ҵ�
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipGearManager.OnEquipGearChanged -= Update_EquipSlots;
        PlayerStats.OnPlayerStatChanged -= Update_StatTexts;

        _weaponInvenButton.onClick.RemoveAllListeners();
        _armorInvenButton.onClick.RemoveAllListeners();
        _helmetInvenButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public override void Show()
    {
        PlayerStatArgs args = PlayerStats.GetCurrentPlayerStatArgs(0);
        Update_StatTexts(args);

        OnEquipGearChangedArgs equipArgs = new OnEquipGearChangedArgs();
        Update_EquipSlots(equipArgs);

        _gearInvenPopup.Hide();

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
    private void Update_EquipSlots(OnEquipGearChangedArgs args)
    {
        foreach (var kvp in _equipSlotDict)
        {
            ItemType itemType = kvp.Key;
            EquipSlot equipSlot = kvp.Value;    

            IItem equipItem = EquipGearManager.GetEquipGear(itemType);
            
            if (equipItem != null)
                equipSlot.ShowInfoGO(equipItem);
            else
                equipSlot.ShowEmptyGO();
        }
    }
}
