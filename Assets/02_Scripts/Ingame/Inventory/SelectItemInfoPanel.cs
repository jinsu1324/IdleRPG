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
            Debug.Log("���� ����.");
        }
    }

    public void OnClickUnEquipButton()
    {
        if (CurrentSelectedItem != null)
        {
            Inventory.Instance.UnEquip(CurrentSelectedItem); // ���� ���� ȣ��
            UIUpdate();
        }
        else
        {
            Debug.Log("���� ����. ���õ� �������� �����ϴ�.");
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
            _unEquipButton.gameObject.SetActive(Inventory.Instance.IsEquipped(CurrentSelectedItem)); // ���� ���� ���������� Ȯ���Ͽ� ���� ��ư Ȱ��ȭ
        }
        
    }
}
