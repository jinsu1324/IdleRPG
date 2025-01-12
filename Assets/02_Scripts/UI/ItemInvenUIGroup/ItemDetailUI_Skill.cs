using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ų������ ������ UI
/// </summary>
public class ItemDetailUI_Skill : ItemDetailUI
{
    [Title("��ų������ ������ UI", bold: false)]
    [SerializeField] private TextMeshProUGUI _levelText;                // ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // ��ȭ ������ ������ ���� �ؽ�Ʈ
    [SerializeField] private Slider _countSlider;                       // ���� ǥ�� �����̴�
    [SerializeField] private GameObject _equipIcon;                     // ���� ������
    [SerializeField] private GameObject _enhanceableArrowIcon;          // ��ȭ���� ������

    [Title("��ư��", bold: false)]
    [SerializeField] private Button _equipButton;                       // ���� ��ư
    [SerializeField] private Button _unEquipButton;                     // ���� ���� ��ư
    [SerializeField] private Button _enhanceButton;                     // ��ȭ ��ư

    [Title("������ ��ư", bold: false)]
    [SerializeField] private Button _exitButton;                        // ������ ��ư

    /// <summary>
    /// OnEnable
    /// </summary>
    protected override void OnEnable()
    {
        _equipButton.onClick.AddListener(OnClick_EquipButton);      // ������ư �ڵ鷯 ���
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);  // ����������ư �ڵ鷯 ���
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);  // ��ȭ��ư �ڵ鷯 ���
        _exitButton.onClick.AddListener(Hide);                      // ������ ��ư �ڵ鷯 ���
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    protected override void OnDisable()
    {
        _equipButton.onClick.RemoveAllListeners();
        _unEquipButton.onClick.RemoveAllListeners();
        _enhanceButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    protected override void UpdateUI(Item item)
    {
        base.UpdateUI(item);

        if (item is IEnhanceableItem enhanceableItem)
        {
            _levelText.text = $"Lv.{enhanceableItem.Level}";
            _countText.text = $"{item.Count}";
            _enhanceableCountText.text = $"{enhanceableItem.EnhanceableCount}";
            _countSlider.value = (float)item.Count / (float)enhanceableItem.EnhanceableCount;
            _enhanceableArrowIcon.gameObject.SetActive(enhanceableItem.CanEnhance());
            _enhanceButton.gameObject.SetActive(enhanceableItem.CanEnhance());
        }

        bool isEquipped = EquipGearManager.IsEquipped(item);
        _equipButton.gameObject.SetActive(!isEquipped);
        _unEquipButton.gameObject.SetActive(isEquipped);
        _equipIcon.SetActive(isEquipped);
    }

    /// <summary>
    /// ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_EquipButton()
    {
        //if (CurrentItem == null)
        //    return;

        //EquipGearManager.Equip(CurrentItem, 0); // ��ų�� ���������� ���°� �ʿ��ҵ�
        //UpdateUI();
    }

    /// <summary>
    /// ���� ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_UnEquipButton()
    {
        //if (CurrentItem == null)
        //    return;

        //EquipGearManager.UnEquipGear(CurrentItem);
        //UpdateUI();
    }

    /// <summary>
    /// ��ȭ ��ư Ŭ��
    /// </summary>
    public void OnClick_EnhanceButton()
    {
        //if (CurrentItem == null)
        //    return;

        //ItemEnhanceManager.Enhance(CurrentItem);
        //UpdateUI();
    }
}
