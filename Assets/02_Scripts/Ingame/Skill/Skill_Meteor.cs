using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_Meteor : SkillItem
{
    public float Delay { get; private set; }                // ������ (��ų��Ÿ��)
    public float AttackPercentage { get; private set; }     // ���� �ۼ�Ƽ��
    public float Range { get; private set; }                // ��Ÿ�
    public float SplashRadius { get; private set; }         // ���� ���÷��� ����

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
        SplashRadius = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.SplashRadius, Level));

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
        Debug.Log("Skill_Meteor!!");

        // Ÿ�� 1���� ã�Ƽ� �����ؼ� �� ��ġ�� �����ϰ�
        Vector3 targetPos = 
            (FieldTargetManager.GetClosestLivingTarget(PlayerSpawner.PlayerInstance.transform.position) as Component).
            transform.position;

        // ������Ÿ�� �����ϰ�
        SkillProjectile_Meteor projectile = 
            GameObject.Instantiate(SkillDataSO.SkillPrefab, targetPos, Quaternion.identity).
            GetComponent<SkillProjectile_Meteor>();

        // ġ��Ÿ ���� �����ϰ�, ���������� ����ϰ�
        bool isCritical = CriticalManager.IsCritical();
        float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        // ������Ÿ�Ͽ� ����
        projectile.Init(finalDamage, isCritical);



        // �� ������Ÿ���� �������� �ִϸ��̼Ǹ� �ְ�, �״����� �˾Ƽ� ������ ������ (����go �� collider����)

        // collider �����ؼ� ������ �ֱ�

        // �˾Ƽ� �������



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
        return string.Format(Desc, AttackPercentage);
    }
}
