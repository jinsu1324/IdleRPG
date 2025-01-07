using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPopup : BottomTabPopupBase
{
    [SerializeField] EquipSlotSkill[] _equipSlotArr = new EquipSlotSkill[3];  // 장착 슬롯 배열 (3 : 장착가능한 스킬 갯수)
    [SerializeField] ItemSlotManager _itemSlotManager;              // 아이템 슬롯 매니저
    [SerializeField] SelectItemInfoUI _selectItemInfoUI;            // 선택된 아이템 정보 UI
    
    private ItemType _itemType = ItemType.Skill;                    // 스킬팝업의 아이템타입 설정

   
    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public override void Show()
    {
        _itemSlotManager.Init(_itemType);
        _selectItemInfoUI.Hide();
        Update_EquipSlots();

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
        Skill[] equipSkillArr = EquipSkillManager.GetEquippedSkillArr();

        // 장착스킬 아무것도 없으면, 슬롯 다 끄고 리턴
        if (equipSkillArr == null)
        {
            for (int i = 0; i < _equipSlotArr.Length; i++)
                _equipSlotArr[i].ShowEmptyGO();

            return;
        }

        // 장착한 스킬 갯수에 맞게 장착슬롯 업데이트
        for (int i = 0; i < _equipSlotArr.Length; i++)
        {
            EquipSlotSkill equipSlotSkill = _equipSlotArr[i];
            Skill skill = equipSkillArr[i];

            if (skill == null)
            {
                equipSlotSkill.ShowEmptyGO();
                continue;
            }

            equipSlotSkill.ShowInfoGO(skill);
        }
    }

}
