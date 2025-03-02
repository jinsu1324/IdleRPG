using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToastTotalPower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;            // 현재 전투력 수치 표시할 텍스트
    [SerializeField] private TextMeshProUGUI _changedValueText;     // 변경된 전투력 수치 텍스트
    [SerializeField] private GameObject _arrowIncrease;             // 위 방향 화살표
    [SerializeField] private GameObject _arrowDecrease;             // 아래 방향 화살표

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(PlayerStatArgs args)
    {
        float totalPower = args.TotalPower;
        float beforeTotalPower = args.BeforeTotalPower;

        // 수치 표시
        _valueText.text = NumberConverter.ConvertAlphabet(totalPower); // 알파벳으로 표시
        //_valueText.text = $"{totalPower}"; 

        // 화살표 방향 표시
        if (totalPower > beforeTotalPower)
            ArrowUp();
        else
            ArrowDown();

        // 전투력 변경된 수치 표시
        UpdateChangedValueText(beforeTotalPower, totalPower);

        Show();
    }

    /// <summary>
    /// 전투력 바뀐 값 표시
    /// </summary>
    private void UpdateChangedValueText(float before, float current)
    {
        float changedValue = Mathf.Abs(current - before);
        _changedValueText.text = NumberConverter.ConvertAlphabet(changedValue); // 알파벳으로 표시
        //_changedValueText.text = $"{changedValue}";
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

    /// <summary>
    /// 켜기
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 끄기
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
