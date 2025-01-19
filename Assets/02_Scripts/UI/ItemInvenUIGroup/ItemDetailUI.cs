using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 상세정보 UI
/// </summary>
public abstract class ItemDetailUI : MonoBehaviour
{
    public Item CurrentItem { get; private set; }             // 현재 아이템

    [Title("공통 정보들", bold: false)]
    [SerializeField] protected Image _itemIcon;               // 아이템 아이콘
    [SerializeField] protected Image _gradeFrame;             // 등급 프레임
    [SerializeField] protected TextMeshProUGUI _nameText;     // 이름 텍스트
    [SerializeField] protected TextMeshProUGUI _gradeText;    // 등급 텍스트
    [SerializeField] protected TextMeshProUGUI _countText;    // 갯수 텍스트
    
    /// <summary>
    /// 보여주기
    /// </summary>
    public void Show(Item item)
    {
        CurrentItem = item;
        UpdateUI(CurrentItem);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 감추기
    /// </summary>
    public void Hide(Item item = null)
    {
        CurrentItem = null;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 감추기 (오버로딩용)
    /// </summary>
    public void Hide()
    {
        CurrentItem = null;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    protected virtual void UpdateUI(Item item)
    {
        //_itemIcon.sprite = item.Icon;
        //_gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);
        //_nameText.text = $"{item.Name}";
        //_nameText.color = ResourceManager.Instance.GetItemGradeColor(item.Grade);
        //_gradeText.text = $"{item.Grade}";
        //_gradeText.color = ResourceManager.Instance.GetItemGradeColor(item.Grade);
    }
}
