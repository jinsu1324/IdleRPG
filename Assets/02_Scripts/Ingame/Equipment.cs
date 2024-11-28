using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment
{
    public string Name;        // ��� �̸�
    public string Type;        // ��� ���� (��: "Weapon", "Armor")
    public int Level;          // ��� ����
    public float AttackBoost;  // ���ݷ� ����
    public float DefenseBoost; // ���� ����
    public bool IsEquipped;    // ���� ����
}
