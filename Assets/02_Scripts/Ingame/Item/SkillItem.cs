using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ų ������
/// </summary>
public abstract class SkillItem : Item, IEnhanceableItem, ISkill
{
    public SkillDataSO SkillDataSO { get; private set; }                            // ��ų ������
    public Dictionary<SkillAttributeType, float> AttributeDict { get; private set; }  // ������ �´� �Ӽ��� ��ųʸ�
    public int Level { get; private set; }                                          // ����
    public int EnhanceableCount { get; private set; }                               // ��ȭ ������ ����
    public float CurrentTime { get; set; }                                          // ��Ÿ�� ����� �ð�

    /// <summary>
    /// �ʱ�ȭ
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
    /// ������ ������
    /// </summary>
    public virtual void ItemLevelUp()
    {
        // ������
        Level++;

        // �Ӽ��� ������ �°� �ֽ�ȭ
        AttributeDict = new Dictionary<SkillAttributeType, float>(SkillDataSO.GetAttributeDict_ByLevel(Level));
    }

    /// <summary>
    /// ������ ������ ��ȭ ������ŭ �Һ�
    /// </summary>
    public void RemoveCountByEnhance() => Count -= EnhanceableCount;

    /// <summary>
    /// ��ȭ ��������?
    /// </summary>
    public bool CanEnhance() => Count >= EnhanceableCount;

    /// <summary>
    /// ��Ÿ�� üũ
    /// </summary>
    public abstract bool CheckCoolTime();

    /// <summary>
    /// ��ų ����
    /// </summary>
    public abstract void ExecuteSkill();

    /// <summary>
    /// ���� ��Ÿ�� �����Ȳ ��������
    /// </summary>
    public abstract float GetCurrentCoolTimeProgress();

    /// <summary>
    /// �󼼰��� �����Ҵ�� Desc��������
    /// </summary>
    public abstract string GetDynamicDesc();
}
