using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

/// <summary>
/// ���Ӽ� UI
/// </summary>
public class GearStatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;     // �Ӽ� �̸� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _valueText;    // �Ӽ� �� �ؽ�Ʈ

    /// <summary>
    /// �����ֱ�
    /// </summary>
    public void Show(StatType statType, float value)
    {
        _nameText.text = StatTypeTranslator.Translate(statType); // �ѱ۷� ��ȯ�ؼ� ǥ��

        switch (statType)
        {
            case StatType.CriticalRate:
            case StatType.CriticalMultiple:
                _valueText.text = NumberConverter.ConvertPercentage(value); // ũ��Ƽ�� ������ �ۼ�Ƽ���� ǥ��
                break;
            case StatType.AttackSpeed:
                _valueText.text = NumberConverter.ConvertFixedDecimals(value); // ���ݼӵ��� �Ҽ��� ���ؼ� ǥ��
                break;
            default:
                _valueText.text = NumberConverter.ConvertAlphabet(value);   // �������� ���ĺ�ǥ��������
                break;
        }

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
