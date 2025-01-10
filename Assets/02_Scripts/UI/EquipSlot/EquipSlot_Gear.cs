using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReddotComponent))]
public class EquipSlot_Gear : EquipSlot
{
    public static event Action<ItemType> OnClickGearInvenOpenButton;

    [Title("��� �������� ����", bold: false)]
    [SerializeField] private Button _invenOpenButton;           // �κ��丮 ���� ��ư
    [SerializeField] private ReddotComponent _reddotComponent;  // ����� ������Ʈ


    /// <summary>
    /// OnEnable
    /// </summary>
    protected override void OnEnable()
    {
        ItemInven.OnItemInvenChanged += UpdateReddotComponent; // ������ �ִ� �������� ����Ǿ��� ��, ����������� ����� ������Ʈ
        EquipItemManager.OnEquipGearChanged += TryUpdate_EquipSlotGear; // ������ ��� ����Ǿ��� ��, ����������� ������Ʈ �õ�

        _invenOpenButton.onClick.AddListener(Notify_OnClickGearInvenOpenButton);   
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    protected override void OnDisable()
    {
        ItemInven.OnItemInvenChanged -= UpdateReddotComponent;
        EquipItemManager.OnEquipGearChanged -= TryUpdate_EquipSlotGear;

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
        // ������ ������ ��������
        Item equippedItem = EquipItemManager.GetEquippedItem(_slotItemType, _slotIndex);

        // ������ ��� ������ ���� �����ְ�, ������ �󽽷� �����ֱ�
        if (equippedItem != null)
            ShowInfo(equippedItem);
        else
            ShowEmpty();

        UpdateReddotComponent(); // ����� ������Ʈ
    }


    private void Notify_OnClickGearInvenOpenButton()
    {
        OnClickGearInvenOpenButton?.Invoke(_slotItemType);
    }

    

    /// <summary>
    /// ����� ������Ʈ ������Ʈ (�κ��丮�� ��ȭ������ �������� �ִ���?)
    /// </summary>
    public void UpdateReddotComponent()
    {
        _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_slotItemType));
    }
}
