using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Egg : SkillItem
{
    public float Delay { get; private set; }                // 딜레이 (스킬쿨타임)
    public float AttackPercentage { get; private set; }     // 공격 퍼센티지
    public float Range { get; private set; }                // 사거리
    public float ProjectileCount { get; private set; }      // 투사체 갯수

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
        ProjectileCount = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.ProjectileCount, Level));

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
        Debug.Log("Skill_Egg!!");
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
        return string.Format(Desc, ProjectileCount, NumberConverter.ConvertPercentage(AttackPercentage));
    }
}
