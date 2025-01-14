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
    public string Desc;
    public Sprite Icon;
    public List<LevelAttributes> LevelAttributesList;
}

/// <summary>
/// 레벨별 속성들
/// </summary>
[System.Serializable]
public class LevelAttributes
{
    public string Level;                    // 레벨
    public List<Attribute> AttributeList;   // 가지고 있는 속성들 리스트
}

/// <summary>
/// 상세 속성
/// </summary>
[System.Serializable]
public class Attribute
{
    public string Type;
    public string Value;
}