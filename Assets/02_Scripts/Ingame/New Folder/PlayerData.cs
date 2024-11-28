using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int CurrentHp;   // ���� HP
    public int CurrentGold = 100; // ���� Gold
    public List<StatData> StatList = new List<StatData>(); // ���ȵ� ����Ʈ

    // ���ȵ� ����Ʈ -> ��ųʸ�
    private Dictionary<string, StatData> _statDict = new Dictionary<string, StatData>();

    /// <summary>
    /// ��ųʸ� �ʱ�ȭ
    /// </summary>
    public void InitializeDict()
    {
        _statDict = StatList.ToDictionary(statData => statData.ID);
    }

    /// <summary>
    /// Ư�� ���� ��������
    /// </summary>
    public StatData GetStat(string id)
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
    /// ������
    /// </summary>
    public void LevelUpStat(string id)
    {
        if (_statDict.TryGetValue(id, out var stat))
        {
            stat.LevelUp();
        }
        else
        {
            Debug.Log($"{id} ������ �������� �ʽ��ϴ�.");
        }
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void InitializeStats(DataManager dataManager)
    {
        StatDatasSO statDataSO = dataManager.StatDatasSO;

        // ������ �ʱ�ȭ
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

        // MaxHp�� CurrentHp ����
        StatData maxHpStat = StatList.Find(statData => statData.ID == StatID.MaxHp.ToString());
        CurrentHp = maxHpStat.Value;
    }
}
