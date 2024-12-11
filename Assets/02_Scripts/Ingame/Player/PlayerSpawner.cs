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
            UpgradeList = UpgradeManager.GetAllUpgrades(),
            TotalPower = (int)PlayerStats.Instance.GetAllFinalStat(),
            AttackPower = (int)PlayerStats.Instance.GetFinalStat(StatType.AttackPower),
            AttackSpeed = (int)PlayerStats.Instance.GetFinalStat(StatType.AttackSpeed),
            MaxHp = (int)PlayerStats.Instance.GetFinalStat(StatType.MaxHp),
            CriticalRate = (int)PlayerStats.Instance.GetFinalStat(StatType.CriticalRate),
            CriticalMultiple = (int)PlayerStats.Instance.GetFinalStat(StatType.CriticalMultiple)
        };

        Debug.Log($"AttackSpeed : {args.AttackSpeed}");
        PlayerInstance.Init(args); // �ν��Ͻ� �ʱ�ȭ
    }

    /// <summary>
    /// �÷��̾� ���� ����
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
