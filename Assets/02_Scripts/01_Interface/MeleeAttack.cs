using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : IAttackable
{
    public void ExecuteAttack(int attackPower)
    {
        Debug.Log("MeleeAttack!");
    }
}
