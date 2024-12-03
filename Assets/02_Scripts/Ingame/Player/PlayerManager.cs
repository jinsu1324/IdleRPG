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

    public static PlayerData PlayerData { get; private set; }      // 플레이어 데이터
    public static Player PlayerInstance { get; private set; }      // 실제 필드에 스폰된 플레이어 인스턴스를 저장할 변수
    
    
    [SerializeField] private Player _playerPrefab;          // 플레이어 프리팹
    [SerializeField] private Transform _playerSpawnPos;     // 플레이어 스폰 위치

    /// <summary>
    /// 플레이어 데이터 로드
    /// </summary>
    private void PlayerDataLoad()
    {
        // 데이터 로드
        PlayerData = SaveLoadManager.LoadPlayerData();

        // PlayerData가 null이면 기본값으로 새로 생성
        if (PlayerData == null)
        {
            Debug.Log("PlayerData가 null입니다. 새 데이터를 생성합니다.");

            // 플레이어 데이터 새로 생성
            PlayerData = new PlayerData();
            PlayerData.SetToStartingStats();

            // 딕셔너리도 셋팅
            PlayerData.SetDictFromStatList();

            // 플레이어 프리팹 생성
            PlayerPrefabSpawn();
        }
        else
        {
            // 딕셔너리도 셋팅
            PlayerData.SetDictFromStatList();

            // 플레이어 프리팹 생성
            PlayerPrefabSpawn();
        }
    }

    /// <summary>
    /// 플레이어 프리팹 스폰
    /// </summary>
    private void PlayerPrefabSpawn()
    {
        PlayerInstance = Instantiate(_playerPrefab, _playerSpawnPos);
        PlayerInstance.transform.position = _playerSpawnPos.position;
        PlayerInstance.Init();
    }

    /// <summary>
    /// 특정 스탯 레벨업 시도
    /// </summary>
    public bool TryLevelUpStatByID(string id)
    {
        // id 에 맞는 스탯 가져오기
        Stat stat = PlayerData.GetStat(id);

        // 스탯이 있고 + 자금이 된다면
        if (stat != null && GoldManager.Instance.HasEnoughCurrency(stat.Cost))
        {
            // 돈 감소
            //ReduceGold(stat.Cost);
            GoldManager.Instance.ReduceCurrency(stat.Cost);

            // 그 스탯 레벨업
            PlayerData.LevelUpStat(id);

            // 플레이어 프리팹 스탯 업데이트
            PlayerInstance.UpdateStat();

            return true;
        }
        return false;
    }

    /// <summary>
    /// 현재 플레이어데이터를 저장
    /// </summary>
    public void SavePlayerData()
    {
        SaveLoadManager.SavePlayerData(PlayerData);
    }

    #region - 플레이어 데이터 가져오는 함수들 -
    /// <summary>
    /// 특정 스탯 가져오기
    /// </summary>
    public Stat GetStatByID(string id) { return PlayerData.GetStat(id); }

    /// <summary>
    /// 모든 스탯들 리스트를 반환
    /// </summary>
    public List<Stat> GetAllStats() { return PlayerData.StatList; }

    /// <summary>
    /// 총합 전투력 가져오기
    /// </summary>
    public int GetTotalCombatPower() { return PlayerData.TotalCombatPower; }

    /// <summary>
    /// 이전 총합 전투력 가져오기
    /// </summary>
    public int GetBeforeTotalCombatPower() { return PlayerData.BeforeTotalCombatPower; }
    #endregion
}
