using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    public int CriticalRate;        // ũ��Ƽ�� Ȯ��
    public int CriticalMultiple;    // ũ��Ƽ�� ����
}

/// <summary>
/// �÷��̾� ���� �Ŵ���
/// </summary>
public class PlayerStatManager
{
    public static event Action<OnStatChangedArgs> OnStatChanged;    // ������ ����Ǿ��� �� �̺�Ʈ
    public static int TotalPower { get; private set; }              // ���� ������
    public static int BeforeTotalPower { get; private set; }        // ���� ���� ������

    private static Dictionary<string, Stat> _statDict;              // ���ȵ� ��ųʸ�

    /// <summary>
    /// ���� ��ųʸ� ����
    /// </summary>
    public void Init(List<StatData> startingStatDataList)
    {
        // ��ųʸ� �ʱ�ȭ
        _statDict = new Dictionary<string, Stat>();

        // ��Ÿ�� ���� ������ ����Ʈ ��������
        //List<StatData> startingDataList = DataManager.Instance.StartingStatDatasSO.DataList;

        // ���� ID�� �迭
        StatID[] statIDArr = (StatID[])Enum.GetValues(typeof(StatID));

        // ���� ID ��ŭ �ݺ�
        foreach (StatID statID in statIDArr)
        {
            // ���� ID ���ڿ��� ��ȯ
            string id = statID.ToString();

            // �ʱ⽺�� ����Ʈ�߿��� ID ��Ī�Ǵ°� ã��
            StatData findStatData = startingStatDataList.FirstOrDefault(x => x.ID == id);

            // null üũ
            if (findStatData == null)
            {
                Debug.Log($"�ʱ⽺�� ��ũ���ͺ� ������Ʈ���� {statID}�� �ش��ϴ� �����͸� ã�� �� �����ϴ�.");
                return;
            }

            // ã�� �ʱ⽺�� �����ͷ� ���� ���� 
            Stat stat = new Stat(
                            findStatData.ID,
                            findStatData.Name,
                            findStatData.Level,
                            findStatData.Value,
                            findStatData.ValueIncrease,
                            findStatData.Cost,
                            findStatData.CostIncrease
                           );

            // ��ųʸ� ID �ߺ�üũ
            if (_statDict.ContainsKey(id) == true)
            {
                Debug.LogWarning($"{id} �� �̹� ��ųʸ� �� �ߺ��� ���� ID �Դϴ�");
                return;
            }

            // ��ųʸ��� �߰�
            _statDict.Add(id, stat);
        }

        UpdateTotalPower();  // ���� ������ ������Ʈ
    }

    /// <summary>
    /// Ư�� ���� ��������
    /// </summary>
    public static Stat GetStat(string id)
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
    public static List<Stat> GetAllStats()
    {
        List<Stat> statList = new List<Stat>();
        statList = _statDict.Values.ToList();

        return statList;
    }

    /// <summary>
    /// Ư�� ���� ������ �õ�
    /// </summary>
    public static bool TryStatLevelUp(string id)
    {
        Stat stat = GetStat(id); // id �� �´� ���� ��������

        if (stat != null && GoldManager.HasEnoughCurrency(stat.Cost)) // ������ �ְ� + �ڱ��� �ȴٸ�
        {
            StatLevelUp(id); // �� ���� ������
            return true;
        }

        return false;
    }

    /// <summary>
    /// Ư�� ���� ������
    /// </summary>
    public static void StatLevelUp(string id)
    {
        if (_statDict.TryGetValue(id.ToString(), out var stat))
        {
            // ��� ����
            GoldManager.ReduceCurrency(stat.Cost);

            // ���� ������
            stat.LevelUp();
            
            // ���� ������ ������Ʈ
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
    public static void UpdateTotalPower()
    {
        BeforeTotalPower = TotalPower;

        List<int> statValueList = GetAllStats().Select(stat => stat.Value).ToList();

        TotalPower = 0;
        foreach (int value in statValueList)
            TotalPower += value;
    }

    ///// <summary>
    ///// ���� ������ ��������
    ///// </summary>
    //public int GetTotalPower() 
    //{ 
    //    return TotalPower; 
    //}

    ///// <summary>
    ///// ���� ���� ������ ��������
    ///// </summary>
    //public int GetBeforeTotalPower() 
    //{ 
    //    return BeforeTotalPower; 
    //}



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
