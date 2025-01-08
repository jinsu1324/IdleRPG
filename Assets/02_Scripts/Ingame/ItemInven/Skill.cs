using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 스킬 아이템
/// </summary>
public class Skill : IItem
{
    // IItem 상속
    public string ID { get; private set; }                  // ID
    public string Name { get; private set; }                // 이름
    public string Grade { get; private set; }               // 등급
    public int Level { get; private set; }                  // 레벨
    public ItemType ItemType { get; private set; }          // 아이템 타입
    public int Count { get; private set; }                  // 갯수
    public int EnhanceableCount { get; private set; }       // 강화 가능 갯수
    public Sprite Icon { get; private set; }                // 아이콘

    // Skill 고유
    public SkillDataSO SkillDataSO { get; private set; }    // 스킬 데이터
    public string Desc { get; private set; }                // 설명
    public Dictionary<SkillAbilityType, int> AbilityDict { get; private set; }  // 제공하는 어빌리티들 딕셔너리

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
        AbilityDict = new Dictionary<SkillAbilityType, int>(skillDataSO.GetAbilityDict_ByLevel(level));
    }

    /// <summary>
    /// 아이템 갯수 추가
    /// </summary>
    public void AddCount() => Count++;

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
    public bool IsEnhanceable() => Count >= EnhanceableCount;
}
