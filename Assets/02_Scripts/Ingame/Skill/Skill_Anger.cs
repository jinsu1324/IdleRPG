using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �г� ��ų
/// </summary>
public class Skill_Anger : Skill
{
    private float _addAttackSpeed;   // ���ݼӵ� �߰���              
    private float _duration;         // ��ų���ӽð�

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Anger(CreateSkillArgs args) : base(args)
    {
        Debug.Log("Skill_Anger ������!");

        Update_SkillStatValues();
    }

    /// <summary>
    /// ��ų���� ���� ������Ʈ
    /// </summary>
    private void Update_SkillStatValues()
    {
        _addAttackSpeed = GetSkillStatValue(SkillStatType.AddAttackSpeed);
        _duration = GetSkillStatValue(SkillStatType.Duration);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log($"Skill_Anger!! ���������� : {_addAttackSpeed}");

        //// ���� ��ųʸ� ����
        //Dictionary<StatType, float> buffDict = new Dictionary<StatType, float>()
        //{
        //    { StatType.AttackSpeed, _addAttackSpeed}
        //};

        //// ���� ����
        //PlayerStatUpdateArgs args = new PlayerStatUpdateArgs()
        //{
        //    DetailStatDict = buffDict,
        //    Source = this,
        //};
        //PlayerBuffSystem.Instance.StartBuffToPlayer(_duration, args);

        //// ����Ʈ ����
        //GameObject fx = FXManager.Instance.SpawnFX(FXName.FX_Skill_Anger, PlayerManager.PlayerInstance.transform);
        //fx.GetComponent<FX_Skill_Anger>().Init(_duration);
    }
}
