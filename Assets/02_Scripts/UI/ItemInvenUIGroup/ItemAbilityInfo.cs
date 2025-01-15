using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemAbilityInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _abilityNameText;     // �����Ƽ �̸� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _abilityValueText;    // �����Ƽ �� �ؽ�Ʈ

    /// <summary>
    /// �����ֱ�
    /// </summary>
    public void Show(StatType statType, float value)
    {
        _abilityNameText.text = statType.ToString();
        _abilityValueText.text = value.ToString();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �����
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
