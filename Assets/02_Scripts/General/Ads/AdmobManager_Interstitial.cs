using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

/// <summary>
/// 전면광고 로드
/// 전면 광고 표시
/// 전면광고 이벤트 접수
/// 전면광고 정리
/// 다음 전면 광고 로드
/// </summary>

/// <summary>
/// 전면광고
/// </summary>
public class AdmobManager_Interstitial : MonoBehaviour
{

#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/1033173712"; // 테스트 광고임
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
    private string _adUnitId = "unused";
#endif

    private InterstitialAd _interstitialAd; // 전면 광고

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        // Google 모바일 광고 SDK를 초기화합니다.
        MobileAds.Initialize((InitializationStatus initStatus) => { }); // 초기화 완료 후 콜백 가능함
    }

    /// <summary>
    /// 샘플용 버튼
    /// </summary>
    public void TestButton_Interstitial()
    {
        LoadInterstitialAd();
        ShowInterstitialAd();
        RegisterReloadHandler(_interstitialAd);
    }

    /// <summary>
    /// 전면 광고 로드
    /// </summary>
    public void LoadInterstitialAd()
    {
        // 이미 있으면 정리
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("전면 광고를 로드하는 중입니다.");
        
        // 광고 로드 요청
        var adRequest = new AdRequest();
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // 오류가 null이거나 광고가 null 이면, 로드 요청이 실패한 것임
                if (error != null || ad == null)
                {
                    Debug.LogError($"전면 광고를 로드하지 못했습니다. 에러 : {error}");
                    return;
                }

                Debug.Log($"응답된 전면 광고 : {ad.GetResponseInfo()}");

                _interstitialAd = ad;
            });
    }

    /// <summary>
    /// 전면 광고 표시
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("전면 광고 표시");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("전면 광고가 아직 준비되지 않았습니다.");
        }
    }

    /// <summary>
    /// 다음 전면 광고 로드 (최대한 빨리 다른 광고를 표시할 수 있도록)
    /// </summary>
    private void RegisterReloadHandler(InterstitialAd interstitialAd)
    {
        // 광고가 전체 화면 콘텐츠를 닫을 때 발생
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("전면 광고 전체 화면 콘텐츠가 닫혔습니다.");
            LoadInterstitialAd();
        };

        // 광고가 전체 화면 콘텐츠를 열지 못했을 때 발생
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError($"전면 광고가 전체 화면 콘텐츠를 열지 못했습니다. 에러 : {error}");
            LoadInterstitialAd();
        };
    }




    /// <summary>
    /// [참고용] 공식 문서에 나온 전면광고 이벤트 핸들러들(리스너들)
    /// </summary>
    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // 광고로 수익이 발생한 것으로 추정되는 경우 발생합니다.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("전면 광고 유료 {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // 광고에 대한 노출이 기록되면 발생합니다.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("전면 광고에 노출이 기록되었습니다.");
        };
        // 광고 클릭이 기록되면 발생합니다.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("전면 광고를 클릭했습니다.");
        };
        // 광고가 전체 화면 콘텐츠를 열 때 발생합니다.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("전면 광고 전체 화면 콘텐츠가 열렸습니다.");
        };
        // 광고가 전체 화면 콘텐츠를 닫을 때 발생합니다.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("전면 광고 전체 화면 콘텐츠가 닫혔습니다.");
        };
        // 광고가 전체 화면 콘텐츠를 열지 못했을 때 발생합니다.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("전면 광고가 전체 화면 콘텐츠를 열지 못했습니다. " +
                           "에러는 : " + error);
        };
    }
}
