using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������� ������ UI
/// </summary>
public class ItemDetailUI_Gear : ItemDetailUI
{
    [Title("�������� ������", bold: false)]
    [SerializeField] protected TextMeshProUGUI _descText;               // �󼼼��� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _levelText;                // ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // ��ȭ ������ ������ ���� �ؽ�Ʈ
    [SerializeField] private Slider _countSlider;                       // ���� ǥ�� �����̴�
    [SerializeField] private GameObject _equipIcon;                     // ���� ������
    [SerializeField] private GameObject _enhanceableArrowIcon;          // ��ȭ���� ������

    [Title("��ư��", bold: false)]
    [SerializeField] private Button _equipButton;                       // ���� ��ư
    [SerializeField] private Button _unEquipButton;                     // ���� ���� ��ư
    [SerializeField] private Button _enhanceButton;                     // ��ȭ ��ư

    [Title("���Ӽ� UI��", bold: false)]
    [SerializeField] private List<GearStatUI> _gearStatUIList;          // ���Ӽ� UI ����Ʈ
    
    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGear += UpdateUI; // ��� �����Ҷ� -> �������� ������ UI ������Ʈ
        EquipGearManager.OnUnEquipGear += UpdateUI; // ��� �����Ҷ� -> �������� ������ UI ������Ʈ
        ItemEnhanceManager.OnItemEnhance += UpdateUI; // ������ ��ȭ�Ҷ� -> �������� ������ UI ������Ʈ

        _equipButton.onClick.AddListener(OnClick_EquipButton);      // ������ư �ڵ鷯 ���
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);  // ����������ư �ڵ鷯 ���
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);  // ��ȭ��ư �ڵ鷯 ���
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipGearManager.OnEquipGear -= UpdateUI;
        EquipGearManager.OnUnEquipGear -= UpdateUI;
        ItemEnhanceManager.OnItemEnhance -= UpdateUI;

        _equipButton.onClick.RemoveAllListeners();
        _unEquipButton.onClick.RemoveAllListeners();
        _enhanceButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    protected override void UpdateUI(Item item)
    {
        base.UpdateUI(item);
        ItemDataSO itemDataSO = ItemManager.GetItemDataSO(item.ID);

        _descText.text = itemDataSO.Desc;
        _levelText.text = $"Lv.{item.Level}";
        _countText.text = $"{item.Count}";
        _enhanceableCountText.text = $"{itemDataSO.GetEnhanceCount(item.Level)}";
        _countSlider.value = (float)item.Level / (float)itemDataSO.GetEnhanceCount(item.Level);
        _enhanceableArrowIcon.gameObject.SetActive(ItemManager.CanEnhance(item));
        _enhanceButton.gameObject.SetActive(ItemManager.CanEnhance(item));

        Update_GearStatUI(item);

        bool isEquipped = EquipGearManager.IsEquipped(item);
        _equipButton.gameObject.SetActive(!isEquipped);
        _unEquipButton.gameObject.SetActive(isEquipped);
        _equipIcon.SetActive(isEquipped);

    }

    /// <summary>
    /// ���Ӽ��� UI ������Ʈ
    /// </summary>
    private void Update_GearStatUI(Item item)
    {
        ItemDataSO itemDataSO = ItemManager.GetItemDataSO(item.ID);

        if (itemDataSO is GearDataSO gearDataSO)
        {
            int index = 0;
            foreach (var kvp in gearDataSO.GetGearStats(item.Level)) 
            {
                StatType statType = kvp.Key;
                float value = kvp.Value;
                _gearStatUIList[index].Show(statType, value); // ���Ȱ�����ŭ UI ǥ��
                
                index++;
            }

            for (int i = index; i < _gearStatUIList.Count; i++)
                _gearStatUIList[index].Hide(); // �������� ��Ȱ��ȭ
        }
    }

    /// <summary>
    /// ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_EquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipGearManager.Equip(CurrentItem);
    }

    /// <summary>
    /// ���� ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_UnEquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipGearManager.UnEquip(CurrentItem);
    }

    /// <summary>
    /// ��ȭ ��ư Ŭ��
    /// </summary>
    public void OnClick_EnhanceButton()
    {
        if (CurrentItem == null)
            return;

        ItemEnhanceManager.Enhance(CurrentItem);
    }
}
