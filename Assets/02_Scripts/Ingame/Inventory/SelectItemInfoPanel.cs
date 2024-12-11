using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemInfoPanel : MonoBehaviour
{
    public Equipment CurrentItem { get; private set; }  // ���� ������

    [SerializeField] private Image _itemIcon;           // ������ ������
    [SerializeField] private Button _equipButton;       // ���� ��ư
    [SerializeField] private Button _unEquipButton;     // ���� ���� ��ư

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _equipButton.onClick.AddListener(OnClickEquipButton);       
        _unEquipButton.onClick.AddListener(OnClickUnEquipButton);   
    }

    /// <summary>
    /// �ʱ�ȭ �� ����
    /// </summary>
    public void OpenAndInit(InventorySlot selectedSlot)
    {
        CurrentItem = selectedSlot.CurrentItem;
        UIUpdate();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UIUpdate()
    {
        // ���� �������� ������ ���� �� ���� ����
        if (CurrentItem == null) 
        {
            ClearItemIcon();
            ButtonsOFF();

            return;
        }

        // ������ ���� �� ��ư �ѱ�
        SetItemIcon();
        ButtonsON();
    }

    /// <summary>
    /// ������ư Ŭ��
    /// </summary>
    public void OnClickEquipButton()
    {
        if (CurrentItem == null)
            return;

        Inventory.Instance.Equip(CurrentItem);  // ���� ������ ����
        UIUpdate();
    }

    /// <summary>
    /// ���� ���� ��ư Ŭ��
    /// </summary>
    public void OnClickUnEquipButton()
    {
        if (CurrentItem == null)
            return;

        Inventory.Instance.UnEquip(CurrentItem); // ���� ������ ���� ����
        UIUpdate();
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
        _itemIcon = null;
        _itemIcon.gameObject.SetActive(false);
    }

    /// <summary>
    /// ��ư�� �ѱ�
    /// </summary>
    private void ButtonsON()
    {
        _unEquipButton.gameObject.SetActive(true);
        _unEquipButton.gameObject.SetActive(Inventory.Instance.IsEquipped(CurrentItem)); // ������ư�� '�г� ������' = '������ ������' �� ���� Ȱ��ȭ
    }

    /// <summary>
    /// ��ư�� ����
    /// </summary>
    private void ButtonsOFF()
    {
        _equipButton.gameObject.SetActive(false);
        _unEquipButton.gameObject.SetActive(false);
    }
}
