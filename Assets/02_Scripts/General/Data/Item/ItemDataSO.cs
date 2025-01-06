using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���
/// </summary>
public enum ItemGrade
{
    Normal,
    Rare,
    Legendary
}

/// <summary>
/// ������ Ÿ��
/// </summary>
public enum ItemType
{
    Weapon,
    Armor,
    Helmet,
    Skill
}

/// <summary>
/// �����۵� �θ� ��ũ���ͺ� ������Ʈ
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
/// ������ ������ �׿��´� ������(�����Ƽ ��)
/// </summary>
[System.Serializable]
public class ItemLevelInfo
{
    public string Level;                        // ����
    public List<ItemAbility> ItemAbilityList;   // ������ �ִ� �����Ƽ�� ����Ʈ
}

/// <summary>
/// ������ �����Ƽ
/// </summary>
[System.Serializable]
public class ItemAbility
{
    public string AbilityType;
    public string AbilityValue;
}