using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public static event Action<ItemSlot> OnSlotClickedAction;           // ���� Ŭ���Ǿ��� �� �̺�Ʈ
    public Item CurrentItem { get; private set; }                       // ���� ���� ������
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

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _slotClickButton.onClick.AddListener(OnSlotClicked);       // ���� Ŭ�� �� ��ư �̺�Ʈ ����

        SelectItemInfo.OnItemInfoChanged -= Update_ItemSlotInfo;   // �ߺ����� ����
        SelectItemInfo.OnItemInfoChanged += Update_ItemSlotInfo;   // ������ ������ �ٲ��, �����۽��� ������ ������Ʈ
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init_ItemSlotInfo(Item item)
    {
        CurrentItem = item;
        Update_ItemSlotInfo();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �����۽��� ������ ������Ʈ
    /// </summary>
    private void Update_ItemSlotInfo()
    {
        if (CurrentItem == null)
            return;

        Debug.Log($"������ ���� ���� ������Ʈ! {CurrentItem.Name}");

        _itemIcon.sprite = CurrentItem.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(CurrentItem.Grade);
        _levelText.text = $"Lv.{CurrentItem.Level}";
        _countText.text = $"{CurrentItem.Count}";
        _enhanceableCountText.text = $"{CurrentItem.EnhanceableCount}";
        _countSlider.value = (float)CurrentItem.Count / (float)CurrentItem.EnhanceableCount;
        _enhanceableArrowGO.gameObject.SetActive(CurrentItem.IsEnhanceable());
        _equipGO.SetActive(EquipItemManager.IsEquipped(CurrentItem));

        _infoParentGO.SetActive(true);
    }

    /// <summary>
    /// ���� Ŭ������ �� 
    /// </summary>
    private void OnSlotClicked()
    {
        if (IsSlotEmpty == true)
            return;

        OnSlotClickedAction?.Invoke(this);
    }

    /// <summary>
    /// ���̶���Ʈ ON
    /// </summary>
    public void Highlight_ON() => _highlightGO.SetActive(true);

    /// <summary>
    /// ���̶���Ʈ OFF
    /// </summary>
    public void Highlight_OFF() => _highlightGO.SetActive(false);


    /// <summary>
    /// ������ ���� ����
    /// </summary>
    public void OFF_ItemSlotInfo()
    {
        _infoParentGO.SetActive(false);
    }
}
