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
    [SerializeField] private SelectItemInfoUI _selectItemInfoUI;        // ���õ� ������ ���� UI
    [SerializeField] private ItemSlotManager _itemSlotManager;          // ������ ���Ե� �Ŵ���
    [SerializeField] private Button _exitButton;                        // ������ ��ư

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemSlot.OnSlotSelected += TurnOn_SelectItemInfoUI; // ������ ���� ���õǾ��� ��, ���õ� ������ ���� UI �ѱ�
        _exitButton.onClick.AddListener(Hide);  // ������ ��ư ������ �˾�����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemSlot.OnSlotSelected -= TurnOn_SelectItemInfoUI;
        _exitButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public void Show(ItemType itemType)
    {
        _titleText.text = itemType.ToString();

        _itemSlotManager.Init(itemType);    // ������ ���Ե� �ʱ�ȭ
        _selectItemInfoUI.Hide();           // ���� ������ ���� UI�� ���� ����

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ���õ� ������ ���� UI �ѱ�
    /// </summary>
    private void TurnOn_SelectItemInfoUI(ItemSlot selectSlot)
    {
        _selectItemInfoUI.Show(selectSlot);
    }
}
