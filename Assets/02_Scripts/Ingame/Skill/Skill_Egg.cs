using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Egg : SkillItem
{
    [JsonIgnore] public float AttackPercentage { get; private set; }     // ���� �ۼ�Ƽ��
    [JsonIgnore] public float Range { get; private set; }                // ��Ÿ�
    [JsonIgnore] public float ProjectileCount { get; private set; }      // ����ü ����

    [JsonIgnore] private float _skillAttackPower;                        // ��ų ���ݷ�

    /// <summary>
    /// �Ӽ��� ������Ʈ
    /// </summary>
    protected override void UpdateAttributes()
    {
        AttackPercentage = GetAttributeValue_ByCurrentLevel(SkillAttributeType.AttackPercentage);
        Range = GetAttributeValue_ByCurrentLevel(SkillAttributeType.Range);
        ProjectileCount = GetAttributeValue_ByCurrentLevel(SkillAttributeType.ProjectileCount);

        _skillAttackPower = Calculate_SkillAttackPower(AttackPercentage);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log("Skill_Egg!!");
    }

    /// <summary>
    /// Desc�������� 
    /// </summary>
    public override string GetDynamicDesc()
    {
        return string.Format(Desc, ProjectileCount, NumberConverter.ConvertPercentage(AttackPercentage));
    }
}
