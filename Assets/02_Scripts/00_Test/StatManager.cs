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

        Debug.Log("1. ���ȸŴ��� awake");
    }
    #endregion
    
    public int TotalCombatPower { get; private set; }                // ���� ������
    public int BeforeTotalCombatPower { get; private set; }          // ���� ���� ������


    private Dictionary<StatID, StatComponent> _statComponentDict;    // ���ȵ� ������Ʈ �����ͼ� ������ ��ųʸ�


    public event Action<OnPlayerStatSettingArgs> OnPlayerStatSetting;


    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        // ���� ������Ʈ ����Ʈ ��������
        List<StatComponent> statComponentList = GetComponentsInChildren<StatComponent>().ToList();

        // ��Ÿ�� ���� ������ ����Ʈ ��������
        List<StatData> startingStatDataList = DataManager.Instance.StartingStatDatasSO.DataList;

        // ��ųʸ� �ʱ�ȭ
        _statComponentDict = new Dictionary<StatID, StatComponent>();

        // ��ġ�ϴ� StatID�� ID�� ã�� �����͸� ����
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

                // ��ųʸ��� �߰�
                if (_statComponentDict.ContainsKey(statComponent.StatID) == false)
                {
                    _statComponentDict.Add(statComponent.StatID, statComponent);
                }
                else
                {
                    Debug.LogWarning($"��ųʸ� �� �ߺ��� ���� ID �Դϴ� : {statComponent.StatID}");
                }
            }
            else
            {
                Debug.Log($"{statComponent.StatID}�� �ش��ϴ� �����͸� ã�� �� �����ϴ�.");
            }
        }

        UpdateTotalCombatPower();



        Debug.Log("2. ���ȸŴ��� �ʱ�ȭ");


    }

    private void Start()
    {


        OnPlayerStatSettingArgs args = new OnPlayerStatSettingArgs()
        {
            AttackPower = GetStat(StatID.AttackPower).Value,
            AttackSpeed = GetStat(StatID.AttackSpeed).Value,
            MaxHp = GetStat(StatID.MaxHp).Value
        };

        Debug.Log("4. �̺�Ʈ ����");
        OnPlayerStatSetting?.Invoke(args);
    }

    /// <summary>
    /// Ư�� ���� ��������
    /// </summary>
    public StatComponent GetStat(StatID id)
    {
        if (_statComponentDict.TryGetValue(id, out var statComponent))
        {
            return statComponent;
        }
        else
        {
            Debug.Log($"{id} ������ �������� �ʽ��ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ��� ���� ��������
    /// </summary>
    public List<StatComponent> GetAllStats()
    {
        List<StatComponent> statComponents = new List<StatComponent>();
        statComponents = _statComponentDict.Values.ToList();

        return statComponents;
    }

    /// <summary>
    /// Ư�� ���� ������
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
            Debug.Log($"{id} ������ �������� �ʽ��ϴ�.");
        }
    }

    /// <summary>
    /// Ư�� ���� ������ �õ�
    /// </summary>
    public bool TryLevelUpStatByID(StatID id)
    {
        // id �� �´� ���� ��������
        StatComponent statComponent = GetStat(id);

        // ������ �ְ� + �ڱ��� �ȴٸ�
        if (statComponent != null && GoldManager.Instance.HasEnoughCurrency(statComponent.Cost))
        {
            // �� ����
            GoldManager.Instance.ReduceCurrency(statComponent.Cost);

            // �� ���� ������
            LevelUpStat(id);

            return true;
        }
        return false;
    }

    /// <summary>
    /// ���� ������ ������Ʈ
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
    /// ���� ������ ��������
    /// </summary>
    public int GetTotalCombatPower() 
    { 
        return TotalCombatPower; 
    }

    /// <summary>
    /// ���� ���� ������ ��������
    /// </summary>
    public int GetBeforeTotalCombatPower() 
    { 
        return BeforeTotalCombatPower; 
    }
}
