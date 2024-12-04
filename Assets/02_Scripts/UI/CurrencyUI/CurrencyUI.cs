using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;     // ���� ��� �ؽ�Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GoldManager.OnCurrencyChanged += UpdateCurrencyUI;  // ��ȭ�� ����� ��, ��ȭUI ������Ʈ
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        UpdateCurrencyUI(GoldManager.Instance.GetCurrencyCount());
    }

    /// <summary>
    /// UI ������Ʈ
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
