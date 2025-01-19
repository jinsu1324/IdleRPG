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
        GoldManager.OnGoldChange += UpdateGoldUI;  // ��尡 ����� ��, ��� UI ������Ʈ
        GemManager.OnGemChange += UpdateGemUI; // ���� ����� ��, �� UI ������Ʈ
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
    /// ���UI ������Ʈ
    /// </summary>
    public void UpdateGoldUI(int amount)
    {
        _goldText.text = NumberConverter.ConvertAlphabet(amount); // ���ĺ����� ǥ��
    }

    /// <summary>
    /// ��UI ������Ʈ
    /// </summary>
    public void UpdateGemUI(int amount)
    {
        _gemText.text = NumberConverter.ConvertAlphabet(amount); // ���ĺ����� ǥ��
    }
}
