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
    public static event Action<Item> OnSlotSelected;                    // ������ ���õǾ��� �� �̺�Ʈ
    public Item CurrentItem { get; private set; }                       // ���� ���� ������
    public bool IsSlotEmpty => CurrentItem == null;                     // ������ ����ִ��� 

    [Title("������ ������ ��ü�θ� GO", bold: false)]
    [SerializeField] private GameObject _infoParentGO;                  // ������ ������ ��ü�θ� GO

    [Title("������ �⺻����", bold: false)]
    [SerializeField] private Image _itemIcon;                           // ������ ������
    [SerializeField] private Image _gradeFrame;                         // ��� ������
    [SerializeField] private TextMeshProUGUI _countText;                // ������ ���� �ؽ�Ʈ

    [Title("���� Ŭ�� ����", bold: false)]
    [SerializeField] private GameObject _highlightGO;                   // ���� �������� �� ���̶���Ʈ
    [SerializeField] private Button _slotClickButton;                   // ���� Ŭ�� ��ư

    [Title("��ȭ ����", bold: false)]
    [SerializeField] private TextMeshProUGUI _levelText;                // ������ ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // ��ȭ ������ ������ ���� �ؽ�Ʈ
    [SerializeField] private Slider _countSlider;                       // ���� ǥ�� �����̴�
    [SerializeField] private GameObject _enhanceableArrowGO;            // ��ȭ ������ �� ȭ��ǥ ���ӿ�����Ʈ

    [Title("���� ����", bold: false)]
    [SerializeField] private GameObject _equipGO;                       // �����Ǿ��� �� ������ ���ӿ�����Ʈ

    private Action<RectTransform> _moveHilightImageAction;              // ���̶���Ʈ �̹��� �����̱� �Լ� ������ ����     

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGear += UpdateSlot;  // ��� �����Ҷ� -> �����۽��� ������Ʈ
        EquipGearManager.OnUnEquipGear += UpdateSlot;  // ��� �����Ҷ� -> �����۽��� ������Ʈ
        ItemEnhanceManager.OnItemEnhance += UpdateSlot; // ������ ��ȭ�Ҷ� -> �����۽��� ������Ʈ

        _slotClickButton.onClick.AddListener(OnSlotClicked);  // ���� Ŭ�� �� ��ư �̺�Ʈ ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipGearManager.OnEquipGear -= UpdateSlot;
        EquipGearManager.OnUnEquipGear -= UpdateSlot;
        ItemEnhanceManager.OnItemEnhance -= UpdateSlot;

        _slotClickButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(Item item, Action<RectTransform> moveHighlight)
    {
        CurrentItem = item;
        _moveHilightImageAction = moveHighlight;
        UpdateSlot(CurrentItem);

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �����۽��� ������ ������Ʈ
    /// </summary>
    public void UpdateSlot(Item item)
    {
        // ���� ������� ������Ʈ ���� �ʰ� ����
        if (IsSlotEmpty)    
            return;

        // ������ �⺻���� ������Ʈ
        _itemIcon.sprite = CurrentItem.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(CurrentItem.Grade);
        _countText.text = $"{CurrentItem.Count}";


        // ��ȭ������ �������̸� ��ȭ���� �����鵵 ������Ʈ
        if (CurrentItem is IEnhanceableItem enhanceableItem)
        {
            _levelText.text = $"Lv.{enhanceableItem.Level}"; ;
            _enhanceableCountText.text = $"{enhanceableItem.EnhanceableCount}";
            _countSlider.value = (float)CurrentItem.Count / (float)enhanceableItem.EnhanceableCount;
            _enhanceableArrowGO.gameObject.SetActive(enhanceableItem.CanEnhance());
        }

        if (CurrentItem is GearItem gearItem)
        {
            _equipGO.SetActive(EquipGearManager.IsEquipped(gearItem));
        }
        

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
