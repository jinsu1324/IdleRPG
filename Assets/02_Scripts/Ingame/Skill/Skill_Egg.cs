using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Egg : SkillItem
{
    [JsonIgnore] public float AttackPercentage { get; private set; }     // 공격 퍼센티지
    [JsonIgnore] public float Range { get; private set; }                // 사거리
    [JsonIgnore] public float ProjectileCount { get; private set; }      // 투사체 갯수

    [JsonIgnore] private float _skillAttackPower;                        // 스킬 공격력

    /// <summary>
    /// 속성값 업데이트
    /// </summary>
    protected override void UpdateAttributes()
    {
        AttackPercentage = GetAttributeValue_ByCurrentLevel(SkillAttributeType.AttackPercentage);
        Range = GetAttributeValue_ByCurrentLevel(SkillAttributeType.Range);
        ProjectileCount = GetAttributeValue_ByCurrentLevel(SkillAttributeType.ProjectileCount);

        _skillAttackPower = Calculate_SkillAttackPower(AttackPercentage);
    }

    /// <summary>
    /// 스킬 실행
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log("Skill_Egg!!");
    }

    /// <summary>
    /// Desc가져오기 
    /// </summary>
    public override string GetDynamicDesc()
    {
        return string.Format(Desc, ProjectileCount, NumberConverter.ConvertPercentage(AttackPercentage));
    }
}
