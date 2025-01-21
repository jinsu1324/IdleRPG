using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 스킬 캐스터
/// </summary>
public class SkillCaster : MonoBehaviour
{
    public static event Action<Skill[]> OnUpdateCastSkills;     // 캐스팅할 스킬들이 바뀌었을때 이벤트
    private static Skill[] _castSkillArr;                       // 장착스킬을 받아와 캐스팅할 스킬들 배열 

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_CastSkillArr; // 장착스킬 바뀌었을때 -> 캐스팅할 스킬들 업데이트
        ItemEnhanceManager.OnSkillEnhance += Update_CastSkillArr; // 스킬강화했을때 -> 캐스팅할 스킬들 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipSkillManager.OnEquipSkillChanged -= Update_CastSkillArr;
        ItemEnhanceManager.OnSkillEnhance -= Update_CastSkillArr;
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Update_CastSkillArr();
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (IsCastSkillEmpty())
            return;

        CastSkills();
    }

    /// <summary>
    /// 스킬들 캐스팅
    /// </summary>
    private void CastSkills()
    {
        foreach (Skill skill in _castSkillArr)
        {
            if (skill == null)
                continue;

            if (skill.CheckCoolTime())
                skill.ExecuteSkill();
        }
    }

    /// <summary>
    /// 캐스팅할 스킬배열 업데이트
    /// </summary>
    private void Update_CastSkillArr(Item item = null)
    {
        _castSkillArr = EquipSkillManager.GetEquipSkills_SkillType();
        
        OnUpdateCastSkills?.Invoke(_castSkillArr);
    }

    /// <summary>
    /// 캐스팅할 스킬이 비었는지?
    /// </summary>
    private bool IsCastSkillEmpty()
    {
        if (_castSkillArr == null)
            return true;

        return _castSkillArr.All(skill => skill == null);
    }

    /// <summary>
    /// 캐스팅할 스킬들 배열 가져오기
    /// </summary>
    public static Skill[] GetCastSkillArr()
    {
        return _castSkillArr;
    }
}
