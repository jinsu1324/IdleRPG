using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_Meteor : SkillItem
{
    public float Delay { get; private set; }                // 딜레이 (스킬쿨타임)
    public float AttackPercentage { get; private set; }     // 공격 퍼센티지
    public float Range { get; private set; }                // 사거리
    public float SplashRadius { get; private set; }         // 공격 스플래시 범위

    private float _skillAttackPower;                        // 스킬 공격력


    /// <summary>
    /// 초기화
    /// </summary>
    public override void Init(SkillDataSO skillDataSO, int level)
    {
        base.Init(skillDataSO, level); 
        UpdateAttributeValue(skillDataSO, level);
    }

    /// <summary>
    /// 아이템 레벨업
    /// </summary>
    public override void ItemLevelUp()
    {
        base.ItemLevelUp();
        UpdateAttributeValue(SkillDataSO, Level);
    }

    /// <summary>
    /// 속성값 업데이트
    /// </summary>
    private void UpdateAttributeValue(SkillDataSO skillDataSO, int level)
    {
        Delay = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.Delay, Level));
        AttackPercentage = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.AttackPercentage, Level));
        Range = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.Range, Level));
        SplashRadius = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.SplashRadius, Level));

        _skillAttackPower = PlayerStats.GetStatValue(StatType.AttackPower) * AttackPercentage;
    }

    /// <summary>
    /// 쿨타임 계산
    /// </summary>
    public override bool CheckCoolTime()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime > Delay)
        {
            CurrentTime %= Delay;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 스킬 실행
    /// </summary>
    public override void ExecuteSkill()
    {
        Debug.Log("Skill_Meteor!!");

        // 타겟 1명을 찾아서 설정해서 그 위치에 생성하고
        Vector3 targetPos = 
            (FieldTargetManager.GetClosestLivingTarget(PlayerSpawner.PlayerInstance.transform.position) as Component).
            transform.position;

        // 프로젝타일 생성하고
        SkillProjectile_Meteor projectile = 
            GameObject.Instantiate(SkillDataSO.SkillPrefab, targetPos, Quaternion.identity).
            GetComponent<SkillProjectile_Meteor>();

        // 치명타 여부 결정하고, 최종데미지 계산하고
        bool isCritical = CriticalManager.IsCritical();
        float finalDamage = CriticalManager.CalculateFinalDamage(_skillAttackPower, isCritical);

        // 프로젝타일에 주입
        projectile.Init(finalDamage, isCritical);



        // 그 프로젝타일은 내려오는 애니메이션만 있고, 그다음에 알아서 폭팔이 켜지고 (폭발go 에 collider있음)

        // collider 감지해서 데미지 주기

        // 알아서 사라지기



    }

    /// <summary>
    /// 현재 쿨타임 진행상황 가져오기
    /// </summary>
    public override float GetCurrentCoolTimeProgress()
    {
        return Mathf.Clamp01(CurrentTime / Delay);
    }

    /// <summary>
    /// 상세값들 동적할당된 Desc가져오기
    /// </summary>
    public override string GetDynamicDesc()
    {
        return string.Format(Desc, AttackPercentage);
    }
}
