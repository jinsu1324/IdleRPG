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

    public PlayerData PlayerData { get; private set; }      // �÷��̾� ������
    private Player _playerInstance;                         // ���� �ʵ忡 ������ �÷��̾� �ν��Ͻ��� ������ ����
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
            PlayerPrefabSpawn(PlayerData);
        }
        else
        {
            // ��ųʸ��� ����
            PlayerData.SetDictFromStatList();

            // �÷��̾� ������ ����
            PlayerPrefabSpawn(PlayerData);
        }
    }

    /// <summary>
    /// �÷��̾� ������ ����
    /// </summary>
    private void PlayerPrefabSpawn(PlayerData playerData)
    {
        _playerInstance = Instantiate(_playerPrefab, _playerSpawnPos);
        _playerInstance.transform.position = _playerSpawnPos.position;
        _playerInstance.Initialize(playerData);
    }

    /// <summary>
    /// Ư�� ���� ������ �õ�
    /// </summary>
    public bool TryLevelUpStatByID(string id)
    {
        // id �� �´� ���� ��������
        Stat stat = PlayerData.GetStat(id);

        // ������ �ְ� + �ڱ��� �ȴٸ�
        if (stat != null && HasEnoughGold(stat.Cost))
        {
            // �� ����
            MinusGold(stat.Cost);

            // �� ���� ������
            PlayerData.LevelUpStat(id);

            // �÷��̾� ������ ���� ������Ʈ
            _playerInstance.UpdateStat(PlayerData);

            return true;
        }
        return false;
    }

    /// <summary>
    /// �÷��̾� �������� �������� ������
    /// </summary>
    public void StageLevelUp_of_PlayerData()
    {
        PlayerData.StageLevelUp();
    }

    /// <summary>
    /// ���׷��̵� �Ҹ��� ���� ����� ������ �ִ���
    /// </summary>
    public bool HasEnoughGold(int cost)
    {
        return PlayerData.CurrentGold >= cost;
    }

    /// <summary>
    /// ��� ȹ��
    /// </summary>
    public void AddGold(int value)
    {
        PlayerData.CurrentGold += value;
    }

    /// <summary>
    /// ��� ����
    /// </summary>
    private void MinusGold(int value)
    {
        PlayerData.CurrentGold -= value;
    }

    /// <summary>
    /// ���� �ʵ忡 ��ȯ�Ǿ��ִ� �÷��̾� �ν��Ͻ� ��������
    /// </summary>
    public Player GetPlayerInstance()
    {
        return _playerInstance;
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
    /// ���� �������� ��������
    /// </summary>
    public int GetCurrentStage() { return PlayerData.CurrentStage; }

    /// <summary>
    /// ���� é�� ��������
    /// </summary>
    public int GetCurrentChapter() { return PlayerData.CurrentChapter; }

    /// <summary>
    /// ���� ������ ��������
    /// </summary>
    public int GetTotalCombatPower() { return PlayerData.TotalCombatPower; }

    /// <summary>
    /// ���� ���� ������ ��������
    /// </summary>
    public int GetBeforeTotalCombatPower() { return PlayerData.BeforeTotalCombatPower; }

    /// <summary>
    /// ���� ��� ��������
    /// </summary>
    public int GetCurrentGold() { return PlayerData.CurrentGold; }
    #endregion





    // �ӽ� ġƮ
    public void OnPlusGold()
    {
        PlayerData.CurrentGold += 1000;
    }
}
