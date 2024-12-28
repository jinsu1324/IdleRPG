using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomTabPanel : SerializedMonoBehaviour
{
    [SerializeField] private List<BottomTab> _bottomTabList;    // 하단탭 리스트
    private BottomTab _currentOpenTab;                          // 현재 열려있는 하단탭

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        BottomTab.OnOpenButtonClicked += Update_CurrentOpenTab; // 열기버튼 눌렀을 때, 현재열린 탭 업데이트
        BottomTab.OnCloseButtonClicked += Clear_CurrentOpenTab; // 닫기버튼 눌렀을 때, 현재열린 탭 비우기

        ItemInven.OnItemInvenChanged += Update_BottomTabReddots; // 가지고 있는 아이템이 변경되었을 때, 하단탭들 레드닷 업데이트
        SelectItemInfoUI.OnItemStatueChanged += Update_BottomTabReddots; // 아이템 상태가 바뀌었을 때, 하단탭들 레드닷 업데이트
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Update_BottomTabReddots();
    }

    /// <summary>
    /// 현재열린탭 업데이트
    /// </summary>
    public void Update_CurrentOpenTab(BottomTab bottomTab)
    {
        // 1. 아직 열린 탭이 하나도 없으면 현재탭으로 
        if (_currentOpenTab == null)
            _currentOpenTab = bottomTab;

        // 2. 같은 탭이 눌리면 그냥 그대로
        if (_currentOpenTab == bottomTab)
            return;

        // 3. 다른 탭이 눌리면
        if (_currentOpenTab != bottomTab)
        {
            _currentOpenTab.CloseTabPopup();  // 원래 탭 끄고
            _currentOpenTab = bottomTab;      // 새로운 탭이 현재탭을 대체
        }
    }

    /// <summary>
    /// 현재열린탭 비우기
    /// </summary>
    public void Clear_CurrentOpenTab()
    {
        _currentOpenTab = null;   // 현재 열린탭을 아무것도 없게
    }

    /// <summary>
    /// 하단탭들 레드닷 업데이트
    /// </summary>
    private void Update_BottomTabReddots()
    {
        // 하단탭 레드닷 조건들 (여기서 수정)
        Dictionary<BottomTabType, Func<bool>> redDotConditionDict = new Dictionary<BottomTabType, Func<bool>>
        {
            { BottomTabType.PlayerDetail, () => ItemInven.HasEnhanceableItemAllInven()},
            { BottomTabType.Shop, () => false} // shop은 임시
        };

        // 하단탭들 레드닷에 조건 설정
        foreach (BottomTab bottomTab in _bottomTabList)
        {
            if (redDotConditionDict.TryGetValue(bottomTab.GetBottomTabType(), out Func<bool> condition))
                bottomTab.SetReddotCondition(condition);
            else
                Debug.Log("레드닷 조건을 찾을 수 없습니다");
        }

    }
    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        BottomTab.OnOpenButtonClicked -= Update_CurrentOpenTab;
        BottomTab.OnCloseButtonClicked -= Clear_CurrentOpenTab;

        ItemInven.OnItemInvenChanged -= Update_BottomTabReddots;
        SelectItemInfoUI.OnItemStatueChanged -= Update_BottomTabReddots;
    }
}
