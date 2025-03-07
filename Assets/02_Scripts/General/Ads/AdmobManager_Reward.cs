using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using System.Threading.Tasks;
using TMPro;

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
    public bool CanBuyItem;
}

/// <summary>
/// 리워드 광고
/// </summary>
public class AdmobManager_Reward : SingletonBase<AdmobManager_Reward>
{

#if UNITY_ANDROID
    //private string _adUnitId = "ca-app-pub-3940256099942544/5224354917"; // 테스트 광고임
    private string _adUnitId = "ca-app-pub-5785473322772543/5145070371"; // 진짜 앱 리워드 광고 ID
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
    private string _adUnitId = "unused";
#endif

    public static event Action OnAdLoadStart;       // 광고 로드 시작될때 이벤트
    public static event Action OnAdLoadComplete;    // 광고 로드 끝날때 이벤트

    public static event Action<OnRewardedArgs> OnRewarded; // 리워드 획득 이벤트
    private RewardedAd _rewardedAd;  // 리워드 광고

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        // Google 모바일 광고 SDK를 초기화합니다.
        MobileAds.Initialize((InitializationStatus initStatus) => { Debug.Log("광고 초기화 완료!"); }); // 초기화 완료 후 콜백 가능함
    }

    /// <summary>
    /// 리워드 광고 로드 및 보여주기
    /// </summary>
    public async void LoadAndShow_RewardedAd()
    {
        await LoadRewardedAd();
        ShowRewardedAd();
    }

    /// <summary>
    /// 보상형 광고 로드
    /// </summary>
    public Task LoadRewardedAd()
    {
        TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

        // 기존 광고 정리
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log($"광고를 요청중입니다.");
        OnAdLoadStart?.Invoke();

        // 광고 로드 요청
        var adRequest = new AdRequest();
        RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null) // 오류가 null이거나 광고가 null 이면, 로드 요청이 실패한 것임
            {
                string errorMessage = error.GetMessage();

                if (error.GetCode() == 3) // 특정 에러에 대한 추가 로그 출력
                    Debug.LogError("광고 없음 (No Fill). AdMob 인벤토리가 부족할 수 있음.");
                else if (errorMessage.Contains("No fill from ad server"))
                    Debug.LogError("No fill from ad server - 광고 요청은 성공했으나 광고 없음.");

                taskCompletionSource.SetResult(false);
                OnAdLoadComplete?.Invoke();
                return;
            }

            Debug.Log($"응답된 보상형 광고 : {ad.GetResponseInfo()}");
            _rewardedAd = ad;

            taskCompletionSource.SetResult(true); // 로드 성공 시 true 반환
            OnAdLoadComplete?.Invoke();
        });

        return taskCompletionSource.Task; // Task 반환 (await로 기다릴 수 있음)
    }

    /// <summary>
    /// 보상형 광고 표시 및 보상제공
    /// </summary>
    public void ShowRewardedAd()
    {
        if (_rewardedAd == null)
        {
            Debug.LogError("광고 객체가 null 상태입니다. 광고를 다시 로드하세요.");
            return;
        }

        if (!_rewardedAd.CanShowAd())
        {
            Debug.LogError("광고가 로드되었지만 CanShowAd()가 false입니다.");
            return;
        }

        _rewardedAd.Show((Reward reward) =>
        {
            string rewardMsg = "보상 제공! 유형: {0}, 갯수: {1}.";
            Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));

            StartCoroutine(UpdateUIAfterReward());
        });
    }

    /// <summary>
    /// 유니티 UI 업데이트는 메인스레드에서만 가능하기때문에 코루틴 (비동기라서 광고 콜백은 백그라운드 스레드에서 실행될 확률이 높기때문)
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateUIAfterReward()
    {
        yield return null; // 다음 프레임에서 실행되도록 대기

        // TODO: 사용자에게 보상 제공
        int rewardAmount = 100;
        GemManager.AddGem(rewardAmount);
        SoundManager.Instance.PlaySFX(SFXType.SFX_AddCurrency);

        OnRewarded?.Invoke(new OnRewardedArgs { Count = rewardAmount, CanBuyItem = GemManager.HasEnoughGem(ItemDropMachine.DropCost) });
    }


    //// [참고용] 공식문서에 나온 보상형 광고 이벤트 리스너
    //private void RegisterEventHandlers(RewardedAd ad)
    //{
    //    // 광고로 수익이 발생한 것으로 추정되는 경우 발생
    //    ad.OnAdPaid += (AdValue adValue) =>
    //    {
    //        Debug.Log(String.Format("보상형 광고 유료 {0} {1}.",
    //            adValue.Value,
    //            adValue.CurrencyCode));
    //    };

    //    // 광고에 대한 노출이 기록되면 발생합니다.
    //    ad.OnAdImpressionRecorded += () =>
    //    {
    //        Debug.Log("보상형 광고는 노출을 기록했습니다.");
    //    };

    //    // 광고 클릭이 기록되면 발생합니다.
    //    ad.OnAdClicked += () =>
    //    {
    //        Debug.Log("보상형 광고가 클릭되었습니다");
    //    };

    //    // 광고가 전체 화면 콘텐츠를 열 때 발생합니다.
    //    ad.OnAdFullScreenContentOpened += () =>
    //    {
    //        Debug.Log("보상형 광고 전체 화면 콘텐츠가 열렸습니다..");
    //    };

    //    // 광고가 전체 화면 콘텐츠를 닫을 때 발생합니다.
    //    ad.OnAdFullScreenContentClosed += () =>
    //    {
    //        Debug.Log("보상형 광고 전체 화면 콘텐츠가 닫혔습니다.");
    //    };

    //    // 광고가 전체 화면 콘텐츠를 열지 못한 경우 발생합니다.
    //    ad.OnAdFullScreenContentFailed += (AdError error) =>
    //    {
    //        Debug.LogError("보상형 광고가 전체 화면 콘텐츠를 열지 못했습니다. " +
    //                       "에러는 : " + error);
    //    };
    //}

    //// 다음 보상형 광고를 미리 로드
    //private void RegisterReloadHandler(RewardedAd ad)
    //{
    //    // 광고가 전체 화면 콘텐츠를 닫을 때 발생합니다.
    //    ad.OnAdFullScreenContentClosed += () =>
    //    {
    //        Debug.Log("보상형 광고 전체 화면 콘텐츠가 닫혔습니다.");

    //        // 최대한 빨리 다른 광고를 표시할 수 있도록 광고를 다시 로드하세요.
    //        LoadRewardedAd();
    //    };

    //    // 광고가 전체 화면 콘텐츠를 열지 못했을 때 발생합니다.
    //    ad.OnAdFullScreenContentFailed += (AdError error) =>
    //    {
    //        Debug.LogError("보상형 광고가 전체 화면 콘텐츠를 열지 못했습니다 " +
    //                       "에러는 : " + error);

    //        // 최대한 빨리 다른 광고를 표시할 수 있도록 광고를 다시 로드하세요.
    //        LoadRewardedAd();
    //    };
    //}
}
