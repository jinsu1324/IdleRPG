using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;     // ���� ��� �ؽ�Ʈ
    
    /// <summary>
    /// Start
    /// </summary>
    private void OnEnable()
    {
        GoldManager.Instance.OnCurrencyChanged += UpdateUI;
        Debug.Log("CurrencyUI OnEnable �����Ϸ�!");

        UpdateUI(GoldManager.Instance.GetCurrencyCount());
    }

    /// <summary>
    /// UI ������Ʈ
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
        GoldManager.Instance.OnCurrencyChanged -= UpdateUI;
    }
}
