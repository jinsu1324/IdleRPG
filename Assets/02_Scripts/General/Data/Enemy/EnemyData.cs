using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyID
{
    Monster_SlimeBlue = 0,
    Monster_SlimeGreen = 1,
    Monster_SlimePink = 2,
    Boss_KingSlime = 3
}

[System.Serializable]
public class EnemyData : BaseData
{
    public string Name;
    public int MaxHp;
    public int MoveSpeed;
    public int AttackPower;
    public int AttackSpeed;
    public int DropGold;
}
