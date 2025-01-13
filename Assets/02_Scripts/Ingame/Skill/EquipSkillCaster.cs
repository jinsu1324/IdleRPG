using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSkillCaster : MonoBehaviour
{

    private IActiveSkill[] _equipSkillArr;

    private void Start()
    {
        //_equipSkillArr = EquipSkillManager.GetEquipSkillArr();
    }

    private void Update()
    {
        foreach (Item skill in _equipSkillArr)
        {

        }
    }
}
