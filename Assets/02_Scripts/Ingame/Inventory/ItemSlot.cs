using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item CurrentItem { get; private set; }      // ���� ���� ������
    public bool IsSlotEmpty => CurrentItem == null;         // ������ ����ִ��� 
    
    [SerializeField] private Image _itemIcon;               // ������ ������
    [SerializeField] private Button _slotClickButton;       // ���� Ŭ�� ��ư
    [SerializeField] private GameObject _equippedIcon;      // �����Ǿ��� �� ������
    [SerializeField] private GameObject _slotSelectedFrame; // ���� �������� �� ������

    [SerializeField] private TextMeshProUGUI _levelText;               // ������ ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _countText;               // ������ ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;    // ��ȭ ������ ������ ���� �ؽ�Ʈ

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

        //Inventory.Instance.HighlightingSelectdSlot(this);
        //ItemSlotManager.Instance.HighlightingSelectdSlot(this);

        InventoryManager.Instance.GetItemSlotManager(CurrentItem.ItemType).HighlightingSelectdSlot(this);

    }

    /// <summary>
    /// ������ �߰�
    /// </summary>
    public void AddItem(Item item)
    {
        CurrentItem = item;
        UpdateItemInfoUI();
    }        

    /// <summary>
    /// ������ ����
    /// </summary>
    public void ClearItem()
    {
        CurrentItem = null;
        UpdateItemInfoUI();
    }

    /// <summary>
    /// ������ ���� UI ������Ʈ
    /// </summary>
    public void UpdateItemInfoUI()
    {
        if (CurrentItem == null) 
            ClearItemInfoUI();

        SetItemInfoUI();
    }

    /// <summary>
    /// ������ ���� UI ����
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
    /// ������ ���� UI Ŭ����
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
    public void EquippedIconOFF() => _equippedIcon.SetActive(false);
}
