using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Egg : SkillItem
{
    public float Delay { get; private set; }                // ������ (��ų��Ÿ��)
    public float AttackPercentage { get; private set; }     // ���� �ۼ�Ƽ��
    public float Range { get; private set; }                // ��Ÿ�
    public float ProjectileCount { get; private set; }      // ����ü ����

    private float _skillAttackPower;                        // ��ų ���ݷ�

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public override void Init(SkillDataSO skillDataSO, int level)
    {
        base.Init(skillDataSO, level);
        UpdateAttributeValue(skillDataSO, level);
    }

    /// <summary>
    /// ������ ������
    /// </summary>
    public override void ItemLevelUp()
    {
        base.ItemLevelUp();
        UpdateAttributeValue(SkillDataSO, Level);
    }

    /// <summary>
    /// �Ӽ��� ������Ʈ
    /// </summary>
    private void UpdateAttributeValue(SkillDataSO skillDataSO, int level)
    {
        Delay = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.Delay, Level));
        AttackPercentage = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.AttackPercentage, Level));
        Range = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.Range, Level));
        ProjectileCount = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.ProjectileCount, Level));

        _skillAttackPower = PlayerStats.GetStatValue(StatType.AttackPower) * AttackPercentage;
    }

    /// <summary>
    /// ��Ÿ�� ���
    /// </summary>
    public override bool CheckCoolTime()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime > Delay)
        {
            CurrentTime %= Delay;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log("Skill_Egg!!");
    }

    /// <summary>
    /// ���� ��Ÿ�� �����Ȳ ��������
    /// </summary>
    public override float GetCurrentCoolTimeProgress()
    {
        return Mathf.Clamp01(CurrentTime / Delay);
    }

    /// <summary>
    /// �󼼰��� �����Ҵ�� Desc��������
    /// </summary>
    public override string GetDynamicDesc()
    {
        return string.Format(Desc, ProjectileCount, NumberConverter.ConvertPercentage(AttackPercentage));
    }
}
