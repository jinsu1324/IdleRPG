using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToastCombatPower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;            // ���� ������ ��ġ ǥ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _changedValueText;     // ����� ������ ��ġ �ؽ�Ʈ
    [SerializeField] private GameObject _arrowIncrease;             // �� ���� ȭ��ǥ
    [SerializeField] private GameObject _arrowDecrease;             // �Ʒ� ���� ȭ��ǥ

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        UpdateUI();
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UpdateUI()
    {
        // ��ġ ǥ��
        _valueText.text = AlphabetNumConverter.Convert(PlayerStatContainer.Instance.GetTotalPower());

        // ȭ��ǥ ���� ǥ��
        int TotalCombatPower = PlayerStatContainer.Instance.GetTotalPower();
        int beforeTotalCombatPower = PlayerStatContainer.Instance.GetBeforeTotalPower();
        if (TotalCombatPower > beforeTotalCombatPower)
            ArrowUp();
        else
            ArrowDown();
        
        // ������ ����� ��ġ ǥ��
        UpdateChangedValueText(beforeTotalCombatPower, TotalCombatPower);
    }

    /// <summary>
    /// ������ �ٲ� �� ǥ��
    /// </summary>
    private void UpdateChangedValueText(int before, int current)
    {
        int changedValue = Mathf.Abs(current - before);
        _changedValueText.text = AlphabetNumConverter.Convert(changedValue);
    }

    /// <summary>
    /// ������ ȭ��ǥ �ѱ�
    /// </summary>
    private void ArrowUp()
    {
        _arrowIncrease.gameObject.SetActive(true);
        _arrowDecrease.gameObject.SetActive(false);
    }

    /// <summary>
    /// �Ʒ����� ȭ��ǥ �ѱ�
    /// </summary>
    private void ArrowDown()
    {
        _arrowIncrease.gameObject.SetActive(false);
        _arrowDecrease.gameObject.SetActive(true);
    }

}
