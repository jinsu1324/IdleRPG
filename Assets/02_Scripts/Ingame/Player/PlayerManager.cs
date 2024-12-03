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

    [SerializeField] private Player _playerPrefab;                 // �÷��̾� ������
    [SerializeField] private Transform _playerSpawnPos;            // �÷��̾� ���� ��ġ
   
    /// <summary>
    /// �÷��̾� ������ �ε�
    /// </summary>
    private void PlayerDataLoad()
    {
        //// ������ �ε�
        //PlayerData = SaveLoadManager.LoadPlayerData();

        // ���ȵ� �ʱ�ȭ �ӽ� TODO
        StatManager.Instance.Init();

        

        // �������� �ʱ�ȭ
        StageManager.Instance.Init();

    }

    private void OnEnable()
    {
        PlayerPrefabSpawn();
    }


    /// <summary>
    /// �÷��̾� ������ ����
    /// </summary>
    private void PlayerPrefabSpawn()
    {
        Player player = Instantiate(_playerPrefab, _playerSpawnPos);
        player.transform.position = _playerSpawnPos.position;
    }

  

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
