using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��ų �����Ҷ� ���Ǵ� �͵� ����ü
/// </summary>
public struct CreateSkillArgs
{
    public SkillDataSO SkillDataSO;
    public int Level;
}

/// <summary>
/// ��ų ���丮
/// </summary>
public class SkillFactory
{
    /// <summary>
    /// ��ų ���� (��ųID�� �� ��ųŬ������ �̸��� �����ؾ���)
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
                Debug.Log($"{id} �� �´� ��ų Ŭ������ ã�����߽��ϴ�.");

            CreateSkillArgs args = new CreateSkillArgs()
            {
                SkillDataSO = skillDataSO,
                Level = level
            };
            
            var skill = Activator.CreateInstance(skillClass, new object[] { args }) as Skill;
            
            return skill;
        }

        Debug.Log("��ų�� �������� ���߽��ϴ�.");
        return null;
    }
}
