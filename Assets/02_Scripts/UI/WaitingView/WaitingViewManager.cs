using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingViewManager : MonoBehaviour
{
    [SerializeField] private WaitingView _waitingView;  // 웨이팅 뷰

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        AdmobManager_Reward.OnAdLoadStart += _waitingView.Show; // 광고 로드 시작될때, 웨이팅뷰 켜기
        AdmobManager_Reward.OnAdLoadComplete += _waitingView.Hide; // 광고 로드 끝나면, 웨이팅뷰 끄기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        AdmobManager_Reward.OnAdLoadStart -= _waitingView.Show;
        AdmobManager_Reward.OnAdLoadComplete -= _waitingView.Hide;

    }
}
