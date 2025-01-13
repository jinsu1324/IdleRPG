using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThunder : SkillItem, IActiveSkill
{
    public float CurrentTime { get; set; }
    public float Delay { get; set; }

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

    public void ExecuteSkill()
    {
        Debug.Log("SkillThunder!!");
    }
}
