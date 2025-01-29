using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AttackUp : Skill
{
    private float _addAttackPower;  // ���ݷ� �߰���              
    private float _duration;        // ��ų���ӽð�

    /// <summary>
    /// ������
    /// </summary>
    public Skill_AttackUp(CreateSkillArgs args) : base(args)
    {
        Update_SkillStatValues();
    }

    /// <summary>
    /// ��ų���� ���� ������Ʈ
    /// </summary>
    private void Update_SkillStatValues()
    {
        _addAttackPower = GetSkillStatValue(SkillStatType.AddAttackPower);
        _duration = GetSkillStatValue(SkillStatType.Duration);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        Dictionary<StatType, float> buffDict = new Dictionary<StatType, float>() { { StatType.AttackPower, _addAttackPower } };

        PlayerStatUpdateArgs args = new PlayerStatUpdateArgs() { DetailStatDict = buffDict, SourceID = ID };
        PlayerBuffSystem.Instance.StartBuffToPlayer(_duration, args);

        GameObject fx = FXManager.Instance.SpawnFX(FXName.FX_Skill_AttackUp, PlayerManager.PlayerInstance.transform);
        fx.GetComponent<FX_Skill_AttackUp>().Init(_duration);
    }
}
