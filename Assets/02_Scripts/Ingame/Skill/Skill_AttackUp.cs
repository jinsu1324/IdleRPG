using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AttackUp : SkillItem
{
    [JsonIgnore] public float AddAttackPower { get; private set; }   // 공격력 추가량              
    [JsonIgnore] public float Duration { get; private set; }         // 스킬지속시간

    /// <summary>
    /// 속성값 업데이트
    /// </summary>
    protected override void UpdateAttributes()
    {
        AddAttackPower = GetAttributeValue_ByCurrentLevel(SkillAttributeType.AddAttackPower);
        Duration = GetAttributeValue_ByCurrentLevel(SkillAttributeType.Duration);
    }

    /// <summary>
    /// 스킬 실행
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log("Skill_AttackUp!!");

        //// 버프 적용
        //Dictionary<StatType, float> buffDict = new Dictionary<StatType, float>() { { StatType.AttackSpeed, AddAttackPower } };
        //PlayerBuffSystem.Instance.StartBuffToPlayer(Duration, buffDict, this);

        //// 이펙트 시작
        //GameObject fx = FXManager.Instance.SpawnFX(FXName.FX_Skill_Anger, PlayerSpawner.PlayerInstance.transform);
        //fx.GetComponent<FX_Skill_Anger>().Init(Duration);
    }

    /// <summary>
    /// Desc가져오기 
    /// </summary>
    public override string GetDynamicDesc()
    {
        return string.Format(Desc, Duration, AddAttackPower);
    }
}
