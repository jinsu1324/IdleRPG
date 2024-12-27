using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour
{
    [SerializeField] private List<RewardItemSlot> _rewardItemSlotList;  // 획득 아이템 슬롯 리스트
    [SerializeField] private Button _exitButton;                        // 나가기 버튼
    [SerializeField] private GameObject _exitGuideTextGO;               // 탭하여 계속하기 텍스트 GO

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _exitButton.onClick.AddListener(Hide);  // 나가기버튼(딤드) 누르면 꺼지게
    }

    /// <summary>
    /// 켜기
    /// </summary>
    public void Show(List<Item> dropItemList)
    {
        Change_ExitGuideTextGO(false);
        Change_ExitButtonInteractable(false);
        gameObject.SetActive(true);
        StartCoroutine(Init_RewardItemSlotList(dropItemList));
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
            if (i < dropItemList.Count)  // 드롭 카운트만큼만 rewardItemSlot 초기화
            {
                Item dropItem = dropItemList[i];
                _rewardItemSlotList[i].Init(dropItem);
                
                yield return new WaitForSecondsRealtime(0.1f);
            }
            else
            {
                _rewardItemSlotList[i].Hide();
            }
        }

        Change_ExitGuideTextGO(true);
        Change_ExitButtonInteractable(true);
    }

    /// <summary>
    /// 나가기버튼(딤드) 활성화 / 비활성화
    /// </summary>
    private void Change_ExitButtonInteractable(bool flag) => _exitButton.interactable = flag;

    /// <summary>
    /// 탭하여 계속하기 텍스트 GO 활성화 / 비활성화
    /// </summary>
    private void Change_ExitGuideTextGO(bool flag) => _exitGuideTextGO.gameObject.SetActive(flag);

    /// <summary>
    /// 끄기
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
