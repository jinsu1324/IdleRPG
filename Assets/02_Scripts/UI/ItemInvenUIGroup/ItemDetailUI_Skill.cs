using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI_Skill : ItemDetailUI
{
    [SerializeField] private TextMeshProUGUI _levelText;                // ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // ��ȭ ������ ������ ���� �ؽ�Ʈ
    [SerializeField] private Slider _countSlider;                       // ���� ǥ�� �����̴�

    [Title("��ȭȭ��ǥ GO", bold: false)]
    [SerializeField] private GameObject _equipIcon;                       // �����Ǿ��� �� ������ ���ӿ�����Ʈ
    [SerializeField] private GameObject _enhanceableArrowGO;            // ��ȭ ������ �� ȭ��ǥ ���ӿ�����Ʈ

    [Title("��ư��", bold: false)]
    [SerializeField] private Button _equipButton;                       // ���� ��ư
    [SerializeField] private Button _unEquipButton;                     // ���� ���� ��ư
    [SerializeField] private Button _enhanceButton;                     // ��ȭ ��ư

    [Title("������ ��ư", bold: false)]
    [SerializeField] private Button _exitButton;                        // ������ ��ư


    protected override void OnEnable()
    {
        ItemSlot.OnSlotSelected += Show; // ������ ���� ���õǾ��� ��, ���õ� ������ ���� UI �ѱ�

        _equipButton.onClick.AddListener(OnClick_EquipButton);      // ������ư �ڵ鷯 ���
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);  // ����������ư �ڵ鷯 ���
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);  // ��ȭ��ư �ڵ鷯 ���
        _exitButton.onClick.AddListener(Hide);                      // ������ ��ư �ڵ鷯 ���
    }

    protected override void OnDisable()
    {
        ItemSlot.OnSlotSelected -= Show;

        _equipButton.onClick.RemoveAllListeners();
        _unEquipButton.onClick.RemoveAllListeners();
        _enhanceButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    public override void Show(Item item)
    {
        base.Show(item);
    }
    public override void Hide()
    {
        base.Hide();
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();

        if (CurrentItem is IEnhanceableItem enhanceableItem)
        {
            _levelText.text = $"Lv.{enhanceableItem.Level}";
            _countText.text = $"{CurrentItem.Count}";
            _enhanceableCountText.text = $"{enhanceableItem.EnhanceableCount}";
            _countSlider.value = (float)CurrentItem.Count / (float)enhanceableItem.EnhanceableCount;
            _enhanceableArrowGO.gameObject.SetActive(enhanceableItem.CanEnhance());
            _enhanceButton.gameObject.SetActive(enhanceableItem.CanEnhance());
        }

        bool isEquipped = EquipItemManager.IsEquipped(CurrentItem);
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

        EquipItemManager.Equip(CurrentItem, 0); // ��ų�� ���������� ���°� �ʿ��ҵ�
        UpdateUI();
    }

    /// <summary>
    /// ���� ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_UnEquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipItemManager.UnEquip(CurrentItem);
        UpdateUI();
    }

    /// <summary>
    /// ��ȭ ��ư Ŭ��
    /// </summary>
    public void OnClick_EnhanceButton()
    {
        if (CurrentItem == null)
            return;

        ItemEnhanceManager.Enhance(CurrentItem);
        UpdateUI();
    }
}
