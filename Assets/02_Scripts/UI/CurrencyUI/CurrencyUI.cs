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
        PlayerManager.Instance.PlayerData.OnGoldChange += UpdateUI; // ��� ���� �̺�Ʈ ����
        UpdateUI();
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerManager.Instance.PlayerData.OnGoldChange -= UpdateUI; // ��� ���� �̺�Ʈ �������� 
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    public void UpdateUI()
    {
        _goldText.text = AlphabetNumConverter.Convert(PlayerManager.Instance.GetCurrentGold());
    }
}
