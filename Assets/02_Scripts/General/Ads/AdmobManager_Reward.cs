using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

/// <summary>
/// ������ ���� �ε�
/// [���û���] ������ Ȯ��(SSV) �ݹ� ��ȿ�� �˻�
/// ���� �ݹ����� ������ ���� ǥ��
/// ������ ���� �̺�Ʈ ���
/// ������ ���� ����
/// ���� ������ ���� �̸� �ε��ϼ���
/// </summary>

public struct OnRewardedArgs
{
    public int Count;
}

/// <summary>
/// ������ ����
/// </summary>
public class AdmobManager_Reward : MonoBehaviour
{

#if UNITY_ANDROID
    private static string _adUnitId = "ca-app-pub-3940256099942544/5224354917"; // �׽�Ʈ ������
#elif UNITY_IPHONE
    private static string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
    private static string _adUnitId = "unused";
#endif

    public static event Action<OnRewardedArgs> OnRewarded; // ������ ȹ�� �̺�Ʈ

    private static RewardedAd _rewardedAd;  // ������ ����

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        // Google ����� ���� SDK�� �ʱ�ȭ�մϴ�.
        MobileAds.Initialize((InitializationStatus initStatus) => { }); // �ʱ�ȭ �Ϸ� �� �ݹ� ������

        // ������ ���� ����
        // _rewardedAd.Destroy();
    }

    /// <summary>
    /// ������ ���� �ε� �� �����ֱ�
    /// </summary>
    public static void LoadAndShow_RewardedAd()
    {
        LoadRewardedAd();
        ShowRewardedAd();
    }

    /// <summary>
    /// ������ ���� �ε�
    /// </summary>
    public static void LoadRewardedAd()
    {
        // ���� ���� ����
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("������ ���� �ε� ���Դϴ�.");

        // ���� �ε� ��û
        var adRequest = new AdRequest();
        RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            // ������ null�̰ų� ���� null �̸�, �ε� ��û�� ������ ����
            if (error != null || ad == null)
            {
                Debug.LogError($"������ ���� �ε����� ���߽��ϴ�. ���� : {error}");
                return;
            }

            Debug.Log($"����� ������ ���� : {ad.GetResponseInfo()}"); ;

            _rewardedAd = ad;
        });
    }


    /// <summary>
    /// ������ ���� ǥ�� �� ��������
    /// </summary>
    public static void ShowRewardedAd()
    {
        const string rewardMsg = "���� ����! ����: {0}, ����: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: ����ڿ��� ���� ����
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));

                GemManager.AddGem(100);
                SoundManager.Instance.PlaySFX(SFXType.SFX_AddCurrency);

                OnRewardedArgs args = new OnRewardedArgs() { Count = 100 };
                OnRewarded?.Invoke(args);
            });
        }
    }




    // [�����] ���Ĺ����� ���� ������ ���� �̺�Ʈ ������
    private void RegisterEventHandlers(RewardedAd ad)
    {
        // ����� ������ �߻��� ������ �����Ǵ� ��� �߻�
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("������ ���� ���� {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };

        // ���� ���� ������ ��ϵǸ� �߻��մϴ�.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("������ ����� ������ ����߽��ϴ�.");
        };

        // ���� Ŭ���� ��ϵǸ� �߻��մϴ�.
        ad.OnAdClicked += () =>
        {
            Debug.Log("������ ���� Ŭ���Ǿ����ϴ�");
        };

        // ���� ��ü ȭ�� �������� �� �� �߻��մϴ�.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("������ ���� ��ü ȭ�� �������� ���Ƚ��ϴ�..");
        };

        // ���� ��ü ȭ�� �������� ���� �� �߻��մϴ�.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("������ ���� ��ü ȭ�� �������� �������ϴ�.");
        };

        // ���� ��ü ȭ�� �������� ���� ���� ��� �߻��մϴ�.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("������ ���� ��ü ȭ�� �������� ���� ���߽��ϴ�. " +
                           "������ : " + error);
        };
    }

    // ���� ������ ���� �̸� �ε�
    private void RegisterReloadHandler(RewardedAd ad)
    {
        // ���� ��ü ȭ�� �������� ���� �� �߻��մϴ�.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("������ ���� ��ü ȭ�� �������� �������ϴ�.");

            // �ִ��� ���� �ٸ� ���� ǥ���� �� �ֵ��� ���� �ٽ� �ε��ϼ���.
            LoadRewardedAd();
        };

        // ���� ��ü ȭ�� �������� ���� ������ �� �߻��մϴ�.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("������ ���� ��ü ȭ�� �������� ���� ���߽��ϴ� " +
                           "������ : " + error);

            // �ִ��� ���� �ٸ� ���� ǥ���� �� �ֵ��� ���� �ٽ� �ε��ϼ���.
            LoadRewardedAd();
        };
    }
}
