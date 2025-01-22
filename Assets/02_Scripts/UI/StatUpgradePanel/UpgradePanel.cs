using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private List<UpgradeSlot> _upgradeSlotList;        // 업그레이드 슬롯 리스트

    private void Start()
    {
        InitUpgradeSlotList();
    }

    /// <summary>
    /// 업그레이드 슬롯들 초기화
    /// </summary>
    private void InitUpgradeSlotList()
    {
        foreach (UpgradeSlot upgradeSlot in _upgradeSlotList)
            upgradeSlot.Init();
    }
}
