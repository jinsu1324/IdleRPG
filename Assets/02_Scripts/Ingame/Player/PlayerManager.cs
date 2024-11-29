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

    [SerializeField] private PlayerData _playerData;        // �÷��̾� ������ (SerializeField�� Ȯ�ο�)

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
        }
        else
        {
            // ��ųʸ��� ����
            _playerData.SetDictFromStatList();
        }
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
