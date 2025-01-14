using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipSkillCaster : MonoBehaviour
{
    private ISkill[] _castSkillArr; // ������ų�� �޾ƿ� ĳ������ ��ų�� �迭 

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_CastSkillArr; // ������ų �ٲ������ -> ĳ������ ��ų�鵵 ������Ʈ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipSkillManager.OnEquipSkillChanged -= Update_CastSkillArr;
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Update_CastSkillArr();  // ���ӽ����ϸ� ĳ������ ��ų�迭 �ѹ� ������Ʈ
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (IsCastSkillEmpty()) // ĳ���ý�ų ��������� �׳� ����
            return;

        CastSkills();
    }

    /// <summary>
    /// ��ų�� ĳ����
    /// </summary>
    private void CastSkills()
    {
        foreach (ISkill skill in _castSkillArr)
        {
            if (skill == null) // ��ų���� �ε����� �׳� continue
                continue;

            if (skill.CheckCoolTime())  // ��Ÿ��üũ�ؼ� ����
                skill.ExecuteSkill();
        }
    }


    /// <summary>
    /// ĳ������ ��ų�迭 ������Ʈ
    /// </summary>
    private void Update_CastSkillArr(Item item = null)
    {
        _castSkillArr = EquipSkillManager.GetEquipISkill();
    }
    
    /// <summary>
    /// ĳ������ ��ų�� �������?
    /// </summary>
    private bool IsCastSkillEmpty()
    {
        if (_castSkillArr == null) // ĳ������ ��ų �迭��ü�� �׳� ���������, ����ִٰ� true ��ȯ
            return true;

        return _castSkillArr.All(skill => skill == null); // ��� ��Ұ� ������� Ȯ��
    }
}
