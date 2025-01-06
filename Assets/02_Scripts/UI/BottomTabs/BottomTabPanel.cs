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
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        BottomTab.OnOpenButtonClicked -= Update_CurrentOpenTab;
        BottomTab.OnCloseButtonClicked -= Clear_CurrentOpenTab;
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
}
