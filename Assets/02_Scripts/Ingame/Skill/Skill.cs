using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų �θ�
/// </summary>
public abstract class Skill
{
    public string ID;                   // ��ų ID
    protected SkillDataSO _skillDataSO; // ��ų ������
    protected int _level;               // ���� ��ų�� ����
    protected float _coolTime;          // ��ų ��Ÿ��
    protected float _currentTime;       // ��Ÿ�� üũ�� ����ð�

    /// <summary>
    /// ������
    /// </summary>
    public Skill(CreateSkillArgs args)
    {
        ID = args.ID;
        _skillDataSO = args.SkillDataSO;
        _level = args.Level;
        _coolTime = _skillDataSO.CoolTime;
        _currentTime = 0;
    }

    /// <summary>
    /// ��Ÿ�� üũ
    /// </summary>
    public bool CheckCoolTime()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _coolTime)
        {
            _currentTime %= _coolTime;
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// ���� ��Ÿ�� �����Ȳ ��������
    /// </summary>
    public float Get_CurrentCoolTimeProgress()
    {
        return Mathf.Clamp01(_currentTime / _coolTime);
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public abstract void ExecuteSkill();

    /// <summary>
    /// ��ų ���ݷ� ���
    /// </summary>
    protected float Calculate_SkillAttackPower(float attackPercentage)
    {
        return PlayerStats.GetStatValue(StatType.AttackPower) * attackPercentage;
    }

    /// <summary>
    /// Ư�� ��ų���� �� �������� (���緹���� �°�)
    /// </summary>
    protected float GetSkillStatValue(SkillStatType skillStatType)
    {
        return _skillDataSO.GetSkillStats(_level)[skillStatType];
    }
}
