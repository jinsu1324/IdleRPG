using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���׿� ��ų
/// </summary>
public class Skill_Meteor : SkillItem
{
    //[JsonIgnore] public float AttackPercentage { get; private set; }     // ���� �ۼ�Ƽ��
    //[JsonIgnore] public float Range { get; private set; }                // ��Ÿ�

    //[JsonIgnore] private float _skillAttackPower;                        // ��ų ���ݷ�

    ///// <summary>
    ///// �Ӽ��� ������Ʈ
    ///// </summary>
    //protected override void UpdateAttributes()
    //{
    //    AttackPercentage = GetAttributeValue_ByCurrentLevel(SkillAttributeType.AttackPercentage);
    //    Range = GetAttributeValue_ByCurrentLevel(SkillAttributeType.Range);
        
    //    _skillAttackPower = Calculate_SkillAttackPower(AttackPercentage);
    //}

    ///// <summary>
    ///// ��ų ����
    ///// </summary>
    //public override void ExecuteSkill()
    //{
    //    Debug.Log("Skill_Meteor!!");

    //    // Ÿ�� 1���� ã�Ƽ� �����ؼ� �� ��ġ�� �����ϰ�
    //    Vector3 targetPos = 
    //        (FieldTargetManager.GetClosestLivingTarget(PlayerManager.PlayerInstance.transform.position) as Component).
    //        transform.position;

    //    // ������Ÿ�� �����ϰ�
    //    SkillProjectile_Meteor projectile = 
    //        GameObject.Instantiate(SkillDataSO.Prefab, targetPos, Quaternion.identity).
    //        GetComponent<SkillProjectile_Meteor>();

    //    // ġ��Ÿ ���� �����ϰ�, ���������� ����ϰ�
    //    bool isCritical = CriticalManager.IsCritical();
    //    float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

    //    // ������Ÿ�Ͽ� ����
    //    projectile.Init(finalDamage, isCritical);
    //}

    ///// <summary>
    ///// Desc�������� 
    ///// </summary>
    //public override string GetDynamicDesc()
    //{
    //    return string.Format(Desc, NumberConverter.ConvertPercentage(AttackPercentage));
    //}
}
