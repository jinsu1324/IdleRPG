using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스킬 팝업
/// </summary>
public class SkillPopup : BottomTabPopupBase
{
    [SerializeField] EquipSlot_Skill[] _equipSlotArr = new EquipSlot_Skill[3];      // 장착 슬롯 배열 (3 : 장착가능한 스킬 갯수)
    [SerializeField] ItemSlotManager _itemSlotManager;                              // 아이템 슬롯 매니저
    [SerializeField] ItemDetailUI_Skill _itemDetailUI_Skill;                        // 스킬아이템 상세정보 UI
    private ItemType _itemType = ItemType.Skill;                                    // 스킬팝업의 아이템타입 설정
    
    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemSlot.OnSlotSelected += _itemDetailUI_Skill.Show; // 아이템 슬롯 선택되었을 때 -> 스킬 아이템 상세정보UI 열기
        EquipSlot_Skill.OnClickItemDetailButton += _itemDetailUI_Skill.Show; // 스킬장착슬롯 아이템 디테일버튼 눌렀을때 -> 스킬 아이템 상세정보UI 열기
        EquipSkillManager.OnEquipSkillChanged += _itemDetailUI_Skill.Hide;  // 장착스킬 바뀌었을 때 -> 스킬 아이템 상세정보UI 닫기
        ItemEnhanceManager.OnItemEnhance += _itemDetailUI_Skill.Hide;  // 아이템 강화할때 -> 스킬 아이템 상세정보UI 닫기
        EquipSkillManager.OnSkillSwapStarted += _itemDetailUI_Skill.Hide; // 장착스킬교체 시작할 때 -> 스킬 아이템 상세정보UI 닫기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemSlot.OnSlotSelected -= _itemDetailUI_Skill.Show;
        EquipSlot_Skill.OnClickItemDetailButton -= _itemDetailUI_Skill.Show;
        EquipSkillManager.OnEquipSkillChanged -= _itemDetailUI_Skill.Hide;
        ItemEnhanceManager.OnItemEnhance -= _itemDetailUI_Skill.Hide;
        EquipSkillManager.OnSkillSwapStarted -= _itemDetailUI_Skill.Hide;
    }

    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public override void Show()
    {
        _itemSlotManager.Init(_itemType); // 아이템슬롯 초기화
        _itemDetailUI_Skill.Hide(); // 아이테 디테일 UI(여기서는 팝업) 은 끄고시작 
        Init_SkillEquipSlots(); // 장착슬롯 업데이트

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 팝업 끄기
    /// </summary>
    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 스킬장착슬롯들 초기화
    /// </summary>
    private void Init_SkillEquipSlots()
    {
        foreach (EquipSlot_Skill equipSlotSkill in _equipSlotArr)
            equipSlotSkill.Init();
    }
}
