using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 썬더볼트 스킬
/// </summary>
public class Skill_Thunder : Skill
{
    private float _attackPercentage;     // 공격 퍼센티지
    private float _range;                // 사거리
    private float _skillAttackPower;     // 최종 스킬 공격력

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Thunder(CreateSkillArgs args) : base(args)
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

        _skillAttackPower = Calculate_SkillAttackPower(_attackPercentage);
    }

    /// <summary>
    /// 스킬 실행
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
