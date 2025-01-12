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
    public static event Action<Item> OnEquipSkillChanged; // ������ų �ٲ������ �̺�Ʈ

    private static Item[] _equipSkillArr;           // ������ ��ų �迭
    private static int _maxCount = 3;               // ��ų ���� �ִ� ����



    public static event Action OnEquipSwapStarted;  // ������ų ��ü�� ������ �� �̺�Ʈ
    public static event Action OnEquipSwapFinished; // ������ų ��ü�� ������ �� �̺�Ʈ
    private static Item _swapTargetSkill;          // ��ü�� ��ǥ ��ų







    /// <summary>
    /// ���� ������ (Ŭ������ ó�� ������ �� �� ���� ȣ��)
    /// </summary>
    static EquipSkillManager()
    {
        _equipSkillArr = new SkillItem[_maxCount];
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
            _swapTargetSkill = item;
            //OnEquipSwapStarted?.Invoke();

            return;
        }

        // ���� ���� ���ߴٸ�, ����ִ� ���� �ε��� �ƹ��ų� ��������
        if (slotIndex == -1)
            slotIndex = GetEmptySlotIndex();

        // ����
        _equipSkillArr[slotIndex] = item;

        // ������ų �ٲ������ �̺�Ʈ ��Ƽ
        OnEquipSkillChanged?.Invoke(item);

        Debug.Log($"������ ��ų : {slotIndex} ��°���� - {item.Name}");
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
    public static void Swap(int slotIndex)
    {
        // ���� ��ų ���� ����
        Item oldSkill = GetEquipSkill(slotIndex);
        UnEquip(oldSkill);

        // ���ο� ��ų ����
        Equip(_swapTargetSkill, slotIndex);
        _swapTargetSkill = null;

        //OnEquipSwapFinished?.Invoke();
        //OnEquipSkillChanged?.Invoke();
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
    /// ������ ��ų������ �迭 ��������
    /// </summary>
    public static Item[] GetEquipSkillArr()
    {
        if (_equipSkillArr.All(s => s == null)) // ��� ������ ����ִٸ�(null) null ��ȯ
            return null;

        return _equipSkillArr;
    }

    /// <summary>
    /// �ش� ���Կ� �����Ǿ��ִ� ��ų������ ��������
    /// </summary>
    public static Item GetEquipSkill(int slotIndex)
    {
        return _equipSkillArr[slotIndex];
    }
}
