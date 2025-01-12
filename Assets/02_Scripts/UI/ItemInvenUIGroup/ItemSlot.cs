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
    public static event Action<Item> OnSlotSelected;                    // 슬롯이 선택되었을 때 이벤트
    public Item CurrentItem { get; private set; }                       // 현재 슬롯 아이템
    public bool IsSlotEmpty => CurrentItem == null;                     // 슬롯이 비어있는지 

    [Title("아이템 정보들 전체부모 GO", bold: false)]
    [SerializeField] private GameObject _infoParentGO;                  // 아이템 정보들 전체부모 GO

    [Title("아이템 기본정보", bold: false)]
    [SerializeField] private Image _itemIcon;                           // 아이템 아이콘
    [SerializeField] private Image _gradeFrame;                         // 등급 프레임
    [SerializeField] private TextMeshProUGUI _countText;                // 아이템 갯수 텍스트

    [Title("슬롯 클릭 관련", bold: false)]
    [SerializeField] private GameObject _highlightGO;                   // 슬롯 선택했을 때 하이라이트
    [SerializeField] private Button _slotClickButton;                   // 슬롯 클릭 버튼

    [Title("강화 관련", bold: false)]
    [SerializeField] private TextMeshProUGUI _levelText;                // 아이템 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // 강화 가능한 아이템 갯수 텍스트
    [SerializeField] private Slider _countSlider;                       // 갯수 표시 슬라이더
    [SerializeField] private GameObject _enhanceableArrowGO;            // 강화 가능할 때 화살표 게임오브젝트

    [Title("장착 관련", bold: false)]
    [SerializeField] private GameObject _equipGO;                       // 장착되었을 때 아이콘 게임오브젝트

    private Action<RectTransform> _moveHilightImageAction;              // 하이라이트 이미지 움직이기 함수 저장할 변수     

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGear += UpdateSlot;  // 장비 장착할때 -> 아이템슬롯 업데이트
        EquipGearManager.OnUnEquipGear += UpdateSlot;  // 장비 해제할때 -> 아이템슬롯 업데이트
        ItemEnhanceManager.OnItemEnhance += UpdateSlot; // 아이템 강화할때 -> 아이템슬롯 업데이트

        _slotClickButton.onClick.AddListener(OnSlotClicked);  // 슬롯 클릭 시 버튼 이벤트 연결
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
    /// 초기화
    /// </summary>
    public void Init(Item item, Action<RectTransform> moveHighlight)
    {
        CurrentItem = item;
        _moveHilightImageAction = moveHighlight;
        UpdateSlot(CurrentItem);

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 아이템슬롯 정보들 업데이트
    /// </summary>
    public void UpdateSlot(Item item)
    {
        // 슬롯 비었으면 업데이트 하지 않고 무시
        if (IsSlotEmpty)    
            return;

        // 아이템 기본정보 업데이트
        _itemIcon.sprite = CurrentItem.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(CurrentItem.Grade);
        _countText.text = $"{CurrentItem.Count}";


        // 강화가능한 아이템이면 강화관련 정보들도 업데이트
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
    /// 슬롯 클릭했을 때 
    /// </summary>
    private void OnSlotClicked()
    {
        if (IsSlotEmpty) // 슬롯이 비었으면 선택 안되게
            return;

        OnSlotSelected?.Invoke(CurrentItem);
        _moveHilightImageAction?.Invoke(_gradeFrame.GetComponent<RectTransform>());
    }

    /// <summary>
    /// 아이템 슬롯 비우고 끄기
    /// </summary>
    public void Clear()
    {
        CurrentItem = null;
        _infoParentGO.SetActive(false);
    }
}
