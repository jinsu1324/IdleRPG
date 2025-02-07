using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem_Currency_Ads : MonoBehaviour
{
    [SerializeField] private Button _buyButton;         // 구매버튼
    [SerializeField] private GameObject _dimd;          // 딤드

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        AdmobManager_Reward.OnRewarded += ShowDimd; // 광고다보고 보상받을때 -> 딤드 켜기
        _buyButton.onClick.AddListener(AdmobManager_Reward.LoadAndShow_RewardedAd); // 구매 버튼 누르면 보상광고 뜨기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        AdmobManager_Reward.OnRewarded -= ShowDimd;
        _buyButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 딤드 켜기
    /// </summary>
    private void ShowDimd(OnRewardedArgs args)
    {
        _dimd.gameObject.SetActive(true);
    }
}
