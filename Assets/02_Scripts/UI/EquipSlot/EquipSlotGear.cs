using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReddotComponent))]
public class EquipSlotGear : EquipSlotBase
{
    [Title("��� �������� ����", bold: false)]
    [SerializeField] private GearInvenPopup _gearInvenPopup;    // ��� �κ��丮 �˾�
    [SerializeField] private Button _invenOpenButton;           // �κ��丮 ���� ��ư
    [SerializeField] private ReddotComponent _reddotComponent;  // ����� ������Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    protected override void OnEnable()
    {
        ItemInven.OnItemInvenChanged += UpdateReddotComponent; // ������ �ִ� �������� ����Ǿ��� ��, ����������� ����� ������Ʈ
        EquipGearManager.OnEquipGearChanged += TryUpdate_EquipSlotGear; // ������ ��� ����Ǿ��� ��, ����������� ������Ʈ �õ�

        _invenOpenButton.onClick.AddListener(ShowInvenPopup);   // �κ����� ��ư Ŭ���ϸ�, �κ� ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    protected override void OnDisable()
    {
        ItemInven.OnItemInvenChanged -= UpdateReddotComponent;
        EquipGearManager.OnEquipGearChanged -= TryUpdate_EquipSlotGear;

        _invenOpenButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// ��� ���� ���� ������Ʈ �õ�
    /// </summary>
    private void TryUpdate_EquipSlotGear(OnEquipGearChangedArgs args)
    {
        if (IsSameSlotItemType(args.ItemType) == false) 
            return;

        UpdateSlot();
    }

    /// <summary>
    /// ���� or �������� �������� ������Ÿ����, ���� ������ ������Ÿ�԰� ������?
    /// </summary>
    private bool IsSameSlotItemType(ItemType itemType)
    {
        return _slotItemType == itemType;
    }

    /// <summary>
    /// ���� ������Ʈ
    /// </summary>
    public void UpdateSlot()
    {
        // ������ ��� ��������
        Gear gear = EquipGearManager.GetEquipGear(_slotItemType);

        // ������ ��� ������ ���� �����ְ�, ������ �󽽷� �����ֱ�
        if (gear != null)
            ShowInfo(gear);
        else
            ShowEmpty();

        UpdateReddotComponent(); // ����� ������Ʈ
        HideInvenPopup(); // �κ��˾� ����
    }

    /// <summary>
    /// �κ��˾� ����
    /// </summary>
    private void ShowInvenPopup()
    {
        _gearInvenPopup.Show(_slotItemType);
    }

    /// <summary>
    /// �κ��˾� ����
    /// </summary>
    private void HideInvenPopup()
    {
        _gearInvenPopup.Hide();
    } 

    /// <summary>
    /// ����� ������Ʈ ������Ʈ (�κ��丮�� ��ȭ������ �������� �ִ���?)
    /// </summary>
    public void UpdateReddotComponent()
    {
        _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_slotItemType));
    }
}
