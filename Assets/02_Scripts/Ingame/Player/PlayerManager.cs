using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SerializedMonoBehaviour
{   
    [SerializeField] private PlayerData _playerData;        // �÷��̾� ������ (SerializeField�� Ȯ�ο�)
    private DataManager _dataManager;                       // ������ �Ŵ���

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Initialize(DataManager dataManager)
    {
        _dataManager = dataManager;

        // ������ �ε�
        _playerData = SaveLoadManager.LoadPlayerData();

        // PlayerData�� null�̸� �⺻������ ���� ����
        if (_playerData == null)
        {
            Debug.Log("PlayerData�� null�Դϴ�. �� �����͸� �����մϴ�.");
            
            // �÷��̾� ������ ���� ����
            _playerData = new PlayerData();
            _playerData.SetToStartingStats(_dataManager);

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
