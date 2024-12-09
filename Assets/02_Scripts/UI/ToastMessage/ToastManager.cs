using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastManager : MonoBehaviour
{
    [SerializeField] private ToastCombatPower _toastCombatPower;    // 전투력 수치 토스트 메시지
    private Coroutine _toastCombatPowerCoroutine;                   // 전투려 수치 토스트 메시지 코루틴 담을 변수

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStatManager.OnStatChanged += ShowToastCombatPower; // 스탯이 변경될 때, 총합공격력 토스트메시지 보여주기
    }

    /// <summary>
    /// 전투력 수치 토스트메시지 보여주기
    /// </summary>
    public void ShowToastCombatPower(OnStatChangedArgs args)
    {
        // 이미 코루틴 있으면 실행 중단
        if (_toastCombatPowerCoroutine != null) 
            StopCoroutine(_toastCombatPowerCoroutine);

        // 새로운 코루틴 시작
        _toastCombatPowerCoroutine = StartCoroutine(ShowToastCombatPowerCoroutine());
    }

    /// <summary>
    /// 전투력 수치 토스트메시지 보여주는 코루틴
    /// </summary>
    private IEnumerator ShowToastCombatPowerCoroutine()
    {
        _toastCombatPower.gameObject.SetActive(false);

        _toastCombatPower.Init();
        _toastCombatPower.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _toastCombatPower.gameObject.SetActive(false);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStatManager.OnStatChanged -= ShowToastCombatPower;
    }
}
