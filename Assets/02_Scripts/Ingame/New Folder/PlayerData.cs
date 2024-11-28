using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int CurrentHp;   // 현재 HP
    public int CurrentGold = 100; // 현재 Gold
    public List<StatData> StatList = new List<StatData>(); // 스탯들 리스트

    // 스탯들 리스트 -> 딕셔너리
    private Dictionary<string, StatData> _statDict = new Dictionary<string, StatData>();

    /// <summary>
    /// 딕셔너리 초기화
    /// </summary>
    public void InitializeDict()
    {
        _statDict = StatList.ToDictionary(statData => statData.ID);
    }

    /// <summary>
    /// 특정 스탯 가져오기
    /// </summary>
    public StatData GetStat(string id)
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
    /// 레벨업
    /// </summary>
    public void LevelUpStat(string id)
    {
        if (_statDict.TryGetValue(id, out var stat))
        {
            stat.LevelUp();
        }
        else
        {
            Debug.Log($"{id} 스탯이 존재하지 않습니다.");
        }
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void InitializeStats(DataManager dataManager)
    {
        StatDatasSO statDataSO = dataManager.StatDatasSO;

        // 데이터 초기화
        StatList = statDataSO.DataList.Select(stat => new StatData
        {
            ID = stat.ID,
            Name = stat.Name,
            Level = stat.Level,
            Value = stat.Value,
            ValueIncrease = stat.ValueIncrease,
            Cost = stat.Cost,
            CostIncrease = stat.CostIncrease
        }).ToList();

        // MaxHp로 CurrentHp 설정
        StatData maxHpStat = StatList.Find(statData => statData.ID == StatID.MaxHp.ToString());
        CurrentHp = maxHpStat.Value;
    }
}
