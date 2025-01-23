using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��ų ���� ����
/// </summary>
public enum SkillSlot
{
    None,   // ���� ���� ǥ�ÿ�
    First,  // ù��° ����
    Second, // �ι�° ����
    Third,  // ����° ����
}

/// <summary>
/// ������ ��ų ����
/// </summary>
public class EquipSkillManager : ISavable
{
    public static event Action<Item> OnEquipSkillChanged;   // ������ų �ٲ������ �̺�Ʈ
    public static event Action OnSkillSwapStarted;          // ������ų��ü ������ �� �̺�Ʈ
    public static event Action OnSkillSwapFinished;         // ������ų��ü ������ �� �̺�Ʈ
    
    private static int _maxCount = 3;                       // ��ų �������� ����
    private static Item _swapTargetItem;                    // ���� Ÿ�� ������
    public string Key => nameof(EquipSkillManager);         // Firebase ������ ����� ���� Ű ����
    [SaveField] private static Dictionary<SkillSlot, Item> _equipSkillDict = new Dictionary<SkillSlot, Item>(); // ���� ��ų ��ųʸ�

    /// <summary>
    /// ������ �ҷ������Ҷ� �½�ũ��
    /// </summary>
    public void DataLoadTask()
    {
        OnEquipSkillChanged?.Invoke(new Item("Weapon_Sword", "Weapon", 1, 1)); // �Ű������� �ӽõ������Դϴ�. args�� ��ü������ �����ؾ���
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public static void Equip(Item item)
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

        // ����ִ� ���� �ε��� �ƹ��ų� ��������
        SkillSlot skillSlot = GetEmptySlot();
        
        // ����
        _equipSkillDict[skillSlot] = item;

        // ������ų �ٲ������ �̺�Ʈ ��Ƽ
        OnEquipSkillChanged?.Invoke(item);
    }

    /// <summary>
    /// ��ų ���� ����
    /// </summary>
    public static void UnEquip(Item item)
    {
        // �ش� �������� �����Ǿ��ִ� ���� ��������
        SkillSlot skillSlot = GetEquippedIndex(item); 

        // �ε��� ��ã������ �׳� ����
        if (skillSlot == SkillSlot.None)
            return;

        // ���� ����
        _equipSkillDict[skillSlot] = null;

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
        return _equipSkillDict.Values.Any(x => x != null && x.ID == item.ID);
    }

    /// <summary>
    /// �ִ�� �����ߴ���?
    /// </summary>
    private static bool IsEquipMax()
    {
        CheckAnd_SetDict();

        return _equipSkillDict.Values.All(x => x != null);
    }

    /// <summary>
    /// �ش� �������� �����Ǿ��ִ� ���� ��������
    /// </summary>
    private static SkillSlot GetEquippedIndex(Item item)
    {
        foreach (var kvp in _equipSkillDict)
        {
            SkillSlot keyIndex = kvp.Key;
            Item existItem = kvp.Value;

            if (existItem == null)
                continue;

            if (existItem.ID == item.ID)
                return keyIndex;
        }

        return SkillSlot.None;
    }

    /// <summary>
    /// ����ִ� ���� ��������
    /// </summary>
    private static SkillSlot GetEmptySlot()
    {
        CheckAnd_SetDict();

        foreach (var kvp in _equipSkillDict)
        {
            SkillSlot skillSlot = kvp.Key;
            Item item = kvp.Value;

            if (item == null)
                return skillSlot;
        }

        return SkillSlot.None;
    }

    /// <summary>
    /// �ش� ���Կ� �����Ǿ��ִ� ��ų������ ��������
    /// </summary>
    public static Item GetEquipSkill(SkillSlot skillSlot)
    {
        CheckAnd_SetDict();

        return _equipSkillDict[skillSlot];
    }

    /// <summary>
    /// ������ ��ų�� �������� (����ϱ� ���� '��ų'���·� ��ȯ�ؼ�)
    /// </summary>
    public static Skill[] GetEquipSkills_SkillType()
    {
        CheckAnd_SetDict();

        Skill[] castSkillArr = new Skill[_maxCount];

        SkillSlot[] allSlots = (SkillSlot[])Enum.GetValues(typeof(SkillSlot));
        SkillSlot[] validSlots = allSlots.Where(s => s != SkillSlot.None).ToArray();

        for (int i = 0; i < _equipSkillDict.Values.Count; i++)
        {
            Item item = _equipSkillDict[validSlots[i]];

            if (item == null)
            {
                castSkillArr[i] = null;
                continue;
            }

            castSkillArr[i] = SkillFactory.CreateSkill(item.ID, item.Level);
        }

        return castSkillArr;
    }


    /// <summary>
    /// ��ųʸ��� Ű üũ�غ��� ������ ��ųʸ� �����
    /// </summary>
    private static void CheckAnd_SetDict()
    {
        SkillSlot[] allSlots = (SkillSlot[])Enum.GetValues(typeof(SkillSlot));
        SkillSlot[] validSlots = allSlots.Where(s => s != SkillSlot.None).ToArray();

        for (int i = 0; i < validSlots.Length; i++)
        {
            if (_equipSkillDict.ContainsKey(validSlots[i]) == false)
                _equipSkillDict[validSlots[i]] = null;
            else
                continue;
        }
    }
}
