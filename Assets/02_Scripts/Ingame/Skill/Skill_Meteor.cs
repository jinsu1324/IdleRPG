using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Meteor : SkillItem
{
    public float AttackPercentage { get; set; }
    public float Range { get; set; }

    public override void ExecuteSkill()
    {
        Debug.Log("Skill_Meteor!!");
    }
}
