using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 스탯이 변경되었을 때 이벤트에 사용되는 구조체
/// </summary>
public struct OnStatChangedArgs
{
    public List<Stat> StatList;     // 스탯 리스트
    public int TotalPower;          // 총합 전투력
    public int AttackPower;         // 공격력
    public int AttackSpeed;         // 공격속도
    public int MaxHp;               // 최대 체력
    public int Critical;            // 크리티컬 확률
}

/// <summary>
/// 플레이어 스탯 컨테이너
/// </summary>
public class PlayerStatContainer : SingletonBase<PlayerStatContainer>
{
    public static event Action<OnStatChangedArgs> OnStatChanged;   // 스탯이 변경되었을 때 이벤트
    public int TotalPower { get; private set; }                     // 총합 전투력
    public int BeforeTotalPower { get; private set; }               // 이전 총합 전투력

    private Dictionary<StatID, Stat> _statDict;                     // 컴포넌트에 있는 스탯들을 딕셔너리로 저장할 변수
    
    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake(); // 싱글톤 먼저

        SetStatDict(); // 스탯 딕셔너리 셋팅
        UpdateTotalPower();  // 총합 전투력 업데이트
    }

    /// <summary>
    /// 스탯 딕셔너리 셋팅
    /// </summary>
    private void SetStatDict()
    {
        List<Stat> statList = GetComponentsInChildren<Stat>().ToList(); // 게임오브젝트에 붙어있는 스탯 컴포넌트들 가져오기
        List<StatData> startingDataList = DataManager.Instance.StartingStatDatasSO.DataList;// 스타팅 스탯 데이터 리스트 가져오기
        _statDict = new Dictionary<StatID, Stat>(); // 딕셔너리 초기화

        // 데이터의 ID와 스탯의 StatID가 일치하는 스탯컴포넌트를 찾아 컴포넌트의 데이터를 설정
        foreach (Stat stat in statList)
        {
            // ID 일치하는 데이터 검색
            StatData findData = startingDataList.FirstOrDefault(statData => statData.ID == stat.StatID.ToString());

            // null 체크
            if (findData == null)
            {
                Debug.Log($"{stat.StatID}에 해당하는 데이터를 찾을 수 없습니다.");
                return;
            }

            // 데이터 셋팅
            stat.Init(
                findData.Name,
                findData.Level,
                findData.Value,
                findData.ValueIncrease,
                findData.Cost,
                findData.CostIncrease
            );
           
            // 중복 키 체크
            if (_statDict.ContainsKey(stat.StatID) == true)
            {
                Debug.LogWarning($"딕셔너리 속 중복된 스탯 ID 입니다 : {stat.StatID}");
                return;
            }

            // 딕셔너리에 추가
            _statDict.Add(stat.StatID, stat); 
        }
    }

    /// <summary>
    /// 특정 스탯 가져오기
    /// </summary>
    public Stat GetStat(StatID id)
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
    /// 모든 스탯 가져오기
    /// </summary>
    public List<Stat> GetAllStats()
    {
        List<Stat> statList = new List<Stat>();
        statList = _statDict.Values.ToList();

        return statList;
    }

    /// <summary>
    /// 특정 스탯 레벨업 시도
    /// </summary>
    public bool TryStatLevelUp(StatID id)
    {
        Stat stat = GetStat(id); // id 에 맞는 스탯 가져오기

        if (stat != null && GoldManager.Instance.HasEnoughCurrency(stat.Cost)) // 스탯이 있고 + 자금이 된다면
        {
            StatLevelUp(id); // 그 스탯 레벨업
            return true;
        }

        return false;
    }

    /// <summary>
    /// 특정 스탯 레벨업
    /// </summary>
    public void StatLevelUp(StatID id)
    {
        if (_statDict.TryGetValue(id, out var stat))
        {
            // 골드 감소
            GoldManager.Instance.ReduceCurrency(stat.Cost);

            // 스탯 레벨업
            stat.LevelUp();
            
            // 총합 전투력 업데이트
            UpdateTotalPower();

            OnStatChangedArgs args = new OnStatChangedArgs()
            {
                StatList = GetAllStats(),
                TotalPower = TotalPower,
                AttackPower = GetStat(StatID.AttackPower).Value,
                AttackSpeed = GetStat(StatID.AttackSpeed).Value,
                MaxHp = GetStat(StatID.MaxHp).Value,
                Critical = GetStat(StatID.Critical).Value,
            };

            // 스탯 변경 이벤트 호출
            OnStatChanged?.Invoke(args);
        }
        else
        {
            Debug.Log($"{id} 스탯이 존재하지 않습니다.");
        }
    }

    /// <summary>
    /// 총합 전투력 업데이트
    /// </summary>
    public void UpdateTotalPower()
    {
        BeforeTotalPower = TotalPower;

        List<int> statValueList = GetAllStats().Select(stat => stat.Value).ToList();

        TotalPower = 0;
        foreach (int value in statValueList)
            TotalPower += value;
    }

    /// <summary>
    /// 총합 전투력 가져오기
    /// </summary>
    public int GetTotalPower() 
    { 
        return TotalPower; 
    }

    /// <summary>
    /// 이전 총합 전투력 가져오기
    /// </summary>
    public int GetBeforeTotalPower() 
    { 
        return BeforeTotalPower; 
    }



    ///// <summary>
    ///// 플레이어 데이터 로드
    ///// </summary>
    //private void PlayerDataLoad()
    //{
    //    //// 데이터 로드
    //    //PlayerData = SaveLoadManager.LoadPlayerData();

    //}




    ///// <summary>
    ///// 현재 플레이어데이터를 저장
    ///// </summary>
    //public void SavePlayerData()
    //{
    //    SaveLoadManager.SavePlayerData(PlayerData);
    //}


    ///// <summary>
    ///// 게임 종료 시 저장 호출
    ///// </summary>
    //private void OnApplicationQuit()
    //{
    //    SavePlayerData();
    //}
}
