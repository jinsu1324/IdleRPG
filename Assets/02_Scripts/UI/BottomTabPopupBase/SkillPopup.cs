using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPopup : BottomTabPopupBase
{
    [SerializeField] List<EquipSlot> _equipSlotList;        // 장착 슬롯 리스트
    [SerializeField] ItemSlotManager _itemSlotManager;      // 아이템 슬롯 매니저
    [SerializeField] SelectItemInfoUI _selectItemInfoUI;    // 선택된 아이템 정보 UI
    
    private ItemType _itemType = ItemType.Skill;            // 스킬팝업의 아이템타입 설정

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemSlot.OnSlotSelected += TurnOn_SelectItemInfoUI; // 아이템 슬롯 선택되었을 때, 선택된 아이템 정보 UI 켜기
        EquipSkillManager.OnEquipSkillChanged += Update_EquipSlots; // 장착 스킬이 변경되었을 때, 스킬팝업의 장착 슬롯들도 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemSlot.OnSlotSelected -= TurnOn_SelectItemInfoUI;
        EquipSkillManager.OnEquipSkillChanged -= Update_EquipSlots;
    }
   
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
        // 장착 스킬 리스트 가져오기
        List<Skill> equipSkillList = EquipSkillManager.GetEquippedSkillList();

        // 장착스킬 아무것도 없으면, 슬롯 다 끄고 리턴
        if (equipSkillList == null)
        {
            foreach (EquipSlot equipSlot in _equipSlotList)
                equipSlot.ShowEmptyGO();

            Debug.Log("장착스킬 아무것도 없으면, 슬롯 다 끄고 리턴");
            return;
        }

        // 장착한 스킬 갯수에 맞게 장착슬롯 업데이트
        for (int i = 0; i < _equipSlotList.Count; i++)
        {
            EquipSlot equipSlot = _equipSlotList[i];
            

            Debug.Log($"{i} 번째 슬롯 작업  / {equipSkillList.Count} 장착갯수");

            if (i < equipSkillList.Count)
            {
                Skill skill = equipSkillList[i];
                equipSlot.ShowInfoGO(skill);
                Debug.Log($"{i} 번째 슬롯 Show");

            }
            else 
            {
                equipSlot.ShowEmptyGO();
                Debug.Log($"{i} 번째 슬롯 Hide");

            }
        }
    }

    /// <summary>
    /// 선택된 아이템 정보 UI 켜기
    /// </summary>
    private void TurnOn_SelectItemInfoUI(ItemSlot selectSlot)
    {
        _selectItemInfoUI.Show(selectSlot);
    }
}
