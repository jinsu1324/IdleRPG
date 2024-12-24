using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���õ� ������ ������������ ǥ�� �� ����, ����, ��ȭ��ư ����
/// </summary>
public class SelectItemInfoUI : MonoBehaviour
{
    public static event Action OnItemStatueChanged;                     // ������ ���°� �ٲ���� �� �̺�Ʈ
    public Item CurrentItem { get; private set; }                       // ���� ������

    [Title("������ ��ü�θ� GO", bold: false)]
    [SerializeField] private GameObject _infoParentGO;                  // ������ ��ü�θ� GO

    [Title("������ ������", bold: false)]
    [SerializeField] private Image _itemIcon;                           // ������ ������
    [SerializeField] private Image _gradeFrame;                         // ��� ������
    [SerializeField] private TextMeshProUGUI _nameText;                 // �̸� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _gradeText;                // ��� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _levelText;                // ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _countText;                // ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // ��ȭ ������ ������ ���� �ؽ�Ʈ
    [SerializeField] private Slider _countSlider;                       // ���� ǥ�� �����̴�

    [Title("������ ��ȭȭ��ǥ GO", bold: false)]
    [SerializeField] private GameObject _equipGO;                       // �����Ǿ��� �� ������ ���ӿ�����Ʈ
    [SerializeField] private GameObject _enhanceableArrowGO;            // ��ȭ ������ �� ȭ��ǥ ���ӿ�����Ʈ

    [Title("��ư��", bold: false)]
    [SerializeField] private Button _equipButton;                       // ���� ��ư
    [SerializeField] private Button _unEquipButton;                     // ���� ���� ��ư
    [SerializeField] private Button _enhanceButton;                     // ��ȭ ��ư

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // ��ư�鿡 �ڵ鷯 ���
        _equipButton.onClick.AddListener(OnClick_EquipButton);       
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);

        ItemSlot.OnSlotSelected += Show;    // ������ ���õǾ��� ��, ����������UI �����ֱ�
    }

    /// <summary>
    /// �����ֱ�
    /// </summary>
    public void Show(ItemSlot selectSlot)
    {
        CurrentItem = selectSlot.CurrentItem;
        UpdateUI();
        _infoParentGO.SetActive(true);
    }

    /// <summary>
    /// ���߱�
    /// </summary>
    public void Hide()
    {
        CurrentItem = null;
        _infoParentGO.SetActive(false);
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UpdateUI()
    {
        _itemIcon.sprite = CurrentItem.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(CurrentItem.Grade);
        _nameText.text = $"{CurrentItem.Name}";
        _nameText.color = ResourceManager.Instance.GetItemGradeColor(CurrentItem.Grade);
        _gradeText.text = $"{CurrentItem.Grade}";
        _gradeText.color = ResourceManager.Instance.GetItemGradeColor(CurrentItem.Grade);
        _levelText.text = $"Lv.{CurrentItem.Level}";
        _countText.text = $"{CurrentItem.Count}";
        _enhanceableCountText.text = $"{CurrentItem.EnhanceableCount}";
        _countSlider.value = (float)CurrentItem.Count / (float)CurrentItem.EnhanceableCount;
        _equipGO.SetActive(EquipItemManager.IsEquipped(CurrentItem));
        _enhanceableArrowGO.gameObject.SetActive(CurrentItem.IsEnhanceable());

        UpdateButtons();
    }

    /// <summary>
    /// ��ư�� ������Ʈ
    /// </summary>
    private void UpdateButtons()
    {
        bool isEquipped = EquipItemManager.IsEquipped(CurrentItem);
        bool isEnhanceable = CurrentItem.IsEnhanceable();

        _equipButton.gameObject.SetActive(!isEquipped); // �����Ǿ�������, ������ư OFF
        _unEquipButton.gameObject.SetActive(isEquipped);   // �����Ǿ�������, ����������ư ON
        _enhanceButton.gameObject.SetActive(isEnhanceable); // ��ȭ �����Ҷ�, ��ȭ��ư ON
    }

    /// <summary>
    /// ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_EquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipItemManager.Equip(CurrentItem); // ����
        UpdateUI();
        OnItemStatueChanged?.Invoke();
    }

    /// <summary>
    /// ���� ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_UnEquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipItemManager.UnEquip(CurrentItem);  // ���� ����
        UpdateUI();
        OnItemStatueChanged?.Invoke();
    }

    /// <summary>
    /// ��ȭ ��ư Ŭ��
    /// </summary>
    public void OnClick_EnhanceButton()
    {
        if (CurrentItem == null)
            return;

        ItemEnhanceManager.Enhance(CurrentItem); // ��ȭ
        UpdateUI();
        OnItemStatueChanged?.Invoke();
    }

    /// <summary>
    /// OnDestroy
    /// </summary>
    private void OnDestroy()
    {
        ItemSlot.OnSlotSelected -= Show;
    }
}
