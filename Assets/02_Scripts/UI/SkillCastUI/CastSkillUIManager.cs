using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastSkillUIManager : MonoBehaviour
{
    [SerializeField] private CastSkillUI[] _castSkillUIArr; // ĳ���� ��ų UI �� �迭

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        SkillCaster.OnUpdateCastSkills += Update_CastSkillUIArr; // ĳ������ ��ų���� �ٲ������, UI�� ������Ʈ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        SkillCaster.OnUpdateCastSkills -= Update_CastSkillUIArr;
    }

    /// <summary>
    /// ĳ���ý�ų UI ������Ʈ
    /// </summary>
    private void Update_CastSkillUIArr(Skill[] castSkillArr)
    {
        if (castSkillArr == null)
        {
            foreach (CastSkillUI castSkillUI in _castSkillUIArr)
                castSkillUI.Hide();

            return;
        }

        for (int i = 0; i < castSkillArr.Length; i++)
        {
            if (castSkillArr[i] == null)
            {
                _castSkillUIArr[i].Hide();
                continue;
            }

            _castSkillUIArr[i].Init_BindSkill(castSkillArr[i]);
        }
    }
}
