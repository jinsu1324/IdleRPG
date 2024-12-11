using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemInfoPanel : MonoBehaviour
{
    public Equipment CurrentItem { get; private set; }  // 현재 아이템

    [SerializeField] private Image _itemIcon;           // 아이템 아이콘
    [SerializeField] private Button _equipButton;       // 장착 버튼
    [SerializeField] private Button _unEquipButton;     // 장착 해제 버튼

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _equipButton.onClick.AddListener(OnClickEquipButton);       
        _unEquipButton.onClick.AddListener(OnClickUnEquipButton);   
    }

    /// <summary>
    /// 초기화 및 열기
    /// </summary>
    public void OpenAndInit(InventorySlot selectedSlot)
    {
        CurrentItem = selectedSlot.CurrentItem;
        UIUpdate();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UIUpdate()
    {
        // 현재 아이템이 없으면 전부 다 끄고 리턴
        if (CurrentItem == null) 
        {
            ClearItemIcon();
            ButtonsOFF();

            return;
        }

        // 아이콘 셋팅 및 버튼 켜기
        SetItemIcon();
        ButtonsON();
    }

    /// <summary>
    /// 장착버튼 클릭
    /// </summary>
    public void OnClickEquipButton()
    {
        if (CurrentItem == null)
            return;

        Inventory.Instance.Equip(CurrentItem);  // 현재 아이템 장착
        UIUpdate();
    }

    /// <summary>
    /// 장착 해제 버튼 클릭
    /// </summary>
    public void OnClickUnEquipButton()
    {
        if (CurrentItem == null)
            return;

        Inventory.Instance.UnEquip(CurrentItem); // 현재 아이템 장착 해제
        UIUpdate();
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
        _itemIcon = null;
        _itemIcon.gameObject.SetActive(false);
    }

    /// <summary>
    /// 버튼들 켜기
    /// </summary>
    private void ButtonsON()
    {
        _unEquipButton.gameObject.SetActive(true);
        _unEquipButton.gameObject.SetActive(Inventory.Instance.IsEquipped(CurrentItem)); // 해제버튼은 '패널 아이템' = '장착된 아이템' 일 때만 활성화
    }

    /// <summary>
    /// 버튼들 끄기
    /// </summary>
    private void ButtonsOFF()
    {
        _equipButton.gameObject.SetActive(false);
        _unEquipButton.gameObject.SetActive(false);
    }
}
