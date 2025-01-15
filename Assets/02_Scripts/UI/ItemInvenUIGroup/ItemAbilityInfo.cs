using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemAbilityInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _abilityNameText;     // 어빌리티 이름 텍스트
    [SerializeField] private TextMeshProUGUI _abilityValueText;    // 어빌리티 값 텍스트

    /// <summary>
    /// 보여주기
    /// </summary>
    public void Show(StatType statType, float value)
    {
        _abilityNameText.text = statType.ToString();
        _abilityValueText.text = value.ToString();

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
