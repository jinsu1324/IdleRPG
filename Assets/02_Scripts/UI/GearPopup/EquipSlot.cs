using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장착 슬롯
/// </summary>
[RequireComponent(typeof(ReddotComponent))]
public class EquipSlot : MonoBehaviour
{
    public static event Action<IItem> OnClickEquipSlotDetailButton;

    public IItem CurrentItem { get; private set; }              // 이 슬롯에 들어가있는 아이템

    [Title("장착슬롯의 아이템타입", bold: false)]
    [SerializeField] private ItemType _itemType;                // 장착슬롯의 아이템타입

    [Title("On Off GO", bold: false)]
    [SerializeField] private GameObject _emptyGO;               // 빈슬롯 GO
    [SerializeField] private GameObject _infoGO;                // 정보들 GO

    [Title("정보들", bold: false)]
    [SerializeField] private Image _itemIcon;                   // 아이템 아이콘
    [SerializeField] private Image _gradeFrame;                 // 등급 프레임
    [SerializeField] private TextMeshProUGUI _levelText;        // 아이템 레벨 텍스트

    [Title("레드닷 컴포넌트 (장비만 업데이트됨)", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;  // 레드닷 컴포넌트

    [Title("슬롯 스킬 교체 관련", bold: false)]
    [SerializeField] private GameObject _swapGuideGO;           // 스왑 가이드 GO
    [SerializeField] private Button _swapButton;                // 교체 버튼
    private int _slotIndex;                                     // 슬롯 인덱스

    [Title("상세정보 버튼", bold: false)]
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
        ItemInven.OnItemInvenChanged += UpdateReddotComponent; // 가지고 있는 아이템이 변경되었을 때, 장착슬롯 레드닷 업데이트



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
    /// 정보 보여주기
    /// </summary>
    public void ShowInfoGO(IItem item, int slotIndex = -1)
    {
        CurrentItem = item;
        _itemIcon.sprite = item.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);
        _levelText.text = $"Lv.{item.Level}";
        _slotIndex = slotIndex;

        _emptyGO.SetActive(false);
        _infoGO.SetActive(true); // 정보 켜기

        UpdateReddotComponent(); // 레드닷 업데이트

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
    /// 빈슬롯 보여주기
    /// </summary>
    public void ShowEmptyGO(int slotIndex = -1)
    {
        CurrentItem = null;
        _emptyGO.SetActive(true);
        _infoGO.SetActive(false); // 정보 끄기
        _slotIndex = slotIndex;

        UpdateReddotComponent(); // 레드닷 업데이트

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
    /// 레드닷 컴포넌트 업데이트
    /// </summary>
    public void UpdateReddotComponent()
    {
        switch (_itemType)
        {
            case ItemType.Weapon: // 장비들은 인벤토리에 강화가능한 아이템이 있는지? 레드닷 업데이트
            case ItemType.Armor:
            case ItemType.Helmet:
                _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_itemType)); 
                break;
            case ItemType.Skill: // 스킬은 그냥 레드닷 숨기기
                _reddotComponent.Hide();
                break;
        }
    }
}
