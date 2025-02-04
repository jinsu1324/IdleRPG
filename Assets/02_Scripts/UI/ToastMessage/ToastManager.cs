using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastManager : SingletonBase<ToastManager>
{
    [SerializeField] private ToastTotalPower _toastTotalPower;      // 전투력 수치 토스트 메시지
    private Coroutine _toastTotalPowerCoroutine;                    // 전투력 수치 토스트 메시지 코루틴 담을 변수

    [SerializeField] private ToastDefeat _toastDefeat;              // 패배 토스트메시지
    private Coroutine _toastDefeatCoroutine;                        // 패배 토스트메시지 코루틴

    [SerializeField] private ToastCommon _toastCommon;              // 커먼 토스트메시지
    private Coroutine _toastCommonCoroutine;                        // 커먼 토스트메시지 코루틴

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += StartShow_ToastTotalPower;  // 플레이어 스탯 변경되었을 때, 전투력 토스트메시지 보여주기
        StageManager.OnStageDefeat += StartShow_ToastDefeat;    // 스테이지 패배시 패배토스트 보여주기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= StartShow_ToastTotalPower;
        StageManager.OnStageDefeat -= StartShow_ToastDefeat;
    }




    /// <summary>
    /// 전투력 수치 토스트메시지 보여주기
    /// </summary>
    public void StartShow_ToastTotalPower(PlayerStatArgs args)
    {
        // 이미 코루틴 있으면 실행 중단
        if (_toastTotalPowerCoroutine != null) 
            StopCoroutine(_toastTotalPowerCoroutine);

        // 새로운 코루틴 시작
        _toastTotalPowerCoroutine = StartCoroutine(Show_ToastTotalPower(args));
    }

    /// <summary>
    /// 전투력 수치 토스트메시지 보여주는 코루틴
    /// </summary>
    private IEnumerator Show_ToastTotalPower(PlayerStatArgs args)
    {
        _toastTotalPower.Hide();

        _toastTotalPower.Init(args);
        yield return new WaitForSecondsRealtime(1f);

        _toastTotalPower.Hide();
    }



    /// <summary>
    /// 패배 토스트메시지 보여주기
    /// </summary>
    public void StartShow_ToastDefeat()
    {
        // 이미 코루틴 있으면 실행 중단
        if (_toastDefeatCoroutine != null)
            StopCoroutine(_toastDefeatCoroutine);

        // 새로운 코루틴 시작
        _toastDefeatCoroutine = StartCoroutine(Show_ToastDefeat());
    }

    /// <summary>
    /// 패배 토스트메시지 코루틴
    /// </summary>
    private IEnumerator Show_ToastDefeat()
    {
        _toastDefeat.Hide();

        _toastDefeat.Show();
        yield return new WaitForSecondsRealtime(3f);
     
        _toastDefeat.Hide();
    }






    /// <summary>
    /// 커먼 토스트메시지 보여주기
    /// </summary>
    public void StartShow_ToastCommon(string text)
    {
        // 이미 코루틴 있으면 실행 중단
        if (_toastCommonCoroutine != null)
            StopCoroutine(_toastCommonCoroutine);

        // 새로운 코루틴 시작
        _toastCommonCoroutine = StartCoroutine(Show_ToastCommon(text));
    }

    /// <summary>
    /// 커먼 토스트메시지 코루틴
    /// </summary>
    private IEnumerator Show_ToastCommon(string text)
    {
        _toastCommon.Hide();

        _toastCommon.Show(text);
        yield return new WaitForSecondsRealtime(1f);

        _toastCommon.Hide();
    }
}
