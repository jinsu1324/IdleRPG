using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReddotComponent))]
public class EquipSlot_Gear : EquipSlot
{
    public static event Action<ItemType> OnClickGearInvenButton;    // ����κ���ư ������ �� �̺�Ʈ

    [SerializeField] private Button _gearInvenButton;               // ����κ���ư
    [SerializeField] private ReddotComponent _reddotComponent;      // ����� ������Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGear += TryUpdate_EquipSlotGear; // ��� �����Ҷ� -> ����������� ������Ʈ
        EquipGearManager.OnUnEquipGear += TryUpdate_EquipSlotGear; // ��� �����Ҷ� -> ����������� ������Ʈ
        ItemEnhanceManager.OnItemEnhance += TryUpdate_EquipSlotGear; // ������ ��ȭ�Ҷ� -> ����������� ������Ʈ

        _gearInvenButton.onClick.AddListener(Notify_OnClickGearInvenButton); // ����κ���ư ������ -> �̺�Ʈ ��Ƽ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipGearManager.OnEquipGear -= TryUpdate_EquipSlotGear;
        EquipGearManager.OnUnEquipGear -= TryUpdate_EquipSlotGear;
        ItemEnhanceManager.OnItemEnhance -= TryUpdate_EquipSlotGear;

        _gearInvenButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        Update_EquipSlotGear();
    }

    /// <summary>
    /// ����������� ������Ʈ
    /// </summary>
    private void TryUpdate_EquipSlotGear(Item item)
    {
        // �� �����ϰ� ������Ÿ���� �ٸ��� ����
        if (_slotItemType != item.ItemType) 
            return;

        Update_EquipSlotGear();
        
    }

    /// <summary>
    /// ���� ����������� ������Ʈ ����
    /// </summary>
    private void Update_EquipSlotGear()
    {
        // ������ ��� ��������
        Item equipGear = EquipGearManager.GetEquippedItem(_slotItemType);

        // ������ ��� ������ ���� �����ְ�(+������Ʈ), ������ ����
        if (equipGear != null)
            UpdateSlot(equipGear);
        else
            EmptySlot();

        // ����� ������Ʈ
        Update_ReddotComponent();
    }

    /// <summary>
    /// ����κ���ư Ŭ�� �̺�Ʈ ��Ƽ
    /// </summary>
    private void Notify_OnClickGearInvenButton()
    {
        OnClickGearInvenButton?.Invoke(_slotItemType);
    }

    /// <summary>
    /// ����� ������Ʈ ������Ʈ (�κ��丮�� ��ȭ������ �������� �ִ���?)
    /// </summary>
    public void Update_ReddotComponent()
    {
        _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_slotItemType));
    }
}
