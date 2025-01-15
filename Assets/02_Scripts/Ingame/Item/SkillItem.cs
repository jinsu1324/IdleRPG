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
    public SkillDataSO SkillDataSO { get; private set; }                            // 스킬 데이터
    public Dictionary<SkillAttributeType, float> AttributeDict { get; private set; }  // 레벨에 맞는 속성들 딕셔너리
    public int Level { get; private set; }                                          // 레벨
    public int EnhanceableCount { get; private set; }                               // 강화 가능한 갯수
    public float CurrentTime { get; set; }                                          // 쿨타임 계산할 시간

    /// <summary>
    /// 초기화
    /// </summary>
    public virtual void Init(SkillDataSO skillDataSO, int level)
    {
        SkillDataSO = skillDataSO;
        ID = skillDataSO.ID;
        ItemType = (ItemType)Enum.Parse(typeof(ItemType), skillDataSO.ItemType);
        Name = skillDataSO.Name;
        Grade = skillDataSO.Grade;
        Desc = skillDataSO.Desc;
        Count = 1;
        Icon = skillDataSO.Icon;
        AttributeDict = new Dictionary<SkillAttributeType, float>(skillDataSO.GetAttributeDict_ByLevel(level));
        Level = level;
        EnhanceableCount = 10;
    }

    /// <summary>
    /// 아이템 레벨업
    /// </summary>
    public virtual void ItemLevelUp()
    {
        // 레벨업
        Level++;

        // 속성들 레벨에 맞게 최신화
        AttributeDict = new Dictionary<SkillAttributeType, float>(SkillDataSO.GetAttributeDict_ByLevel(Level));
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
    public abstract bool CheckCoolTime();

    /// <summary>
    /// 스킬 실행
    /// </summary>
    public abstract void ExecuteSkill();

    /// <summary>
    /// 현재 쿨타임 진행상황 가져오기
    /// </summary>
    public abstract float GetCurrentCoolTimeProgress();

    /// <summary>
    /// 상세값들 동적할당된 Desc가져오기
    /// </summary>
    public abstract string GetDynamicDesc();
}
