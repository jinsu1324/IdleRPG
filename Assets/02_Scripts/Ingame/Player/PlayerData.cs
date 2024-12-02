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
    /// Json ������ ������
    /// </summary>
    
    // ���ȵ� ����Ʈ
    public List<Stat> StatList = new List<Stat>();  

    // ���� é��
    [SerializeField]
    private int _currentChapter = 1;                  
    public int CurrentChapter { get { return _currentChapter; } set { _currentChapter = value; } }

    // ���� ��������
    [SerializeField]
    private int _currentStage = 1;                    
    public int CurrentStage { get { return _currentStage; } set { _currentStage = value; } }
    public event Action OnStageChange; // �������� ���� �� �̺�Ʈ

    // ���� HP
    [SerializeField]
    private int _currentHp;                           
    public int CurrentHp { get { return _currentHp; } set { _currentHp = value; } }

    // ���� ������
    [SerializeField]
    private int _totalCombatPower;
    public int TotalCombatPower { get { return _totalCombatPower; } set { _totalCombatPower = value; } }

    // ���� Gold              
    [SerializeField] 
    private int _currentGold = 100000;  // Todo : ���� ��� Ȯ���ϱ�
    public int CurrentGold                              
    {
        get { return _currentGold; }
        set { _currentGold = value; OnGoldChange?.Invoke(); }
    }
    public event Action OnGoldChange; // ��� ����� �̺�Ʈ                  


    /// <summary>
    /// Json ������� ������
    /// </summary>
   
    // ���� ���� ������
    public int BeforeTotalCombatPower { get; private set; }

    // ���ȵ� ����Ʈ -> ��ųʸ���
    private Dictionary<string, Stat> _statDict = new Dictionary<string, Stat>(); 


    /// <summary>
    /// ��ųʸ� ����
    /// </summary>
    public void SetDictFromStatList()
    {
        _statDict = StatList.ToDictionary(statData => statData.ID);
    }

    /// <summary>
    /// Ư�� ���� ��������
    /// </summary>
    public Stat GetStat(string id)
    {
        if (_statDict.TryGetValue(id, out var stat))
        {
            return stat;
        }
        else
        {
            Debug.Log($"{id} ������ �������� �ʽ��ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ���� ������
    /// </summary>
    public void LevelUpStat(string id)
    {
        if (_statDict.TryGetValue(id, out var stat))
        {
            stat.LevelUp();
            UpdateTotalCombatPower(); // ���� ������ ������Ʈ
        }
        else
        {
            Debug.Log($"{id} ������ �������� �ʽ��ϴ�.");
        }
    }

    /// <summary>
    /// ���ȵ� ��Ÿ�ð����� ����
    /// </summary>
    public void SetToStartingStats()
    {
        StartingStatsSO startingStatsSO = DataManager.Instance.StartingStatsSO;

        // ���ȵ� ��Ÿ�ð����� ����
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

        // ��Ÿ�� MaxHp�� CurrentHp�� ����
        Stat maxHpStat = StatList.Find(statData => statData.ID == StatID.MaxHp.ToString());
        _currentHp = maxHpStat.Value;

        // ���� ������ ������Ʈ
        UpdateTotalCombatPower();
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    public void StageLevelUp()
    {
        _currentStage++;

        if (_currentStage > 5)
        {
            _currentStage = 1;
            _currentChapter++;
        }

        OnStageChange?.Invoke(); // �������� ���� �̺�Ʈ ����
    }

    /// <summary>
    /// ���� ������ ������Ʈ
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
