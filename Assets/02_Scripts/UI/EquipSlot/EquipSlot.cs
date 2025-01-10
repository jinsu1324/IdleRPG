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
    public Item CurrentItem { get; private set; }       // �� �������Կ� ���ִ� ������

    [Title("���������� ������Ÿ��", bold: false)]
    [SerializeField] protected ItemType _slotItemType;      // ���������� ������Ÿ��

    [Title("�������� �ε���", bold: false)]
    [SerializeField] protected int _slotIndex;              // ���� �ε���


    [Title("On / Off GO", bold: false)]
    [SerializeField] private GameObject _emptyGO;        // �󽽷� GO
    [SerializeField] private GameObject _infoGO;         // ������ GO

    [Title("������", bold: false)]
    [SerializeField] private Image _itemIcon;            // ������ ������
    [SerializeField] private Image _gradeFrame;          // ��� ������
    [SerializeField] private TextMeshProUGUI _levelText; // ������ ���� �ؽ�Ʈ

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
    /// �󽽷� �����ֱ�
    /// </summary>
    public virtual void ShowEmpty()
    {
        CurrentItem = null;

        EmptyON_InfoOFF();
    }

    /// <summary>
    /// ���� �Ѱ�, �󽽷� ����
    /// </summary>
    private void InfoON_EmptyOFF()
    {
        _emptyGO.SetActive(false);
        _infoGO.SetActive(true);
    }

    /// <summary>
    /// �󽽷� �Ѱ�, ���� ����
    /// </summary>
    private void EmptyON_InfoOFF()
    {
        _emptyGO.SetActive(true);
        _infoGO.SetActive(false);
    }
}
