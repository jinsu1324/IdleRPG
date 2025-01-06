using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPopup : BottomTabPopupBase
{
    [SerializeField] List<EquipSlot> _equipSlotList;        // ���� ���� ����Ʈ
    [SerializeField] ItemSlotManager _itemSlotManager;      // ������ ���� �Ŵ���
    [SerializeField] SelectItemInfoUI _selectItemInfoUI;    // ���õ� ������ ���� UI
    
    private ItemType _itemType = ItemType.Skill;            // ��ų�˾��� ������Ÿ�� ����

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemSlot.OnSlotSelected += TurnOn_SelectItemInfoUI; // ������ ���� ���õǾ��� ��, ���õ� ������ ���� UI �ѱ�
        EquipSkillManager.OnEquipSkillChanged += Update_EquipSlots; // ���� ��ų�� ����Ǿ��� ��, ��ų�˾��� ���� ���Ե鵵 ������Ʈ
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
        // ���� ��ų ����Ʈ ��������
        List<Skill> equipSkillList = EquipSkillManager.GetEquippedSkillList();

        // ������ų �ƹ��͵� ������, ���� �� ���� ����
        if (equipSkillList == null)
        {
            foreach (EquipSlot equipSlot in _equipSlotList)
                equipSlot.ShowEmptyGO();

            Debug.Log("������ų �ƹ��͵� ������, ���� �� ���� ����");
            return;
        }

        // ������ ��ų ������ �°� �������� ������Ʈ
        for (int i = 0; i < _equipSlotList.Count; i++)
        {
            EquipSlot equipSlot = _equipSlotList[i];
            

            Debug.Log($"{i} ��° ���� �۾�  / {equipSkillList.Count} ��������");

            if (i < equipSkillList.Count)
            {
                Skill skill = equipSkillList[i];
                equipSlot.ShowInfoGO(skill);
                Debug.Log($"{i} ��° ���� Show");

            }
            else 
            {
                equipSlot.ShowEmptyGO();
                Debug.Log($"{i} ��° ���� Hide");

            }
        }
    }

    /// <summary>
    /// ���õ� ������ ���� UI �ѱ�
    /// </summary>
    private void TurnOn_SelectItemInfoUI(ItemSlot selectSlot)
    {
        _selectItemInfoUI.Show(selectSlot);
    }
}
