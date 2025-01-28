using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스킬 부모
/// </summary>
public abstract class Skill
{
    public string ID;                   // 스킬 ID
    protected SkillDataSO _skillDataSO; // 스킬 데이터
    protected int _level;               // 현재 스킬의 레벨
    protected float _coolTime;          // 스킬 쿨타임
    protected float _currentTime;       // 쿨타임 체크할 현재시간

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill(CreateSkillArgs args)
    {
        ID = args.ID;
        _skillDataSO = args.SkillDataSO;
        _level = args.Level;
        _coolTime = _skillDataSO.CoolTime;
        _currentTime = 0;
    }

    /// <summary>
    /// 쿨타임 체크
    /// </summary>
    public bool CheckCoolTime()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _coolTime)
        {
            _currentTime %= _coolTime;
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// 현재 쿨타임 진행상황 가져오기
    /// </summary>
    public float Get_CurrentCoolTimeProgress()
    {
        return Mathf.Clamp01(_currentTime / _coolTime);
    }

    /// <summary>
    /// 스킬 실행
    /// </summary>
    public abstract void ExecuteSkill();

    /// <summary>
    /// 스킬 공격력 계산
    /// </summary>
    protected float Calculate_SkillAttackPower(float attackPercentage)
    {
        return PlayerStats.GetStatValue(StatType.AttackPower) * attackPercentage;
    }

    /// <summary>
    /// 특정 스킬스탯 값 가져오기 (현재레벨에 맞게)
    /// </summary>
    protected float GetSkillStatValue(SkillStatType skillStatType)
    {
        return _skillDataSO.GetSkillStats(_level)[skillStatType];
    }
}
