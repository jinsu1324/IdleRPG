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
        IDamagable target = FieldTargetManager.GetClosestLivingTarget(PlayerManager.PlayerInstance.transform.position, _range);
        if (target == null)
            return;

        Vector3 targetPos = (target as Component).transform.position;
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(ID);

        SkillProjectile_Thunder projectile = GameObject.Instantiate(itemDataSO.Prefab, targetPos, Quaternion.identity).GetComponent<SkillProjectile_Thunder>();

        bool isCritical = CriticalManager.IsCritical();
        float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        projectile.Init(finalDamage, isCritical);
    }
}
