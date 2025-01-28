using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

/// <summary>
/// 장비속성 UI
/// </summary>
public class GearStatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;     // 속성 이름 텍스트
    [SerializeField] private TextMeshProUGUI _valueText;    // 속성 값 텍스트

    /// <summary>
    /// 보여주기
    /// </summary>
    public void Show(StatType statType, float value)
    {
        _nameText.text = StatTypeTranslator.Translate(statType); // 한글로 변환해서 표시

        switch (statType)
        {
            case StatType.CriticalRate:
            case StatType.CriticalMultiple:
                _valueText.text = NumberConverter.ConvertPercentage(value); // 크리티컬 관련은 퍼센티지로 표현
                break;
            case StatType.AttackSpeed:
                _valueText.text = NumberConverter.ConvertFixedDecimals(value); // 공격속도는 소수점 정해서 표현
                break;
            default:
                _valueText.text = NumberConverter.ConvertAlphabet(value);   // 나머지는 알파벳표현식으로
                break;
        }

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 숨기기
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
