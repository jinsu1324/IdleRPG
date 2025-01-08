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
    public static event Action OnEquipSkillChanged; // ���� ��ų�� ����Ǿ��� �� �̺�Ʈ
    public static event Action OnEquipSwapStarted;  // ������ų ��ü�� ������ �� �̺�Ʈ
    public static event Action OnEquipSwapFinished; // ������ų ��ü�� ������ �� �̺�Ʈ

    private static Skill[] _equipSkillArr;          // ������ ��ų �迭
    private static int _maxCount = 3;               // ��ų ���� �ִ� ����

    private static Skill _swapTargetSkill;          // ��ü�� ��ǥ ��ų

    /// <summary>
    /// ���� ������ (Ŭ������ ó�� ������ �� �� ���� ȣ��)
    /// </summary>
    static EquipSkillManager()
    {
        _equipSkillArr = new Skill[_maxCount];
    }
    
    /// <summary>
    /// ��ų ����
    /// </summary>
    public static void EquipSkill(Skill skill, int slotIndex = -1)
    {
        // �̹� ������ ��ų�̸� �׳� ����
        if (IsEquippedSkill(skill))
            return;

        // �̹� �ִ�� ���������� ��ü����
        if (IsEquipMax())
        {
            _swapTargetSkill = skill;
            OnEquipSwapStarted?.Invoke();

            return;
        }

        // ���� ���� ���ߴٸ�, ����ִ� ���� �ε��� ��������
        if (slotIndex == -1)
            slotIndex = GetEmptySlotIndex();

        // ����
        _equipSkillArr[slotIndex] = skill;
        OnEquipSkillChanged?.Invoke();
    }

    /// <summary>
    /// ��ų ���� ����
    /// </summary>
    public static void UnEquipSkill(Skill skill)
    {
        // �ش� ��ų�� ��� �ε����� �����Ǿ��ִ��� ã��
        int equippedIndex = FindEquippedSkillIndex(skill); 

        // �ε����� ã���� �����ٸ� (-1) �׳ɸ���
        if (IsIndexFound(equippedIndex) == false)
        {
            Debug.Log($"{skill.Name} ��ų�� �������� �ȿ� ��� Index�� ã�� �� �����ϴ�.");
            return;
        }
        
        // ���� ����
        _equipSkillArr[equippedIndex] = null;
        OnEquipSkillChanged?.Invoke();
    }
    
    /// <summary>
    /// ��ų ��ü
    /// </summary>
    public static void SwapSkill(int slotIndex)
    {
        // ���� ��ų ���� ����
        Skill oldSkill = GetEquippedSkill(slotIndex);
        UnEquipSkill(oldSkill);

        // ���ο� ��ų ����
        EquipSkill(_swapTargetSkill, slotIndex);
        _swapTargetSkill = null;

        OnEquipSwapFinished?.Invoke();
        OnEquipSkillChanged?.Invoke();
    }

    /// <summary>
    /// ������ ��ų ���� ��������
    /// </summary>
    public static Skill[] GetEquippedSkillArr()
    {
        if (_equipSkillArr.All(s => s == null)) // ��� ������ ����ִٸ� null ��ȯ
            return null;

        return _equipSkillArr;
    }

    /// <summary>
    /// �ش� ���Կ� �����Ǿ��ִ� ��ų ��������
    /// </summary>
    public static Skill GetEquippedSkill(int slotIndex)
    {
        return _equipSkillArr[slotIndex];
    }

    /// <summary>
    /// �ش� ��ų�� ��� ���Կ� �����Ǿ��ִ��� ã��
    /// </summary>
    private static int FindEquippedSkillIndex(Skill skill)
    {
        return Array.FindIndex(_equipSkillArr, s => s == skill);
    }

    /// <summary>
    /// ������ ��ų����?
    /// </summary>
    public static bool IsEquippedSkill(Skill skill)
    {
        return _equipSkillArr.Contains(skill);
    }

    /// <summary>
    /// ����ִ� ���� �ε��� ��������
    /// </summary>
    private static int GetEmptySlotIndex()
    {
        return Array.FindIndex(_equipSkillArr, s => s == null);
    }

    /// <summary>
    /// �ִ�� �����ߴ���?
    /// </summary>
    private static bool IsEquipMax()
    {
        return GetEmptySlotIndex() == -1;
    }

    /// <summary>
    /// �ε����� ã�� �� �־�����?
    /// </summary>
    private static bool IsIndexFound(int equippedIndex)
    {
        if (equippedIndex >= 0)
            return true;
        else
            return false;
    }
}
