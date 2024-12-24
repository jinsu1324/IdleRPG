using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    [Title("������ ��ü�θ� GO", bold: false)]
    [SerializeField] private GameObject _infoParentGO;       // ������ ��ü�θ� GO

    [Title("������ ������", bold: false)]
    [SerializeField] private Image _itemIcon;                // ������ ������
    [SerializeField] private Image _gradeFrame;              // ��� ������
    [SerializeField] private TextMeshProUGUI _levelText;     // ������ ���� �ؽ�Ʈ

    /// <summary>
    /// �������� �����ֱ�
    /// </summary>
    public void Show(Item item)
    {
        _itemIcon.sprite = item.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);
        _levelText.text = $"Lv.{item.Level}";
        
        _infoParentGO.SetActive(true);
    }

    /// <summary>
    /// �������� ����
    /// </summary>
    public void Hide()
    {
        _infoParentGO.SetActive(false);
    }
}
