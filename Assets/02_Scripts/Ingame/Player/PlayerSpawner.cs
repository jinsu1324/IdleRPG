using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;                  // 생성할 플레이어 프리팹
    [SerializeField] private Transform _playerSpawnPos;             // 생성할 플레이어의 스폰위치
    public static Player PlayerInstance { get; private set; }      // 생성한 플레이어 인스턴스

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
    private void SpawnPlayer()
    {
        // 플레이어 인스턴스 생성 및 위치설정
        PlayerInstance = Instantiate(_playerPrefab, _playerSpawnPos);
        PlayerInstance.transform.position = _playerSpawnPos.position;

        // 생성된 플레이어 인스턴스 초기화
        InitialPlayer();
    }

    /// <summary>
    /// 플레이어 스탯 복구
    /// </summary>
    public static void RestorePlayerStats()
    {
        // 생성된 플레이어 인스턴스 초기화
        InitialPlayer();
    }

    /// <summary>
    /// 플레이어 스탯 가져와서 초기화
    /// </summary>
    public static void InitialPlayer()
    {
        // 이전 전투력 계산
        int beforeTotalPower = PlayerStats.GetAllStatValue();

        // 플레이어 인스턴스 초기화
        PlayerInstance.Init(PlayerStats.GetCurrentPlayerStatArgs(beforeTotalPower));
    }
}
