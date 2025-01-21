using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 스킬 생성할때 사용되는 것들 구조체
/// </summary>
public struct CreateSkillArgs
{
    public SkillDataSO SkillDataSO;
    public int Level;
}

/// <summary>
/// 스킬 팩토리
/// </summary>
public class SkillFactory
{
    /// <summary>
    /// 스킬 생성 (스킬ID와 각 스킬클래스의 이름이 동일해야함)
    /// </summary>
    public static Skill CreateSkill(string id, int level)
    {
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(id);

        if (itemDataSO is SkillDataSO skillDataSO)
        {
            var skillClass = typeof(Skill).Assembly.GetTypes().FirstOrDefault(type => type.IsClass &&
                                                                              !type.IsAbstract &&
                                                                              type.IsSubclassOf(typeof(Skill)) &&
                                                                              type.Name == id);
            if (skillClass == null)
                Debug.Log($"{id} 에 맞는 스킬 클래스를 찾지못했습니다.");

            CreateSkillArgs args = new CreateSkillArgs()
            {
                SkillDataSO = skillDataSO,
                Level = level
            };
            
            var skill = Activator.CreateInstance(skillClass, new object[] { args }) as Skill;
            
            return skill;
        }

        Debug.Log("스킬을 생성하지 못했습니다.");
        return null;
    }
}
