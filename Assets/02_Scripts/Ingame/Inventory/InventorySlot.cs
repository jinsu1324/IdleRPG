using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Equipment CurrentItem { get; private set; }      // 현재 슬롯 아이템
    public bool IsSlotEmpty => CurrentItem == null;         // 슬롯이 비어있는지 
    
    [SerializeField] private Image _itemIcon;               // 아이템 아이콘
    [SerializeField] private Button _slotClickButton;       // 슬롯 클릭 버튼
    [SerializeField] private GameObject _equippedIcon;      // 장착되었을 때 아이콘
    [SerializeField] private GameObject _slotSelectedFrame; // 슬롯 선택했을 때 프레임      

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

        Inventory.Instance.HighlightingSelectdSlot(this);
    }

    /// <summary>
    /// 아이템 추가
    /// </summary>
    public void AddItem(Equipment item)
    {
        CurrentItem = item;
        UpdateItemIcon();
    }        

    /// <summary>
    /// 아이템 삭제
    /// </summary>
    public void ClearItem()
    {
        CurrentItem = null;
        UpdateItemIcon();
    }

    /// <summary>
    /// 아이콘 표시 업데이트
    /// </summary>
    private void UpdateItemIcon()
    {
        if (CurrentItem == null) 
            ClearItemIcon();

        SetItemIcon();
    }

    /// <summary>
    /// 아이템 아이콘 셋팅
    /// </summary>
    private void SetItemIcon()
    {
        _itemIcon.sprite = CurrentItem.Icon;
        _itemIcon.gameObject.SetActive(true);
    }

    /// <summary>
    /// 아이템 아이콘 클리어
    /// </summary>
    private void ClearItemIcon()
    {
        _itemIcon.sprite = null;
        _itemIcon.gameObject.SetActive(false);
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
    public void EqiuppedIconOFF() => _equippedIcon.SetActive(false);

}
