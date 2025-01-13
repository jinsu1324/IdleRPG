using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipSkillCaster : MonoBehaviour
{

    private ISkill[] _castSkillArr;

    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_CastSkillArr;
    }

    private void OnDisable()
    {
        EquipSkillManager.OnEquipSkillChanged -= Update_CastSkillArr;

    }


    private void Start()
    {
        Update_CastSkillArr();
    }

    private void Update()
    {
        if (IsCastSkillEmpty())
            return;


        foreach (ISkill skill in _castSkillArr)
        {
            if (skill == null)
                continue;

            if (skill.CheckCoolTime())
                skill.ExecuteSkill();
        }
    }

    private void Update_CastSkillArr(Item item = null)
    {
        _castSkillArr = EquipSkillManager.GetEquipISkill();
    }
    
    private bool IsCastSkillEmpty()
    {
        if (_castSkillArr == null) // 캐스팅할 스킬 배열자체가 그냥 비어있으면, 비어있다고 true 반환
            return true;

        return _castSkillArr.All(skill => skill == null); // 모든 요소가 null인지 확인
    }

}
