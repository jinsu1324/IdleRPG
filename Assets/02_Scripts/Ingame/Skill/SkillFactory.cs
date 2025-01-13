using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFactory
{
    // 스킬 ID 별 스킬클래스 생성 딕셔너리
    private static readonly Dictionary<string, Func<SkillItem>> _skillCreatorDict = new Dictionary<string, Func<SkillItem>>()
    {
        { "Skill_Meteor", () => new SkillMeteor()},
        { "Skill_Anger", () => new SkillAnger()},
        { "Skill_Egg", () => new SkillEgg()},
        { "Skill_Thunder", () => new SkillThunder()}
    };

    /// <summary>
    /// 스킬ID를 기반으로 스킬 생성
    /// </summary>
    public static SkillItem CreateSkill(string skillId)
    {
        if (_skillCreatorDict.TryGetValue(skillId, out Func<SkillItem> creator))
        {
            return creator();
        }

        Debug.Log($"'{skillId}' 에 맞는 스킬클래스를 생성할수 없었습니다.");
        return null;
    }
}
