using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �г� ��ų
/// </summary>
public class Skill_Anger
{
    //[JsonIgnore] public float AddAttackSpeed { get; private set; }   // ���ݼӵ� �߰���              
    //[JsonIgnore] public float Duration { get; private set; }         // ��ų���ӽð�

    ///// <summary>
    ///// �Ӽ��� ������Ʈ
    ///// </summary>
    //protected override void UpdateAttributes()
    //{
    //    AddAttackSpeed = GetAttributeValue_ByCurrentLevel(SkillAttributeType.AddAttackSpeed);
    //    Duration = GetAttributeValue_ByCurrentLevel(SkillAttributeType.Duration);
    //}

    ///// <summary>
    ///// ��ų ����
    ///// </summary>
    //public override void ExecuteSkill()
    //{
    //    Debug.Log("Skill_Anger!!");

    //    // ���� ��ųʸ� ����
    //    Dictionary<StatType, float> buffDict = new Dictionary<StatType, float>() 
    //    { 
    //        { StatType.AttackSpeed, AddAttackSpeed}
    //    };

    //    // ���� ����
    //    PlayerStatUpdateArgs args = new PlayerStatUpdateArgs()
    //    {
    //        AttributeDict = buffDict,
    //        Source = this,
    //    };
    //    PlayerBuffSystem.Instance.StartBuffToPlayer(Duration, args);

    //    // ����Ʈ ����
    //    GameObject fx = FXManager.Instance.SpawnFX(FXName.FX_Skill_Anger, PlayerManager.PlayerInstance.transform);
    //    fx.GetComponent<FX_Skill_Anger>().Init(Duration);
    //}

    ///// <summary>
    ///// Desc�������� 
    ///// </summary>
    //public override string GetDynamicDesc()
    //{
    //    return string.Format(Desc, Duration, AddAttackSpeed);
    //}
}
