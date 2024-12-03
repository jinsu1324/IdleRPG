using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public struct OnPlayerStatSettingArgs
{
    public int AttackPower;
    public int AttackSpeed;
    public int MaxHp;
}



public class StatManager : MonoBehaviour
{
    #region Singleton
    public static StatManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("1. 스탯매니저 awake");
    }
    #endregion
    
    public int TotalCombatPower { get; private set; }                // 총합 전투력
    public int BeforeTotalCombatPower { get; private set; }          // 이전 총합 전투력


    private Dictionary<StatID, StatComponent> _statComponentDict;    // 스탯들 컴포넌트 가져와서 저장할 딕셔너리


    public event Action<OnPlayerStatSettingArgs> OnPlayerStatSetting;


    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        // 스탯 컴포넌트 리스트 가져오기
        List<StatComponent> statComponentList = GetComponentsInChildren<StatComponent>().ToList();

        // 스타팅 스탯 데이터 리스트 가져오기
        List<StatData> startingStatDataList = DataManager.Instance.StartingStatDatasSO.DataList;

        // 딕셔너리 초기화
        _statComponentDict = new Dictionary<StatID, StatComponent>();

        // 일치하는 StatID와 ID를 찾아 데이터를 설정
        foreach (StatComponent statComponent in statComponentList)
        {
            StatData matchingData = startingStatDataList.FirstOrDefault(statData => statData.ID == statComponent.StatID.ToString());
            if (matchingData != null)
            {
                statComponent.Init(
                    matchingData.Name,
                    matchingData.Level,
                    matchingData.Value,
                    matchingData.ValueIncrease,
                    matchingData.Cost,
                    matchingData.CostIncrease
                );

                // 딕셔너리에 추가
                if (_statComponentDict.ContainsKey(statComponent.StatID) == false)
                {
                    _statComponentDict.Add(statComponent.StatID, statComponent);
                }
                else
                {
                    Debug.LogWarning($"딕셔너리 속 중복된 스탯 ID 입니다 : {statComponent.StatID}");
                }
            }
            else
            {
                Debug.Log($"{statComponent.StatID}에 해당하는 데이터를 찾을 수 없습니다.");
            }
        }

        UpdateTotalCombatPower();



        Debug.Log("2. 스탯매니저 초기화");


    }

    private void Start()
    {


        OnPlayerStatSettingArgs args = new OnPlayerStatSettingArgs()
        {
            AttackPower = GetStat(StatID.AttackPower).Value,
            AttackSpeed = GetStat(StatID.AttackSpeed).Value,
            MaxHp = GetStat(StatID.MaxHp).Value
        };

        Debug.Log("4. 이벤트 실행");
        OnPlayerStatSetting?.Invoke(args);
    }

    /// <summary>
    /// 특정 스탯 가져오기
    /// </summary>
    public StatComponent GetStat(StatID id)
    {
        if (_statComponentDict.TryGetValue(id, out var statComponent))
        {
            return statComponent;
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
    public List<StatComponent> GetAllStats()
    {
        List<StatComponent> statComponents = new List<StatComponent>();
        statComponents = _statComponentDict.Values.ToList();

        return statComponents;
    }

    /// <summary>
    /// 특정 스탯 레벨업
    /// </summary>
    public void LevelUpStat(StatID id)
    {
        if (_statComponentDict.TryGetValue(id, out var statComponent))
        {
            statComponent.LevelUp();

            // OnStatChanged?.Invoke(id, statComponent.Value);


            UpdateTotalCombatPower();
        }
        else
        {
            Debug.Log($"{id} 스탯이 존재하지 않습니다.");
        }
    }

    /// <summary>
    /// 특정 스탯 레벨업 시도
    /// </summary>
    public bool TryLevelUpStatByID(StatID id)
    {
        // id 에 맞는 스탯 가져오기
        StatComponent statComponent = GetStat(id);

        // 스탯이 있고 + 자금이 된다면
        if (statComponent != null && GoldManager.Instance.HasEnoughCurrency(statComponent.Cost))
        {
            // 돈 감소
            GoldManager.Instance.ReduceCurrency(statComponent.Cost);

            // 그 스탯 레벨업
            LevelUpStat(id);

            return true;
        }
        return false;
    }

    /// <summary>
    /// 총합 전투력 업데이트
    /// </summary>
    public void UpdateTotalCombatPower()
    {
        BeforeTotalCombatPower = TotalCombatPower;

        List<int> statValueList = GetAllStats().Select(stat => stat.Value).ToList();

        TotalCombatPower = 0;
        foreach (int value in statValueList)
            TotalCombatPower += value;
    }

    /// <summary>
    /// 총합 전투력 가져오기
    /// </summary>
    public int GetTotalCombatPower() 
    { 
        return TotalCombatPower; 
    }

    /// <summary>
    /// 이전 총합 전투력 가져오기
    /// </summary>
    public int GetBeforeTotalCombatPower() 
    { 
        return BeforeTotalCombatPower; 
    }
}
