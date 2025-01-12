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
        EquipGearManager.OnEquipGear += Update_EquipSlotGear; // ��� �����Ҷ� -> ����������� ������Ʈ
        EquipGearManager.OnUnEquipGear += Update_EquipSlotGear; // ��� �����Ҷ� -> ����������� ������Ʈ
        ItemEnhanceManager.OnItemEnhance += Update_EquipSlotGear; // ������ ��ȭ�Ҷ� -> ����������� ������Ʈ

        _gearInvenButton.onClick.AddListener(Notify_OnClickGearInvenButton); // ����κ���ư ������ -> �̺�Ʈ ��Ƽ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipGearManager.OnEquipGear -= Update_EquipSlotGear;
        EquipGearManager.OnUnEquipGear -= Update_EquipSlotGear;
        ItemEnhanceManager.OnItemEnhance -= Update_EquipSlotGear;

        _gearInvenButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        Choice_ShowAndEmpty_ByEquipped();
        Update_ReddotComponent();
    }

    /// <summary>
    /// ���� ������Ʈ
    /// </summary>
    private void Update_EquipSlotGear(Item item)
    {
        // �� �����ϰ� ������Ÿ���� �ٸ��� ����
        if (_slotItemType != item.ItemType) 
            return;

        Choice_ShowAndEmpty_ByEquipped();
        Update_ReddotComponent();
    }

    /// <summary>
    /// ��� �������ο� ����, �����ֱ� vs �Ⱥ����ֱ� ����
    /// </summary>
    private void Choice_ShowAndEmpty_ByEquipped()
    {
        // ������ ������ ��������
        Item equippedItem = EquipGearManager.GetEquippedItem(_slotItemType);

        // ������ ������ ������ ���� �����ְ�(+������Ʈ), ������ ����
        if (equippedItem != null)
            UpdateSlot(equippedItem);
        else
            EmptySlot();
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
