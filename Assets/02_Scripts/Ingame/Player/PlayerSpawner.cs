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
        // �÷��̾� �ν��Ͻ� ���� �� ��ġ����
        PlayerInstance = Instantiate(_playerPrefab, _playerSpawnPos);
        PlayerInstance.transform.position = _playerSpawnPos.position;

        // ������ �÷��̾� �ν��Ͻ� �ʱ�ȭ
        InitialPlayer();
    }

    /// <summary>
    /// �÷��̾� ���� ����
    /// </summary>
    public static void RestorePlayerStats()
    {
        // ������ �÷��̾� �ν��Ͻ� �ʱ�ȭ
        InitialPlayer();
    }

    /// <summary>
    /// �÷��̾� ���� �����ͼ� �ʱ�ȭ
    /// </summary>
    public static void InitialPlayer()
    {
        // ���� ������ ���
        int beforeTotalPower = PlayerStats.GetAllStatValue();

        // �÷��̾� �ν��Ͻ� �ʱ�ȭ
        PlayerInstance.Init(PlayerStats.GetCurrentPlayerStatArgs(beforeTotalPower));
    }
}
