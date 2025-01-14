using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipSkillCaster : MonoBehaviour
{
    private ISkill[] _castSkillArr; // 장착스킬을 받아와 캐스팅할 스킬들 배열 

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_CastSkillArr; // 장착스킬 바뀌었을때 -> 캐스팅할 스킬들도 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipSkillManager.OnEquipSkillChanged -= Update_CastSkillArr;
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Update_CastSkillArr();  // 게임시작하면 캐스팅할 스킬배열 한번 업데이트
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (IsCastSkillEmpty()) // 캐스팅스킬 비어있으면 그냥 리턴
            return;

        CastSkills();
    }

    /// <summary>
    /// 스킬들 캐스팅
    /// </summary>
    private void CastSkills()
    {
        foreach (ISkill skill in _castSkillArr)
        {
            if (skill == null) // 스킬없는 인덱스는 그냥 continue
                continue;

            if (skill.CheckCoolTime())  // 쿨타임체크해서 실행
                skill.ExecuteSkill();
        }
    }


    /// <summary>
    /// 캐스팅할 스킬배열 업데이트
    /// </summary>
    private void Update_CastSkillArr(Item item = null)
    {
        _castSkillArr = EquipSkillManager.GetEquipISkill();
    }
    
    /// <summary>
    /// 캐스팅할 스킬이 비었는지?
    /// </summary>
    private bool IsCastSkillEmpty()
    {
        if (_castSkillArr == null) // 캐스팅할 스킬 배열자체가 그냥 비어있으면, 비어있다고 true 반환
            return true;

        return _castSkillArr.All(skill => skill == null); // 모든 요소가 비었는지 확인
    }
}
