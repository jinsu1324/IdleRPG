using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ų ������
/// </summary>
public class SkillItem : Item, IEnhanceableItem
{
    public SkillDataSO SkillDataSO { get; private set; }    // ��ų ������
    public Dictionary<SkillAbilityType, int> AbilityDict { get; private set; }  // �����ϴ� �����Ƽ�� ��ųʸ�
    public int Level { get; private set; }
    public int EnhanceableCount { get; private set; }

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
    public bool CanEnhance() => Count >= EnhanceableCount;

    /// <summary>
    /// ���� ��������?
    /// </summary>
    public bool CanEquip() => true;

    public override T GetItemData<T>()
    {
        throw new NotImplementedException();
    }

    public override void UseItem()
    {
        throw new NotImplementedException();
    }
}
