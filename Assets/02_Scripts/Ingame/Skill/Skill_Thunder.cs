using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 썬더볼트 스킬
/// </summary>
public class Skill_Thunder : SkillItem
{
    //[JsonIgnore] public float AttackPercentage { get; private set; }     // 공격 퍼센티지
    //[JsonIgnore] public float Range { get; private set; }                // 사거리

    //[JsonIgnore] private float _skillAttackPower;                        // 스킬 공격력

    ///// <summary>
    ///// 속성값 업데이트
    ///// </summary>
    //protected override void UpdateAttributes()
    //{
    //    AttackPercentage = GetAttributeValue_ByCurrentLevel(SkillAttributeType.AttackPercentage);
    //    Range = GetAttributeValue_ByCurrentLevel(SkillAttributeType.Range);

    //    _skillAttackPower = Calculate_SkillAttackPower(AttackPercentage);
    //}

    ///// <summary>
    ///// 스킬 실행
    ///// </summary>
    //public override void ExecuteSkill()
    //{
    //    Debug.Log("Skill_Thunder!!");

    //    //// 타겟 1명을 찾아서 설정해서 그 위치에 생성하고
    //    //Vector3 targetPos =
    //    //    (FieldTargetManager.GetClosestLivingTarget(PlayerSpawner.PlayerInstance.transform.position) as Component).
    //    //    transform.position;

    //    //// 프로젝타일 생성하고
    //    //SkillProjectile_Thunder projectile =
    //    //    GameObject.Instantiate(SkillDataSO.SkillPrefab, targetPos, Quaternion.identity).
    //    //    GetComponent<SkillProjectile_Thunder>();

    //    //// 치명타 여부 결정하고, 최종데미지 계산하고
    //    //bool isCritical = CriticalManager.IsCritical();
    //    //float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

    //    //// 프로젝타일에 주입
    //    //projectile.Init(finalDamage, isCritical);
    //}

    ///// <summary>
    ///// Desc가져오기
    ///// </summary>
    //public override string GetDynamicDesc()
    //{
    //    return string.Format(Desc, NumberConverter.ConvertPercentage(AttackPercentage));
    //}
}
