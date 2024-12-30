using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;     // ���� ��� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _gemText;      // ���� �� �ؽ�Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GoldManager.OnGoldChanged += UpdateGoldUI;  // ��尡 ����� ��, ��� UI ������Ʈ
        GemManager.OnGemChanged += UpdateGemUI; // ���� ����� ��, �� UI ������Ʈ
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
    /// ���UI ������Ʈ
    /// </summary>
    public void UpdateGoldUI(int amount)
    {
        _goldText.text = AlphabetNumConverter.Convert(amount);
    }

    /// <summary>
    /// ��UI ������Ʈ
    /// </summary>
    public void UpdateGemUI(int amount)
    {
        _gemText.text = AlphabetNumConverter.Convert(amount);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        GoldManager.OnGoldChanged -= UpdateGoldUI;
        GemManager.OnGemChanged -= UpdateGemUI;
    }
}
