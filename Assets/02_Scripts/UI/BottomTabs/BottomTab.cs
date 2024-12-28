using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BottomTabType
{
    PlayerDetail,
    Shop
}

[RequireComponent(typeof(ReddotComponent))]
public class BottomTab : MonoBehaviour
{
    public static event Action<BottomTab> OnOpenButtonClicked;   // 열기버튼 눌렀을 때 이벤트
    public static event Action OnCloseButtonClicked;             // 닫기버튼 눌렀을 때 이벤트

    [SerializeField] private BottomTabType _bottomTabType;       // 하단탭 타입
    [SerializeField] private TabPopupBase _tabPopup;             // 이 탭 누르면 열릴 팝업
    [SerializeField] private Button _openButton;                 // 열기 버튼
    [SerializeField] private Button _closeButton;                // 닫기 버튼

    [Title("레드닷 컴포넌트", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;   // 레드닷 컴포넌트

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _openButton.onClick.AddListener(OpenTabPopup);
        _closeButton.onClick.AddListener(CloseTabPopup);
    }

    /// <summary>
    /// 팝업 열기
    /// </summary>
    public void OpenTabPopup()
    {
        _tabPopup.Show();  // 팝업 열기
        _openButton.gameObject.SetActive(false);  // 열기 버튼은 끔
        _closeButton.gameObject.SetActive(true);  // 닫기 버튼은 켬

        OnOpenButtonClicked?.Invoke(this);
    }

    /// <summary>
    /// 팝업 닫기
    /// </summary>
    public void CloseTabPopup()
    {
        _tabPopup.Hide(); // 팝업 닫기
        _openButton.gameObject.SetActive(true);   // 열기 버튼은 켬
        _closeButton.gameObject.SetActive(false); // 닫기 버튼은 끔

        OnCloseButtonClicked?.Invoke();
    }

    /// <summary>
    /// 하단탭 타입 가져오기
    /// </summary>
    public BottomTabType GetBottomTabType()
    {
        return _bottomTabType;
    }

    /// <summary>
    /// 레드닷 조건 설정
    /// </summary>
    public void SetReddotCondition(Func<bool> condition)
    {
        _reddotComponent.UpdateReddot(condition);
    }
}
