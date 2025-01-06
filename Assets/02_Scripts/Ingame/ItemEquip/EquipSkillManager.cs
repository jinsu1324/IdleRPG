using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipSkillManager
{
    public static event Action OnEquipSkillChanged;                 // 장착 스킬이 변경되었을 때 이벤트

    // 슬롯 그래도 필요하니 배열로 변경
    private static Skill[] _equipSkillArr;                          // 장착한 스킬 배열
    private static int _maxCount = 3;                               // 스킬 장착 최대 갯수

    


    public static event Action OnEquipSwapStarted;
    public static event Action OnEquipSwapFinished;

    private static Skill _swapTargetSkill;

    /// <summary>
    /// 정적 생성자 (클래스가 처음 참조될 때 한 번만 호출)
    /// </summary>
    static EquipSkillManager()
    {
        _equipSkillArr = new Skill[_maxCount];
    }




    public static void SwapSkill(int slotIndex)
    {

        // 기존 스킬 해제
        Skill oldSkill = _equipSkillArr[slotIndex];
        UnEquipSkill(oldSkill);

        // 새로운 스킬 장착
        EquipSkill(_swapTargetSkill, slotIndex);

        OnEquipSwapFinished?.Invoke();

        
        _swapTargetSkill = null;
    }

    /// <summary>
    /// 스킬 장착
    /// </summary>
    public static void EquipSkill(Skill skill, int slotIndex = -1)
    {
        // 이미 장착된 스킬이면 그냥 리턴
        if (IsEquippedSkill(skill))
            return;

        // 슬롯 지정 안했다면
        if (slotIndex == -1)
        {
            // 비어있는 슬롯 찾기
            slotIndex = GetEmptySlotIndex();

            // 비어있는 슬롯이 없으면 교체 프로세스 시작
            if (IsExistEmptySlot(slotIndex) == false)
            {
                _swapTargetSkill = skill;

                OnEquipSwapStarted?.Invoke();

                return;
            }

        }


        // 장착
        _equipSkillArr[slotIndex] = skill;

        // 장착스킬이 변경되었을 때 이벤트 실행
        OnEquipSkillChanged?.Invoke();
    }

    /// <summary>
    /// 스킬 장착 해제
    /// </summary>
    public static void UnEquipSkill(Skill skill)
    {
        int slotIndex = Array.FindIndex(_equipSkillArr, s => s == skill); // 스킬을 찾음

        if (slotIndex >= 0) // 조건 존재한다는 뜻 (존재안하면 -1 반환)
        {
            // 장착 해제
            _equipSkillArr[slotIndex] = null;

            // 장착스킬이 변경되었을 때 이벤트 실행
            OnEquipSkillChanged?.Invoke();
        }

        
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
    /// 비어있는 슬롯이 존재하는지?
    /// </summary>
    private static bool IsExistEmptySlot(int slotIndex)
    {
        return slotIndex >= 0;
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
}
