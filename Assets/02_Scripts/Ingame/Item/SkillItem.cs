using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ų ������
/// </summary>
public abstract class SkillItem
{
    //[JsonIgnore] public SkillDataSO SkillDataSO { get; private set; }                                // ��ų ������

    ///// <summary>
    ///// �ʱ�ȭ
    ///// </summary>
    //public virtual void Init(SkillDataSO skillDataSO, int level)
    //{
    //    SkillDataSO = skillDataSO;
    //    ID = skillDataSO.ID;
    //    ItemType = (ItemType)Enum.Parse(typeof(ItemType), skillDataSO.ItemType);
    //    Name = skillDataSO.Name;
    //    Grade = skillDataSO.Grade;
    //    Desc = skillDataSO.Desc;
    //    Count = 1;
    //    Icon = skillDataSO.Icon;
    //    AttributeDict = new Dictionary<SkillAttributeType, float>(skillDataSO.GetAttributeDict_ByLevel(level));
    //    Level = level;
    //    EnhanceableCount = 10;
    //    CoolTime = skillDataSO.CoolTime;

    //    TryUpdateAttributes(); // �Ӽ����� ������Ʈ �õ�
    //}

    ///// <summary>
    ///// ������ ������
    ///// </summary>
    //public virtual void ItemLevelUp()
    //{
    //    // ������
    //    Level++;

    //    // �Ӽ��� ������ �°� �ֽ�ȭ
    //    AttributeDict = new Dictionary<SkillAttributeType, float>(SkillDataSO.GetAttributeDict_ByLevel(Level));

    //    // �Ӽ����� ������Ʈ �õ�
    //    TryUpdateAttributes();
    //}

    ///// <summary>
    ///// ������ ������ ��ȭ ������ŭ �Һ�
    ///// </summary>
    //public void RemoveCountByEnhance()
    //{
    //    Count -= EnhanceableCount;
    //}

    ///// <summary>
    ///// ��ȭ ��������?
    ///// </summary>
    //public bool CanEnhance()
    //{
    //    return Count >= EnhanceableCount;
    //}

    ///// <summary>
    ///// ��Ÿ�� üũ
    ///// </summary>
    //public bool CheckCoolTime()
    //{
    //    CurrentTime += Time.deltaTime;

    //    if (CurrentTime > CoolTime)
    //    {
    //        CurrentTime %= CoolTime;
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    ///// <summary>
    ///// ���� ��Ÿ�� �����Ȳ ��������
    ///// </summary>
    //public float GetCurrentCoolTimeProgress()
    //{
    //    return Mathf.Clamp01(CurrentTime / CoolTime);
    //}

    ///// <summary>
    ///// �Ӽ����� ������Ʈ �õ�
    ///// </summary>
    //protected void TryUpdateAttributes()
    //{
    //    // ������Ʈ �Ұ��ϸ� �׳� ����
    //    if (CanUpdateAttributes() == false)
    //        return;

    //    // ���� �Ӽ����� ������Ʈ
    //    UpdateAttributes();
    //}

    ///// <summary>
    ///// ���� �Ӽ����� ������Ʈ
    ///// </summary>
    //protected abstract void UpdateAttributes();

    ///// <summary>
    ///// ��ų ����
    ///// </summary>
    //public abstract void ExecuteSkill();

    ///// <summary>
    ///// �󼼰��� �����Ҵ�� Desc��������
    ///// </summary>
    //public abstract string GetDynamicDesc();

    ///// <summary>
    ///// �Ӽ����� ������Ʈ ��������?
    ///// </summary>
    //protected bool CanUpdateAttributes()
    //{
    //    if (SkillDataSO == null || Level == 0)
    //    {
    //        Debug.Log("��ų �Ӽ��� ������Ʈ �Ұ�");
    //        return false;
    //    }
    //    else
    //        return true;
    //}

    ///// <summary>
    ///// ���� ������ �´� �Ӽ��� ��������
    ///// </summary>
    //protected float GetAttributeValue_ByCurrentLevel(SkillAttributeType skillAttributeType)
    //{
    //    if (float.TryParse(SkillDataSO.GetAttributeValue(skillAttributeType, Level), out float resultValue))
    //        return resultValue;
    //    else
    //        return 0;
    //}

    ///// <summary>
    ///// ��ų ���ݷ� ���
    ///// </summary>
    //protected float Calculate_SkillAttackPower(float attackPercentage)
    //{
    //    return PlayerStats.GetStatValue(StatType.AttackPower) * attackPercentage;
    //}
}
