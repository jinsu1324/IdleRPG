using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;                // �˾� Ÿ��Ʋ �ؽ�Ʈ
    [SerializeField] private SelectItemInfoUI _selectItemInfoUI;        // ���õ� ������ ���� UI
    [SerializeField] private ItemSlotManager _itemSlotManager;          // ������ ���Ե� �Ŵ���
    [SerializeField] private Button _exitButton;                        // ������ ��ư

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _exitButton.onClick.AddListener(Hide);  // ������ ��ư ������ �˾�����
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public void Show(ItemType itemType)
    {
        _titleText.text = itemType.ToString();

        _itemSlotManager.Init(itemType);    // ������ ���Ե� �ʱ�ȭ
        _selectItemInfoUI.Hide();           // ���þ����� ����UI�� ���� ����

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
