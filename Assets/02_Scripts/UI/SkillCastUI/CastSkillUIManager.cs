using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastSkillUIManager : MonoBehaviour
{
    [SerializeField] private CastSkillUI[] _castSkillUIArr; // 캐스팅 스킬 UI 들 배열

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        SkillCaster.OnUpdateCastSkills += Update_CastSkillUIArr; // 캐스팅할 스킬들이 바뀌었을때, UI도 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        SkillCaster.OnUpdateCastSkills -= Update_CastSkillUIArr;
    }

    /// <summary>
    /// 캐스팅스킬 UI 업데이트
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
