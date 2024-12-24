using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 선택된 슬롯의 아이템정보들 표시 및 장착, 해제, 강화버튼 관리
/// </summary>
public class SelectItemInfoUI : MonoBehaviour
{
    public static event Action OnItemStatueChanged;                     // 아이템 상태가 바뀌었을 때 이벤트
    public Item CurrentItem { get; private set; }                       // 현재 아이템

    [Title("정보들 전체부모 GO", bold: false)]
    [SerializeField] private GameObject _infoParentGO;                  // 정보들 전체부모 GO

    [Title("아이템 정보들", bold: false)]
    [SerializeField] private Image _itemIcon;                           // 아이템 아이콘
    [SerializeField] private Image _gradeFrame;                         // 등급 프레임
    [SerializeField] private TextMeshProUGUI _nameText;                 // 이름 텍스트
    [SerializeField] private TextMeshProUGUI _gradeText;                // 등급 텍스트
    [SerializeField] private TextMeshProUGUI _levelText;                // 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _countText;                // 갯수 텍스트
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // 강화 가능한 아이템 갯수 텍스트
    [SerializeField] private Slider _countSlider;                       // 갯수 표시 슬라이더

    [Title("아이템 강화화살표 GO", bold: false)]
    [SerializeField] private GameObject _equipGO;                       // 장착되었을 때 아이콘 게임오브젝트
    [SerializeField] private GameObject _enhanceableArrowGO;            // 강화 가능할 때 화살표 게임오브젝트

    [Title("버튼들", bold: false)]
    [SerializeField] private Button _equipButton;                       // 장착 버튼
    [SerializeField] private Button _unEquipButton;                     // 장착 해제 버튼
    [SerializeField] private Button _enhanceButton;                     // 강화 버튼

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // 버튼들에 핸들러 등록
        _equipButton.onClick.AddListener(OnClick_EquipButton);       
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);

        ItemSlot.OnSlotSelected += Show;    // 슬롯이 선택되었을 때, 아이템정보UI 보여주기
    }

    /// <summary>
    /// 보여주기
    /// </summary>
    public void Show(ItemSlot selectSlot)
    {
        CurrentItem = selectSlot.CurrentItem;
        UpdateUI();
        _infoParentGO.SetActive(true);
    }

    /// <summary>
    /// 감추기
    /// </summary>
    public void Hide()
    {
        CurrentItem = null;
        _infoParentGO.SetActive(false);
    }

    /// <summary>
    /// UI 업데이트
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
    /// 버튼들 업데이트
    /// </summary>
    private void UpdateButtons()
    {
        bool isEquipped = EquipItemManager.IsEquipped(CurrentItem);
        bool isEnhanceable = CurrentItem.IsEnhanceable();

        _equipButton.gameObject.SetActive(!isEquipped); // 장착되어있으면, 장착버튼 OFF
        _unEquipButton.gameObject.SetActive(isEquipped);   // 장착되어있으면, 장착해제버튼 ON
        _enhanceButton.gameObject.SetActive(isEnhanceable); // 강화 가능할때, 강화버튼 ON
    }

    /// <summary>
    /// 장착 버튼 클릭
    /// </summary>
    public void OnClick_EquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipItemManager.Equip(CurrentItem); // 장착
        UpdateUI();
        OnItemStatueChanged?.Invoke();
    }

    /// <summary>
    /// 장착 해제 버튼 클릭
    /// </summary>
    public void OnClick_UnEquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipItemManager.UnEquip(CurrentItem);  // 장착 해제
        UpdateUI();
        OnItemStatueChanged?.Invoke();
    }

    /// <summary>
    /// 강화 버튼 클릭
    /// </summary>
    public void OnClick_EnhanceButton()
    {
        if (CurrentItem == null)
            return;

        ItemEnhanceManager.Enhance(CurrentItem); // 강화
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
