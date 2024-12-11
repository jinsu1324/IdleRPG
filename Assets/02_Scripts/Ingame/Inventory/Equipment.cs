using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    private readonly EquipmentDataSO _baseData;                 // 장비 데이터
    public string ID { get; private set; }                      // 장비 ID
    public string Name { get; private set; }                    // 장비 이름
    public Sprite Icon { get; private set; }                    // 장비 아이콘

    private Dictionary<StatType, int> _statDictionaryByLevel;   // 장비가 제공하는 스탯들 딕셔너리 (레벨에 맞게)

    /// <summary>
    /// 생성자
    /// </summary>
    public Equipment(EquipmentDataSO baseData, int level)
    {
        _baseData = baseData;
        ID = _baseData.ID;
        Name = _baseData.Name;
        Icon = _baseData.Icon;

        // 생성된 장비의 level에 맞는 스탯들을 딕셔너리로 가져오기
        _statDictionaryByLevel = new Dictionary<StatType, int>(_baseData.GetStatDictionaryByLevel(level));
    }

    /// <summary>
    /// 장비가 제공하는 스탯들 딕셔너리 리턴 (레벨에 맞게)
    /// </summary>
    public Dictionary<StatType, int> GetStatDictionaryByLevel() => _statDictionaryByLevel;
}
