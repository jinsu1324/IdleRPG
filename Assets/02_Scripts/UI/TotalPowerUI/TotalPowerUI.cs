using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalPowerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalPowerText;     // ���� ������ �ؽ�Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += Update_TotalPowerText;    // �÷��̾� ���� ����Ǿ��� ��, ���� ������ �ؽ�ƮUI ������Ʈ 
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= Update_TotalPowerText;
    }

    /// <summary>
    /// ���� ������ �ؽ�Ʈ ������Ʈ
    /// </summary>
    private void Update_TotalPowerText(PlayerStatArgs args)
    {
        _totalPowerText.text = NumberConverter.ConvertAlphabet(args.TotalPower);
    }
}
