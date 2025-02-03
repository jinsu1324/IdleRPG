using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOutViewManager : MonoBehaviour
{
    [SerializeField] private BlackOutView _blackOutView;    // 블랙아웃 뷰

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageChallange += FadeInOut; // 스테이지 도전시 -> 페이드인아웃
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageChallange -= FadeInOut;
    }


    /// <summary>
    /// 페이드 인 아웃
    /// </summary>
    private void FadeInOut()
    {
        StartCoroutine(FadeInOutCoroutine());
    }

    /// <summary>
    /// 페이드 인 아웃 코루틴
    /// </summary>
    private IEnumerator FadeInOutCoroutine()
    {
        _blackOutView.Show();
        yield return new WaitForSecondsRealtime(2.0f);
        _blackOutView.Hide();
    }

}
