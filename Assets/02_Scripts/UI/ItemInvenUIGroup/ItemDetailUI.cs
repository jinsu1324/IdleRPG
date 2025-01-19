using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ������ UI
/// </summary>
public abstract class ItemDetailUI : MonoBehaviour
{
    public Item CurrentItem { get; private set; }             // ���� ������

    [Title("���� ������", bold: false)]
    [SerializeField] protected Image _itemIcon;               // ������ ������
    [SerializeField] protected Image _gradeFrame;             // ��� ������
    [SerializeField] protected TextMeshProUGUI _nameText;     // �̸� �ؽ�Ʈ
    [SerializeField] protected TextMeshProUGUI _gradeText;    // ��� �ؽ�Ʈ
    [SerializeField] protected TextMeshProUGUI _countText;    // ���� �ؽ�Ʈ
    
    /// <summary>
    /// �����ֱ�
    /// </summary>
    public void Show(Item item)
    {
        CurrentItem = item;
        UpdateUI(CurrentItem);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ���߱�
    /// </summary>
    public void Hide(Item item = null)
    {
        CurrentItem = null;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ���߱� (�����ε���)
    /// </summary>
    public void Hide()
    {
        CurrentItem = null;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// UI ������Ʈ
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
