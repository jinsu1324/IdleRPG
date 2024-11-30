using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // Json 저장되는 것들
    public int CurrentChapter = 1;                  // 현재 챕터
    public int CurrentStage = 1;                    // 현재 스테이지
    public int CurrentGold = 1000000;               // (!!!!!!!!!!!!!임시값!!!!!!!!!!!!!!) 현재 Gold 
    public int CurrentHp;                           // 현재 HP
    public int TotalCombatPower;                    // 총합 전투력
    public List<Stat> StatList = new List<Stat>();  // 스탯들 리스트

    // Json 저장 안되는 것들
    public int BeforeTotalCombatPower { get; private set; }                      // 이전 총합 전투력
    private Dictionary<string, Stat> _statDict = new Dictionary<string, Stat>(); // 스탯들 리스트 -> 딕셔너리용

    /// <summary>
    /// 딕셔너리 설정
    /// </summary>
    public void SetDictFromStatList()
    {
        _statDict = StatList.ToDictionary(statData => statData.ID);
    }

    /// <summary>
    /// 특정 스탯 가져오기
    /// </summary>
    public Stat GetStat(string id)
    {
        if (_statDict.TryGetValue(id, out var stat))
        {
            return stat;
        }
        else
        {
            Debug.Log($"{id} 스탯이 존재하지 않습니다.");
            return null;
        }
    }

    /// <summary>
    /// 스탯 레벨업
    /// </summary>
    public void LevelUpStat(string id)
    {
        if (_statDict.TryGetValue(id, out var stat))
        {
            stat.LevelUp();
            UpdateTotalCombatPower(); // 총합 전투력 업데이트
        }
        else
        {
            Debug.Log($"{id} 스탯이 존재하지 않습니다.");
        }
    }

    /// <summary>
    /// 스탯들 스타팅값으로 설정
    /// </summary>
    public void SetToStartingStats()
    {
        StartingStatsSO startingStatsSO = DataManager.Instance.StartingStatsSO;

        // 스탯들 스타팅값으로 설정
        StatList = startingStatsSO.DataList.Select(stat => new Stat
        {
            ID = stat.ID,
            Name = stat.Name,
            Level = stat.Level,
            Value = stat.Value,
            ValueIncrease = stat.ValueIncrease,
            Cost = stat.Cost,
            CostIncrease = stat.CostIncrease
        }).ToList();

        // 스타팅 MaxHp로 CurrentHp도 설정
        Stat maxHpStat = StatList.Find(statData => statData.ID == StatID.MaxHp.ToString());
        CurrentHp = maxHpStat.Value;

        // 총합 전투력 업데이트
        UpdateTotalCombatPower();
    }

    /// <summary>
    /// 스테이지 레벨업
    /// </summary>
    public void StageLevelUp()
    {
        CurrentStage++;

        if (CurrentStage > 5)
        {
            CurrentStage = 1;
            CurrentChapter++;
        }
    }

    /// <summary>
    /// 총합 전투력 업데이트
    /// </summary>
    public void UpdateTotalCombatPower()
    {
        BeforeTotalCombatPower = TotalCombatPower;

        List<int> statValueList = StatList.Select(stat => stat.Value).ToList();

        TotalCombatPower = 0;
        foreach (int value in statValueList)
            TotalCombatPower += value;
    }
}
