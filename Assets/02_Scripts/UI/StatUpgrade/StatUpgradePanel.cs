using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgradePanel : MonoBehaviour
{
    [SerializeField] private StatUpgradeSlot _statUpgradeSlotPrefab;      // 생성할 스탯 업그레이드 슬롯 프리팹
    [SerializeField] private RectTransform _slotParent;                   // 슬롯들 생성할 부모
    private PlayerManager _playerManager;                                 // 플레이어 매니저

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize(PlayerManager playerManager)
    {
        _playerManager = playerManager;
        
        SpawnSlots();
    }

    /// <summary>
    /// 슬롯들 생성 + 초기화
    /// </summary>
    private void SpawnSlots()
    {
        // 플레이어 스탯 갯수만큼 반복
        foreach (Stat stat in _playerManager.GetAllStats())
        {
            StatUpgradeSlot statUpgradeSlot = Instantiate(_statUpgradeSlotPrefab, _slotParent);
            statUpgradeSlot.Initialize(stat.ID, _playerManager);
        }
    }
}
