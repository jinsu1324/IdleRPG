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
        Debug.Log("Skill_Thunder 생성자!");

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
        Debug.Log($"Skill_Thunder!! 공격력 : {_skillAttackPower}");

        //// 타겟 1명을 찾아서 설정해서 그 위치에 생성하고
        //Vector3 targetPos =
        //    (FieldTargetManager.GetClosestLivingTarget(PlayerSpawner.PlayerInstance.transform.position) as Component).
        //    transform.position;

        //// 프로젝타일 생성하고
        //SkillProjectile_Thunder projectile =
        //    GameObject.Instantiate(SkillDataSO.SkillPrefab, targetPos, Quaternion.identity).
        //    GetComponent<SkillProjectile_Thunder>();

        //// 치명타 여부 결정하고, 최종데미지 계산하고
        //bool isCritical = CriticalManager.IsCritical();
        //float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        //// 프로젝타일에 주입
        //projectile.Init(finalDamage, isCritical);
    }
}
