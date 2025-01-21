using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 메테오 스킬
/// </summary>
public class Skill_Meteor : Skill
{
    private float _attackPercentage;     // 공격 퍼센티지
    private float _range;                // 사거리
    private float _skillAttackPower;     // 최종 스킬 공격력
    
    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Meteor(CreateSkillArgs args) : base(args)
    {
        Debug.Log("Skill_Meteor 생성자!");

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
        Debug.Log($"Skill_Meteor!! 공격력 : {_skillAttackPower}");

        //// 타겟 1명을 찾아서 설정해서 그 위치에 생성하고
        //Vector3 targetPos =
        //    (FieldTargetManager.GetClosestLivingTarget(PlayerManager.PlayerInstance.transform.position) as Component).
        //    transform.position;

        //// 프로젝타일 생성하고
        //SkillProjectile_Meteor projectile =
        //    GameObject.Instantiate(SkillDataSO.Prefab, targetPos, Quaternion.identity).
        //    GetComponent<SkillProjectile_Meteor>();

        //// 치명타 여부 결정하고, 최종데미지 계산하고
        //bool isCritical = CriticalManager.IsCritical();
        //float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        //// 프로젝타일에 주입
        //projectile.Init(finalDamage, isCritical);
    }

    ///// <summary>
    ///// Desc가져오기 
    ///// </summary>
    //public override string GetDynamicDesc()
    //{
    //    return string.Format(Desc, NumberConverter.ConvertPercentage(_attackPercentage));
    //}

}
