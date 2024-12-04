using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// 스탯이 변경되었을 때 이벤트에 사용되는 구조체
/// </summary>
public struct OnStatChangedArgs
{
    public List<Stat> StatList;                     // 스탯 리스트
    public int TotalCombatPower;                    // 총합 전투력
    public int AttackPower;                         // 공격력
    public int AttackSpeed;                         // 공격속도
    public int MaxHp;                               // 최대 체력
    public int Critical;                            // 크리티컬 확률
}

/// <summary>
/// 플레이어 관리자
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }      // 싱글톤 인스턴스

    public event Action<OnStatChangedArgs?> OnStatChanged;          // 스탯이 변경되었을 때 이벤트

    [SerializeField] private Player _playerPrefab;                  // 생성할 플레이어 프리팹
    [SerializeField] private Transform _playerSpawnPos;             // 생성할 플레이어의 스폰위치

    private Dictionary<StatID, Stat> _statDict;                     // 컴포넌트에 있는 스탯들을 딕셔너리로 저장할 변수
    private Player _playerInstance;                                 // 생성한 플레이어 인스턴스
    private int _totalPower;                                        // 총합 전투력
    private int _beforeTotalPower;                                  // 이전 총합 전투력

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        Debug.Log("Awake 1");
        if (Instance == null)
        {
            Debug.Log("Awake 2");

            Instance = this;

            Debug.Log("Awake 3");

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Awake 4");
            Destroy(gameObject);
        }

        Debug.Log("Awake 5");
        SetStatDict(); // 스탯 딕셔너리 셋팅

        Debug.Log("Awake 6");
        UpdateTotalPower();  // 총합 전투력 업데이트
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        SpawnPlayer(); // 플레이어 스폰
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
    /// 플레이어 스폰
    /// </summary>
    public void SpawnPlayer()
    {
        // 플레이어 인스턴스 생성
        _playerInstance = Instantiate(_playerPrefab, _playerSpawnPos);
        _playerInstance.transform.position = _playerSpawnPos.position;
        
        OnStatChangedArgs args = new OnStatChangedArgs()
        {
            StatList = GetAllStats(),
            TotalCombatPower = _totalPower,
            AttackPower = GetStat(StatID.AttackPower).Value,
            AttackSpeed = GetStat(StatID.AttackSpeed).Value,
            MaxHp = GetStat(StatID.MaxHp).Value,
            Critical = GetStat(StatID.Critical).Value,
        };
        _playerInstance.Init(args); // 인스턴스 초기화
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
                TotalCombatPower = _totalPower,
                AttackPower = GetStat(StatID.AttackPower).Value,
                AttackSpeed = GetStat(StatID.AttackSpeed).Value,
                MaxHp = GetStat(StatID.MaxHp).Value,
                Critical = GetStat(StatID.Critical).Value,
            };

            Debug.Log("OnStatChanged 이벤트 호출!");
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
        _beforeTotalPower = _totalPower;

        List<int> statValueList = GetAllStats().Select(stat => stat.Value).ToList();

        _totalPower = 0;
        foreach (int value in statValueList)
            _totalPower += value;
    }

    /// <summary>
    /// 총합 전투력 가져오기
    /// </summary>
    public int GetTotalPower() 
    { 
        return _totalPower; 
    }

    /// <summary>
    /// 이전 총합 전투력 가져오기
    /// </summary>
    public int GetBeforeTotalPower() 
    { 
        return _beforeTotalPower; 
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
