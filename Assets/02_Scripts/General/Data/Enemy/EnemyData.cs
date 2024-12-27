using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적 ID
/// </summary>
public enum EnemyID
{
    Monster_SlimeBlue = 0,
    Monster_SlimeGreen = 1,
    Monster_SlimePink = 2,
    Boss_KingSlime = 3
}

/// <summary>
/// 적 데이터
/// </summary>
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
