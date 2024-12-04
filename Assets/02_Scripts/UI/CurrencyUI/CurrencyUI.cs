using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;     // 현재 골드 텍스트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GoldManager.OnCurrencyChanged += UpdateCurrencyUI;  // 재화가 변경될 때, 재화UI 업데이트
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        UpdateCurrencyUI(GoldManager.Instance.GetCurrencyCount());
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    public void UpdateCurrencyUI(int amout)
    {
        _goldText.text = AlphabetNumConverter.Convert(amout);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        GoldManager.OnCurrencyChanged -= UpdateCurrencyUI;
    }
}
