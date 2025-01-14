using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static Player PlayerInstance { get; private set; }       // ������ �÷��̾� �ν��Ͻ�

    [SerializeField] private Player _playerPrefab;                  // ������ �÷��̾� ������
    [SerializeField] private Transform _playerSpawnPos;             // ������ �÷��̾��� ������ġ

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
        PlayerInstance.Init();
    }

    /// <summary>
    /// ������(�ʵ忡 �ִ�) �÷��̾� �ν��Ͻ� ��ġ(Transform) �������� 
    /// </summary>
    public static Transform GetPlayerInstancePos()
    {
        return PlayerInstance.transform;
    }
}
