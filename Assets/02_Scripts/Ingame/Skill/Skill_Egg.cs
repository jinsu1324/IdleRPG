using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Egg : Skill
{
    private float _attackPercentage;     // ���� �ۼ�Ƽ��
    private float _range;                // ��Ÿ�
    private float _projectileCount;      // ����ü ����
    private float _skillAttackPower;     // ���� ��ų ���ݷ� 

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Egg(CreateSkillArgs args) : base(args)
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
        _projectileCount = GetSkillStatValue(SkillStatType.ProjectileCount);

        _skillAttackPower = Calculate_SkillAttackPower(_attackPercentage);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void ExecuteSkill()
    {
        IDamagable target = FieldTargetManager.GetClosestLivingTarget(PlayerManager.PlayerInstance.transform.position);

        if (target == null)
            return;

        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(ID);

        SkillProjectile_Egg projectile = GameObject.Instantiate(
                itemDataSO.Prefab,
                PlayerManager.PlayerInstance.transform.position,
                Quaternion.identity).
                GetComponent<SkillProjectile_Egg>();

        bool isCritical = CriticalManager.IsCritical();
        float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        projectile.Init(finalDamage, isCritical, target);
    }
}
