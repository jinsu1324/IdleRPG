using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastManager : MonoBehaviour
{
    #region Singleton
    public static ToastManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private ToastCombatPower _toastCombatPower;    // 전투력 수치 토스트 메시지

    private Coroutine _toastCombatPowerCoroutine;                   // 전투려 수치 토스트 메시지 코루틴 담을 변수

    /// <summary>
    /// 전투력 수치 토스트메시지 보여주기
    /// </summary>
    public void ShowToastCombatPower()
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

        _toastCombatPower.Initialize();
        _toastCombatPower.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _toastCombatPower.gameObject.SetActive(false);
    }
}
