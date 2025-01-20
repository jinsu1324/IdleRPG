using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardViewManager : MonoBehaviour
{
    [SerializeField] private RewardItemView _rewardItemView;    // æ∆¿Ã≈€ »πµÊ ∫‰

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemDropMachine.OnDroppedItem += _rewardItemView.Show; // æ∆¿Ã≈€ µÂ∑”øœ∑·µ«∏È -> æ∆¿Ã≈€»πµÊ∫‰ ƒ—±‚
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemDropMachine.OnDroppedItem -= _rewardItemView.Show;
    }
}
