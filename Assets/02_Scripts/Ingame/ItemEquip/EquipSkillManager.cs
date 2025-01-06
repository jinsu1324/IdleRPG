using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipSkillManager
{
    public static event Action OnEquipSkillChanged;                 // 장착 스킬이 변경되었을 때 이벤트
    
    private static List<Skill> _equipSkillList = new List<Skill>(); // 장착한 스킬 리스트
    private static int _maxCount = 3;                               // 스킬 장착 최대 갯수

    /// <summary>
    /// 스킬 장착
    /// </summary>
    public static void EquipSkill(Skill skill)
    {
        // 이미 장착된 스킬이면 그냥 리턴
        if (IsEquippedSkill(skill))
            return;

        // 이미 최대로 장착했으면 그냥 리턴
        if (IsEquippedMax())
            return;

        // 장착
        _equipSkillList.Add(skill);

        // 장착스킬이 변경되었을 때 이벤트 실행
        OnEquipSkillChanged?.Invoke();
    }

    /// <summary>
    /// 스킬 장착 해제
    /// </summary>
    public static void UnEquipSkill(Skill skill)
    {
        // 장착된 스킬이 없으면 그냥 리턴
        if (IsEquippedSkill(skill) == false)
        {
            Debug.Log("장착된 스킬이 없습니다.");
            return;
        }

        // 장착 해제
        _equipSkillList.Remove(skill);

        // 장착스킬이 변경되었을 때 이벤트 실행
        OnEquipSkillChanged?.Invoke();
    }

    /// <summary>
    /// 장착한 스킬인지?
    /// </summary>
    public static bool IsEquippedSkill(Skill skill)
    {
        return _equipSkillList.Contains(skill);
    }

    /// <summary>
    /// 최대로 장착했는지?
    /// </summary>
    public static bool IsEquippedMax()
    {
        return _equipSkillList.Count >= _maxCount; 
    }

    /// <summary>
    /// 장착한 스킬 리스트 가져오기
    /// </summary>
    public static List<Skill> GetEquippedSkillList() 
    { 
        if (_equipSkillList.Count <= 0)
            return null;

        return _equipSkillList; 
    }
}
