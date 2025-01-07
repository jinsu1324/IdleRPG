using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPopup : BottomTabPopupBase
{
    [SerializeField] EquipSlotSkill[] _equipSlotArr = new EquipSlotSkill[3];  // ���� ���� �迭 (3 : ���������� ��ų ����)
    [SerializeField] ItemSlotManager _itemSlotManager;              // ������ ���� �Ŵ���
    [SerializeField] SelectItemInfoUI _selectItemInfoUI;            // ���õ� ������ ���� UI
    
    private ItemType _itemType = ItemType.Skill;                    // ��ų�˾��� ������Ÿ�� ����

   
    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public override void Show()
    {
        _itemSlotManager.Init(_itemType);
        _selectItemInfoUI.Hide();
        Update_EquipSlots();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �������� ������Ʈ
    /// </summary>
    private void Update_EquipSlots()
    {
        // ���� ��ų �迭 ��������
        Skill[] equipSkillArr = EquipSkillManager.GetEquippedSkillArr();

        // ������ų �ƹ��͵� ������, ���� �� ���� ����
        if (equipSkillArr == null)
        {
            for (int i = 0; i < _equipSlotArr.Length; i++)
                _equipSlotArr[i].ShowEmptyGO();

            return;
        }

        // ������ ��ų ������ �°� �������� ������Ʈ
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
