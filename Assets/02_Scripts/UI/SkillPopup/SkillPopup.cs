using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스킬 팝업
/// </summary>
public class SkillPopup : BottomTabPopupBase
{
    [SerializeField] EquipSlot_Skill[] _equipSlotArr = new EquipSlot_Skill[3];    // 장착 슬롯 배열 (3 : 장착가능한 스킬 갯수)
    [SerializeField] ItemSlotManager _itemSlotManager;                          // 아이템 슬롯 매니저
    [SerializeField] ItemDetailUI _selectItemInfoUI;                        // 선택된 아이템 정보 UI
    private ItemType _itemType = ItemType.Skill;                                // 스킬팝업의 아이템타입 설정
   
    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public override void Show()
    {
        _itemSlotManager.Init(_itemType); // 아이템슬롯 초기화
        _selectItemInfoUI.Hide(); // 아이테 디테일 UI(여기서는 팝업) 은 끄고시작 
        Update_EquipSlots(); // 장착슬롯 업데이트

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
    /// 장착슬롯 업데이트
    /// </summary>
    private void Update_EquipSlots()
    {
        // 장착 스킬 배열 가져오기
        SkillItem[] equipSkillArr = EquipSkillManager.GetEquippedSkillArr();

        // 장착스킬 아무것도 없으면, 슬롯 다 끄고 리턴
        if (equipSkillArr == null)
        {
            for (int i = 0; i < _equipSlotArr.Length; i++)
                _equipSlotArr[i].ShowEmpty();

            return;
        }

        // 장착한 스킬 갯수에 맞게 장착슬롯 업데이트
        for (int i = 0; i < _equipSlotArr.Length; i++)
        {
            EquipSlot_Skill equipSlotSkill = _equipSlotArr[i];
            SkillItem skill = equipSkillArr[i];

            if (skill == null)
            {
                equipSlotSkill.ShowEmpty();
                continue;
            }

            equipSlotSkill.ShowInfo(skill);
        }
    }
}
