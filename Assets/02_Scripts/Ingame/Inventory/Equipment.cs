using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    private readonly EquipmentDataSO _baseData;                 // ��� ������
    public string ID { get; private set; }                      // ��� ID
    public string Name { get; private set; }                    // ��� �̸�
    public Sprite Icon { get; private set; }                    // ��� ������

    private Dictionary<StatType, int> _statDictionaryByLevel;   // ��� �����ϴ� ���ȵ� ��ųʸ� (������ �°�)

    /// <summary>
    /// ������
    /// </summary>
    public Equipment(EquipmentDataSO baseData, int level)
    {
        _baseData = baseData;
        ID = _baseData.ID;
        Name = _baseData.Name;
        Icon = _baseData.Icon;

        // ������ ����� level�� �´� ���ȵ��� ��ųʸ��� ��������
        _statDictionaryByLevel = new Dictionary<StatType, int>(_baseData.GetStatDictionaryByLevel(level));
    }

    /// <summary>
    /// ��� �����ϴ� ���ȵ� ��ųʸ� ���� (������ �°�)
    /// </summary>
    public Dictionary<StatType, int> GetStatDictionaryByLevel() => _statDictionaryByLevel;
}
