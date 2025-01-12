using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReddotComponent))]
public class EquipSlot_Skill : EquipSlot
{
    [SerializeField] protected int _slotIndex;                  // 슬롯 인덱스
    [SerializeField] private ReddotComponent _reddotComponent;  // 레드닷 컴포넌트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_EquipSlotSkill; // 장착스킬 바뀌었을때 -> 스킬장착슬롯 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipSkillManager.OnEquipSkillChanged -= Update_EquipSlotSkill;

    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        Update_EquipSlotSkill();
    }

    /// <summary>
    /// 스킬장착슬롯 업데이트
    /// </summary>
    private void Update_EquipSlotSkill(Item item = null)
    {
        // 해당 인덱스에 장착한 스킬 가져오기
        Item equipSkill = EquipSkillManager.GetEquipSkill(_slotIndex);

        // 장착한 스킬 있으면 슬롯 보여주거(+업데이트), 없으면 비우기
        if (equipSkill != null)
            UpdateSlot(equipSkill);
        else
            EmptySlot();

        // 레드닷 업데이트
        Update_ReddotComponent();
    }

    /// <summary>
    /// 레드닷 컴포넌트 업데이트 (인벤토리에 강화가능한 아이템이 있는지?)
    /// </summary>
    public void Update_ReddotComponent()
    {
        _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_slotItemType));
    }
}
