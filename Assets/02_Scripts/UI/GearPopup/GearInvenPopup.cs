using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��� �κ��丮 �˾�
/// </summary>
public class GearInvenPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;                // �˾� Ÿ��Ʋ �ؽ�Ʈ
    [SerializeField] private ItemDetailUI_Gear _itemDetailUI_Gear;      // ���õ� ������ ���� UI
    [SerializeField] private ItemSlotManager _itemSlotManager;          // ������ ���Ե� �Ŵ���
    [SerializeField] private Button _exitButton;                        // ������ ��ư

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemSlot.OnSlotSelected += _itemDetailUI_Gear.Show; // ������ ���� ���õǾ��� ��, �������� ������ UI �ѱ�
        _exitButton.onClick.AddListener(() => Hide());  // ������ ��ư ������ �˾�����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemSlot.OnSlotSelected -= _itemDetailUI_Gear.Show;
        _exitButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public void Show(ItemType itemType)
    {
        _titleText.text = itemType.ToString();

        _itemSlotManager.Init(itemType);    // ������ ���Ե� �ʱ�ȭ
        _itemDetailUI_Gear.Hide();          // ���� ������ ���� UI�� ���� ����

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public void Hide(Item item = null)
    {
        gameObject.SetActive(false);
    }
}
