using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;                  // 생성할 플레이어 프리팹
    [SerializeField] private Transform _playerSpawnPos;             // 생성할 플레이어의 스폰위치
    private Player _playerInstance;                                 // 생성한 플레이어 인스턴스

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        SpawnPlayer(); // 플레이어 스폰
    }

    /// <summary>
    /// 플레이어 스폰
    /// </summary>
    public void SpawnPlayer()
    {
        // 플레이어 인스턴스 생성
        _playerInstance = Instantiate(_playerPrefab, _playerSpawnPos);
        _playerInstance.transform.position = _playerSpawnPos.position;

        OnStatChangedArgs args = new OnStatChangedArgs()
        {
            StatList = PlayerStatContainer.Instance.GetAllStats(),
            TotalPower = PlayerStatContainer.Instance.TotalPower,
            AttackPower = PlayerStatContainer.Instance.GetStat(StatID.AttackPower).Value,
            AttackSpeed = PlayerStatContainer.Instance.GetStat(StatID.AttackSpeed).Value,
            MaxHp = PlayerStatContainer.Instance.GetStat(StatID.MaxHp).Value,
            Critical = PlayerStatContainer.Instance.GetStat(StatID.Critical).Value,
        };
        _playerInstance.Init(args); // 인스턴스 초기화
    }
}
