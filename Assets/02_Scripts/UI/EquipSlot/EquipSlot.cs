using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장착 슬롯 베이스(부모)
/// </summary>
public abstract class EquipSlot : MonoBehaviour
{
    public Item CurrentItem { get; private set; }           // 이 장착슬롯에 들어가있는 아이템

    [Title("장착슬롯의 아이템타입", bold: false)]
    [SerializeField] protected ItemType _slotItemType;      // 장착슬롯의 아이템타입

    [Title("정보들", bold: false)]
    [SerializeField] private GameObject _infoGO;            // 정보들 GO
    [SerializeField] private Image _itemIcon;               // 아이템 아이콘
    [SerializeField] private Image _gradeFrame;             // 등급 프레임
    [SerializeField] private TextMeshProUGUI _levelText;    // 아이템 레벨 텍스트

    /// <summary>
    /// 슬롯 보여주기(+업데이트)
    /// </summary>
    public void UpdateSlot(Item item)
    {
        CurrentItem = item;
        _itemIcon.sprite = item.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);

        if (item is IEnhanceableItem enhancableItem)
            _levelText.text = $"Lv.{enhancableItem.Level}";
        else
            _levelText.text = $"";

        InfoGO_ON();
    }
    
    /// <summary>
    /// 슬롯 비우기
    /// </summary>
    public void EmptySlot()
    {
        CurrentItem = null;
        InfoGO_OFF();
    }

    /// <summary>
    /// 정보GO 켜기
    /// </summary>
    private void InfoGO_ON()
    {
        _infoGO.SetActive(true);
    }

    /// <summary>
    /// 정보GO 끄기
    /// </summary>
    private void InfoGO_OFF()
    {
        _infoGO.SetActive(false);
    }
}
