using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatPowerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _combatPowerText;     // ���� ������ �ؽ�Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += Update_CombatPowerText;    // �÷��̾� ���� ����Ǿ��� ��, ���� ������ �ؽ�ƮUI ������Ʈ 
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= Update_CombatPowerText;
    }

    private void Start()
    {
        Update_CombatPowerText(PlayerStats.GetCurrentPlayerStatArgs());
    }

    /// <summary>
    /// ���� ������ �ؽ�Ʈ ������Ʈ
    /// </summary>
    private void Update_CombatPowerText(PlayerStatArgs args)
    {
        _combatPowerText.text = NumberConverter.ConvertAlphabet(args.TotalPower);
    }
}
