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
/// ���õ� ������ ����ǥ�� �� ��ȣ�ۿ�(����, ����, ��ȭ)
/// </summary>
public abstract class ItemDetailUI : MonoBehaviour
{
    //public static event Action OnSelectItemInfoChanged;                 // ���� ������ ������ �ٲ���� �� �̺�Ʈ
    public Item CurrentItem { get; private set; }                      // ���� ������

    [Title("���� �ֻ�� �θ�", bold: false)]
    [SerializeField] protected GameObject _uiRoot;                      // �ֻ�� �θ�

    [Title("������", bold: false)]
    [SerializeField] protected Image _itemIcon;                           // ������ ������
    [SerializeField] protected Image _gradeFrame;                         // ��� ������
    [SerializeField] protected TextMeshProUGUI _nameText;                 // �̸� �ؽ�Ʈ
    [SerializeField] protected TextMeshProUGUI _gradeText;                // ��� �ؽ�Ʈ
    [SerializeField] protected TextMeshProUGUI _countText;                // ���� �ؽ�Ʈ

    [Title("�󼼼��� �ؽ�Ʈ", bold: false)]
    [SerializeField] protected TextMeshProUGUI _descText;                 // �󼼼��� �ؽ�Ʈ


    /// <summary>
    /// OnEnable
    /// </summary>
    protected abstract void OnEnable();

    /// <summary>
    /// OnDisable
    /// </summary>
    protected abstract void OnDisable();

    /// <summary>
    /// �����ֱ�
    /// </summary>
    public virtual void Show(Item item)
    {
        CurrentItem = item;
        UpdateUI();
        _uiRoot.SetActive(true);
    }

    /// <summary>
    /// ���߱�
    /// </summary>
    public virtual void Hide()
    {
        CurrentItem = null;
        _uiRoot.SetActive(false);
    }

    /// <summary>
    /// UI ������Ʈ
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
