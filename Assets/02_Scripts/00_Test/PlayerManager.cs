using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public struct OnStatEventArgs
{
    public List<StatComponent> StatComponentList;
    public int TotalCombatPower;
    public int AttackPower;
    public int AttackSpeed;
    public int MaxHp;
    public int Critical;

}


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }      // �̱��� �ν��Ͻ�

    public event Action<OnStatEventArgs> OnStatChanged;

    [SerializeField] private Player _playerPrefab;                  // �÷��̾� ������
    [SerializeField] private Transform _playerSpawnPos;             // �÷��̾� ���� ��ġ

    private Dictionary<StatID, StatComponent> _statComponentDict;   // ���ȵ� ������Ʈ �����ͼ� ������ ��ųʸ�
    private Player _playerInstance;                                 // �÷��̾� �ν��Ͻ�
    private int _totalCombatPower;                                  // ���� ������
    private int _beforeTotalCombatPower;                            // ���� ���� ������


    /// <summary>
    /// Awake
    /// </summary>
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

        Init();
    }
    
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


        OnStatEventArgs args = new OnStatEventArgs()
        {
            StatComponentList = GetAllStats(),
            TotalCombatPower = _totalCombatPower,
            AttackPower = GetStat(StatID.AttackPower).Value,
            AttackSpeed = GetStat(StatID.AttackSpeed).Value,
            MaxHp = GetStat(StatID.MaxHp).Value,
            Critical = GetStat(StatID.Critical).Value,
        };


        PlayerPrefabSpawn();
        _playerInstance.Init(args);



    }

    /// <summary>
    /// �÷��̾� ������ ����
    /// </summary>
    private void PlayerPrefabSpawn()
    {
        _playerInstance = Instantiate(_playerPrefab, _playerSpawnPos);
        _playerInstance.transform.position = _playerSpawnPos.position;
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
            
            UpdateTotalCombatPower();

            OnStatEventArgs args = new OnStatEventArgs()
            {
                StatComponentList = GetAllStats(),
                TotalCombatPower = _totalCombatPower,
                AttackPower = GetStat(StatID.AttackPower).Value,
                AttackSpeed = GetStat(StatID.AttackSpeed).Value,
                MaxHp = GetStat(StatID.MaxHp).Value,
                Critical = GetStat(StatID.Critical).Value,
            };

            Debug.Log("OnStatChanged ����!");
            OnStatChanged?.Invoke(args);

            // �佺Ʈ �޽��� ȣ��
            ToastManager.Instance.ShowToastCombatPower();
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
        _beforeTotalCombatPower = _totalCombatPower;

        List<int> statValueList = GetAllStats().Select(stat => stat.Value).ToList();

        _totalCombatPower = 0;
        foreach (int value in statValueList)
            _totalCombatPower += value;
    }

    /// <summary>
    /// ���� ������ ��������
    /// </summary>
    public int GetTotalCombatPower() 
    { 
        return _totalCombatPower; 
    }

    /// <summary>
    /// ���� ���� ������ ��������
    /// </summary>
    public int GetBeforeTotalCombatPower() 
    { 
        return _beforeTotalCombatPower; 
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
