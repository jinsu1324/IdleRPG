using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공격 애니메이션 타입
/// </summary>
public enum AttackAnimType
{
    Hand = 0,
    Melee = 1,
    Magic = 2,
}

/// <summary>
/// 장비 아이템 스크립터블 오브젝트
/// </summary>
[System.Serializable]
public class GearDataSO : ItemDataSO
{
    public string AttackAnimType;
    public GameObject Prefab;
    public Sprite Icon;
}
