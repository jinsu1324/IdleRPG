using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipSkillManager
{
    public static event Action OnEquipSkillChanged;                 // ���� ��ų�� ����Ǿ��� �� �̺�Ʈ

    // ���� �׷��� �ʿ��ϴ� �迭�� ����
    private static Skill[] _equipSkillArr;                          // ������ ��ų �迭
    private static int _maxCount = 3;                               // ��ų ���� �ִ� ����

    


    public static event Action OnEquipSwapStarted;
    public static event Action OnEquipSwapFinished;

    private static Skill _swapTargetSkill;

    /// <summary>
    /// ���� ������ (Ŭ������ ó�� ������ �� �� ���� ȣ��)
    /// </summary>
    static EquipSkillManager()
    {
        _equipSkillArr = new Skill[_maxCount];
    }




    public static void SwapSkill(int slotIndex)
    {

        // ���� ��ų ����
        Skill oldSkill = _equipSkillArr[slotIndex];
        UnEquipSkill(oldSkill);

        // ���ο� ��ų ����
        EquipSkill(_swapTargetSkill, slotIndex);

        OnEquipSwapFinished?.Invoke();

        
        _swapTargetSkill = null;
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public static void EquipSkill(Skill skill, int slotIndex = -1)
    {
        // �̹� ������ ��ų�̸� �׳� ����
        if (IsEquippedSkill(skill))
            return;

        // ���� ���� ���ߴٸ�
        if (slotIndex == -1)
        {
            // ����ִ� ���� ã��
            slotIndex = GetEmptySlotIndex();

            // ����ִ� ������ ������ ��ü ���μ��� ����
            if (IsExistEmptySlot(slotIndex) == false)
            {
                _swapTargetSkill = skill;

                OnEquipSwapStarted?.Invoke();

                return;
            }

        }


        // ����
        _equipSkillArr[slotIndex] = skill;

        // ������ų�� ����Ǿ��� �� �̺�Ʈ ����
        OnEquipSkillChanged?.Invoke();
    }

    /// <summary>
    /// ��ų ���� ����
    /// </summary>
    public static void UnEquipSkill(Skill skill)
    {
        int slotIndex = Array.FindIndex(_equipSkillArr, s => s == skill); // ��ų�� ã��

        if (slotIndex >= 0) // ���� �����Ѵٴ� �� (������ϸ� -1 ��ȯ)
        {
            // ���� ����
            _equipSkillArr[slotIndex] = null;

            // ������ų�� ����Ǿ��� �� �̺�Ʈ ����
            OnEquipSkillChanged?.Invoke();
        }

        
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
    /// ����ִ� ������ �����ϴ���?
    /// </summary>
    private static bool IsExistEmptySlot(int slotIndex)
    {
        return slotIndex >= 0;
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
}
