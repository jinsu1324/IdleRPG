using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Equipment
{
    private readonly EquipmentDataSO _baseData;                 // ��� ������
    public string ID { get; private set; }                      // ��� ID
    public string Name { get; private set; }                    // ��� �̸�
    public Sprite Icon { get; private set; }                    // ��� ������

    private Dictionary<StatType, int> _statDictionaryByLevel;   // ��� �����ϴ� ���ȵ� ��ųʸ� (������ �°�)

    public int Level { get; private set; }  // ����
    public int Count { get; private set; }  // ����
    public int EnhanceableCount { get; private set; }   // ��ȭ ������ ����



    /// <summary>
    /// ������
    /// </summary>
    public Equipment(EquipmentDataSO baseData, int level)
    {
        _baseData = baseData;
        ID = _baseData.ID;
        Name = _baseData.Name;
        Icon = _baseData.Icon;
        Level = level;

        Count = 1;
        EnhanceableCount = 10;


        // ������ ����� level�� �´� ���ȵ��� ��ųʸ��� ��������
        _statDictionaryByLevel = new Dictionary<StatType, int>(_baseData.GetStatDictionaryByLevel(level));
    }

    /// <summary>
    /// ��� �����ϴ� ���ȵ� ��ųʸ� ���� (������ �°�)
    /// </summary>
    public Dictionary<StatType, int> GetStatDictionaryByLevel() => _statDictionaryByLevel;


    public void AddCount()
    {
        Count++;
    }

    public bool IsEnhanceable()
    {
        return Count >= EnhanceableCount;
    }

    public void Enhance()
    {
        Debug.Log("��ȭ��ư ���Ƚ��ϴ�! ��ȭ�մϴ�!");

        Count -= EnhanceableCount; // ���� �Һ�

        Level++;    // ���� ����


        _statDictionaryByLevel = new Dictionary<StatType, int>(_baseData.GetStatDictionaryByLevel(Level));  // ������ �´� ���ο� ���ȵ� ����



        Inventory.Instance.FindSlotByItem(this).UpdateItemInfoUI();                // ���� UI ������Ʈ


        foreach (var kvp in _statDictionaryByLevel)
            PlayerStats.Instance.UpdateModifier(kvp.Key, kvp.Value, this);             // �÷��̾�� ���� ����



        PlayerStats.Instance.AllStatUIUpdate();                 // UI ������Ʈ
    }
}
