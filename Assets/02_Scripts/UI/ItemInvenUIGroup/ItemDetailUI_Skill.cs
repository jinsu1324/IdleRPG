using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ų������ ������ UI
/// </summary>
public class ItemDetailUI_Skill : ItemDetailUI
{
    [Title("��ų������ ������ UI", bold: false)]
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

    [Title("������ ��ư", bold: false)]
    [SerializeField] private Button _exitButton;                        // ������ ��ư

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _equipButton.onClick.AddListener(OnClick_EquipButton);      // ������ư �ڵ鷯 ���
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);  // ����������ư �ڵ鷯 ���
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);  // ��ȭ��ư �ڵ鷯 ���
        _exitButton.onClick.AddListener(() => Hide());              // ������ ��ư �ڵ鷯 ���
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _equipButton.onClick.RemoveAllListeners();
        _unEquipButton.onClick.RemoveAllListeners();
        _enhanceButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    protected override void UpdateUI(Item item)
    {
        base.UpdateUI(item);
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);

        _descText.text = itemDataSO.Desc; // Todo Diynamic �ؽ�Ʈ�� �ؾ���
        _levelText.text = $"Lv.{item.Level}";
        _countText.text = $"{item.Count}";
        _enhanceableCountText.text = $"{itemDataSO.GetEnhanceCount(item.Level)}";
        _countSlider.value = (float)item.Level / (float)itemDataSO.GetEnhanceCount(item.Level);
        _enhanceableArrowIcon.gameObject.SetActive(ItemEnhanceManager.CanEnhance(item));
        _enhanceButton.gameObject.SetActive(ItemEnhanceManager.CanEnhance(item));

        bool isEquipped = EquipSkillManager.IsEquipped(item);
        _equipButton.gameObject.SetActive(!isEquipped);
        _unEquipButton.gameObject.SetActive(isEquipped);
        _equipIcon.SetActive(isEquipped);
    }

    /// <summary>
    /// ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_EquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipSkillManager.Equip(CurrentItem);
    }

    /// <summary>
    /// ���� ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_UnEquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipSkillManager.UnEquip(CurrentItem);
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
