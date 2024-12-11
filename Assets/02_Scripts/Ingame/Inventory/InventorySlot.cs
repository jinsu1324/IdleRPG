using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Equipment CurrentItem { get; private set; }      // ���� ���� ������
    public bool IsSlotEmpty => CurrentItem == null;         // ������ ����ִ��� 
    
    [SerializeField] private Image _itemIcon;               // ������ ������
    [SerializeField] private Button _slotClickButton;       // ���� Ŭ�� ��ư
    [SerializeField] private GameObject _equippedIcon;      // �����Ǿ��� �� ������
    [SerializeField] private GameObject _slotSelectedFrame; // ���� �������� �� ������      

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
        if (CurrentItem == null)
            return;

        Inventory.Instance.HighlightingSelectdSlot(this);
    }

    /// <summary>
    /// ������ �߰�
    /// </summary>
    public void AddItem(Equipment item)
    {
        CurrentItem = item;
        UpdateItemIcon();
    }        

    /// <summary>
    /// ������ ����
    /// </summary>
    public void ClearItem()
    {
        CurrentItem = null;
        UpdateItemIcon();
    }

    /// <summary>
    /// ������ ǥ�� ������Ʈ
    /// </summary>
    private void UpdateItemIcon()
    {
        if (CurrentItem == null) 
            ClearItemIcon();

        SetItemIcon();
    }

    /// <summary>
    /// ������ ������ ����
    /// </summary>
    private void SetItemIcon()
    {
        _itemIcon.sprite = CurrentItem.Icon;
        _itemIcon.gameObject.SetActive(true);
    }

    /// <summary>
    /// ������ ������ Ŭ����
    /// </summary>
    private void ClearItemIcon()
    {
        _itemIcon.sprite = null;
        _itemIcon.gameObject.SetActive(false);
    }

    /// <summary>
    /// ���� ������ ON
    /// </summary>
    public void SelectFrameON() => _slotSelectedFrame.SetActive(true);

    /// <summary>
    /// ���� ������ OFF
    /// </summary>
    public void SelectFrameOFF() => _slotSelectedFrame.SetActive(false);


    /// <summary>
    /// ���� ������ ON
    /// </summary>
    public void EquippedIconON() => _equippedIcon.SetActive(true);

    /// <summary>
    /// ���� ������ OFF
    /// </summary>
    public void EqiuppedIconOFF() => _equippedIcon.SetActive(false);

}
