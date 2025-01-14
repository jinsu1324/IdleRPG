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

    [Title("��� �پ��ִ� �����Ƽ��", bold: false)]
    [SerializeField] private List<ItemAbilityInfo> _abilityInfoList;    // ��� �پ��ִ� �����Ƽ��
    
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
        // �⺻���� ������Ʈ
        base.UpdateUI(item);

        // �󼼼��� ������Ʈ
        _descText.text = item.Desc;

        // ��ȭ�������� ������Ʈ
        if (item is IEnhanceableItem enhanceableItem)
        {
            _levelText.text = $"Lv.{enhanceableItem.Level}";
            _countText.text = $"{item.Count}";
            _enhanceableCountText.text = $"{enhanceableItem.EnhanceableCount}";
            _countSlider.value = (float)CurrentItem.Count / (float)enhanceableItem.EnhanceableCount;
            _enhanceableArrowIcon.gameObject.SetActive(enhanceableItem.CanEnhance());
            _enhanceButton.gameObject.SetActive(enhanceableItem.CanEnhance());
        }

        // �����Ƽ ���� ������Ʈ
        Update_AbilityInfo(item);

        // ������������ + ��ư�� ������Ʈ
        bool isEquipped = EquipGearManager.IsEquipped(item);
        _equipButton.gameObject.SetActive(!isEquipped);
        _unEquipButton.gameObject.SetActive(isEquipped);
        _equipIcon.SetActive(isEquipped);

    }

    /// <summary>
    /// �����Ƽ ���� ������Ʈ
    /// </summary>
    private void Update_AbilityInfo(Item item)
    {
        if (item is GearItem gearItem)
        {
            int index = 0;

            // ���� Dictionary ��ȸ
            foreach (var kvp in gearItem.GetAttributeDict())
            {
                StatType statType = kvp.Key;
                int value = kvp.Value;

                _abilityInfoList[index].Show(statType, value);
                index++;
            }

            // ������ ����Ʈ ��� ��Ȱ��ȭ
            for (int i = index; i < _abilityInfoList.Count; i++)
            {
                _abilityInfoList[index].Hide();
            }
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
