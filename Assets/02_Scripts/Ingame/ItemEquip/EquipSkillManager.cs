using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 장착한 스킬 관리
/// </summary>
public class EquipSkillManager
{
    public static event Action OnEquipSkillChanged; // 장착 스킬이 변경되었을 때 이벤트
    public static event Action OnEquipSwapStarted;  // 장착스킬 교체를 시작할 때 이벤트
    public static event Action OnEquipSwapFinished; // 장착스킬 교체가 끝났을 때 이벤트

    private static Skill[] _equipSkillArr;          // 장착한 스킬 배열
    private static int _maxCount = 3;               // 스킬 장착 최대 갯수

    private static Skill _swapTargetSkill;          // 교체할 목표 스킬

    /// <summary>
    /// 정적 생성자 (클래스가 처음 참조될 때 한 번만 호출)
    /// </summary>
    static EquipSkillManager()
    {
        _equipSkillArr = new Skill[_maxCount];
    }
    
    /// <summary>
    /// 스킬 장착
    /// </summary>
    public static void EquipSkill(Skill skill, int slotIndex = -1)
    {
        // 이미 장착된 스킬이면 그냥 리턴
        if (IsEquippedSkill(skill))
            return;

        // 이미 최대로 장착했으면 교체시작
        if (IsEquipMax())
        {
            _swapTargetSkill = skill;
            OnEquipSwapStarted?.Invoke();

            return;
        }

        // 슬롯 지정 안했다면, 비어있는 슬롯 인덱스 가져오기
        if (slotIndex == -1)
            slotIndex = GetEmptySlotIndex();

        // 장착
        _equipSkillArr[slotIndex] = skill;
        OnEquipSkillChanged?.Invoke();
    }

    /// <summary>
    /// 스킬 장착 해제
    /// </summary>
    public static void UnEquipSkill(Skill skill)
    {
        // 해당 스킬이 어느 인덱스에 장착되어있는지 찾음
        int equippedIndex = FindEquippedSkillIndex(skill); 

        // 인덱스를 찾을수 없었다면 (-1) 그냥리턴
        if (IsIndexFound(equippedIndex) == false)
        {
            Debug.Log($"{skill.Name} 스킬이 장착슬롯 안에 없어서 Index를 찾을 수 없습니다.");
            return;
        }
        
        // 장착 해제
        _equipSkillArr[equippedIndex] = null;
        OnEquipSkillChanged?.Invoke();
    }
    
    /// <summary>
    /// 스킬 교체
    /// </summary>
    public static void SwapSkill(int slotIndex)
    {
        // 기존 스킬 장착 해제
        Skill oldSkill = GetEquippedSkill(slotIndex);
        UnEquipSkill(oldSkill);

        // 새로운 스킬 장착
        EquipSkill(_swapTargetSkill, slotIndex);
        _swapTargetSkill = null;

        OnEquipSwapFinished?.Invoke();
        OnEquipSkillChanged?.Invoke();
    }

    /// <summary>
    /// 장착한 스킬 베열 가져오기
    /// </summary>
    public static Skill[] GetEquippedSkillArr()
    {
        if (_equipSkillArr.All(s => s == null)) // 모든 슬롯이 비어있다면 null 반환
            return null;

        return _equipSkillArr;
    }

    /// <summary>
    /// 해당 슬롯에 장착되어있는 스킬 가져오기
    /// </summary>
    public static Skill GetEquippedSkill(int slotIndex)
    {
        return _equipSkillArr[slotIndex];
    }

    /// <summary>
    /// 해당 스킬이 어느 슬롯에 장착되어있는지 찾기
    /// </summary>
    private static int FindEquippedSkillIndex(Skill skill)
    {
        return Array.FindIndex(_equipSkillArr, s => s == skill);
    }

    /// <summary>
    /// 장착한 스킬인지?
    /// </summary>
    public static bool IsEquippedSkill(Skill skill)
    {
        return _equipSkillArr.Contains(skill);
    }

    /// <summary>
    /// 비어있는 슬롯 인덱스 가져오기
    /// </summary>
    private static int GetEmptySlotIndex()
    {
        return Array.FindIndex(_equipSkillArr, s => s == null);
    }

    /// <summary>
    /// 최대로 착용했는지?
    /// </summary>
    private static bool IsEquipMax()
    {
        return GetEmptySlotIndex() == -1;
    }

    /// <summary>
    /// 인덱스를 찾을 수 있었는지?
    /// </summary>
    private static bool IsIndexFound(int equippedIndex)
    {
        if (equippedIndex >= 0)
            return true;
        else
            return false;
    }
}
