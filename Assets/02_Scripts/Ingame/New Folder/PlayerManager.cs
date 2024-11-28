using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SerializedMonoBehaviour
{
    public void OnPlusGold()
    {
        _playerData.CurrentGold += 1000;
    }



    [SerializeField] private PlayerData _playerData;            // �÷��̾� ������ 
    private DataManager _dataManager;          // ������ �Ŵ���


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
            
            // ���� ����
            _playerData = new PlayerData();
            _playerData.InitializeStats(_dataManager);

            // ���� ��ųʸ� �ʱ�ȭ
            _playerData.InitializeDict();
        }
        else
        {
            // �ٷ� ���� ��ųʸ� �ʱ�ȭ
            _playerData.InitializeDict();
        }
    }

    /// <summary>
    /// Ư�� ���� ������ �õ�
    /// </summary>
    public bool LevelUpStat(string id)
    {
        var stat = _playerData.GetStat(id);
        if (stat != null && CanAffordStat(stat.Cost))
        {
            DeductResources(stat.Cost);
            _playerData.LevelUpStat(id);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Ư�� ���� ��������
    /// </summary>
    public StatData GetStat(string id)
    {
        return _playerData.GetStat(id); 
    }

    /// <summary>
    /// ��� ���ȵ� ����Ʈ�� ��ȯ
    /// </summary>
    public List<StatData> GetAllStat()
    {
        return _playerData.StatList;
    }

    /// <summary>
    /// ��� Ȯ��
    /// </summary>
    public bool CanAffordStat(int cost)
    {
        return _playerData.CurrentGold >= cost;
    }

    /// <summary>
    /// ��� ����
    /// </summary>
    private void DeductResources(int cost)
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
}
