using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ����
/// </summary>
public class EquipSlot : MonoBehaviour
{
    [Title("������ ��ü�θ� GO", bold: false)]
    [SerializeField] private GameObject _infoParentGO;          // ������ ��ü�θ� GO

    [Title("������ ������", bold: false)]
    [SerializeField] private Image _itemIcon;                   // ������ ������
    [SerializeField] private Image _gradeFrame;                 // ��� ������
    [SerializeField] private TextMeshProUGUI _levelText;        // ������ ���� �ؽ�Ʈ

    [Title("����� ������Ʈ", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;  // ����� ������Ʈ

    /// <summary>
    /// �������� �����ֱ�
    /// </summary>
    public void Show(Item item, ItemType itemType)
    {
        _itemIcon.sprite = item.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);
        _levelText.text = $"Lv.{item.Level}";

        Update_ReddotComponent(itemType);

        _infoParentGO.SetActive(true);
    }

    /// <summary>
    /// �������� ����
    /// </summary>
    public void Hide(ItemType itemType)
    {
        Update_ReddotComponent(itemType);

        _infoParentGO.SetActive(false);
    }

    /// <summary>
    /// ����� ������Ʈ ������Ʈ
    /// </summary>
    public void Update_ReddotComponent(ItemType itemType)
    {
        _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(itemType));
    }
}
