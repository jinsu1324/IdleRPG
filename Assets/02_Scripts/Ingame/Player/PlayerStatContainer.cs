using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ������ ����Ǿ��� �� �̺�Ʈ�� ���Ǵ� ����ü
/// </summary>
public struct OnStatChangedArgs
{
    public List<Stat> StatList;     // ���� ����Ʈ
    public int TotalPower;          // ���� ������
    public int AttackPower;         // ���ݷ�
    public int AttackSpeed;         // ���ݼӵ�
    public int MaxHp;               // �ִ� ü��
    public int Critical;            // ũ��Ƽ�� Ȯ��
}

/// <summary>
/// �÷��̾� ���� �����̳�
/// </summary>
public class PlayerStatContainer : SingletonBase<PlayerStatContainer>
{
    public static event Action<OnStatChangedArgs> OnStatChanged;   // ������ ����Ǿ��� �� �̺�Ʈ
    public int TotalPower { get; private set; }                     // ���� ������
    public int BeforeTotalPower { get; private set; }               // ���� ���� ������

    private Dictionary<StatID, Stat> _statDict;                     // ������Ʈ�� �ִ� ���ȵ��� ��ųʸ��� ������ ����
    
    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake(); // �̱��� ����

        SetStatDict(); // ���� ��ųʸ� ����
        UpdateTotalPower();  // ���� ������ ������Ʈ
    }

    /// <summary>
    /// ���� ��ųʸ� ����
    /// </summary>
    private void SetStatDict()
    {
        List<Stat> statList = GetComponentsInChildren<Stat>().ToList(); // ���ӿ�����Ʈ�� �پ��ִ� ���� ������Ʈ�� ��������
        List<StatData> startingDataList = DataManager.Instance.StartingStatDatasSO.DataList;// ��Ÿ�� ���� ������ ����Ʈ ��������
        _statDict = new Dictionary<StatID, Stat>(); // ��ųʸ� �ʱ�ȭ

        // �������� ID�� ������ StatID�� ��ġ�ϴ� ����������Ʈ�� ã�� ������Ʈ�� �����͸� ����
        foreach (Stat stat in statList)
        {
            // ID ��ġ�ϴ� ������ �˻�
            StatData findData = startingDataList.FirstOrDefault(statData => statData.ID == stat.StatID.ToString());

            // null üũ
            if (findData == null)
            {
                Debug.Log($"{stat.StatID}�� �ش��ϴ� �����͸� ã�� �� �����ϴ�.");
                return;
            }

            // ������ ����
            stat.Init(
                findData.Name,
                findData.Level,
                findData.Value,
                findData.ValueIncrease,
                findData.Cost,
                findData.CostIncrease
            );
           
            // �ߺ� Ű üũ
            if (_statDict.ContainsKey(stat.StatID) == true)
            {
                Debug.LogWarning($"��ųʸ� �� �ߺ��� ���� ID �Դϴ� : {stat.StatID}");
                return;
            }

            // ��ųʸ��� �߰�
            _statDict.Add(stat.StatID, stat); 
        }
    }

    /// <summary>
    /// Ư�� ���� ��������
    /// </summary>
    public Stat GetStat(StatID id)
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
    /// ��� ���� ��������
    /// </summary>
    public List<Stat> GetAllStats()
    {
        List<Stat> statList = new List<Stat>();
        statList = _statDict.Values.ToList();

        return statList;
    }

    /// <summary>
    /// Ư�� ���� ������ �õ�
    /// </summary>
    public bool TryStatLevelUp(StatID id)
    {
        Stat stat = GetStat(id); // id �� �´� ���� ��������

        if (stat != null && GoldManager.Instance.HasEnoughCurrency(stat.Cost)) // ������ �ְ� + �ڱ��� �ȴٸ�
        {
            StatLevelUp(id); // �� ���� ������
            return true;
        }

        return false;
    }

    /// <summary>
    /// Ư�� ���� ������
    /// </summary>
    public void StatLevelUp(StatID id)
    {
        if (_statDict.TryGetValue(id, out var stat))
        {
            // ��� ����
            GoldManager.Instance.ReduceCurrency(stat.Cost);

            // ���� ������
            stat.LevelUp();
            
            // ���� ������ ������Ʈ
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

            // ���� ���� �̺�Ʈ ȣ��
            OnStatChanged?.Invoke(args);
        }
        else
        {
            Debug.Log($"{id} ������ �������� �ʽ��ϴ�.");
        }
    }

    /// <summary>
    /// ���� ������ ������Ʈ
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
    /// ���� ������ ��������
    /// </summary>
    public int GetTotalPower() 
    { 
        return TotalPower; 
    }

    /// <summary>
    /// ���� ���� ������ ��������
    /// </summary>
    public int GetBeforeTotalPower() 
    { 
        return BeforeTotalPower; 
    }



    ///// <summary>
    ///// �÷��̾� ������ �ε�
    ///// </summary>
    //private void PlayerDataLoad()
    //{
    //    //// ������ �ε�
    //    //PlayerData = SaveLoadManager.LoadPlayerData();

    //}




    ///// <summary>
    ///// ���� �÷��̾���͸� ����
    ///// </summary>
    //public void SavePlayerData()
    //{
    //    SaveLoadManager.SavePlayerData(PlayerData);
    //}


    ///// <summary>
    ///// ���� ���� �� ���� ȣ��
    ///// </summary>
    //private void OnApplicationQuit()
    //{
    //    SavePlayerData();
    //}
}
