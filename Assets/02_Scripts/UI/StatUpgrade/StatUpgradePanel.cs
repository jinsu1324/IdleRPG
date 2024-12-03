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


    private void Awake()
    {
        if (PlayerManager.Instance != null)
            PlayerManager.Instance.OnStatChanged += UpdateTotalCombatPowerText;
    }

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
        foreach (StatComponent stat in PlayerManager.Instance.GetAllStats())
        {
            StatUpgradeSlot statUpgradeSlot = Instantiate(_statUpgradeSlotPrefab, _slotParent);
            statUpgradeSlot.Init(stat.StatID);
        }

        OnStatChangedArgs args = new OnStatChangedArgs() { TotalCombatPower = PlayerManager.Instance.GetTotalCombatPower() };
        UpdateTotalCombatPowerText(args);
    }

    /// <summary>
    /// 총합 전투력 텍스트 업데이트
    /// </summary>
    private void UpdateTotalCombatPowerText(OnStatChangedArgs args)
    {
        Debug.Log("3 - 2. Update_TotalCombatPowerText!");
        _totalCombatPowerText.text = AlphabetNumConverter.Convert(args.TotalCombatPower);
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance != null)
            PlayerManager.Instance.OnStatChanged -= UpdateTotalCombatPowerText;
    }
}
