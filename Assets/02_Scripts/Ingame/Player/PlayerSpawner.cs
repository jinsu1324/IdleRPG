using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;                  // ������ �÷��̾� ������
    [SerializeField] private Transform _playerSpawnPos;             // ������ �÷��̾��� ������ġ
    public static Player PlayerInstance { get; private set; }      // ������ �÷��̾� �ν��Ͻ�

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        SpawnPlayer(); // �÷��̾� ����
    }

    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    private void SpawnPlayer()
    {
        // �÷��̾� �ν��Ͻ� ����
        PlayerInstance = Instantiate(_playerPrefab, _playerSpawnPos);
        PlayerInstance.transform.position = _playerSpawnPos.position;

        PlayerStatArgs args = new PlayerStatArgs()
        {
            TotalPower = (int)Mathf.Floor(PlayerStats.Instance.GetAllFinalStat()),
            AttackPower = (int)Mathf.Floor(PlayerStats.Instance.GetFinalStat(StatType.AttackPower)),
            AttackSpeed = (int)Mathf.Floor(PlayerStats.Instance.GetFinalStat(StatType.AttackSpeed)),
            MaxHp = (int)Mathf.Floor(PlayerStats.Instance.GetFinalStat(StatType.MaxHp)),
            CriticalRate = (int)Mathf.Floor(PlayerStats.Instance.GetFinalStat(StatType.CriticalRate)),
            CriticalMultiple = (int)Mathf.Floor(PlayerStats.Instance.GetFinalStat(StatType.CriticalMultiple))
        };
        PlayerInstance.Init(args); // �ν��Ͻ� �ʱ�ȭ
    }

    /// <summary>
    /// �÷��̾� ���� ����
    /// </summary>
    public static void RestorePlayerStats()
    {
        PlayerStatArgs args = new PlayerStatArgs()
        {
            TotalPower = (int)Mathf.Floor(PlayerStats.Instance.GetAllFinalStat()),
            AttackPower = (int)Mathf.Floor(PlayerStats.Instance.GetFinalStat(StatType.AttackPower)),
            AttackSpeed = (int)Mathf.Floor(PlayerStats.Instance.GetFinalStat(StatType.AttackSpeed)),
            MaxHp = (int)Mathf.Floor(PlayerStats.Instance.GetFinalStat(StatType.MaxHp)),
            CriticalRate = (int)Mathf.Floor(PlayerStats.Instance.GetFinalStat(StatType.CriticalRate)),
            CriticalMultiple = (int)Mathf.Floor(PlayerStats.Instance.GetFinalStat(StatType.CriticalMultiple))
        };
        PlayerInstance.Init(args);
    }
}
