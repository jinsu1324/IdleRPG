using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 등급
/// </summary>
public enum ItemGrade
{
    Normal,
    Rare,
    Legendary
}

/// <summary>
/// 아이템 타입
/// </summary>
public enum ItemType
{
    Weapon,
    Armor,
    Helmet,
    Skill
}

/// <summary>
/// 아이템들 부모 스크립터블 오브젝트
/// </summary>
[System.Serializable]
public class ItemDataSO : ScriptableObject
{
    public string ID;
    public string ItemType;
    public string Name;
    public string Grade;
    public Sprite Icon;
    public List<ItemLevelInfo> ItemLevelInfoList;
}

/// <summary>
/// 아이템 레벨과 그에맞는 정보들(어빌리티 등)
/// </summary>
[System.Serializable]
public class ItemLevelInfo
{
    public string Level;                        // 레벨
    public List<ItemAbility> ItemAbilityList;   // 가지고 있는 어빌리티들 리스트
}

/// <summary>
/// 아이템 어빌리티
/// </summary>
[System.Serializable]
public class ItemAbility
{
    public string AbilityType;
    public string AbilityValue;
}