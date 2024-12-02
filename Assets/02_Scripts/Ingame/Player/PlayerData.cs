using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    /// <summary>
    /// Json 저장할 데이터
    /// </summary>
    
    // 스탯들 리스트
    public List<Stat> StatList = new List<Stat>();  

    // 현재 챕터
    [SerializeField]
    private int _currentChapter = 1;                  
    public int CurrentChapter { get { return _currentChapter; } set { _currentChapter = value; } }

    // 현재 스테이지
    [SerializeField]
    private int _currentStage = 1;                    
    public int CurrentStage { get { return _currentStage; } set { _currentStage = value; } }
    public event Action OnStageChange; // 스테이지 변경 시 이벤트

    // 현재 HP
    [SerializeField]
    private int _currentHp;                           
    public int CurrentHp { get { return _currentHp; } set { _currentHp = value; } }

    // 총합 전투력
    [SerializeField]
    private int _totalCombatPower;
    public int TotalCombatPower { get { return _totalCombatPower; } set { _totalCombatPower = value; } }

    // 현재 Gold              
    [SerializeField] 
    private int _currentGold = 100000;  // Todo : 최초 골드 확정하기
    public int CurrentGold                              
    {
        get { return _currentGold; }
        set { _currentGold = value; OnGoldChange?.Invoke(); }
    }
    public event Action OnGoldChange; // 골드 변경시 이벤트                  


    /// <summary>
    /// Json 저장안할 데이터
    /// </summary>
   
    // 이전 총합 전투력
    public int BeforeTotalCombatPower { get; private set; }

    // 스탯들 리스트 -> 딕셔너리용
    private Dictionary<string, Stat> _statDict = new Dictionary<string, Stat>(); 


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
        _currentHp = maxHpStat.Value;

        // 총합 전투력 업데이트
        UpdateTotalCombatPower();
    }

    /// <summary>
    /// 스테이지 레벨업
    /// </summary>
    public void StageLevelUp()
    {
        _currentStage++;

        if (_currentStage > 5)
        {
            _currentStage = 1;
            _currentChapter++;
        }

        OnStageChange?.Invoke(); // 스테이지 변경 이벤트 실행
    }

    /// <summary>
    /// 총합 전투력 업데이트
    /// </summary>
    public void UpdateTotalCombatPower()
    {
        BeforeTotalCombatPower = _totalCombatPower;

        List<int> statValueList = StatList.Select(stat => stat.Value).ToList();

        _totalCombatPower = 0;
        foreach (int value in statValueList)
            _totalCombatPower += value;
    }
}
