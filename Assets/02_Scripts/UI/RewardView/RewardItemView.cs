using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 획득 뷰
/// </summary>
public class RewardItemView : MonoBehaviour
{
    [SerializeField] private List<RewardItemSlot> _rewardItemSlotList;  // 획득 아이템 슬롯 리스트
    [SerializeField] private Button _exitButton;                        // 나가기 버튼
    [SerializeField] private GameObject _exitGuideTextGO;               // 탭하여 계속하기 텍스트 GO

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _exitButton.onClick.AddListener(Hide);  // 나가기버튼(딤드) 누르면 꺼지게
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _exitButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 켜기
    /// </summary>
    public void Show(List<Item> dropItemList)
    {
        gameObject.SetActive(true);
        OFF_ExitButtonAndGuide();
        StartCoroutine(Init_RewardItemSlotList(dropItemList));
    }

    /// <summary>
    /// 끄기
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 획득 아이템 슬롯 리스트 초기화
    /// </summary>
    private IEnumerator Init_RewardItemSlotList(List<Item> dropItemList)
    {
        // 일단 다 끄기
        foreach (RewardItemSlot rewardItemSlot in _rewardItemSlotList)
            rewardItemSlot.Hide();

        for (int i = 0; i < _rewardItemSlotList.Count; i++)
        {
            if (i < dropItemList.Count)  
            {
                _rewardItemSlotList[i].Init(dropItemList[i]); // 드롭 아이템리스트 갯수만큼만 보여주고, 나머지는 가리기
                yield return new WaitForSecondsRealtime(0.1f);
            }
            else
                _rewardItemSlotList[i].Hide();
        }

        ON_ExitButtonAndGuide();
    }

    /// <summary>
    /// 나가기버튼과 가이드 끄기
    /// </summary>
    private void OFF_ExitButtonAndGuide()
    {
        _exitButton.interactable = false;
        _exitGuideTextGO.gameObject.SetActive(false);
    }

    /// <summary>
    /// 나가기버튼과 가이드 켜기
    /// </summary>
    private void ON_ExitButtonAndGuide()
    {
        _exitButton.interactable = true;
        _exitGuideTextGO.gameObject.SetActive(true);
    }
}
