using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemInfoPanel : MonoBehaviour
{
    public Item CurrentItem { get; private set; }  // ���� ������

    [SerializeField] private Image _itemIcon;           // ������ ������
    [SerializeField] private Button _equipButton;       // ���� ��ư
    [SerializeField] private Button _unEquipButton;     // ���� ���� ��ư
    [SerializeField] private Button _enhanceButton;     // ��ȭ ��ư

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _equipButton.onClick.AddListener(OnClickEquipButton);       
        _unEquipButton.onClick.AddListener(OnClickUnEquipButton);
        _enhanceButton.onClick.AddListener(OnClickEnhanceButton);
    }

    /// <summary>
    /// �ʱ�ȭ �� ����
    /// </summary>
    public void OpenAndInit(ItemSlot selectedSlot)
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

        //ItemEquipManager.Instance.Equip(CurrentItem);  // ���� ������ ����
        InventoryManager.Instance.GetItemEquipManager(CurrentItem.ItemType).Equip(CurrentItem);
        UIUpdate();
    }

    /// <summary>
    /// ���� ���� ��ư Ŭ��
    /// </summary>
    public void OnClickUnEquipButton()
    {
        if (CurrentItem == null)
            return;

        //ItemEquipManager.Instance.UnEquip(CurrentItem); // ���� ������ ���� ����
        InventoryManager.Instance.GetItemEquipManager(CurrentItem.ItemType).UnEquip(CurrentItem);
        UIUpdate();
    }

    public void OnClickEnhanceButton()
    {
        if (CurrentItem == null)
            return;

        CurrentItem.Enhance();


        UIUpdate(); // �г� UI ������Ʈ
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

        //bool isEquipped = ItemEquipManager.Instance.IsEquipped(CurrentItem);
        bool isEquipped = InventoryManager.Instance.GetItemEquipManager(CurrentItem.ItemType).IsEquipped(CurrentItem);

        bool isEnhanceable = CurrentItem.IsEnhanceable();

        _equipButton.gameObject.SetActive(!isEquipped);  // ������ư�� �����Ǿ������� ��Ȱ��ȭ
        _unEquipButton.gameObject.SetActive(isEquipped);   // ������ư�� '�г� ������' = '������ ������' �� ���� Ȱ��ȭ
        _enhanceButton.gameObject.SetActive(isEnhanceable); // ��ȭ �����Ҷ��� Ȱ��ȭ

    }

    /// <summary>
    /// ��ư�� ����
    /// </summary>
    private void ButtonsOFF()
    {
        _equipButton.gameObject.SetActive(false);
        _unEquipButton.gameObject.SetActive(false);
        _enhanceButton.gameObject.SetActive(false);
    }
}
