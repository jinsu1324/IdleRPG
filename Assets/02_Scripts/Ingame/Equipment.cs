using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment
{
    public string Name;        // 장비 이름
    public string Type;        // 장비 유형 (예: "Weapon", "Armor")
    public int Level;          // 장비 레벨
    public float AttackBoost;  // 공격력 증가
    public float DefenseBoost; // 방어력 증가
    public bool IsEquipped;    // 장착 여부
}
