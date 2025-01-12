using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų �˾�
/// </summary>
public class SkillPopup : BottomTabPopupBase
{
    [SerializeField] EquipSlot_Skill[] _equipSlotArr = new EquipSlot_Skill[3];      // ���� ���� �迭 (3 : ���������� ��ų ����)
    [SerializeField] ItemSlotManager _itemSlotManager;                              // ������ ���� �Ŵ���
    [SerializeField] ItemDetailUI_Skill _itemDetailUI_Skill;                        // ��ų������ ������ UI
    private ItemType _itemType = ItemType.Skill;                                    // ��ų�˾��� ������Ÿ�� ����
    
    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemSlot.OnSlotSelected += _itemDetailUI_Skill.Show; // ������ ���� ���õǾ��� ��, ��ų ������ ������UI ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemSlot.OnSlotSelected -= _itemDetailUI_Skill.Show;

    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public override void Show()
    {
        _itemSlotManager.Init(_itemType); // �����۽��� �ʱ�ȭ
        _itemDetailUI_Skill.Hide(); // ������ ������ UI(���⼭�� �˾�) �� ������� 
        Update_EquipSlots(); // �������� ������Ʈ

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
        SkillItem[] equipSkillArr = EquipSkillManager.GetEquippedSkillArr();

        // ������ų �ƹ��͵� ������, ���� �� ���� ����
        if (equipSkillArr == null)
        {
            for (int i = 0; i < _equipSlotArr.Length; i++)
                _equipSlotArr[i].EmptySlot();

            return;
        }

        // ������ ��ų ������ �°� �������� ������Ʈ
        for (int i = 0; i < _equipSlotArr.Length; i++)
        {
            EquipSlot_Skill equipSlotSkill = _equipSlotArr[i];
            SkillItem skill = equipSkillArr[i];

            if (skill == null)
            {
                equipSlotSkill.EmptySlot();
                continue;
            }

            equipSlotSkill.ShowSlot(skill);
        }
    }
}
