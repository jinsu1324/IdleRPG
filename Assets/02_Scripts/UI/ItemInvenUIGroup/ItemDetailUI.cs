using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 선택된 아이템 정보표시 및 상호작용(장착, 해제, 강화)
/// </summary>
public abstract class ItemDetailUI : MonoBehaviour
{
    //public static event Action OnSelectItemInfoChanged;                 // 선택 아이템 정보가 바뀌었을 때 이벤트
    public Item CurrentItem { get; private set; }                      // 현재 아이템

    [Title("가장 최상단 부모", bold: false)]
    [SerializeField] protected GameObject _uiRoot;                      // 최상단 부모

    [Title("정보들", bold: false)]
    [SerializeField] protected Image _itemIcon;                           // 아이템 아이콘
    [SerializeField] protected Image _gradeFrame;                         // 등급 프레임
    [SerializeField] protected TextMeshProUGUI _nameText;                 // 이름 텍스트
    [SerializeField] protected TextMeshProUGUI _gradeText;                // 등급 텍스트
    [SerializeField] protected TextMeshProUGUI _countText;                // 갯수 텍스트

    [Title("상세설명 텍스트", bold: false)]
    [SerializeField] protected TextMeshProUGUI _descText;                 // 상세설명 텍스트


    /// <summary>
    /// OnEnable
    /// </summary>
    protected abstract void OnEnable();

    /// <summary>
    /// OnDisable
    /// </summary>
    protected abstract void OnDisable();

    /// <summary>
    /// 보여주기
    /// </summary>
    public virtual void Show(Item item)
    {
        CurrentItem = item;
        UpdateUI();
        _uiRoot.SetActive(true);
    }

    /// <summary>
    /// 감추기
    /// </summary>
    public virtual void Hide()
    {
        CurrentItem = null;
        _uiRoot.SetActive(false);
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    protected virtual void UpdateUI()
    {
        _itemIcon.sprite = CurrentItem.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(CurrentItem.Grade);
        _nameText.text = $"{CurrentItem.Name}";
        _nameText.color = ResourceManager.Instance.GetItemGradeColor(CurrentItem.Grade);
        _gradeText.text = $"{CurrentItem.Grade}";
        _gradeText.color = ResourceManager.Instance.GetItemGradeColor(CurrentItem.Grade);
        _descText.text = CurrentItem.Desc;
    }
}
