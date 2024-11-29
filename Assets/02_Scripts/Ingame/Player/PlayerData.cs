using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int CurrentChapter = 1;                  // ���� é��
    public int CurrentStage = 1;                    // ���� ��������
    public int CurrentGold = 1000000;               // (!!!!!!!!!!!!!�ӽð�!!!!!!!!!!!!!!) ���� Gold 
    public int CurrentHp;                           // ���� HP
    public List<Stat> StatList = new List<Stat>();  // ���ȵ� ����Ʈ
    private Dictionary<string, Stat> _statDict = new Dictionary<string, Stat>(); // ���ȵ� ����Ʈ -> ��ųʸ���

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
        CurrentHp = maxHpStat.Value;
    }

    /// <summary>
    /// �������� ������
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
}
