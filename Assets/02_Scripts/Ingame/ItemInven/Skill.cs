using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ų ������
/// </summary>
public class Skill : IItem
{
    // IItem ���
    public string ID { get; private set; }                  // ID
    public string Name { get; private set; }                // �̸�
    public string Grade { get; private set; }               // ���
    public int Level { get; private set; }                  // ����
    public ItemType ItemType { get; private set; }          // ������ Ÿ��
    public int Count { get; private set; }                  // ����
    public int EnhanceableCount { get; private set; }       // ��ȭ ���� ����
    public Sprite Icon { get; private set; }                // ������

    // Skill ����
    public SkillDataSO SkillDataSO { get; private set; }    // ��ų ������
    public string Desc { get; private set; }                // ����
    public Dictionary<SkillAbilityType, int> AbilityDict { get; private set; }  // �����ϴ� �����Ƽ�� ��ųʸ�

    /// <summary>
    /// �ʱ�ȭ
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
    /// ������ ���� �߰�
    /// </summary>
    public void AddCount() => Count++;

    /// <summary>
    /// ������ ������
    /// </summary>
    public void ItemLevelUp()
    { 
        Level++; // ������
        AbilityDict = new Dictionary<SkillAbilityType, int>(SkillDataSO.GetAbilityDict_ByLevel(Level));  // ������ �´� ���ο� ���ȵ� ����
    }

    /// <summary>
    /// ������ ������ ��ȭ ������ŭ �Һ�
    /// </summary>
    public void RemoveCountByEnhance() => Count -= EnhanceableCount;

    /// <summary>
    /// ��ȭ ��������?
    /// </summary>
    public bool IsEnhanceable() => Count >= EnhanceableCount;
}
