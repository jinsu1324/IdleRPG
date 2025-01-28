using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AttackUp : Skill
{
    private float _addAttackPower;  // 공격력 추가량              
    private float _duration;        // 스킬지속시간

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_AttackUp(CreateSkillArgs args) : base(args)
    {
        Update_SkillStatValues();
    }

    /// <summary>
    /// 스킬스탯 값들 업데이트
    /// </summary>
    private void Update_SkillStatValues()
    {
        _addAttackPower = GetSkillStatValue(SkillStatType.AddAttackPower);
        _duration = GetSkillStatValue(SkillStatType.Duration);
    }

    /// <summary>
    /// 스킬 실행
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log($"Skill_Anger!! 공격력 증가값 : {_addAttackPower}");

        //// 버프 적용
        //Dictionary<StatType, float> buffDict = new Dictionary<StatType, float>() { { StatType.AttackSpeed, AddAttackPower } };
        //PlayerBuffSystem.Instance.StartBuffToPlayer(Duration, buffDict, this);

        //// 이펙트 시작
        //GameObject fx = FXManager.Instance.SpawnFX(FXName.FX_Skill_Anger, PlayerSpawner.PlayerInstance.transform);
        //fx.GetComponent<FX_Skill_Anger>().Init(Duration);
    }
}
