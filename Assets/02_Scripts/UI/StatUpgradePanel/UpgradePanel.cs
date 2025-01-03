using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private UpgradeSlot _upgradeSlotPrefab;              // 생성할 업그레이드 슬롯 프리팹
    [SerializeField] private RectTransform _slotParent;                   // 슬롯들 생성할 부모
    [SerializeField] private TextMeshProUGUI _totalCombatPowerText;       // 총합 전투력 텍스트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += UpdateTotalPowerTextUI;    // 플레이어 스탯 변경되었을 때, 총합 전투력 텍스트UI 업데이트 
    }

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
        // 플레이어 업그레이드 갯수만큼 반복
        foreach (Upgrade upgrade in UpgradeManager.GetAllUpgrades())
        {
            UpgradeSlot statUpgradeSlot = Instantiate(_upgradeSlotPrefab, _slotParent);
            statUpgradeSlot.Init(upgrade.ID);
        }

        PlayerStatArgs args = new PlayerStatArgs() { TotalPower = (int)Mathf.Floor(PlayerStats.GetAllStatValue()) };
        UpdateTotalPowerTextUI(args);
    }

    /// <summary>
    /// 총합 전투력 텍스트 업데이트
    /// </summary>
    private void UpdateTotalPowerTextUI(PlayerStatArgs args)
    {
        _totalCombatPowerText.text = AlphabetNumConverter.Convert(args.TotalPower);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= UpdateTotalPowerTextUI;
    }
}
