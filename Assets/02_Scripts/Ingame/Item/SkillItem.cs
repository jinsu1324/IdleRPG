using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 스킬 아이템
/// </summary>
public abstract class SkillItem
{
    //[JsonIgnore] public SkillDataSO SkillDataSO { get; private set; }                                // 스킬 데이터

    ///// <summary>
    ///// 초기화
    ///// </summary>
    //public virtual void Init(SkillDataSO skillDataSO, int level)
    //{
    //    SkillDataSO = skillDataSO;
    //    ID = skillDataSO.ID;
    //    ItemType = (ItemType)Enum.Parse(typeof(ItemType), skillDataSO.ItemType);
    //    Name = skillDataSO.Name;
    //    Grade = skillDataSO.Grade;
    //    Desc = skillDataSO.Desc;
    //    Count = 1;
    //    Icon = skillDataSO.Icon;
    //    AttributeDict = new Dictionary<SkillAttributeType, float>(skillDataSO.GetAttributeDict_ByLevel(level));
    //    Level = level;
    //    EnhanceableCount = 10;
    //    CoolTime = skillDataSO.CoolTime;

    //    TryUpdateAttributes(); // 속성값들 업데이트 시도
    //}

    ///// <summary>
    ///// 아이템 레벨업
    ///// </summary>
    //public virtual void ItemLevelUp()
    //{
    //    // 레벨업
    //    Level++;

    //    // 속성들 레벨에 맞게 최신화
    //    AttributeDict = new Dictionary<SkillAttributeType, float>(SkillDataSO.GetAttributeDict_ByLevel(Level));

    //    // 속성값들 업데이트 시도
    //    TryUpdateAttributes();
    //}

    ///// <summary>
    ///// 아이템 갯수를 강화 갯수만큼 소비
    ///// </summary>
    //public void RemoveCountByEnhance()
    //{
    //    Count -= EnhanceableCount;
    //}

    ///// <summary>
    ///// 강화 가능한지?
    ///// </summary>
    //public bool CanEnhance()
    //{
    //    return Count >= EnhanceableCount;
    //}

    ///// <summary>
    ///// 쿨타임 체크
    ///// </summary>
    //public bool CheckCoolTime()
    //{
    //    CurrentTime += Time.deltaTime;

    //    if (CurrentTime > CoolTime)
    //    {
    //        CurrentTime %= CoolTime;
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    ///// <summary>
    ///// 현재 쿨타임 진행상황 가져오기
    ///// </summary>
    //public float GetCurrentCoolTimeProgress()
    //{
    //    return Mathf.Clamp01(CurrentTime / CoolTime);
    //}

    ///// <summary>
    ///// 속성값들 업데이트 시도
    ///// </summary>
    //protected void TryUpdateAttributes()
    //{
    //    // 업데이트 불가하면 그냥 리턴
    //    if (CanUpdateAttributes() == false)
    //        return;

    //    // 실제 속성값들 업데이트
    //    UpdateAttributes();
    //}

    ///// <summary>
    ///// 실제 속성값들 업데이트
    ///// </summary>
    //protected abstract void UpdateAttributes();

    ///// <summary>
    ///// 스킬 실행
    ///// </summary>
    //public abstract void ExecuteSkill();

    ///// <summary>
    ///// 상세값들 동적할당된 Desc가져오기
    ///// </summary>
    //public abstract string GetDynamicDesc();

    ///// <summary>
    ///// 속성값들 업데이트 가능한지?
    ///// </summary>
    //protected bool CanUpdateAttributes()
    //{
    //    if (SkillDataSO == null || Level == 0)
    //    {
    //        Debug.Log("스킬 속성값 업데이트 불가");
    //        return false;
    //    }
    //    else
    //        return true;
    //}

    ///// <summary>
    ///// 현재 레벨에 맞는 속성값 가져오기
    ///// </summary>
    //protected float GetAttributeValue_ByCurrentLevel(SkillAttributeType skillAttributeType)
    //{
    //    if (float.TryParse(SkillDataSO.GetAttributeValue(skillAttributeType, Level), out float resultValue))
    //        return resultValue;
    //    else
    //        return 0;
    //}

    ///// <summary>
    ///// 스킬 공격력 계산
    ///// </summary>
    //protected float Calculate_SkillAttackPower(float attackPercentage)
    //{
    //    return PlayerStats.GetStatValue(StatType.AttackPower) * attackPercentage;
    //}
}
