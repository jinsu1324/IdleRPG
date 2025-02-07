using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

/// <summary>
/// 보상형 광고 로드
/// [선택사항] 서버측 확인(SSV) 콜백 유효성 검사
/// 보상 콜백으로 보상형 광고 표시
/// 보상형 광고 이벤트 듣기
/// 보상형 광고 정리
/// 다음 보상형 광고를 미리 로드하세요
/// </summary>

public struct OnRewardedArgs
{
    public int Count;
}

/// <summary>
/// 리워드 광고
/// </summary>
public class AdmobManager_Reward : MonoBehaviour
{

#if UNITY_ANDROID
    private static string _adUnitId = "ca-app-pub-3940256099942544/5224354917"; // 테스트 광고임
#elif UNITY_IPHONE
    private static string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
    private static string _adUnitId = "unused";
#endif

    public static event Action<OnRewardedArgs> OnRewarded; // 리워드 획득 이벤트

    private static RewardedAd _rewardedAd;  // 리워드 광고

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        // Google 모바일 광고 SDK를 초기화합니다.
        MobileAds.Initialize((InitializationStatus initStatus) => { }); // 초기화 완료 후 콜백 가능함

        // 보상형 광고 정리
        // _rewardedAd.Destroy();
    }

    /// <summary>
    /// 리워드 광고 로드 및 보여주기
    /// </summary>
    public static void LoadAndShow_RewardedAd()
    {
        LoadRewardedAd();
        ShowRewardedAd();
    }

    /// <summary>
    /// 보상형 광고 로드
    /// </summary>
    public static void LoadRewardedAd()
    {
        // 기존 광고 정리
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("보상형 광고를 로드 중입니다.");

        // 광고 로드 요청
        var adRequest = new AdRequest();
        RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            // 오류가 null이거나 광고가 null 이면, 로드 요청이 실패한 것임
            if (error != null || ad == null)
            {
                Debug.LogError($"보상형 광고를 로드하지 못했습니다. 에러 : {error}");
                return;
            }

            Debug.Log($"응답된 보상형 광고 : {ad.GetResponseInfo()}"); ;

            _rewardedAd = ad;
        });
    }


    /// <summary>
    /// 보상형 광고 표시 및 보상제공
    /// </summary>
    public static void ShowRewardedAd()
    {
        const string rewardMsg = "보상 제공! 유형: {0}, 갯수: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: 사용자에게 보상 제공
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));

                GemManager.AddGem(100);
                SoundManager.Instance.PlaySFX(SFXType.SFX_AddCurrency);

                OnRewardedArgs args = new OnRewardedArgs() { Count = 100 };
                OnRewarded?.Invoke(args);
            });
        }
    }




    // [참고용] 공식문서에 나온 보상형 광고 이벤트 리스너
    private void RegisterEventHandlers(RewardedAd ad)
    {
        // 광고로 수익이 발생한 것으로 추정되는 경우 발생
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("보상형 광고 유료 {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };

        // 광고에 대한 노출이 기록되면 발생합니다.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("보상형 광고는 노출을 기록했습니다.");
        };

        // 광고 클릭이 기록되면 발생합니다.
        ad.OnAdClicked += () =>
        {
            Debug.Log("보상형 광고가 클릭되었습니다");
        };

        // 광고가 전체 화면 콘텐츠를 열 때 발생합니다.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("보상형 광고 전체 화면 콘텐츠가 열렸습니다..");
        };

        // 광고가 전체 화면 콘텐츠를 닫을 때 발생합니다.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("보상형 광고 전체 화면 콘텐츠가 닫혔습니다.");
        };

        // 광고가 전체 화면 콘텐츠를 열지 못한 경우 발생합니다.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("보상형 광고가 전체 화면 콘텐츠를 열지 못했습니다. " +
                           "에러는 : " + error);
        };
    }

    // 다음 보상형 광고를 미리 로드
    private void RegisterReloadHandler(RewardedAd ad)
    {
        // 광고가 전체 화면 콘텐츠를 닫을 때 발생합니다.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("보상형 광고 전체 화면 콘텐츠가 닫혔습니다.");

            // 최대한 빨리 다른 광고를 표시할 수 있도록 광고를 다시 로드하세요.
            LoadRewardedAd();
        };

        // 광고가 전체 화면 콘텐츠를 열지 못했을 때 발생합니다.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("보상형 광고가 전체 화면 콘텐츠를 열지 못했습니다 " +
                           "에러는 : " + error);

            // 최대한 빨리 다른 광고를 표시할 수 있도록 광고를 다시 로드하세요.
            LoadRewardedAd();
        };
    }
}
