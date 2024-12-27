using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectItemInfoUI_Stat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statNameText;     // 스탯 이름 텍스트
    [SerializeField] private TextMeshProUGUI _statValueText;    // 스탯 값 텍스트

    /// <summary>
    /// 보여주기
    /// </summary>
    public void Show(StatType statType, int value)
    {
        _statNameText.text = statType.ToString();
        _statValueText.text = value.ToString();

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
