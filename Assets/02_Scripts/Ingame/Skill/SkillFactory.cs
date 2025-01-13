using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��ų �ν��Ͻ� ���� (��ųID�� �� ��ųŬ������ �̸��� �����ؾ���)
/// </summary>
public class SkillFactory
{
    // ��ų ID �� ��ųŬ���� ���� ��ųʸ�
    private static readonly Dictionary<string, Func<SkillItem>> _skillCreatorDict;

    /// <summary>
    /// ���� ������ : ��ųʸ� �ʱ�ȭ
    /// </summary>
    static SkillFactory()
    {
        // 1. ��ųʸ� �ʱ�ȭ
        _skillCreatorDict = new Dictionary<string, Func<SkillItem>>();

        // 2. Reflection�� ����Ͽ� SkillItem�� ���� Ŭ������ �˻�
        var skillTypes = typeof(SkillItem).Assembly.GetTypes() // SkillItem Ŭ������ ���ǵ� ��������� ��� Ÿ�� ������ ������
            .Where(type => type.IsClass &&                     // Ŭ�������� Ȯ��
                   !type.IsAbstract &&                         // �߻� Ŭ������ ����
                   type.IsSubclassOf(typeof(SkillItem)));      // SkillItem�� ���� Ŭ������ ����

        // 3. ���� Ŭ�������� ��ųʸ��� �߰�
        foreach (var skillType in skillTypes)
        {
            // Ŭ���� �̸��� ��ų ID�� ���
            string skillID = skillType.Name;

            // ������ �Լ��� Func<SkillItem>���� ���� (ex) type�� SkillMeteor��� new SkillMeteor() �� ��������)
            _skillCreatorDict[skillID] = () => (SkillItem)Activator.CreateInstance(skillType); 
        }
    }

    /// <summary>
    /// ��ųID�� ������� ��ų ����
    /// </summary>
    public static SkillItem CreateSkill(string skillId)
    {
        // 4. ��ųʸ����� ��ų ID�� ������ �Լ� ȣ��
        if (_skillCreatorDict.TryGetValue(skillId, out Func<SkillItem> creator))
        {
            return creator();
        }

        // 5. ID�� ���� ��� null
        Debug.Log($"'{skillId}' �� �´� ��ųŬ������ �����Ҽ� �������ϴ�.");
        return null;
    }
}
