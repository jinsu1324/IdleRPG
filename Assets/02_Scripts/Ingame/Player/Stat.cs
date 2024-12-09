using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public string ID { get; set; }                      // ���� ID
    public string Name { get; private set; }            // �̸� 
    public int Level { get; private set; }              // ����
    public int Value { get; private set; }              // ���� ���� ��
    public int ValueIncrease { get; private set; }      // ���� ������ �����ϴ� ��
    public int Cost { get; private set; }               // ���׷��̵� ���
    public int CostIncrease { get; private set; }       // ���� ������ �����ϴ� ���׷��̵� ���

    /// <summary>
    /// ������
    /// </summary>
    public Stat(string id, string name, int level, int value, int valueIncrease, int cost, int costIncrease)
    {
        ID = id;
        Name = name;
        Level = level;
        Value = value;
        ValueIncrease = valueIncrease;
        Cost = cost;
        CostIncrease = costIncrease;
    }

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
