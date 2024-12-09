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

        OnStatChangedArgs args = new OnStatChangedArgs()
        {
            StatList = PlayerStatContainer.Instance.GetAllStats(),
            TotalPower = PlayerStatContainer.Instance.TotalPower,
            AttackPower = PlayerStatContainer.Instance.GetStat(StatID.AttackPower.ToString()).Value,
            AttackSpeed = PlayerStatContainer.Instance.GetStat(StatID.AttackSpeed.ToString()).Value,
            MaxHp = PlayerStatContainer.Instance.GetStat(StatID.MaxHp.ToString()).Value,
            CriticalRate = PlayerStatContainer.Instance.GetStat(StatID.CriticalRate.ToString()).Value,
            CriticalMultiple = PlayerStatContainer.Instance.GetStat(StatID.CriticalMultiple.ToString()).Value
        };
        PlayerInstance.Init(args); // �ν��Ͻ� �ʱ�ȭ
    }

    /// <summary>
    /// �÷��̾� ���� ����
    /// </summary>
    public static void RestorePlayerStats()
    {
        OnStatChangedArgs args = new OnStatChangedArgs()
        {
            StatList = PlayerStatContainer.Instance.GetAllStats(),
            TotalPower = PlayerStatContainer.Instance.TotalPower,
            AttackPower = PlayerStatContainer.Instance.GetStat(StatID.AttackPower.ToString()).Value,
            AttackSpeed = PlayerStatContainer.Instance.GetStat(StatID.AttackSpeed.ToString()).Value,
            MaxHp = PlayerStatContainer.Instance.GetStat(StatID.MaxHp.ToString()).Value,
            CriticalRate = PlayerStatContainer.Instance.GetStat(StatID.CriticalRate.ToString()).Value,
            CriticalMultiple = PlayerStatContainer.Instance.GetStat(StatID.CriticalMultiple.ToString()).Value
        };
        PlayerInstance.Init(args);
    }
}
