using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Lazer : SkillItem
{
    public float AttackPercentage { get; private set; }     // 공격 퍼센티지
    public float ProjectileSpeed { get; private set; }      // 투사체 속도

    private float _skillAttackPower;                        // 스킬 공격력

    /// <summary>
    /// 속성값 업데이트
    /// </summary>
    protected override void UpdateAttributes()
    {
        AttackPercentage = GetAttributeValue_ByCurrentLevel(SkillAttributeType.AttackPercentage);
        ProjectileSpeed = GetAttributeValue_ByCurrentLevel(SkillAttributeType.ProjectileSpeed);
        
        _skillAttackPower = Calculate_SkillAttackPower(AttackPercentage);
    }

    /// <summary>
    /// 스킬 실행
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log("Skill_Lazer!!");

        //// 타겟 1명을 찾아서 설정해서 그 위치에 생성하고
        //Vector3 targetPos =
        //    (FieldTargetManager.GetClosestLivingTarget(PlayerSpawner.PlayerInstance.transform.position) as Component).
        //    transform.position;

        //// 프로젝타일 생성하고
        //SkillProjectile_Meteor projectile =
        //    GameObject.Instantiate(SkillDataSO.SkillPrefab, targetPos, Quaternion.identity).
        //    GetComponent<SkillProjectile_Meteor>();

        //// 치명타 여부 결정하고, 최종데미지 계산하고
        //bool isCritical = CriticalManager.IsCritical();
        //float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        //// 프로젝타일에 주입
        //projectile.Init(finalDamage, isCritical);
    }

    /// <summary>
    /// Desc가져오기 
    /// </summary>
    public override string GetDynamicDesc()
    {
        return string.Format(Desc, NumberConverter.ConvertPercentage(AttackPercentage));
    }
}
