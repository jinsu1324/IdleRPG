using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Lazer : Skill
{
    private float _attackPercentage;     // ���� �ۼ�Ƽ��
    private float _projectileSpeed;      // ����ü �ӵ�
    private float _skillAttackPower;    // ��ų ���ݷ�

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Lazer(CreateSkillArgs args) : base(args)
    {
        Debug.Log("Skill_Lazer ������!");

        Update_SkillStatValues();
    }

    /// <summary>
    /// ��ų���� ���� ������Ʈ
    /// </summary>
    private void Update_SkillStatValues()
    {
        _attackPercentage = GetSkillStatValue(SkillStatType.AttackPercentage);
        _projectileSpeed = GetSkillStatValue(SkillStatType.ProjectileSpeed);

        _skillAttackPower = Calculate_SkillAttackPower(_attackPercentage);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log($"Skill_Lazer!! ���ݷ� : {_skillAttackPower}");
    }
}
