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
        ItemSlot.OnSlotSelected += _itemDetailUI_Skill.Show; // ������ ���� ���õǾ��� �� -> ��ų ������ ������UI ����
        EquipSkillManager.OnEquipSkillChanged += _itemDetailUI_Skill.Hide;  // ������ų �ٲ���� �� -> ��ų ������ ������UI �ݱ�
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemSlot.OnSlotSelected -= _itemDetailUI_Skill.Show;
        EquipSkillManager.OnEquipSkillChanged -= _itemDetailUI_Skill.Hide;
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public override void Show()
    {
        _itemSlotManager.Init(_itemType); // �����۽��� �ʱ�ȭ
        _itemDetailUI_Skill.Hide(); // ������ ������ UI(���⼭�� �˾�) �� ������� 
        Init_SkillEquipSlots(); // �������� ������Ʈ

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
    /// ��ų�������Ե� �ʱ�ȭ
    /// </summary>
    private void Init_SkillEquipSlots()
    {
        foreach (EquipSlot_Skill equipSlotSkill in _equipSlotArr)
            equipSlotSkill.Init();
    }
}
