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
        //Debug.Log($"Skill_Meteor!! 공격력 : {_skillAttackPower}");

        Vector3 targetPos = (FieldTargetManager.GetClosestLivingTarget(PlayerManager.PlayerInstance.transform.position) as Component).transform.position;
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(ID);

        SkillProjectile_Meteor projectile = GameObject.Instantiate(itemDataSO.Prefab, targetPos, Quaternion.identity).GetComponent<SkillProjectile_Meteor>();

        bool isCritical = CriticalManager.IsCritical();
        float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        projectile.Init(finalDamage, isCritical);
    }
}
