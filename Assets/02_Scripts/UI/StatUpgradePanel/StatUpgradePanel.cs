using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatUpgradePanel : MonoBehaviour
{
    [SerializeField] private StatUpgradeSlot _statUpgradeSlotPrefab;      // 생성할 스탯 업그레이드 슬롯 프리팹
    [SerializeField] private RectTransform _slotParent;                   // 슬롯들 생성할 부모
    [SerializeField] private TextMeshProUGUI _totalCombatPowerText;       // 총합 전투력 텍스트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStatManager.OnStatChanged += UpdateTotalPowerTextUI;    // 스탯이 변경될 때, 총합 전투력 텍스트UI 업데이트 
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
        // 플레이어 스탯 갯수만큼 반복
        foreach (Stat stat in PlayerStatManager.GetAllStats())
        {
            StatUpgradeSlot statUpgradeSlot = Instantiate(_statUpgradeSlotPrefab, _slotParent);
            statUpgradeSlot.Init(stat.ID);
        }

        OnStatChangedArgs args = new OnStatChangedArgs() { TotalPower = PlayerStatManager.TotalPower };
        UpdateTotalPowerTextUI(args);
    }

    /// <summary>
    /// 총합 전투력 텍스트 업데이트
    /// </summary>
    private void UpdateTotalPowerTextUI(OnStatChangedArgs args)
    {
        _totalCombatPowerText.text = AlphabetNumConverter.Convert(args.TotalPower);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStatManager.OnStatChanged -= UpdateTotalPowerTextUI;
    }
}
