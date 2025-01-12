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
    public Item CurrentItem { get; private set; }           // �� �������Կ� ���ִ� ������

    [Title("���������� ������Ÿ��", bold: false)]
    [SerializeField] protected ItemType _slotItemType;      // ���������� ������Ÿ��

    [Title("������", bold: false)]
    [SerializeField] private GameObject _infoGO;            // ������ GO
    [SerializeField] private Image _itemIcon;               // ������ ������
    [SerializeField] private Image _gradeFrame;             // ��� ������
    [SerializeField] private TextMeshProUGUI _levelText;    // ������ ���� �ؽ�Ʈ

    /// <summary>
    /// ���� �����ֱ�(+������Ʈ)
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
    /// ���� ����
    /// </summary>
    public void EmptySlot()
    {
        CurrentItem = null;
        InfoGO_OFF();
    }

    /// <summary>
    /// ����GO �ѱ�
    /// </summary>
    private void InfoGO_ON()
    {
        _infoGO.SetActive(true);
    }

    /// <summary>
    /// ����GO ����
    /// </summary>
    private void InfoGO_OFF()
    {
        _infoGO.SetActive(false);
    }
}
