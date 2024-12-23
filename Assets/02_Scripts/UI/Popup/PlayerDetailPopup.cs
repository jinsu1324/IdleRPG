using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �÷��̾� ����, �������� ��� ������ �˾�
/// </summary>
public class PlayerDetailPopup : PopupBase
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

    [Title("�κ��丮 �˾�", bold: false)]
    [SerializeField] private InvenPopup _invenPopup;                // �κ��丮 �˾�



    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // ��ư�� ������ ������ Ÿ�Կ� �´� �˾� ������
        _weaponInvenButton.onClick.AddListener(() => _invenPopup.Show(ItemType.Weapon));
        _armorInvenButton.onClick.AddListener(() => _invenPopup.Show(ItemType.Armor));
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public override void Show()
    {
        UIUpdate();

        SelectItemInfo.OnItemInfoChanged += UIUpdate;   // 

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public override void Hide()
    {
        SelectItemInfo.OnItemInfoChanged -= UIUpdate;   //


        gameObject.SetActive(false);
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UIUpdate()
    {
        _attackPowerText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.AttackPower)}";
        _attackSpeedText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.AttackSpeed)}";
        _maxHpText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.MaxHp)}";
        _criticalRateText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.CriticalRate)}";
        _criticalMultipleText.text = $"{PlayerStats.Instance.GetFinalStat(StatType.CriticalMultiple)}";


        foreach (var kvp in _equipSlotDict)
        {
            ItemType itemType = kvp.Key;
            EquipSlot equipSlot = kvp.Value;    

            Item equipItem = EquipItemManager.GetEquipItem(itemType);
            
            if (equipItem != null)
                equipSlot.Show_EquipSlotInfo(equipItem);
            else
                equipSlot.OFF_EquipSlotInfo();
        }
    }
}
