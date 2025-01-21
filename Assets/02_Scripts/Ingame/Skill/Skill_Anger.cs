using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 분노 스킬
/// </summary>
public class Skill_Anger
{
    //[JsonIgnore] public float AddAttackSpeed { get; private set; }   // 공격속도 추가량              
    //[JsonIgnore] public float Duration { get; private set; }         // 스킬지속시간

    ///// <summary>
    ///// 속성값 업데이트
    ///// </summary>
    //protected override void UpdateAttributes()
    //{
    //    AddAttackSpeed = GetAttributeValue_ByCurrentLevel(SkillAttributeType.AddAttackSpeed);
    //    Duration = GetAttributeValue_ByCurrentLevel(SkillAttributeType.Duration);
    //}

    ///// <summary>
    ///// 스킬 실행
    ///// </summary>
    //public override void ExecuteSkill()
    //{
    //    Debug.Log("Skill_Anger!!");

    //    // 버프 딕셔너리 제작
    //    Dictionary<StatType, float> buffDict = new Dictionary<StatType, float>() 
    //    { 
    //        { StatType.AttackSpeed, AddAttackSpeed}
    //    };

    //    // 버프 적용
    //    PlayerStatUpdateArgs args = new PlayerStatUpdateArgs()
    //    {
    //        AttributeDict = buffDict,
    //        Source = this,
    //    };
    //    PlayerBuffSystem.Instance.StartBuffToPlayer(Duration, args);

    //    // 이펙트 시작
    //    GameObject fx = FXManager.Instance.SpawnFX(FXName.FX_Skill_Anger, PlayerManager.PlayerInstance.transform);
    //    fx.GetComponent<FX_Skill_Anger>().Init(Duration);
    //}

    ///// <summary>
    ///// Desc가져오기 
    ///// </summary>
    //public override string GetDynamicDesc()
    //{
    //    return string.Format(Desc, Duration, AddAttackSpeed);
    //}
}
