using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Equipment CurrentSlotItem { get; private set; }  // 현재 슬롯 아이템
    public bool IsSlotEmpty => CurrentSlotItem == null;     // 슬롯이 비어있는지 
    
    [SerializeField] private Image _itemIcon;               // 아이템 아이콘
    [SerializeField] private Image _equippedIcon;           // 장착되었을 때 아이콘
    [SerializeField] private Button _slotClickButton;       // 슬롯 클릭 버튼
    [SerializeField] private Image _slotSelectedFrame;      // 슬롯 선택했을 때 프레임      

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
        if (CurrentSlotItem == null)
        {
            Debug.Log("이 슬롯은 비어 있습니다.");
            return;
        }

        Inventory.Instance.SelectSlot(this);

    }

    /// <summary>
    /// 아이템 
    /// </summary>
    public void AddItem(Equipment item)
    {
        CurrentSlotItem = item;
        UpdateIcon();
    }        

    /// <summary>
    /// 아이템 삭제
    /// </summary>
    public void ClearItem()
    {
        CurrentSlotItem = null;
        UpdateIcon();
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdateIcon()
    {
        if (CurrentSlotItem == null) 
        {
            _itemIcon.sprite = null;
            _itemIcon.gameObject.SetActive(false);
        }


        _itemIcon.sprite = CurrentSlotItem.Icon;    
        _itemIcon.gameObject.SetActive(true);
    }

   

    public void HigilightIconON()
    {
        _slotSelectedFrame.gameObject.SetActive(true);
    }
    public void HigilightIconOFF()
    {
        _slotSelectedFrame.gameObject.SetActive(false);

    }




    public void EquipIconON()
    {
        _equippedIcon.gameObject.SetActive(true);
    }

    public void EqiupIconOFF()
    {
        _equippedIcon.gameObject.SetActive(false);

    }

}
