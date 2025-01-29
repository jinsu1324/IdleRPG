using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Egg : Skill
{
    private float _attackPercentage;     // 공격 퍼센티지
    private float _range;                // 사거리
    private float _projectileCount;      // 투사체 갯수
    private float _skillAttackPower;     // 최종 스킬 공격력 

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Egg(CreateSkillArgs args) : base(args)
    {
        Update_SkillStatValues();
    }

    /// <summary>
    /// 스킬스탯 값들 업데이트
    /// </summary>
    private void Update_SkillStatValues()
    {
        _attackPercentage = GetSkillStatValue(SkillStatType.AttackPercentage);
        _range = GetSkillStatValue(SkillStatType.Range);
        _projectileCount = GetSkillStatValue(SkillStatType.ProjectileCount);

        _skillAttackPower = Calculate_SkillAttackPower(_attackPercentage);
    }

    /// <summary>
    /// 스킬 실행
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
