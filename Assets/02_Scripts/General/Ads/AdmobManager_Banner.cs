using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

/// <summary>
/// 배너 광고
/// </summary>
public class AdmobManager_Banner : MonoBehaviour
{

#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/6300978111"; // 테스트 광고임
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
    private string _adUnitId = "unused";
#endif

    private BannerView _bannerView; // 배너 뷰

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        // Google 모바일 광고 SDK를 초기화합니다.
        MobileAds.Initialize((InitializationStatus initStatus) => { }); // 초기화 완료 후 콜백 가능함
    }

    /// <summary>
    /// 샘플용 버튼임
    /// </summary>
    public void TestButton_Banner()
    {
        CreateBannerView();
        LoadBannerAd();
    }

    /// <summary>
    /// 화면에 배너 만들기 
    /// </summary>
    public void CreateBannerView()
    {
        Debug.Log("배너를 만듭니다.");

        // 이미 배너가 있는 경우 기존 배너를 파괴
        if (_bannerView != null)
            DestroyBannerView();
        
        // 현재 인터페이스 방향에 대한 배너크기 가져오기
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        // 화면 하단에 적응형 배너 만들기
        _bannerView = new BannerView(_adUnitId, adaptiveSize, AdPosition.Bottom);
    }

    /// <summary>
    /// 배너에 들어갈 광고를 로드
    /// </summary>
    public void LoadBannerAd()
    {
        // 배너 없으면 만들기
        if (_bannerView == null)
            CreateBannerView();
        
        // 광고 로드 요청을 보냅니다.
        var adRequest = new AdRequest();
        Debug.Log("배너 광고를 로드하는 중입니다.");
        _bannerView.LoadAd(adRequest);
    }

    /// <summary>
    /// 배너 제거
    /// </summary>
    public void DestroyBannerView()
    {
        if (_bannerView != null)
        {
            Debug.Log("배너 파괴.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }




    // [참고용] 공식 문서에 나와있는 배너 보기에서 발생할 수 있는 이벤트 리스너들 
    private void ListenToAdEvents()
    {
        // 광고가 배너 보기에 로드될 때 발생합니다.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("배너 보기에 응답이 포함된 광고가 로드됨 : "
                + _bannerView.GetResponseInfo());
        };
        // 광고가 배너 보기에 로드되지 않을 때 발생합니다..
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("오류로 인해 배너 보기에서 광고를 로드하지 못했습니다. : "
                + error);
        };
        // 광고로 수익이 발생한 것으로 추정되는 경우 발생.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("유료 배너 보기 {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // 광고에 대한 노출이 기록되면 발생합니다.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("배너 조회수가 노출수를 기록했습니다.");
        };
        // 광고 클릭이 기록되면 발생합니다.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("배너 보기를 클릭했습니다.");
        };
        // 광고가 전체 화면 콘텐츠를 열 때 발생합니다.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("배너 보기 전체 화면 콘텐츠가 열렸습니다.");
        };
        // 광고가 전체 화면 콘텐츠를 닫을 때 발생합니다.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("배너 보기 전체 화면 콘텐츠가 닫혔습니다.");
        };
    }
}
