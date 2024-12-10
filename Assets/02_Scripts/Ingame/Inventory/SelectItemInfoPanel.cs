using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemInfoPanel : MonoBehaviour
{
    public Equipment CurrentSelectedItem { get; private set; }
    [SerializeField] private Image _selectedItemIcon;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _unEquipButton;

    private void Start()
    {
        _equipButton.onClick.AddListener(OnClickEquipButton);
        _unEquipButton.onClick.AddListener(OnClickUnEquipButton);
    }

    public void OnClickEquipButton()
    {
        if (CurrentSelectedItem != null)
        {
            Inventory.Instance.Equip(CurrentSelectedItem);
            UIUpdate();
        }
        else
        {
            Debug.Log("장착 실패.");
        }
    }

    public void OnClickUnEquipButton()
    {
        if (CurrentSelectedItem != null)
        {
            Inventory.Instance.UnEquip(CurrentSelectedItem); // 장착 해제 호출
            UIUpdate();
        }
        else
        {
            Debug.Log("해제 실패. 선택된 아이템이 없습니다.");
        }
    }




    public void Init(InventorySlot selectInventorySlot)
    {
        CurrentSelectedItem = selectInventorySlot.CurrentSlotItem;
        UIUpdate();

        gameObject.SetActive(true);
    }

    private void UIUpdate()
    {
        if (CurrentSelectedItem == null)
        {
            _selectedItemIcon = null;
            _selectedItemIcon.gameObject.SetActive(false);

            _equipButton.gameObject.SetActive(false);
            _unEquipButton.gameObject.SetActive(false);
        }
        else
        {
            _selectedItemIcon.sprite = CurrentSelectedItem.Icon;
            _selectedItemIcon.gameObject.SetActive(true);

            _unEquipButton.gameObject.SetActive(true);
            _unEquipButton.gameObject.SetActive(Inventory.Instance.IsEquipped(CurrentSelectedItem)); // 장착 중인 아이템인지 확인하여 해제 버튼 활성화
        }
        
    }
}
