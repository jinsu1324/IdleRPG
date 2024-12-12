using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item CurrentItem { get; private set; }      // 현재 슬롯 아이템
    public bool IsSlotEmpty => CurrentItem == null;         // 슬롯이 비어있는지 
    
    [SerializeField] private Image _itemIcon;               // 아이템 아이콘
    [SerializeField] private Button _slotClickButton;       // 슬롯 클릭 버튼
    [SerializeField] private GameObject _equippedIcon;      // 장착되었을 때 아이콘
    [SerializeField] private GameObject _slotSelectedFrame; // 슬롯 선택했을 때 프레임

    [SerializeField] private TextMeshProUGUI _levelText;               // 아이템 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _countText;               // 아이템 갯수 텍스트
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;    // 강화 가능한 아이템 갯수 텍스트

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _slotClickButton.onClick.AddListener(OnSlotClicked);    // 슬롯 클릭 시 버튼 이벤트 연결


    }

    /// <summary>
    /// 슬롯 클릭했을 때 
    /// </summary>
    private void OnSlotClicked()
    {
        if (CurrentItem == null)
            return;

        //Inventory.Instance.HighlightingSelectdSlot(this);
        //ItemSlotManager.Instance.HighlightingSelectdSlot(this);

        InventoryManager.Instance.GetItemSlotManager(CurrentItem.ItemType).HighlightingSelectdSlot(this);

    }

    /// <summary>
    /// 아이템 추가
    /// </summary>
    public void AddItem(Item item)
    {
        CurrentItem = item;
        UpdateItemInfoUI();
    }        

    /// <summary>
    /// 아이템 삭제
    /// </summary>
    public void ClearItem()
    {
        CurrentItem = null;
        UpdateItemInfoUI();
    }

    /// <summary>
    /// 아이템 정보 UI 업데이트
    /// </summary>
    public void UpdateItemInfoUI()
    {
        if (CurrentItem == null) 
            ClearItemInfoUI();

        SetItemInfoUI();
    }

    /// <summary>
    /// 아이템 정보 UI 셋팅
    /// </summary>
    private void SetItemInfoUI()
    {
        _itemIcon.sprite = CurrentItem.Icon;
        _itemIcon.gameObject.SetActive(true);

        _levelText.text = CurrentItem.Level.ToString();
        _levelText.gameObject.SetActive(true);

        _countText.text = CurrentItem.Count.ToString();
        _countText.gameObject.SetActive(true);

        _enhanceableCountText.text = CurrentItem.EnhanceableCount.ToString();
        _enhanceableCountText.gameObject.SetActive(true);
    }

    /// <summary>
    /// 아이템 정보 UI 클리어
    /// </summary>
    private void ClearItemInfoUI()
    {
        _itemIcon.sprite = null;
        _itemIcon.gameObject.SetActive(false);

        _levelText.text = string.Empty;
        _levelText.gameObject.SetActive(false);

        _countText.text = string.Empty;
        _countText.gameObject.SetActive(false);

        _enhanceableCountText.text = string.Empty;
        _enhanceableCountText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 선택 프레임 ON
    /// </summary>
    public void SelectFrameON() => _slotSelectedFrame.SetActive(true);

    /// <summary>
    /// 선택 프레임 OFF
    /// </summary>
    public void SelectFrameOFF() => _slotSelectedFrame.SetActive(false);

    /// <summary>
    /// 장착 아이템 ON
    /// </summary>
    public void EquippedIconON() => _equippedIcon.SetActive(true);

    /// <summary>
    /// 장착 아이템 OFF
    /// </summary>
    public void EquippedIconOFF() => _equippedIcon.SetActive(false);
}
