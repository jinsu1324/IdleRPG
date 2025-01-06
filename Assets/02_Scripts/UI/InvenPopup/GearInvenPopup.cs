using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장비 인벤토리 팝업
/// </summary>
public class GearInvenPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;                // 팝업 타이틀 텍스트
    [SerializeField] private SelectItemInfoUI _selectItemInfoUI;        // 선택된 아이템 정보 UI
    [SerializeField] private ItemSlotManager _itemSlotManager;          // 아이템 슬롯들 매니저
    [SerializeField] private Button _exitButton;                        // 나가기 버튼

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _exitButton.onClick.AddListener(Hide);  // 나가기 버튼 누르면 팝업끄기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _exitButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public void Show(ItemType itemType)
    {
        _titleText.text = itemType.ToString();

        _itemSlotManager.Init(itemType);    // 아이템 슬롯들 초기화
        _selectItemInfoUI.Hide();           // 선택 아이템 정보 UI는 끄고 시작

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 팝업 끄기
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
