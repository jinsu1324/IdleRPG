using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText; // ���� ��� �ؽ�Ʈ
    
    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        //PlayerManager.PlayerData.OnGoldChange += UpdateUI; // ��� ���� �̺�Ʈ ����

        GoldManager.Instance.OnCurrencyChanged += UpdateUI;

        UpdateUI(GoldManager.Instance.GetCurrencyCount());
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        //PlayerManager.PlayerData.OnGoldChange -= UpdateUI; // ��� ���� �̺�Ʈ �������� 
        GoldManager.Instance.OnCurrencyChanged -= UpdateUI;
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    public void UpdateUI(int amout)
    {
        _goldText.text = AlphabetNumConverter.Convert(amout);
    }
}
