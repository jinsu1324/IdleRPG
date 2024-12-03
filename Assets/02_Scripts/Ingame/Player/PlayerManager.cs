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

    [SerializeField] private Player _playerPrefab;                 // 플레이어 프리팹
    [SerializeField] private Transform _playerSpawnPos;            // 플레이어 스폰 위치
   
    /// <summary>
    /// 플레이어 데이터 로드
    /// </summary>
    private void PlayerDataLoad()
    {
        //// 데이터 로드
        //PlayerData = SaveLoadManager.LoadPlayerData();

        // 스탯들 초기화 임시 TODO
        StatManager.Instance.Init();

        

        // 스테이지 초기화
        StageManager.Instance.Init();

    }

    private void OnEnable()
    {
        PlayerPrefabSpawn();
    }


    /// <summary>
    /// 플레이어 프리팹 스폰
    /// </summary>
    private void PlayerPrefabSpawn()
    {
        Player player = Instantiate(_playerPrefab, _playerSpawnPos);
        player.transform.position = _playerSpawnPos.position;
    }

  

    ///// <summary>
    ///// 현재 플레이어데이터를 저장
    ///// </summary>
    //public void SavePlayerData()
    //{
    //    SaveLoadManager.SavePlayerData(PlayerData);
    //}
   

    ///// <summary>
    ///// 게임 종료 시 저장 호출
    ///// </summary>
    //private void OnApplicationQuit()
    //{
    //    SavePlayerData();
    //}
}
