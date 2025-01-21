using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Egg : Skill
{
    private float _attackPercentage;     // ���� �ۼ�Ƽ��
    private float _range;                // ��Ÿ�
    private float _projectileCount;      // ����ü ����
    private float _skillAttackPower;     // ���� ��ų ���ݷ� 

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Egg(CreateSkillArgs args) : base(args)
    {
        Debug.Log("Skill_Egg ������!");

        Update_SkillStatValues();
    }

    /// <summary>
    /// ��ų���� ���� ������Ʈ
    /// </summary>
    private void Update_SkillStatValues()
    {
        _attackPercentage = GetSkillStatValue(SkillStatType.AttackPercentage);
        _range = GetSkillStatValue(SkillStatType.Range);
        _projectileCount = GetSkillStatValue(SkillStatType.ProjectileCount);

        _skillAttackPower = Calculate_SkillAttackPower(_attackPercentage);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log($"Skill_Egg!! ���ݷ� : {_skillAttackPower}");
    }

    ///// <summary>
    ///// Desc�������� 
    ///// </summary>
    //public override string GetDynamicDesc()
    //{
    //    return string.Format(Desc, _projectileCount, NumberConverter.ConvertPercentage(_attackPercentage));
    //}
}
