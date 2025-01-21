using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���׿� ��ų
/// </summary>
public class Skill_Meteor : Skill
{
    private float _attackPercentage;     // ���� �ۼ�Ƽ��
    private float _range;                // ��Ÿ�
    private float _skillAttackPower;     // ���� ��ų ���ݷ�
    
    /// <summary>
    /// ������
    /// </summary>
    public Skill_Meteor(CreateSkillArgs args) : base(args)
    {
        Debug.Log("Skill_Meteor ������!");

        Update_SkillStatValues();
    }

    /// <summary>
    /// ��ų���� ���� ������Ʈ
    /// </summary>
    private void Update_SkillStatValues()
    {
        _attackPercentage = GetSkillStatValue(SkillStatType.AttackPercentage);
        _range = GetSkillStatValue(SkillStatType.Range);

        _skillAttackPower = Calculate_SkillAttackPower(_attackPercentage);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log($"Skill_Meteor!! ���ݷ� : {_skillAttackPower}");

        //// Ÿ�� 1���� ã�Ƽ� �����ؼ� �� ��ġ�� �����ϰ�
        //Vector3 targetPos =
        //    (FieldTargetManager.GetClosestLivingTarget(PlayerManager.PlayerInstance.transform.position) as Component).
        //    transform.position;

        //// ������Ÿ�� �����ϰ�
        //SkillProjectile_Meteor projectile =
        //    GameObject.Instantiate(SkillDataSO.Prefab, targetPos, Quaternion.identity).
        //    GetComponent<SkillProjectile_Meteor>();

        //// ġ��Ÿ ���� �����ϰ�, ���������� ����ϰ�
        //bool isCritical = CriticalManager.IsCritical();
        //float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        //// ������Ÿ�Ͽ� ����
        //projectile.Init(finalDamage, isCritical);
    }

    ///// <summary>
    ///// Desc�������� 
    ///// </summary>
    //public override string GetDynamicDesc()
    //{
    //    return string.Format(Desc, NumberConverter.ConvertPercentage(_attackPercentage));
    //}

}
