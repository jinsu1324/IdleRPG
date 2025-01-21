using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��ų ĳ����
/// </summary>
public class SkillCaster : MonoBehaviour
{
    public static event Action<Skill[]> OnUpdateCastSkills;     // ĳ������ ��ų���� �ٲ������ �̺�Ʈ
    private static Skill[] _castSkillArr;                       // ������ų�� �޾ƿ� ĳ������ ��ų�� �迭 

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_CastSkillArr; // ������ų �ٲ������ -> ĳ������ ��ų�� ������Ʈ
        ItemEnhanceManager.OnSkillEnhance += Update_CastSkillArr; // ��ų��ȭ������ -> ĳ������ ��ų�� ������Ʈ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipSkillManager.OnEquipSkillChanged -= Update_CastSkillArr;
        ItemEnhanceManager.OnSkillEnhance -= Update_CastSkillArr;
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Update_CastSkillArr();
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (IsCastSkillEmpty())
            return;

        CastSkills();
    }

    /// <summary>
    /// ��ų�� ĳ����
    /// </summary>
    private void CastSkills()
    {
        foreach (Skill skill in _castSkillArr)
        {
            if (skill == null)
                continue;

            if (skill.CheckCoolTime())
                skill.ExecuteSkill();
        }
    }

    /// <summary>
    /// ĳ������ ��ų�迭 ������Ʈ
    /// </summary>
    private void Update_CastSkillArr(Item item = null)
    {
        _castSkillArr = EquipSkillManager.GetEquipSkills_SkillType();
        
        OnUpdateCastSkills?.Invoke(_castSkillArr);
    }

    /// <summary>
    /// ĳ������ ��ų�� �������?
    /// </summary>
    private bool IsCastSkillEmpty()
    {
        if (_castSkillArr == null)
            return true;

        return _castSkillArr.All(skill => skill == null);
    }

    /// <summary>
    /// ĳ������ ��ų�� �迭 ��������
    /// </summary>
    public static Skill[] GetCastSkillArr()
    {
        return _castSkillArr;
    }
}
