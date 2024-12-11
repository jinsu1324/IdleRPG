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
        // 플레이어 인스턴스 생성
        PlayerInstance = Instantiate(_playerPrefab, _playerSpawnPos);
        PlayerInstance.transform.position = _playerSpawnPos.position;

        OnStatChangedArgs args = new OnStatChangedArgs()
        {
            UpgradeList = UpgradeManager.GetAllUpgrades(),
            TotalPower = (int)PlayerStats.Instance.GetAllFinalStat(),
            AttackPower = (int)PlayerStats.Instance.GetFinalStat(StatType.AttackPower),
            AttackSpeed = (int)PlayerStats.Instance.GetFinalStat(StatType.AttackSpeed),
            MaxHp = (int)PlayerStats.Instance.GetFinalStat(StatType.MaxHp),
            CriticalRate = (int)PlayerStats.Instance.GetFinalStat(StatType.CriticalRate),
            CriticalMultiple = (int)PlayerStats.Instance.GetFinalStat(StatType.CriticalMultiple)
        };

        Debug.Log($"AttackSpeed : {args.AttackSpeed}");
        PlayerInstance.Init(args); // 인스턴스 초기화
    }

    /// <summary>
    /// 플레이어 스탯 복구
    /// </summary>
    public static void RestorePlayerStats()
    {
        OnStatChangedArgs args = new OnStatChangedArgs()
        {
            UpgradeList = UpgradeManager.GetAllUpgrades(),
            TotalPower = (int)PlayerStats.Instance.GetAllFinalStat(),
            AttackPower = (int)PlayerStats.Instance.GetFinalStat(StatType.AttackPower),
            AttackSpeed = (int)PlayerStats.Instance.GetFinalStat(StatType.AttackSpeed),
            MaxHp = (int)PlayerStats.Instance.GetFinalStat(StatType.MaxHp),
            CriticalRate = (int)PlayerStats.Instance.GetFinalStat(StatType.CriticalRate),
            CriticalMultiple = (int)PlayerStats.Instance.GetFinalStat(StatType.CriticalMultiple)
        };
        PlayerInstance.Init(args);
    }
}
