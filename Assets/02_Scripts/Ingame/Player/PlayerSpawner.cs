using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;                  // ������ �÷��̾� ������
    [SerializeField] private Transform _playerSpawnPos;             // ������ �÷��̾��� ������ġ
    private Player _playerInstance;                                 // ������ �÷��̾� �ν��Ͻ�

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
    public void SpawnPlayer()
    {
        // �÷��̾� �ν��Ͻ� ����
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
        _playerInstance.Init(args); // �ν��Ͻ� �ʱ�ȭ
    }
}
