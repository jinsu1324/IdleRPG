using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인벤토리 속 아이템슬롯
/// </summary>
public class ItemSlot : MonoBehaviour
{
    public static event Action<ItemSlot> OnSlotSelected;                // 슬롯이 선택되었을 때 이벤트
    public Item CurrentItem { get; private set; }                       // 현재 슬롯 아이템
    public bool IsSlotEmpty => CurrentItem == null;                     // 슬롯이 비어있는지 

    [Title("아이템 정보들 전체부모 GO", bold: false)]
    [SerializeField] private GameObject _infoParentGO;                  // 아이템 정보들 전체부모 GO

    [Title("아이템 정보들", bold: false)]
    [SerializeField] private Image _itemIcon;                           // 아이템 아이콘
    [SerializeField] private Image _gradeFrame;                         // 등급 프레임
    [SerializeField] private TextMeshProUGUI _levelText;                // 아이템 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _countText;                // 아이템 갯수 텍스트
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // 강화 가능한 아이템 갯수 텍스트
    [SerializeField] private Slider _countSlider;                       // 갯수 표시 슬라이더

    [Title("아이템 선택 및 장착, 강화화살표 GO", bold: false)]
    [SerializeField] private GameObject _highlightGO;                   // 슬롯 선택했을 때 하이라이트
    [SerializeField] private GameObject _equipGO;                       // 장착되었을 때 아이콘 게임오브젝트
    [SerializeField] private GameObject _enhanceableArrowGO;            // 강화 가능할 때 화살표 게임오브젝트

    [Title("슬롯 클릭 버튼", bold: false)]
    [SerializeField] private Button _slotClickButton;                   // 슬롯 클릭 버튼

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _slotClickButton.onClick.AddListener(OnSlotClicked);  // 슬롯 클릭 시 버튼 이벤트 연결
        SelectItemInfoUI.OnItemStatueChanged += UpdateSlot;   // 아이템 상태가 바뀌면, 아이템슬롯 업데이트
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(Item item)
    {
        CurrentItem = item;
        UpdateSlot();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 아이템슬롯 정보들 업데이트
    /// </summary>
    private void UpdateSlot()
    {
        if (IsSlotEmpty)    // 슬롯 비었으면 업데이트 하지 않고 무시
            return;

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
    /// 슬롯 클릭했을 때 
    /// </summary>
    private void OnSlotClicked()
    {
        if (IsSlotEmpty) // 슬롯이 비었으면 선택 안되게
            return;

        OnSlotSelected?.Invoke(this);
    }

    /// <summary>
    /// 하이라이트 ON
    /// </summary>
    public void Highlight_ON() => _highlightGO.SetActive(true);

    /// <summary>
    /// 하이라이트 OFF
    /// </summary>
    public void Highlight_OFF() => _highlightGO.SetActive(false);

    /// <summary>
    /// 아이템 슬롯 비우고 끄기
    /// </summary>
    public void Clear()
    {
        CurrentItem = null;
        _infoParentGO.SetActive(false);
    }

    /// <summary>
    /// OnDestroy
    /// </summary>
    private void OnDestroy()
    {
        SelectItemInfoUI.OnItemStatueChanged -= UpdateSlot;
    }
}
