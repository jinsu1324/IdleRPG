using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerCore PlayerInstance { get; private set; }       // 생성한 플레이어 인스턴스

    [SerializeField] private PlayerCore _playerPrefab;                  // 생성할 플레이어 프리팹
    [SerializeField] private Transform _playerSpawnPos;             // 생성할 플레이어의 스폰위치

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
        PlayerInstance.Init();
    }

    /// <summary>
    /// 생성한(필드에 있는) 플레이어 인스턴스 위치(Transform) 가져오기 
    /// </summary>
    public static Transform GetPlayerInstancePos()
    {
        return PlayerInstance.transform;
    }
}
