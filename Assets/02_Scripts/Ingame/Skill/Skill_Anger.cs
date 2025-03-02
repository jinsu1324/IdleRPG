using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 분노 스킬
/// </summary>
public class Skill_Anger : Skill
{
    private float _addAttackSpeed;   // 공격속도 추가량              
    private float _duration;         // 스킬지속시간

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Anger(CreateSkillArgs args) : base(args)
    {
        Update_SkillStatValues();
    }

    /// <summary>
    /// 스킬스탯 값들 업데이트
    /// </summary>
    private void Update_SkillStatValues()
    {
        _addAttackSpeed = GetSkillStatValue(SkillStatType.AddAttackSpeed);
        _duration = GetSkillStatValue(SkillStatType.Duration);
    }

    /// <summary>
    /// 스킬 실행
    /// </summary>
    public override void ExecuteSkill()
    {
        Dictionary<StatType, float> buffDict = new Dictionary<StatType, float>(){ { StatType.AttackSpeed, _addAttackSpeed} };

        PlayerStatUpdateArgs args = new PlayerStatUpdateArgs(){ DetailStatDict = buffDict, SourceID = ID };
        PlayerBuffSystem.Instance.StartBuffToPlayer(_duration, args);

        GameObject fx = FXManager.Instance.SpawnFX(FXName.FX_Skill_Anger, PlayerManager.PlayerInstance.transform);
        fx.GetComponent<FX_Skill_Anger>().Init(_duration);
    }
}
