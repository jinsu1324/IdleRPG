using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ����
/// </summary>
[RequireComponent(typeof(ReddotComponent))]
public class EquipSlot : MonoBehaviour
{
    public static event Action<IItem> OnClickEquipSlotDetailButton;

    public IItem CurrentItem { get; private set; }              // �� ���Կ� ���ִ� ������

    [Title("���������� ������Ÿ��", bold: false)]
    [SerializeField] private ItemType _itemType;                // ���������� ������Ÿ��

    [Title("On Off GO", bold: false)]
    [SerializeField] private GameObject _emptyGO;               // �󽽷� GO
    [SerializeField] private GameObject _infoGO;                // ������ GO

    [Title("������", bold: false)]
    [SerializeField] private Image _itemIcon;                   // ������ ������
    [SerializeField] private Image _gradeFrame;                 // ��� ������
    [SerializeField] private TextMeshProUGUI _levelText;        // ������ ���� �ؽ�Ʈ

    [Title("����� ������Ʈ (��� ������Ʈ��)", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;  // ����� ������Ʈ

    [Title("���� ��ų ��ü ����", bold: false)]
    [SerializeField] private GameObject _swapGuideGO;           // ���� ���̵� GO
    [SerializeField] private Button _swapButton;                // ��ü ��ư
    private int _slotIndex;                                     // ���� �ε���

    [Title("������ ��ư", bold: false)]
    [SerializeField] private Button _itemDetailButton;         



    private void TurnOn_SwapGuides()
    {
        _swapGuideGO.SetActive(true);
        _swapButton.gameObject.SetActive(true);

        _itemDetailButton.gameObject.SetActive(false);

    }
    private void TurnOff_SwapGuides()
    {
        _swapGuideGO.SetActive(false);
        _swapButton.gameObject.SetActive(false);

        _itemDetailButton.gameObject.SetActive(false);

    }

    private void OnClickSwapButton()
    {
        if (CurrentItem is Skill skill)
        {
            EquipSkillManager.SwapSkill(_slotIndex);

        }

    }

    private void NotifyOnClickEquipSlotDetailButton()
    {
        OnClickEquipSlotDetailButton?.Invoke(CurrentItem);
    }

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemInven.OnItemInvenChanged += UpdateReddotComponent; // ������ �ִ� �������� ����Ǿ��� ��, �������� ����� ������Ʈ



        EquipSkillManager.OnEquipSwapStarted += TurnOn_SwapGuides;

        _swapButton.onClick.AddListener(OnClickSwapButton);

        _itemDetailButton.onClick.AddListener(NotifyOnClickEquipSlotDetailButton);

    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemInven.OnItemInvenChanged -= UpdateReddotComponent;


        EquipSkillManager.OnEquipSwapStarted -= TurnOn_SwapGuides;

        _swapButton.onClick.RemoveAllListeners();
        _itemDetailButton.onClick.RemoveAllListeners();


    }

    /// <summary>
    /// ���� �����ֱ�
    /// </summary>
    public void ShowInfoGO(IItem item, int slotIndex = -1)
    {
        CurrentItem = item;
        _itemIcon.sprite = item.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);
        _levelText.text = $"Lv.{item.Level}";
        _slotIndex = slotIndex;

        _emptyGO.SetActive(false);
        _infoGO.SetActive(true); // ���� �ѱ�

        UpdateReddotComponent(); // ����� ������Ʈ

        TurnOff_SwapGuides();

        if (CurrentItem is Gear gear)
        {
            _itemDetailButton.gameObject.SetActive(false);
        }

        if (CurrentItem is Skill skill)
        {
            _itemDetailButton.gameObject.SetActive(true);

        }

        if (CurrentItem == null)
        {
            _itemDetailButton.gameObject.SetActive(false);

        }



    }

    /// <summary>
    /// �󽽷� �����ֱ�
    /// </summary>
    public void ShowEmptyGO(int slotIndex = -1)
    {
        CurrentItem = null;
        _emptyGO.SetActive(true);
        _infoGO.SetActive(false); // ���� ����
        _slotIndex = slotIndex;

        UpdateReddotComponent(); // ����� ������Ʈ

        if (CurrentItem is Gear gear)
        {
            _itemDetailButton.gameObject.SetActive(false);
        }

        if (CurrentItem is Skill skill)
        {
            _itemDetailButton.gameObject.SetActive(true);

        }

        if (CurrentItem == null)
        {
            _itemDetailButton.gameObject.SetActive(false);

        }

        TurnOff_SwapGuides();

    }

    /// <summary>
    /// ����� ������Ʈ ������Ʈ
    /// </summary>
    public void UpdateReddotComponent()
    {
        switch (_itemType)
        {
            case ItemType.Weapon: // ������ �κ��丮�� ��ȭ������ �������� �ִ���? ����� ������Ʈ
            case ItemType.Armor:
            case ItemType.Helmet:
                _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_itemType)); 
                break;
            case ItemType.Skill: // ��ų�� �׳� ����� �����
                _reddotComponent.Hide();
                break;
        }
    }
}
