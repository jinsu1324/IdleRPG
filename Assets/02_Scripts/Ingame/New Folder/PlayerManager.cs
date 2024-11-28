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



    [SerializeField] private PlayerData _playerData;            // 플레이어 데이터 
    private DataManager _dataManager;          // 데이터 매니저


    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize(DataManager dataManager)
    {
        _dataManager = dataManager;

        // 데이터 로드
        _playerData = SaveLoadManager.LoadPlayerData();

        // PlayerData가 null이면 기본값으로 새로 생성
        if (_playerData == null)
        {
            Debug.Log("PlayerData가 null입니다. 새 데이터를 생성합니다.");
            
            // 새로 생성
            _playerData = new PlayerData();
            _playerData.InitializeStats(_dataManager);

            // 스탯 딕셔너리 초기화
            _playerData.InitializeDict();
        }
        else
        {
            // 바로 스탯 딕셔너리 초기화
            _playerData.InitializeDict();
        }
    }

    /// <summary>
    /// 특정 스탯 레벨업 시도
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
    /// 특정 스탯 가져오기
    /// </summary>
    public StatData GetStat(string id)
    {
        return _playerData.GetStat(id); 
    }

    /// <summary>
    /// 모든 스탯들 리스트를 반환
    /// </summary>
    public List<StatData> GetAllStat()
    {
        return _playerData.StatList;
    }

    /// <summary>
    /// 비용 확인
    /// </summary>
    public bool CanAffordStat(int cost)
    {
        return _playerData.CurrentGold >= cost;
    }

    /// <summary>
    /// 비용 차감
    /// </summary>
    private void DeductResources(int cost)
    {
        _playerData.CurrentGold -= cost;
        Debug.Log($"남은 골드 : {_playerData.CurrentGold}");
    }

    /// <summary>
    /// 현재 플레이어데이터를 저장
    /// </summary>
    public void SavePlayerData()
    {
        SaveLoadManager.SavePlayerData(_playerData);
    }
}
