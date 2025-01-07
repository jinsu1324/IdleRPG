using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ���� ���̽�(�θ�)
/// </summary>
public abstract class EquipSlot : MonoBehaviour
{
    public IItem CurrentItem { get; private set; }              // �� ���Կ� ���ִ� ������

    [Title("���������� ������Ÿ��", bold: false)]
    [SerializeField] protected ItemType _itemType;                // ���������� ������Ÿ��

    [Title("On Off GO", bold: false)]
    [SerializeField] private GameObject _emptyGO;               // �󽽷� GO
    [SerializeField] private GameObject _infoGO;                // ������ GO

    [Title("������", bold: false)]
    [SerializeField] private Image _itemIcon;                   // ������ ������
    [SerializeField] private Image _gradeFrame;                 // ��� ������
    [SerializeField] private TextMeshProUGUI _levelText;        // ������ ���� �ؽ�Ʈ


    /// <summary>
    /// OnEnable
    /// </summary>
    protected abstract void OnEnable();

    /// <summary>
    /// OnDisable
    /// </summary>
    protected abstract void OnDisable();

    /// <summary>
    /// ���� �����ֱ�
    /// </summary>
    public virtual void ShowInfoGO(IItem item)
    {
        CurrentItem = item;
        _itemIcon.sprite = item.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);
        _levelText.text = $"Lv.{item.Level}";
        

        _emptyGO.SetActive(false);
        _infoGO.SetActive(true); // ���� �ѱ�
    }

    /// <summary>
    /// �󽽷� �����ֱ�
    /// </summary>
    public virtual void ShowEmptyGO()
    {
        CurrentItem = null;
        _emptyGO.SetActive(true);
        _infoGO.SetActive(false); // ���� ����
    }
}
