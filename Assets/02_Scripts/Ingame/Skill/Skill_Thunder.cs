using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Thunder : SkillItem
{
    public float Delay { get; private set; }                                        // ������ (��ų��Ÿ��)
    public float AttackPercentage { get; private set; }
    public float Range { get; private set; }
    public float ProjectileCount { get; private set; }
    public float SplashRadius { get; private set; }






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
        SplashRadius = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.SplashRadius, Level));
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
        Debug.Log("Skill_Thunder!!");
    }

    /// <summary>
    /// �󼼰��� �����Ҵ�� Desc��������
    /// </summary>
    public override string GetDynamicDesc()
    {
        return string.Format(Desc, ProjectileCount, AttackPercentage);
    }
}
