using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Equipment
{
    private readonly EquipmentDataSO _baseData;                 // 장비 데이터
    public string ID { get; private set; }                      // 장비 ID
    public string Name { get; private set; }                    // 장비 이름
    public Sprite Icon { get; private set; }                    // 장비 아이콘

    private Dictionary<StatType, int> _statDictionaryByLevel;   // 장비가 제공하는 스탯들 딕셔너리 (레벨에 맞게)

    public int Level { get; private set; }  // 레벨
    public int Count { get; private set; }  // 갯수
    public int EnhanceableCount { get; private set; }   // 강화 가능한 갯수



    /// <summary>
    /// 생성자
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


        // 생성된 장비의 level에 맞는 스탯들을 딕셔너리로 가져오기
        _statDictionaryByLevel = new Dictionary<StatType, int>(_baseData.GetStatDictionaryByLevel(level));
    }

    /// <summary>
    /// 장비가 제공하는 스탯들 딕셔너리 리턴 (레벨에 맞게)
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
        Debug.Log("강화버튼 눌렸습니다! 강화합니다!");

        Count -= EnhanceableCount; // 갯수 소비

        Level++;    // 레벨 증가


        _statDictionaryByLevel = new Dictionary<StatType, int>(_baseData.GetStatDictionaryByLevel(Level));  // 레벨에 맞는 새로운 스탯들 적용



        Inventory.Instance.FindSlotByItem(this).UpdateItemInfoUI();                // 슬롯 UI 업데이트


        foreach (var kvp in _statDictionaryByLevel)
            PlayerStats.Instance.UpdateModifier(kvp.Key, kvp.Value, this);             // 플레이어에게 스탯 적용



        PlayerStats.Instance.AllStatUIUpdate();                 // UI 업데이트
    }
}
