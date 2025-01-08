using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �κ��丮 �� �����۽���
/// </summary>
public class ItemSlot : MonoBehaviour
{
    public static event Action<IItem> OnSlotSelected;                   // ������ ���õǾ��� �� �̺�Ʈ
    public IItem CurrentItem { get; private set; }                      // ���� ���� ������
    public bool IsSlotEmpty => CurrentItem == null;                     // ������ ����ִ��� 

    [Title("������ ������ ��ü�θ� GO", bold: false)]
    [SerializeField] private GameObject _infoParentGO;                  // ������ ������ ��ü�θ� GO

    [Title("������ ������", bold: false)]
    [SerializeField] private Image _itemIcon;                           // ������ ������
    [SerializeField] private Image _gradeFrame;                         // ��� ������
    [SerializeField] private TextMeshProUGUI _levelText;                // ������ ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _countText;                // ������ ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // ��ȭ ������ ������ ���� �ؽ�Ʈ
    [SerializeField] private Slider _countSlider;                       // ���� ǥ�� �����̴�

    [Title("������ ���� �� ����, ��ȭȭ��ǥ GO", bold: false)]
    [SerializeField] private GameObject _highlightGO;                   // ���� �������� �� ���̶���Ʈ
    [SerializeField] private GameObject _equipGO;                       // �����Ǿ��� �� ������ ���ӿ�����Ʈ
    [SerializeField] private GameObject _enhanceableArrowGO;            // ��ȭ ������ �� ȭ��ǥ ���ӿ�����Ʈ

    [Title("���� Ŭ�� ��ư", bold: false)]
    [SerializeField] private Button _slotClickButton;                   // ���� Ŭ�� ��ư

    private Action<RectTransform> _moveHilightImageAction;              // ���̶���Ʈ �̹��� �����̱� �Լ� ������ ����     

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        SelectItemInfoUI.OnSelectItemInfoChanged += UpdateItemSlot; // ���� ������ ������ �ٲ������, �����۽��� ������Ʈ
        EquipSkillManager.OnEquipSwapFinished += UpdateItemSlot; // ���� ��ų ��ü�� ������ ��, �����۽��� ������Ʈ

        _slotClickButton.onClick.AddListener(OnSlotClicked);  // ���� Ŭ�� �� ��ư �̺�Ʈ ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        SelectItemInfoUI.OnSelectItemInfoChanged -= UpdateItemSlot;
        EquipSkillManager.OnEquipSwapFinished -= UpdateItemSlot;

        _slotClickButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(IItem item, Action<RectTransform> moveHighlight)
    {
        CurrentItem = item;
        _moveHilightImageAction = moveHighlight;
        UpdateItemSlot();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �����۽��� ������ ������Ʈ
    /// </summary>
    public void UpdateItemSlot()
    {
        if (IsSlotEmpty)    // ���� ������� ������Ʈ ���� �ʰ� ����
            return;

        _itemIcon.sprite = CurrentItem.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(CurrentItem.Grade);
        _levelText.text = $"Lv.{CurrentItem.Level}";
        _countText.text = $"{CurrentItem.Count}";
        _enhanceableCountText.text = $"{CurrentItem.EnhanceableCount}";
        _countSlider.value = (float)CurrentItem.Count / (float)CurrentItem.EnhanceableCount;
        _enhanceableArrowGO.gameObject.SetActive(CurrentItem.IsEnhanceable());

        if (CurrentItem is Gear gear)
            _equipGO.SetActive(EquipGearManager.IsEquippedGear(gear));

        if (CurrentItem is Skill skill)
            _equipGO.SetActive(EquipSkillManager.IsEquippedSkill(skill));

        _infoParentGO.SetActive(true);
    }

    /// <summary>
    /// ���� Ŭ������ �� 
    /// </summary>
    private void OnSlotClicked()
    {
        if (IsSlotEmpty) // ������ ������� ���� �ȵǰ�
            return;

        OnSlotSelected?.Invoke(CurrentItem);
        _moveHilightImageAction?.Invoke(_gradeFrame.GetComponent<RectTransform>());
    }

    /// <summary>
    /// ������ ���� ���� ����
    /// </summary>
    public void Clear()
    {
        CurrentItem = null;
        _infoParentGO.SetActive(false);
    }
}
