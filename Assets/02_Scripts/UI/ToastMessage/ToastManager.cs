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
        PlayerStats.OnPlayerStatChanged += StartShow_ToastCombatPower;  // 플레이어 스탯 변경되었을 때, 전투력 토스트메시지 보여주기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= StartShow_ToastCombatPower;
    }

    /// <summary>
    /// 전투력 수치 토스트메시지 보여주기
    /// </summary>
    public void StartShow_ToastCombatPower(PlayerStatArgs args)
    {
        // 이미 코루틴 있으면 실행 중단
        if (_toastCombatPowerCoroutine != null) 
            StopCoroutine(_toastCombatPowerCoroutine);

        // 새로운 코루틴 시작
        _toastCombatPowerCoroutine = StartCoroutine(Show_ToastCombatPower(args));
    }

    /// <summary>
    /// 전투력 수치 토스트메시지 보여주는 코루틴
    /// </summary>
    private IEnumerator Show_ToastCombatPower(PlayerStatArgs args)
    {
        _toastCombatPower.Hide();

        _toastCombatPower.Init(args);
        yield return new WaitForSeconds(1f);

        _toastCombatPower.Hide();
    }
}
