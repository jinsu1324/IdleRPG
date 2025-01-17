using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 분노 스킬
/// </summary>
public class Skill_Anger : SkillItem
{
    public float Delay { get; private set; }            // 딜레이 (스킬쿨타임)
    public float AddAttackSpeed { get; private set; }   // 공격속도 추가량              
    public float Duration { get; private set; }         // 스킬지속시간

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
        AddAttackSpeed = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.AddAttackSpeed, Level));
        Duration = float.Parse(SkillDataSO.GetAttributeValue(SkillAttributeType.Duration, Level));
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
        Debug.Log("Skill_Anger!!");

        // 버프 딕셔너리 제작
        Dictionary<StatType, float> buffDict = new Dictionary<StatType, float>() 
        { 
            { StatType.AttackSpeed, AddAttackSpeed}
        };

        // 버프 적용
        PlayerStatUpdateArgs args = new PlayerStatUpdateArgs()
        {
            AttributeDict = buffDict,
            Source = this,
        };
        PlayerBuffSystem.Instance.StartBuffToPlayer(Duration, args);

        // 이펙트 시작
        GameObject fx = FXManager.Instance.SpawnFX(FXName.FX_Skill_Anger, PlayerManager.PlayerInstance.transform);
        fx.GetComponent<FX_Skill_Anger>().Init(Duration);
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
        return string.Format(Desc, Duration, AddAttackSpeed);
    }
}
