using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Ʈ ��ų
/// </summary>
public class Skill_Thunder : Skill
{
    private float _attackPercentage;     // ���� �ۼ�Ƽ��
    private float _range;                // ��Ÿ�
    private float _skillAttackPower;     // ���� ��ų ���ݷ�

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Thunder(CreateSkillArgs args) : base(args)
    {
        Debug.Log("Skill_Thunder ������!");

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
        Debug.Log($"Skill_Thunder!! ���ݷ� : {_skillAttackPower}");

        //// Ÿ�� 1���� ã�Ƽ� �����ؼ� �� ��ġ�� �����ϰ�
        //Vector3 targetPos =
        //    (FieldTargetManager.GetClosestLivingTarget(PlayerSpawner.PlayerInstance.transform.position) as Component).
        //    transform.position;

        //// ������Ÿ�� �����ϰ�
        //SkillProjectile_Thunder projectile =
        //    GameObject.Instantiate(SkillDataSO.SkillPrefab, targetPos, Quaternion.identity).
        //    GetComponent<SkillProjectile_Thunder>();

        //// ġ��Ÿ ���� �����ϰ�, ���������� ����ϰ�
        //bool isCritical = CriticalManager.IsCritical();
        //float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        //// ������Ÿ�Ͽ� ����
        //projectile.Init(finalDamage, isCritical);
    }
}
