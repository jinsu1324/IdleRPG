using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectItemInfoUI_Stat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statNameText;     // ���� �̸� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _statValueText;    // ���� �� �ؽ�Ʈ

    /// <summary>
    /// �����ֱ�
    /// </summary>
    public void Show(StatType statType, int value)
    {
        _statNameText.text = statType.ToString();
        _statValueText.text = value.ToString();

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
