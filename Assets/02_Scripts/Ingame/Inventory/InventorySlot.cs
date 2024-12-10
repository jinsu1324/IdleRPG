using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Equipment CurrentSlotItem { get; private set; }  // ���� ���� ������
    public bool IsSlotEmpty => CurrentSlotItem == null;     // ������ ����ִ��� 
    
    [SerializeField] private Image _itemIcon;               // ������ ������
    [SerializeField] private Image _equippedIcon;           // �����Ǿ��� �� ������
    [SerializeField] private Button _slotClickButton;       // ���� Ŭ�� ��ư
    [SerializeField] private Image _slotSelectedFrame;      // ���� �������� �� ������      

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _slotClickButton.onClick.AddListener(OnSlotClicked);    // ���� Ŭ�� �� ��ư �̺�Ʈ ����
    }

    /// <summary>
    /// ���� Ŭ������ �� 
    /// </summary>
    private void OnSlotClicked()
    {
        if (CurrentSlotItem == null)
        {
            Debug.Log("�� ������ ��� �ֽ��ϴ�.");
            return;
        }

        Inventory.Instance.SelectSlot(this);

    }

    /// <summary>
    /// ������ 
    /// </summary>
    public void AddItem(Equipment item)
    {
        CurrentSlotItem = item;
        UpdateIcon();
    }        

    /// <summary>
    /// ������ ����
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
