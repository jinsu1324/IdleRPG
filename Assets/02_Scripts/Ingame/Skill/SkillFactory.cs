using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFactory
{
    // ��ų ID �� ��ųŬ���� ���� ��ųʸ�
    private static readonly Dictionary<string, Func<SkillItem>> _skillCreatorDict = new Dictionary<string, Func<SkillItem>>()
    {
        { "Skill_Meteor", () => new SkillMeteor()},
        { "Skill_Anger", () => new SkillAnger()},
        { "Skill_Egg", () => new SkillEgg()},
        { "Skill_Thunder", () => new SkillThunder()}
    };

    /// <summary>
    /// ��ųID�� ������� ��ų ����
    /// </summary>
    public static SkillItem CreateSkill(string skillId)
    {
        if (_skillCreatorDict.TryGetValue(skillId, out Func<SkillItem> creator))
        {
            return creator();
        }

        Debug.Log($"'{skillId}' �� �´� ��ųŬ������ �����Ҽ� �������ϴ�.");
        return null;
    }
}
