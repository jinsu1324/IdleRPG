using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReddotComponent))]
public class EquipSlot_Skill : EquipSlot
{
    [SerializeField] protected int _slotIndex;                  // ���� �ε���
    [SerializeField] private ReddotComponent _reddotComponent;  // ����� ������Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_EquipSlotSkill; // ������ų �ٲ������ -> ��ų�������� ������Ʈ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipSkillManager.OnEquipSkillChanged -= Update_EquipSlotSkill;

    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        Update_EquipSlotSkill();
    }

    /// <summary>
    /// ��ų�������� ������Ʈ
    /// </summary>
    private void Update_EquipSlotSkill(Item item = null)
    {
        // �ش� �ε����� ������ ��ų ��������
        Item equipSkill = EquipSkillManager.GetEquipSkill(_slotIndex);

        // ������ ��ų ������ ���� �����ְ�(+������Ʈ), ������ ����
        if (equipSkill != null)
            UpdateSlot(equipSkill);
        else
            EmptySlot();

        // ����� ������Ʈ
        Update_ReddotComponent();
    }

    /// <summary>
    /// ����� ������Ʈ ������Ʈ (�κ��丮�� ��ȭ������ �������� �ִ���?)
    /// </summary>
    public void Update_ReddotComponent()
    {
        _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_slotItemType));
    }
}
