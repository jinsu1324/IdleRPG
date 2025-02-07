using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

/// <summary>
/// ��� ����
/// </summary>
public class AdmobManager_Banner : MonoBehaviour
{

#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/6300978111"; // �׽�Ʈ ������
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
    private string _adUnitId = "unused";
#endif

    private BannerView _bannerView; // ��� ��

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        // Google ����� ���� SDK�� �ʱ�ȭ�մϴ�.
        MobileAds.Initialize((InitializationStatus initStatus) => { }); // �ʱ�ȭ �Ϸ� �� �ݹ� ������
    }

    /// <summary>
    /// ���ÿ� ��ư��
    /// </summary>
    public void TestButton_Banner()
    {
        CreateBannerView();
        LoadBannerAd();
    }

    /// <summary>
    /// ȭ�鿡 ��� ����� 
    /// </summary>
    public void CreateBannerView()
    {
        Debug.Log("��ʸ� ����ϴ�.");

        // �̹� ��ʰ� �ִ� ��� ���� ��ʸ� �ı�
        if (_bannerView != null)
            DestroyBannerView();
        
        // ���� �������̽� ���⿡ ���� ���ũ�� ��������
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        // ȭ�� �ϴܿ� ������ ��� �����
        _bannerView = new BannerView(_adUnitId, adaptiveSize, AdPosition.Bottom);
    }

    /// <summary>
    /// ��ʿ� �� ���� �ε�
    /// </summary>
    public void LoadBannerAd()
    {
        // ��� ������ �����
        if (_bannerView == null)
            CreateBannerView();
        
        // ���� �ε� ��û�� �����ϴ�.
        var adRequest = new AdRequest();
        Debug.Log("��� ���� �ε��ϴ� ���Դϴ�.");
        _bannerView.LoadAd(adRequest);
    }

    /// <summary>
    /// ��� ����
    /// </summary>
    public void DestroyBannerView()
    {
        if (_bannerView != null)
        {
            Debug.Log("��� �ı�.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }




    // [�����] ���� ������ �����ִ� ��� ���⿡�� �߻��� �� �ִ� �̺�Ʈ �����ʵ� 
    private void ListenToAdEvents()
    {
        // ���� ��� ���⿡ �ε�� �� �߻��մϴ�.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("��� ���⿡ ������ ���Ե� ���� �ε�� : "
                + _bannerView.GetResponseInfo());
        };
        // ���� ��� ���⿡ �ε���� ���� �� �߻��մϴ�..
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("������ ���� ��� ���⿡�� ���� �ε����� ���߽��ϴ�. : "
                + error);
        };
        // ����� ������ �߻��� ������ �����Ǵ� ��� �߻�.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("���� ��� ���� {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // ���� ���� ������ ��ϵǸ� �߻��մϴ�.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("��� ��ȸ���� ������� ����߽��ϴ�.");
        };
        // ���� Ŭ���� ��ϵǸ� �߻��մϴ�.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("��� ���⸦ Ŭ���߽��ϴ�.");
        };
        // ���� ��ü ȭ�� �������� �� �� �߻��մϴ�.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("��� ���� ��ü ȭ�� �������� ���Ƚ��ϴ�.");
        };
        // ���� ��ü ȭ�� �������� ���� �� �߻��մϴ�.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("��� ���� ��ü ȭ�� �������� �������ϴ�.");
        };
    }
}
