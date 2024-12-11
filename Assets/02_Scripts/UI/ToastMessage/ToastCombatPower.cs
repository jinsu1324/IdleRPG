using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToastCombatPower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;            // 현재 전투력 수치 표시할 텍스트
    [SerializeField] private TextMeshProUGUI _changedValueText;     // 변경된 전투력 수치 텍스트
    [SerializeField] private GameObject _arrowIncrease;             // 위 방향 화살표
    [SerializeField] private GameObject _arrowDecrease;             // 아래 방향 화살표

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        UpdateUI();
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UpdateUI()
    {
        // 수치 표시
        _valueText.text = AlphabetNumConverter.Convert(UpgradeManager.TotalPower);

        // 화살표 방향 표시
        int TotalCombatPower = UpgradeManager.TotalPower;
        int beforeTotalCombatPower = UpgradeManager.BeforeTotalPower;
        if (TotalCombatPower > beforeTotalCombatPower)
            ArrowUp();
        else
            ArrowDown();
        
        // 전투력 변경된 수치 표시
        UpdateChangedValueText(beforeTotalCombatPower, TotalCombatPower);
    }

    /// <summary>
    /// 전투력 바뀐 값 표시
    /// </summary>
    private void UpdateChangedValueText(int before, int current)
    {
        int changedValue = Mathf.Abs(current - before);
        _changedValueText.text = AlphabetNumConverter.Convert(changedValue);
    }

    /// <summary>
    /// 위방향 화살표 켜기
    /// </summary>
    private void ArrowUp()
    {
        _arrowIncrease.gameObject.SetActive(true);
        _arrowDecrease.gameObject.SetActive(false);
    }

    /// <summary>
    /// 아래방향 화살표 켜기
    /// </summary>
    private void ArrowDown()
    {
        _arrowIncrease.gameObject.SetActive(false);
        _arrowDecrease.gameObject.SetActive(true);
    }

}
