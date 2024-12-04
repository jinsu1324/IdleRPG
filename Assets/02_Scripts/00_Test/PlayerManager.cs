using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// ������ ����Ǿ��� �� �̺�Ʈ�� ���Ǵ� ����ü
/// </summary>
public struct OnStatChangedArgs
{
    public List<Stat> StatList;                     // ���� ����Ʈ
    public int TotalCombatPower;                    // ���� ������
    public int AttackPower;                         // ���ݷ�
    public int AttackSpeed;                         // ���ݼӵ�
    public int MaxHp;                               // �ִ� ü��
    public int Critical;                            // ũ��Ƽ�� Ȯ��
}

/// <summary>
/// �÷��̾� ������
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }      // �̱��� �ν��Ͻ�

    public event Action<OnStatChangedArgs?> OnStatChanged;          // ������ ����Ǿ��� �� �̺�Ʈ

    [SerializeField] private Player _playerPrefab;                  // ������ �÷��̾� ������
    [SerializeField] private Transform _playerSpawnPos;             // ������ �÷��̾��� ������ġ

    private Dictionary<StatID, Stat> _statDict;                     // ������Ʈ�� �ִ� ���ȵ��� ��ųʸ��� ������ ����
    private Player _playerInstance;                                 // ������ �÷��̾� �ν��Ͻ�
    private int _totalPower;                                        // ���� ������
    private int _beforeTotalPower;                                  // ���� ���� ������

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
        SetStatDict(); // ���� ��ųʸ� ����

        Debug.Log("Awake 6");
        UpdateTotalPower();  // ���� ������ ������Ʈ
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        SpawnPlayer(); // �÷��̾� ����
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
    /// �÷��̾� ����
    /// </summary>
    public void SpawnPlayer()
    {
        // �÷��̾� �ν��Ͻ� ����
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
        _playerInstance.Init(args); // �ν��Ͻ� �ʱ�ȭ
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
                TotalCombatPower = _totalPower,
                AttackPower = GetStat(StatID.AttackPower).Value,
                AttackSpeed = GetStat(StatID.AttackSpeed).Value,
                MaxHp = GetStat(StatID.MaxHp).Value,
                Critical = GetStat(StatID.Critical).Value,
            };

            Debug.Log("OnStatChanged �̺�Ʈ ȣ��!");
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
        _beforeTotalPower = _totalPower;

        List<int> statValueList = GetAllStats().Select(stat => stat.Value).ToList();

        _totalPower = 0;
        foreach (int value in statValueList)
            _totalPower += value;
    }

    /// <summary>
    /// ���� ������ ��������
    /// </summary>
    public int GetTotalPower() 
    { 
        return _totalPower; 
    }

    /// <summary>
    /// ���� ���� ������ ��������
    /// </summary>
    public int GetBeforeTotalPower() 
    { 
        return _beforeTotalPower; 
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
