using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropButton : MonoBehaviour
{
    [SerializeField] private Button _dropButton;        // 드롭 버튼
    [SerializeField] private GameObject _dimd;          // 딤드
    [SerializeField] private TextMeshProUGUI _costText; // 비용 텍스트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemDropMachine.OnDimdUpdate += UpdateDimd; // 딤드 업데이트
        AdmobManager_Reward.OnRewarded += UpdateDimd;   // 딤드 업데이트 (오버로딩)
    }

    /// <summary>
    /// OnDiable
    /// </summary>
    private void OnDisable()
    {
        ItemDropMachine.OnDimdUpdate -= UpdateDimd;
        AdmobManager_Reward.OnRewarded -= UpdateDimd;
    }

    /// <summary>
    /// 딤드 업데이트
    /// </summary>
    public void UpdateDimd(bool canBuy)
    {
        _dimd.SetActive(!canBuy);

        if (canBuy)
            _costText.color = Color.white;
        else
            _costText.color = Color.red;
    }

    /// <summary>
    /// 딤드 업데이트 오버로딩
    /// </summary>
    public void UpdateDimd(OnRewardedArgs args)
    {
        _dimd.SetActive(!args.CanBuyItem);

        if (args.CanBuyItem)
            _costText.color = Color.white;
        else
            _costText.color = Color.red;
    }

    /// <summary>
    /// 버튼에 아이템드롭 함수 리스터 연결
    /// </summary>
    public void ButtonAddListener(UnityEngine.Events.UnityAction dropItem)
    {
        _dropButton.onClick.AddListener(dropItem);
    }

    /// <summary>
    /// 버튼에 리스너 제거
    /// </summary>
    public void ButtonRemoveListener()
    {
        _dropButton.onClick.RemoveAllListeners();
    }
}
