using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem_Currency_Ads : MonoBehaviour
{
    [SerializeField] private Button _buyButton;         // ���Ź�ư
    [SerializeField] private GameObject _dimd;          // ����

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        AdmobManager_Reward.OnRewarded += ShowDimd; // ����ٺ��� ��������� -> ���� �ѱ�
        _buyButton.onClick.AddListener(AdmobManager_Reward.LoadAndShow_RewardedAd); // ���� ��ư ������ ���󱤰� �߱�
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
    /// ���� �ѱ�
    /// </summary>
    private void ShowDimd(OnRewardedArgs args)
    {
        _dimd.gameObject.SetActive(true);
    }
}
