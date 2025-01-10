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
    public Item CurrentItem { get; private set; }       // 이 장착슬롯에 들어가있는 아이템

    [Title("장착슬롯의 아이템타입", bold: false)]
    [SerializeField] protected ItemType _slotItemType;      // 장착슬롯의 아이템타입

    [Title("장착슬롯 인덱스", bold: false)]
    [SerializeField] protected int _slotIndex;              // 슬롯 인덱스


    [Title("On / Off GO", bold: false)]
    [SerializeField] private GameObject _emptyGO;        // 빈슬롯 GO
    [SerializeField] private GameObject _infoGO;         // 정보들 GO

    [Title("정보들", bold: false)]
    [SerializeField] private Image _itemIcon;            // 아이템 아이콘
    [SerializeField] private Image _gradeFrame;          // 등급 프레임
    [SerializeField] private TextMeshProUGUI _levelText; // 아이템 레벨 텍스트

    /// <summary>
    /// OnEnable
    /// </summary>
    protected abstract void OnEnable();

    /// <summary>
    /// OnDisable
    /// </summary>
    protected abstract void OnDisable();

    /// <summary>
    /// 정보 보여주기
    /// </summary>
    public virtual void ShowInfo(Item item)
    {
        CurrentItem = item;
        _itemIcon.sprite = item.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);

        if (item is IEnhanceableItem enhancableItem)
            _levelText.text = $"Lv.{enhancableItem.Level}";
        else
            _levelText.text = $"-";

        InfoON_EmptyOFF();
    }

    /// <summary>
    /// 빈슬롯 보여주기
    /// </summary>
    public virtual void ShowEmpty()
    {
        CurrentItem = null;

        EmptyON_InfoOFF();
    }

    /// <summary>
    /// 정보 켜고, 빈슬롯 끄기
    /// </summary>
    private void InfoON_EmptyOFF()
    {
        _emptyGO.SetActive(false);
        _infoGO.SetActive(true);
    }

    /// <summary>
    /// 빈슬롯 켜고, 정보 끄기
    /// </summary>
    private void EmptyON_InfoOFF()
    {
        _emptyGO.SetActive(true);
        _infoGO.SetActive(false);
    }
}
