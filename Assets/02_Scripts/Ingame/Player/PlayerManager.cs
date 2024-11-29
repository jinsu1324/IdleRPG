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

    private PlayerData _playerData;                         // �÷��̾� ������
    private Player _playerInstance;                         // ���� �ʵ忡 ������ �÷��̾� �ν��Ͻ��� ������ ����
    [SerializeField] private Player _playerPrefab;          // �÷��̾� ������
    [SerializeField] private Transform _playerSpawnPos;     // �÷��̾� ���� ��ġ

    /// <summary>
    /// �÷��̾� ������ �ε�
    /// </summary>
    private void PlayerDataLoad()
    {
        // ������ �ε�
        _playerData = SaveLoadManager.LoadPlayerData();

        // PlayerData�� null�̸� �⺻������ ���� ����
        if (_playerData == null)
        {
            Debug.Log("PlayerData�� null�Դϴ�. �� �����͸� �����մϴ�.");

            // �÷��̾� ������ ���� ����
            _playerData = new PlayerData();
            _playerData.SetToStartingStats();

            // ��ųʸ��� ����
            _playerData.SetDictFromStatList();

            // �÷��̾� ������ ����
            PlayerPrefabSpawn(_playerData);
        }
        else
        {
            // ��ųʸ��� ����
            _playerData.SetDictFromStatList();

            // �÷��̾� ������ ����
            PlayerPrefabSpawn(_playerData);
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
        Stat stat = _playerData.GetStat(id);

        // ������ �ְ� + �ڱ��� �ȴٸ�
        if (stat != null && HasEnoughGold(stat.Cost))
        {
            // �� ����
            ReduceGold(stat.Cost);

            // �� ���� ������
            _playerData.LevelUpStat(id);

            // �÷��̾� ������ ���� ������Ʈ
            _playerInstance.UpdateStat(_playerData);

            return true;
        }
        return false;
    }

    /// <summary>
    /// Ư�� ���� ��������
    /// </summary>
    public Stat GetStatByID(string id)
    {
        return _playerData.GetStat(id); 
    }

    /// <summary>
    /// ��� ���ȵ� ����Ʈ�� ��ȯ
    /// </summary>
    public List<Stat> GetAllStats()
    {
        return _playerData.StatList;
    }

    /// <summary>
    /// ���׷��̵� �Ҹ��� ���� ����� ������ �ִ���
    /// </summary>
    public bool HasEnoughGold(int cost)
    {
        return _playerData.CurrentGold >= cost;
    }

    /// <summary>
    /// ��� ����
    /// </summary>
    private void ReduceGold(int cost)
    {
        _playerData.CurrentGold -= cost;
        Debug.Log($"���� ��� : {_playerData.CurrentGold}");
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
        SaveLoadManager.SavePlayerData(_playerData);
    }




    // �ӽ� ġƮ
    public void OnPlusGold()
    {
        _playerData.CurrentGold += 1000;
    }

}
