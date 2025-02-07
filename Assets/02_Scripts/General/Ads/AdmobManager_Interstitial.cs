using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

/// <summary>
/// ���鱤�� �ε�
/// ���� ���� ǥ��
/// ���鱤�� �̺�Ʈ ����
/// ���鱤�� ����
/// ���� ���� ���� �ε�
/// </summary>

/// <summary>
/// ���鱤��
/// </summary>
public class AdmobManager_Interstitial : MonoBehaviour
{

#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/1033173712"; // �׽�Ʈ ������
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
    private string _adUnitId = "unused";
#endif

    private InterstitialAd _interstitialAd; // ���� ����

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        // Google ����� ���� SDK�� �ʱ�ȭ�մϴ�.
        MobileAds.Initialize((InitializationStatus initStatus) => { }); // �ʱ�ȭ �Ϸ� �� �ݹ� ������
    }

    /// <summary>
    /// ���ÿ� ��ư
    /// </summary>
    public void TestButton_Interstitial()
    {
        LoadInterstitialAd();
        ShowInterstitialAd();
        RegisterReloadHandler(_interstitialAd);
    }

    /// <summary>
    /// ���� ���� �ε�
    /// </summary>
    public void LoadInterstitialAd()
    {
        // �̹� ������ ����
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("���� ���� �ε��ϴ� ���Դϴ�.");
        
        // ���� �ε� ��û
        var adRequest = new AdRequest();
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // ������ null�̰ų� ���� null �̸�, �ε� ��û�� ������ ����
                if (error != null || ad == null)
                {
                    Debug.LogError($"���� ���� �ε����� ���߽��ϴ�. ���� : {error}");
                    return;
                }

                Debug.Log($"����� ���� ���� : {ad.GetResponseInfo()}");

                _interstitialAd = ad;
            });
    }

    /// <summary>
    /// ���� ���� ǥ��
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("���� ���� ǥ��");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("���� ���� ���� �غ���� �ʾҽ��ϴ�.");
        }
    }

    /// <summary>
    /// ���� ���� ���� �ε� (�ִ��� ���� �ٸ� ���� ǥ���� �� �ֵ���)
    /// </summary>
    private void RegisterReloadHandler(InterstitialAd interstitialAd)
    {
        // ���� ��ü ȭ�� �������� ���� �� �߻�
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("���� ���� ��ü ȭ�� �������� �������ϴ�.");
            LoadInterstitialAd();
        };

        // ���� ��ü ȭ�� �������� ���� ������ �� �߻�
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError($"���� ���� ��ü ȭ�� �������� ���� ���߽��ϴ�. ���� : {error}");
            LoadInterstitialAd();
        };
    }




    /// <summary>
    /// [�����] ���� ������ ���� ���鱤�� �̺�Ʈ �ڵ鷯��(�����ʵ�)
    /// </summary>
    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // ����� ������ �߻��� ������ �����Ǵ� ��� �߻��մϴ�.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("���� ���� ���� {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // ���� ���� ������ ��ϵǸ� �߻��մϴ�.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("���� ���� ������ ��ϵǾ����ϴ�.");
        };
        // ���� Ŭ���� ��ϵǸ� �߻��մϴ�.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("���� ���� Ŭ���߽��ϴ�.");
        };
        // ���� ��ü ȭ�� �������� �� �� �߻��մϴ�.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("���� ���� ��ü ȭ�� �������� ���Ƚ��ϴ�.");
        };
        // ���� ��ü ȭ�� �������� ���� �� �߻��մϴ�.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("���� ���� ��ü ȭ�� �������� �������ϴ�.");
        };
        // ���� ��ü ȭ�� �������� ���� ������ �� �߻��մϴ�.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("���� ���� ��ü ȭ�� �������� ���� ���߽��ϴ�. " +
                           "������ : " + error);
        };
    }
}
