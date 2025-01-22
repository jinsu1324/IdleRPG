using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� ID
/// </summary>
public enum EnemyID
{
    Monster_SlimeBlue = 0,
    Monster_SlimeGreen = 1,
    Monster_SlimePink = 2,
    Boss_KingSlime = 3
}

/// <summary>
/// �� ������
/// </summary>
[System.Serializable]
public class EnemyData
{
    public string ID;
    public string Name;
    public float MaxHp;
    public float MoveSpeed;
    public float AttackPower;
    public float AttackSpeed;
    public int DropGold;
}
