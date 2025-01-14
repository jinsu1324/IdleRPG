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
    public string Desc;
    public Sprite Icon;
    public List<LevelAttributes> LevelAttributesList;
}

/// <summary>
/// ������ �Ӽ���
/// </summary>
[System.Serializable]
public class LevelAttributes
{
    public string Level;                    // ����
    public List<Attribute> AttributeList;   // ������ �ִ� �Ӽ��� ����Ʈ
}

/// <summary>
/// �� �Ӽ�
/// </summary>
[System.Serializable]
public class Attribute
{
    public string Type;
    public string Value;
}