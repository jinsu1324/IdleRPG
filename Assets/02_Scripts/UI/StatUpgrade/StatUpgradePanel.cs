using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgradePanel : MonoBehaviour
{
    [SerializeField] private StatUpgradeSlot _statUpgradeSlotPrefab;      // ������ ���� ���׷��̵� ���� ������
    [SerializeField] private RectTransform _slotParent;                   // ���Ե� ������ �θ�
    private PlayerManager _playerManager;                                 // �÷��̾� �Ŵ���

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Initialize(PlayerManager playerManager)
    {
        _playerManager = playerManager;
        
        SpawnSlots();
    }

    /// <summary>
    /// ���Ե� ���� + �ʱ�ȭ
    /// </summary>
    private void SpawnSlots()
    {
        // �÷��̾� ���� ������ŭ �ݺ�
        foreach (Stat stat in _playerManager.GetAllStats())
        {
            StatUpgradeSlot statUpgradeSlot = Instantiate(_statUpgradeSlotPrefab, _slotParent);
            statUpgradeSlot.Initialize(stat.ID, _playerManager);
        }
    }
}
