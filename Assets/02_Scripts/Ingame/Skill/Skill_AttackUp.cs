using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AttackUp : SkillItem
{
    [JsonIgnore] public float AddAttackPower { get; private set; }   // ���ݷ� �߰���              
    [JsonIgnore] public float Duration { get; private set; }         // ��ų���ӽð�

    /// <summary>
    /// �Ӽ��� ������Ʈ
    /// </summary>
    protected override void UpdateAttributes()
    {
        AddAttackPower = GetAttributeValue_ByCurrentLevel(SkillAttributeType.AddAttackPower);
        Duration = GetAttributeValue_ByCurrentLevel(SkillAttributeType.Duration);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log("Skill_AttackUp!!");

        //// ���� ����
        //Dictionary<StatType, float> buffDict = new Dictionary<StatType, float>() { { StatType.AttackSpeed, AddAttackPower } };
        //PlayerBuffSystem.Instance.StartBuffToPlayer(Duration, buffDict, this);

        //// ����Ʈ ����
        //GameObject fx = FXManager.Instance.SpawnFX(FXName.FX_Skill_Anger, PlayerSpawner.PlayerInstance.transform);
        //fx.GetComponent<FX_Skill_Anger>().Init(Duration);
    }

    /// <summary>
    /// Desc�������� 
    /// </summary>
    public override string GetDynamicDesc()
    {
        return string.Format(Desc, Duration, AddAttackPower);
    }
}
