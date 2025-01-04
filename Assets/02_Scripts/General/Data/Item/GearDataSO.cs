using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �ִϸ��̼� Ÿ��
/// </summary>
public enum AttackAnimType
{
    Hand = 0,
    Melee = 1,
    Magic = 2,
}

/// <summary>
/// ��� ������ ��ũ���ͺ� ������Ʈ
/// </summary>
[System.Serializable]
public class GearDataSO : ItemDataSO
{
    public string AttackAnimType;
    public GameObject Prefab;
    public Sprite Icon;
}
