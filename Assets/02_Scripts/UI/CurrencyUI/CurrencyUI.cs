using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText; // 현재 골드 텍스트
    
    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        PlayerManager.Instance.PlayerData.OnGoldChange += UpdateUI; // 골드 변경 이벤트 구독
        UpdateUI();
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerManager.Instance.PlayerData.OnGoldChange -= UpdateUI; // 골드 변경 이벤트 구독해제 
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    public void UpdateUI()
    {
        _goldText.text = AlphabetNumConverter.Convert(PlayerManager.Instance.GetCurrentGold());
    }
}
