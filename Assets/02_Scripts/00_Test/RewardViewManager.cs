using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardViewManager : MonoBehaviour
{
    [SerializeField] private RewardItemView _rewardItemView;    // ������ ȹ�� ��

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemDropMachine.OnDroppedItem += _rewardItemView.Show; // ������ ��ӿϷ�Ǹ� -> ������ȹ��� �ѱ�
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemDropMachine.OnDroppedItem -= _rewardItemView.Show;
    }
}
