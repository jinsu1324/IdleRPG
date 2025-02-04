using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalPowerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalPowerText;     // 총합 전투력 텍스트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += Update_TotalPowerText;    // 플레이어 스탯 변경되었을 때, 총합 전투력 텍스트UI 업데이트 
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= Update_TotalPowerText;
    }

    /// <summary>
    /// 총합 전투력 텍스트 업데이트
    /// </summary>
    private void Update_TotalPowerText(PlayerStatArgs args)
    {
        _totalPowerText.text = NumberConverter.ConvertAlphabet(args.TotalPower);
    }
}
