using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatID
{
    AttackPower,
    AttackDelay,
    MaxHp,
    Critical
}

[System.Serializable]
public class Stat : BaseData
{
    public string Name;          // ���� �̸�
    public int Level;            // ����
    public int Value;            // ���� ���� ��
    public int ValueIncrease;    // ���� ������ �����ϴ� ��
    public int Cost;             // ���׷��̵� ���
    public int CostIncrease;     // ���� ������ �����ϴ� ���׷��̵� ���

    /// <summary>
    /// ������
    /// </summary>
    public void LevelUp()
    {
        Level++;
        Value += ValueIncrease;
        Cost += CostIncrease;
    }
}
