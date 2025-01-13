using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 스킬 아이템
/// </summary>
public abstract class SkillItem : Item, IEnhanceableItem, ISkill
{
    public SkillDataSO SkillDataSO { get; private set; }    // 스킬 데이터
    public Dictionary<SkillAbilityType, int> AbilityDict { get; private set; }  // 제공하는 어빌리티들 딕셔너리
    public int Level { get; private set; }
    public int EnhanceableCount { get; private set; }
    public float CurrentTime { get; private set; }
    public float Delay { get; private set; }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(SkillDataSO skillDataSO, int level)
    {
        SkillDataSO = skillDataSO;
        ID = skillDataSO.ID;
        ItemType = (ItemType)Enum.Parse(typeof(ItemType), skillDataSO.ItemType);
        Name = skillDataSO.Name;
        Grade = skillDataSO.Grade;
        Icon = skillDataSO.Icon;
        Desc = skillDataSO.Desc;
        Level = level;
        Count = 1;
        EnhanceableCount = 10;
        Delay = 1; // Todo 임시값!!!
        AbilityDict = new Dictionary<SkillAbilityType, int>(skillDataSO.GetAbilityDict_ByLevel(level));
    }

    /// <summary>
    /// 아이템 레벨업
    /// </summary>
    public void ItemLevelUp()
    { 
        Level++; // 레벨업
        AbilityDict = new Dictionary<SkillAbilityType, int>(SkillDataSO.GetAbilityDict_ByLevel(Level));  // 레벨에 맞는 새로운 스탯들 적용
    }

    /// <summary>
    /// 아이템 갯수를 강화 갯수만큼 소비
    /// </summary>
    public void RemoveCountByEnhance() => Count -= EnhanceableCount;

    /// <summary>
    /// 강화 가능한지?
    /// </summary>
    public bool CanEnhance() => Count >= EnhanceableCount;

    /// <summary>
    /// 쿨타임 체크
    /// </summary>
    /// <returns></returns>
    public bool CheckCoolTime()
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
    public abstract void ExecuteSkill();
}
