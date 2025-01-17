using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Lazer : SkillItem
{
    public float AttackPercentage { get; private set; }     // ���� �ۼ�Ƽ��
    public float ProjectileSpeed { get; private set; }      // ����ü �ӵ�

    private float _skillAttackPower;                        // ��ų ���ݷ�

    /// <summary>
    /// �Ӽ��� ������Ʈ
    /// </summary>
    protected override void UpdateAttributes()
    {
        AttackPercentage = GetAttributeValue_ByCurrentLevel(SkillAttributeType.AttackPercentage);
        ProjectileSpeed = GetAttributeValue_ByCurrentLevel(SkillAttributeType.ProjectileSpeed);
        
        _skillAttackPower = Calculate_SkillAttackPower(AttackPercentage);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log("Skill_Lazer!!");

        //// Ÿ�� 1���� ã�Ƽ� �����ؼ� �� ��ġ�� �����ϰ�
        //Vector3 targetPos =
        //    (FieldTargetManager.GetClosestLivingTarget(PlayerSpawner.PlayerInstance.transform.position) as Component).
        //    transform.position;

        //// ������Ÿ�� �����ϰ�
        //SkillProjectile_Meteor projectile =
        //    GameObject.Instantiate(SkillDataSO.SkillPrefab, targetPos, Quaternion.identity).
        //    GetComponent<SkillProjectile_Meteor>();

        //// ġ��Ÿ ���� �����ϰ�, ���������� ����ϰ�
        //bool isCritical = CriticalManager.IsCritical();
        //float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        //// ������Ÿ�Ͽ� ����
        //projectile.Init(finalDamage, isCritical);
    }

    /// <summary>
    /// Desc�������� 
    /// </summary>
    public override string GetDynamicDesc()
    {
        return string.Format(Desc, NumberConverter.ConvertPercentage(AttackPercentage));
    }
}
