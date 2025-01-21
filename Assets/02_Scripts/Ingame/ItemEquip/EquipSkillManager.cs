using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ������ ��ų ����
/// </summary>
public class EquipSkillManager
{
    public static event Action<Item> OnEquipSkillChanged;   // ������ų �ٲ������ �̺�Ʈ
    public static event Action OnSkillSwapStarted;          // ������ų��ü ������ �� �̺�Ʈ
    public static event Action OnSkillSwapFinished;         // ������ų��ü ������ �� �̺�Ʈ

    private static Item[] _equipSkillArr;                   // ������ ��ų �迭
    private static int _maxCount = 3;                       // ��ų ���� �ִ� ����

    private static Item _swapTargetItem;                    // ���� Ÿ�� ������

    /// <summary>
    /// ���� ������ (Ŭ������ ó�� ������ �� �� ���� ȣ��)
    /// </summary>
    static EquipSkillManager()
    {
        _equipSkillArr = new Item[_maxCount];
    }
    
    /// <summary>
    /// ��ų ����
    /// </summary>
    public static void Equip(Item item, int slotIndex = -1)
    {
        // �̹� �� �������� �������̸� �׳� ����
        if (IsEquipped(item))
            return;

        // �̹� �ִ�� ���������� ��ü����
        if (IsEquipMax())
        {
            _swapTargetItem = item;
            OnSkillSwapStarted?.Invoke(); // ���� ���� �˸�

            return;
        }

        // ���� ���� ���ߴٸ�, ����ִ� ���� �ε��� �ƹ��ų� ��������
        if (slotIndex == -1)
            slotIndex = GetEmptySlotIndex();

        // ����
        _equipSkillArr[slotIndex] = item;

        // ������ų �ٲ������ �̺�Ʈ ��Ƽ
        OnEquipSkillChanged?.Invoke(item);
    }

    /// <summary>
    /// ��ų ���� ����
    /// </summary>
    public static void UnEquip(Item item)
    {
        // �ش� �������� �����Ǿ��ִ� �����ε��� ��������
        int index = GetEquippedIndex(item); 

        // �ε��� ��ã������ �׳� ����
        if (index < 0)
            return;
        
        // ���� ����
        _equipSkillArr[index] = null;

        // ������ų �ٲ������ �̺�Ʈ ��Ƽ
        OnEquipSkillChanged?.Invoke(item);
    }

    /// <summary>
    /// ��ų ��ü
    /// </summary>
    public static void Swap(Item equipSlotItem)
    {
        UnEquip(equipSlotItem); // �������Բ� ���� ����
        Equip(_swapTargetItem); // ���ο� ������ ����

        OnSkillSwapFinished?.Invoke();
    }

    /// <summary>
    /// ������ ����������?
    /// </summary>
    public static bool IsEquipped(Item item)
    {
        return _equipSkillArr.Contains(item);
    }

    /// <summary>
    /// �ִ�� �����ߴ���?
    /// </summary>
    private static bool IsEquipMax()
    {
        return GetEmptySlotIndex() == -1;   // ����ִ� ������ �ϳ��� ������ �ִ�������
    }

    /// <summary>
    /// �ش� �������� �����Ǿ��ִ� �����ε��� ��������
    /// </summary>
    private static int GetEquippedIndex(Item item)
    {
        return Array.FindIndex(_equipSkillArr, s => s == item);
    }

    /// <summary>
    /// ����ִ� ���� �ε��� ��������
    /// </summary>
    private static int GetEmptySlotIndex()
    {
        return Array.FindIndex(_equipSkillArr, s => s == null);
    }

    /// <summary>
    /// �ش� ���Կ� �����Ǿ��ִ� ��ų������ ��������
    /// </summary>
    public static Item GetEquipSkill(int slotIndex)
    {
        return _equipSkillArr[slotIndex];
    }

    /// <summary>
    /// ������ ��ų�����۵� ��������(������ Ÿ��)
    /// </summary>
    public static Item[] GetEquipSkills_ItemType()
    {
        if (_equipSkillArr.All(s => s == null)) // ��� ������ ����ִٸ�(null) null ��ȯ
            return null;

        return _equipSkillArr;
    }

    /// <summary>
    /// ������ ��ų�� �������� (����ϱ� ���� '��ų'���·� ��ȯ�ؼ�)
    /// </summary>
    public static Skill[] GetEquipSkills_SkillType()
    {
        Skill[] castSkillArr = new Skill[_maxCount];

        for (int i = 0; i < _equipSkillArr.Length; i++)
        {
            Item equipItem = _equipSkillArr[i];

            if (equipItem == null)
            {
                castSkillArr[i] = null;
                continue;
            }

            castSkillArr[i] = SkillFactory.CreateSkill(equipItem.ID, equipItem.Level);
        }

        return castSkillArr;
    }
}
