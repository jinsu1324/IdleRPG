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
    public SkillDataSO SkillDataSO { get; private set; }    // ��ų ������
    public Dictionary<SkillAbilityType, int> AbilityDict { get; private set; }  // �����ϴ� �����Ƽ�� ��ųʸ�
    public int Level { get; private set; }
    public int EnhanceableCount { get; private set; }
    public float CurrentTime { get; private set; }
    public float Delay { get; private set; }

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
        Delay = 1; // Todo �ӽð�!!!
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
    /// ��Ÿ�� üũ
    /// </summary>
    /// <returns></returns>
    public bool CheckCoolTime()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime > Delay)
        {
            CurrentTime %= Delay;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public abstract void ExecuteSkill();
}
