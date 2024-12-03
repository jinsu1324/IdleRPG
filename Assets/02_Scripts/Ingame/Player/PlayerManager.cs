using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SerializedMonoBehaviour
{
    #region Singleton
    public static PlayerManager Instance { get; private set; }

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

        PlayerDataLoad();
    }
    #endregion

    public static PlayerData PlayerData { get; private set; }      // �÷��̾� ������
    public static Player PlayerInstance { get; private set; }      // ���� �ʵ忡 ������ �÷��̾� �ν��Ͻ��� ������ ����
    
    
    [SerializeField] private Player _playerPrefab;          // �÷��̾� ������
    [SerializeField] private Transform _playerSpawnPos;     // �÷��̾� ���� ��ġ

    /// <summary>
    /// �÷��̾� ������ �ε�
    /// </summary>
    private void PlayerDataLoad()
    {
        // ������ �ε�
        PlayerData = SaveLoadManager.LoadPlayerData();

        // PlayerData�� null�̸� �⺻������ ���� ����
        if (PlayerData == null)
        {
            Debug.Log("PlayerData�� null�Դϴ�. �� �����͸� �����մϴ�.");

            // �÷��̾� ������ ���� ����
            PlayerData = new PlayerData();
            PlayerData.SetToStartingStats();

            // ��ųʸ��� ����
            PlayerData.SetDictFromStatList();

            // �÷��̾� ������ ����
            PlayerPrefabSpawn();
        }
        else
        {
            // ��ųʸ��� ����
            PlayerData.SetDictFromStatList();

            // �÷��̾� ������ ����
            PlayerPrefabSpawn();
        }
    }

    /// <summary>
    /// �÷��̾� ������ ����
    /// </summary>
    private void PlayerPrefabSpawn()
    {
        PlayerInstance = Instantiate(_playerPrefab, _playerSpawnPos);
        PlayerInstance.transform.position = _playerSpawnPos.position;
        PlayerInstance.Init();
    }

    /// <summary>
    /// Ư�� ���� ������ �õ�
    /// </summary>
    public bool TryLevelUpStatByID(string id)
    {
        // id �� �´� ���� ��������
        Stat stat = PlayerData.GetStat(id);

        // ������ �ְ� + �ڱ��� �ȴٸ�
        if (stat != null && GoldManager.Instance.HasEnoughCurrency(stat.Cost))
        {
            // �� ����
            //ReduceGold(stat.Cost);
            GoldManager.Instance.ReduceCurrency(stat.Cost);

            // �� ���� ������
            PlayerData.LevelUpStat(id);

            // �÷��̾� ������ ���� ������Ʈ
            PlayerInstance.UpdateStat();

            return true;
        }
        return false;
    }

    /// <summary>
    /// ���� �÷��̾���͸� ����
    /// </summary>
    public void SavePlayerData()
    {
        SaveLoadManager.SavePlayerData(PlayerData);
    }

    #region - �÷��̾� ������ �������� �Լ��� -
    /// <summary>
    /// Ư�� ���� ��������
    /// </summary>
    public Stat GetStatByID(string id) { return PlayerData.GetStat(id); }

    /// <summary>
    /// ��� ���ȵ� ����Ʈ�� ��ȯ
    /// </summary>
    public List<Stat> GetAllStats() { return PlayerData.StatList; }

    /// <summary>
    /// ���� ������ ��������
    /// </summary>
    public int GetTotalCombatPower() { return PlayerData.TotalCombatPower; }

    /// <summary>
    /// ���� ���� ������ ��������
    /// </summary>
    public int GetBeforeTotalCombatPower() { return PlayerData.BeforeTotalCombatPower; }
    #endregion
}
