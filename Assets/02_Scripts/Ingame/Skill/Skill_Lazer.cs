using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Lazer : Skill
{
    private float _attackPercentage;     // 공격 퍼센티지
    private float _projectileSpeed;      // 투사체 속도
    private float _skillAttackPower;    // 스킬 공격력

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Lazer(CreateSkillArgs args) : base(args)
    {
        Debug.Log("Skill_Lazer 생성자!");

        Update_SkillStatValues();
    }

    /// <summary>
    /// 스킬스탯 값들 업데이트
    /// </summary>
    private void Update_SkillStatValues()
    {
        _attackPercentage = GetSkillStatValue(SkillStatType.AttackPercentage);
        _projectileSpeed = GetSkillStatValue(SkillStatType.ProjectileSpeed);

        _skillAttackPower = Calculate_SkillAttackPower(_attackPercentage);
    }

    /// <summary>
    /// 스킬 실행
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log($"Skill_Lazer!! 공격력 : {_skillAttackPower}");
    }
}
