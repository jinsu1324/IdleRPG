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

    private PlayerData _playerData;                         // 플레이어 데이터
    private Player _playerInstance;                         // 실제 필드에 스폰된 플레이어 인스턴스를 저장할 변수
    [SerializeField] private Player _playerPrefab;          // 플레이어 프리팹
    [SerializeField] private Transform _playerSpawnPos;     // 플레이어 스폰 위치

    /// <summary>
    /// 플레이어 데이터 로드
    /// </summary>
    private void PlayerDataLoad()
    {
        // 데이터 로드
        _playerData = SaveLoadManager.LoadPlayerData();

        // PlayerData가 null이면 기본값으로 새로 생성
        if (_playerData == null)
        {
            Debug.Log("PlayerData가 null입니다. 새 데이터를 생성합니다.");

            // 플레이어 데이터 새로 생성
            _playerData = new PlayerData();
            _playerData.SetToStartingStats();

            // 딕셔너리도 셋팅
            _playerData.SetDictFromStatList();

            // 플레이어 프리팹 생성
            PlayerPrefabSpawn(_playerData);
        }
        else
        {
            // 딕셔너리도 셋팅
            _playerData.SetDictFromStatList();

            // 플레이어 프리팹 생성
            PlayerPrefabSpawn(_playerData);
        }
    }

    /// <summary>
    /// 플레이어 프리팹 스폰
    /// </summary>
    private void PlayerPrefabSpawn(PlayerData playerData)
    {
        _playerInstance = Instantiate(_playerPrefab, _playerSpawnPos);
        _playerInstance.transform.position = _playerSpawnPos.position;
        _playerInstance.Initialize(playerData);
    }

    /// <summary>
    /// 특정 스탯 레벨업 시도
    /// </summary>
    public bool TryLevelUpStatByID(string id)
    {
        // id 에 맞는 스탯 가져오기
        Stat stat = _playerData.GetStat(id);

        // 스탯이 있고 + 자금이 된다면
        if (stat != null && HasEnoughGold(stat.Cost))
        {
            // 돈 감소
            ReduceGold(stat.Cost);

            // 그 스탯 레벨업
            _playerData.LevelUpStat(id);

            // 플레이어 프리팹 스탯 업데이트
            _playerInstance.UpdateStat(_playerData);

            return true;
        }
        return false;
    }

    /// <summary>
    /// 특정 스탯 가져오기
    /// </summary>
    public Stat GetStatByID(string id)
    {
        return _playerData.GetStat(id); 
    }

    /// <summary>
    /// 모든 스탯들 리스트를 반환
    /// </summary>
    public List<Stat> GetAllStats()
    {
        return _playerData.StatList;
    }

    /// <summary>
    /// 업그레이드 할만한 돈을 충분히 가지고 있는지
    /// </summary>
    public bool HasEnoughGold(int cost)
    {
        return _playerData.CurrentGold >= cost;
    }

    /// <summary>
    /// 골드 감소
    /// </summary>
    private void ReduceGold(int cost)
    {
        _playerData.CurrentGold -= cost;
        Debug.Log($"남은 골드 : {_playerData.CurrentGold}");
    }

    /// <summary>
    /// 실제 필드에 소환되어있는 플레이어 인스턴스 가져오기
    /// </summary>
    public Player GetPlayerInstance()
    {
        return _playerInstance;
    }

    /// <summary>
    /// 현재 플레이어데이터를 저장
    /// </summary>
    public void SavePlayerData()
    {
        SaveLoadManager.SavePlayerData(_playerData);
    }




    // 임시 치트
    public void OnPlusGold()
    {
        _playerData.CurrentGold += 1000;
    }

}
