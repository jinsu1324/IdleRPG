using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ����
/// </summary>
[RequireComponent(typeof(ReddotComponent))]
public class EquipSlot : MonoBehaviour
{
    [Title("���������� ������Ÿ��", bold: false)]
    [SerializeField] private ItemType _itemType;                // ���������� ������Ÿ��

    [Title("On Off GO", bold: false)]
    [SerializeField] private GameObject _emptyGO;               // �󽽷� GO
    [SerializeField] private GameObject _infoGO;                // ������ GO

    [Title("������", bold: false)]
    [SerializeField] private Image _itemIcon;                   // ������ ������
    [SerializeField] private Image _gradeFrame;                 // ��� ������
    [SerializeField] private TextMeshProUGUI _levelText;        // ������ ���� �ؽ�Ʈ

    [Title("����� ������Ʈ (��� ������Ʈ��)", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;  // ����� ������Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemInven.OnItemInvenChanged += UpdateReddotComponent; // ������ �ִ� �������� ����Ǿ��� ��, �������� ����� ������Ʈ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemInven.OnItemInvenChanged -= UpdateReddotComponent;
    }

    /// <summary>
    /// ���� �����ֱ�
    /// </summary>
    public void ShowInfoGO(IItem item)
    {
        _itemIcon.sprite = item.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);
        _levelText.text = $"Lv.{item.Level}";

        _emptyGO.SetActive(false);
        _infoGO.SetActive(true); // ���� �ѱ�

        UpdateReddotComponent(); // ����� ������Ʈ
    }

    /// <summary>
    /// �󽽷� �����ֱ�
    /// </summary>
    public void ShowEmptyGO()
    {
        _emptyGO.SetActive(true);
        _infoGO.SetActive(false); // ���� ����

        UpdateReddotComponent(); // ����� ������Ʈ
    }

    /// <summary>
    /// ����� ������Ʈ ������Ʈ
    /// </summary>
    public void UpdateReddotComponent()
    {
        switch (_itemType)
        {
            case ItemType.Weapon: // ������ �κ��丮�� ��ȭ������ �������� �ִ���? ����� ������Ʈ
            case ItemType.Armor:
            case ItemType.Helmet:
                _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_itemType)); 
                break;
            case ItemType.Skill: // ��ų�� �׳� ����� �����
                _reddotComponent.Hide();
                break;
        }
    }
}
