using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;     // 현재 골드 텍스트
    [SerializeField] private TextMeshProUGUI _gemText;      // 현재 젬 텍스트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GoldManager.OnGoldChange += UpdateGoldUI;  // 골드가 변경될 때, 골드 UI 업데이트
        GemManager.OnGemChange += UpdateGemUI; // 젬이 변경될 때, 젬 UI 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        GoldManager.OnGoldChange -= UpdateGoldUI;
        GemManager.OnGemChange -= UpdateGemUI;
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        UpdateGoldUI(GoldManager.GetGold());
        UpdateGemUI(GemManager.GetGem());
    }

    /// <summary>
    /// 골드UI 업데이트
    /// </summary>
    public void UpdateGoldUI(int amount)
    {
        _goldText.text = NumberConverter.ConvertAlphabet(amount); // 알파벳으로 표현
    }

    /// <summary>
    /// 젬UI 업데이트
    /// </summary>
    public void UpdateGemUI(int amount)
    {
        _gemText.text = NumberConverter.ConvertAlphabet(amount); // 알파벳으로 표현
    }
}
