using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyRewardViewManager : MonoBehaviour
{
    [SerializeField] private CurrencyRewardView _currencyRewardView;   // ¿Á»≠ »πµÊ ∫‰

    private void OnEnable()
    {
        AdmobManager_Reward.OnRewarded += _currencyRewardView.Show;
    }

    private void OnDisable()
    {
        AdmobManager_Reward.OnRewarded -= _currencyRewardView.Show;
    }
}
