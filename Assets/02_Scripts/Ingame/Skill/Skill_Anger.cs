using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Anger : SkillItem
{
    public float Delay { get; private set; }            // ������ (��ų��Ÿ��)
    public float AddAttackSpeed { get; private set; }   // ���ݼӵ� �߰���              
    public float Duration { get; private set; }         // ��ų���ӽð�

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
        AddAttackSpeed = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.AddAttackSpeed, Level));
        Duration = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.Duration, Level));
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
        Debug.Log("Skill_Anger!!");

        // ���� ����
        Dictionary<StatType, float> buffDict = new Dictionary<StatType, float>() { { StatType.AttackSpeed, AddAttackSpeed} };
        PlayerBuffSystem.Instance.StartBuffToPlayer(Duration, buffDict, this);

        // ����Ʈ ����
        GameObject fx = FXManager.Instance.SpawnFX(FXName.FX_Skill_Anger, PlayerSpawner.PlayerInstance.transform);
        fx.GetComponent<FX_Skill_Anger>().Init(Duration);
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
        return string.Format(Desc, Duration, AddAttackSpeed);
    }
}
