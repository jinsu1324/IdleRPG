using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;     // 현재 골드 텍스트
    
    /// <summary>
    /// Start
    /// </summary>
    private void OnEnable()
    {
        GoldManager.OnCurrencyChanged += UpdateUI;
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        UpdateUI(GoldManager.Instance.GetCurrencyCount());
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    public void UpdateUI(int amout)
    {
        _goldText.text = AlphabetNumConverter.Convert(amout);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        GoldManager.OnCurrencyChanged -= UpdateUI;
    }
}
