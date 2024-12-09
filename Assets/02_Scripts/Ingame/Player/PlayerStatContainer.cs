using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    public int CriticalRate;        // 크리티컬 확률
    public int CriticalMultiple;    // 크리티컬 배율
}

/// <summary>
/// 플레이어 스탯 컨테이너
/// </summary>
public class PlayerStatContainer : SingletonBase<PlayerStatContainer>
{
    public static event Action<OnStatChangedArgs> OnStatChanged;    // 스탯이 변경되었을 때 이벤트
    public int TotalPower { get; private set; }                     // 총합 전투력
    public int BeforeTotalPower { get; private set; }               // 이전 총합 전투력

    private Dictionary<string, Stat> _statDict;                     // 스탯들 딕셔너리

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
        // 딕셔너리 초기화
        _statDict = new Dictionary<string, Stat>();

        // 스타팅 스탯 데이터 리스트 가져오기
        List<StatData> startingDataList = DataManager.Instance.StartingStatDatasSO.DataList;

        // 스탯 ID들 배열
        StatID[] statIDArr = (StatID[])Enum.GetValues(typeof(StatID));

        // 스탯 ID 만큼 반복
        foreach (StatID statID in statIDArr)
        {
            // 스탯 ID 문자열로 변환
            string id = statID.ToString();

            // 초기스탯 리스트중에서 ID 매칭되는것 찾기
            StatData findStatData = startingDataList.FirstOrDefault(x => x.ID == id);

            // null 체크
            if (findStatData == null)
            {
                Debug.Log($"초기스탯 스크립터블 오브젝트에서 {statID}에 해당하는 데이터를 찾을 수 없습니다.");
                return;
            }

            // 찾은 초기스탯 데이터로 스탯 생성 
            Stat stat = new Stat(
                            findStatData.ID,
                            findStatData.Name,
                            findStatData.Level,
                            findStatData.Value,
                            findStatData.ValueIncrease,
                            findStatData.Cost,
                            findStatData.CostIncrease
                           );

            // 딕셔너리 ID 중복체크
            if (_statDict.ContainsKey(id) == true)
            {
                Debug.LogWarning($"{id} 는 이미 딕셔너리 속 중복된 스탯 ID 입니다");
                return;
            }

            // 딕셔너리에 추가
            _statDict.Add(id, stat);
        }
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
    public bool TryStatLevelUp(string id)
    {
        Stat stat = GetStat(id); // id 에 맞는 스탯 가져오기

        if (stat != null && GoldManager.HasEnoughCurrency(stat.Cost)) // 스탯이 있고 + 자금이 된다면
        {
            StatLevelUp(id); // 그 스탯 레벨업
            return true;
        }

        return false;
    }

    /// <summary>
    /// 특정 스탯 레벨업
    /// </summary>
    public void StatLevelUp(string id)
    {
        if (_statDict.TryGetValue(id.ToString(), out var stat))
        {
            // 골드 감소
            GoldManager.ReduceCurrency(stat.Cost);

            // 스탯 레벨업
            stat.LevelUp();
            
            // 총합 전투력 업데이트
            UpdateTotalPower();

            OnStatChangedArgs args = new OnStatChangedArgs()
            {
                StatList = GetAllStats(),
                TotalPower = TotalPower,
                AttackPower = GetStat(StatID.AttackPower.ToString()).Value,
                AttackSpeed = GetStat(StatID.AttackSpeed.ToString()).Value,
                MaxHp = GetStat(StatID.MaxHp.ToString()).Value,
                CriticalRate = GetStat(StatID.CriticalRate.ToString()).Value,
                CriticalMultiple = GetStat(StatID.CriticalMultiple.ToString()).Value
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
