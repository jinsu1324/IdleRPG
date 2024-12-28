using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �÷��̾� ������ �˾� (���罺��, �������� ���)
/// </summary>
public class PlayerDetailPopup : TabPopupBase
{
    [Title("���� ���Ե�", bold: false)]
    [SerializeField]
    private Dictionary<ItemType, EquipSlot> _equipSlotDict = new Dictionary<ItemType, EquipSlot>(); // ���� ���Ե� ��ųʸ�

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

    [Title("�κ��丮 �˾�", bold: false)]
    [SerializeField] private InvenPopup _invenPopup;                // �κ��丮 �˾�

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _weaponInvenButton.onClick.AddListener(() => _invenPopup.Show(ItemType.Weapon)); // ��ư ������ Ÿ�Կ� �´� �κ��丮 ������
        _armorInvenButton.onClick.AddListener(() => _invenPopup.Show(ItemType.Armor));
        _helmetInvenButton.onClick.AddListener(() => _invenPopup.Show(ItemType.Helmet));
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public override void Show()
    {
        SelectItemInfoUI.OnItemStatueChanged += Update_EquipSlots;   // ������ ���°� �ٲ��, ĳ���� �����˾� UI ������Ʈ
        PlayerStats.OnPlayerStatChanged += Update_StatTexts;    // �÷��̾� ������ �ٲ��, �����ؽ�Ʈ ������Ʈ


        PlayerStatArgs args = PlayerStats.Instance.CalculateStats(0);
        Update_StatTexts(args);
        
        Update_EquipSlots();
        
        gameObject.SetActive(true);
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
    /// �������� ������Ʈ
    /// </summary>
    private void Update_EquipSlots()
    {
        // ������ ��� ���� ��񽽷� ������Ʈ
        foreach (var kvp in _equipSlotDict)
        {
            ItemType itemType = kvp.Key;
            EquipSlot equipSlot = kvp.Value;    

            Item equipItem = EquipItemManager.GetEquipItem(itemType);
            
            if (equipItem != null)
                equipSlot.Show(equipItem, itemType);
            else
                equipSlot.Hide(itemType);
        }
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public override void Hide()
    {
        SelectItemInfoUI.OnItemStatueChanged -= Update_EquipSlots;
        PlayerStats.OnPlayerStatChanged -= Update_StatTexts;
        gameObject.SetActive(false);
    }
}
