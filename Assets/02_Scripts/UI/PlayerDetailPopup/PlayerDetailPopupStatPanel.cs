using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetailPopupStatPanel : MonoBehaviour
{
    [SerializeField] private PlayerDetailPopupStatSlot _playerDetailPopupStatSlot;  // 스탯 슬롯
    [SerializeField] private RectTransform _slotParent;                             // 슬롯들 생성할 부모

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        SpawnSlots();

    }

    /// <summary>
    /// 슬롯들 생성 + 초기화
    /// </summary>
    private void SpawnSlots()
    {
        // 플레이어 스탯 갯수만큼 반복
        foreach (Stat stat in PlayerStatContainer.Instance.GetAllStats())
        {
            PlayerDetailPopupStatSlot slot = Instantiate(_playerDetailPopupStatSlot, _slotParent);
            slot.Init(stat.StatID);
        }
    }
}
