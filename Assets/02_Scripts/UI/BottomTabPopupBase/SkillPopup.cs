using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPopup : BottomTabPopupBase
{
    [SerializeField] EquipSlot[] _equipSlotArr = new EquipSlot[3];  // ���� ���� �迭 (3 : ���������� ��ų ����)
    [SerializeField] ItemSlotManager _itemSlotManager;              // ������ ���� �Ŵ���
    [SerializeField] SelectItemInfoUI _selectItemInfoUI;            // ���õ� ������ ���� UI
    
    private ItemType _itemType = ItemType.Skill;                    // ��ų�˾��� ������Ÿ�� ����

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_EquipSlots; // ���� ��ų�� ����Ǿ��� ��, ��ų�˾��� ���� ���Ե鵵 ������Ʈ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipSkillManager.OnEquipSkillChanged -= Update_EquipSlots;
    }
   
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
            foreach (EquipSlot equipSlot in _equipSlotArr)
                equipSlot.ShowEmptyGO();

            return;
        }

        // ������ ��ų ������ �°� �������� ������Ʈ
        for (int i = 0; i < _equipSlotArr.Length; i++)
        {
            EquipSlot equipSlot = _equipSlotArr[i];
            Skill skill = equipSkillArr[i];

            if (skill == null)
            {
                equipSlot.ShowEmptyGO(i);
                continue;
            }

            equipSlot.ShowInfoGO(skill, i);
        }
    }

}
