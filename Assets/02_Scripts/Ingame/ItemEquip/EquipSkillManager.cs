using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipSkillManager
{
    public static event Action OnEquipSkillChanged;                 // ���� ��ų�� ����Ǿ��� �� �̺�Ʈ
    
    private static List<Skill> _equipSkillList = new List<Skill>(); // ������ ��ų ����Ʈ
    private static int _maxCount = 3;                               // ��ų ���� �ִ� ����

    /// <summary>
    /// ��ų ����
    /// </summary>
    public static void EquipSkill(Skill skill)
    {
        // �̹� ������ ��ų�̸� �׳� ����
        if (IsEquippedSkill(skill))
            return;

        // �̹� �ִ�� ���������� �׳� ����
        if (IsEquippedMax())
            return;

        // ����
        _equipSkillList.Add(skill);

        // ������ų�� ����Ǿ��� �� �̺�Ʈ ����
        OnEquipSkillChanged?.Invoke();
    }

    /// <summary>
    /// ��ų ���� ����
    /// </summary>
    public static void UnEquipSkill(Skill skill)
    {
        // ������ ��ų�� ������ �׳� ����
        if (IsEquippedSkill(skill) == false)
        {
            Debug.Log("������ ��ų�� �����ϴ�.");
            return;
        }

        // ���� ����
        _equipSkillList.Remove(skill);

        // ������ų�� ����Ǿ��� �� �̺�Ʈ ����
        OnEquipSkillChanged?.Invoke();
    }

    /// <summary>
    /// ������ ��ų����?
    /// </summary>
    public static bool IsEquippedSkill(Skill skill)
    {
        return _equipSkillList.Contains(skill);
    }

    /// <summary>
    /// �ִ�� �����ߴ���?
    /// </summary>
    public static bool IsEquippedMax()
    {
        return _equipSkillList.Count >= _maxCount; 
    }

    /// <summary>
    /// ������ ��ų ����Ʈ ��������
    /// </summary>
    public static List<Skill> GetEquippedSkillList() 
    { 
        if (_equipSkillList.Count <= 0)
            return null;

        return _equipSkillList; 
    }
}
