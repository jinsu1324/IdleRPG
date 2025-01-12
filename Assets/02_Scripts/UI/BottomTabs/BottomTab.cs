using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BottomTabType
{
    Gear,
    Shop,
    Skill
}

[RequireComponent(typeof(ReddotComponent))]
public class BottomTab : MonoBehaviour
{
    public static event Action<BottomTab> OnOpenButtonClicked;   // 열기버튼 눌렀을 때 이벤트
    public static event Action OnCloseButtonClicked;             // 닫기버튼 눌렀을 때 이벤트

    [SerializeField] private BottomTabType _bottomTabType;       // 하단탭 타입
    [SerializeField] private BottomTabPopupBase _tabPopup;       // 이 탭 누르면 열릴 팝업
    [SerializeField] private Button _openButton;                 // 열기 버튼
    [SerializeField] private Button _closeButton;                // 닫기 버튼

    [Title("레드닷 컴포넌트", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;  // 레드닷 컴포넌트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemInven.OnAddItem += Update_ReddotComponent; // 아이템 추가되었을 때 -> 하단탭 레드닷 컴포넌트 업데이트
        ItemEnhanceManager.OnItemEnhance += Update_ReddotComponent; // 아이템 강화할때 -> 하단탭 레드닷 컴포넌트 업데이트

        _openButton.onClick.AddListener(OpenTabPopup);
        _closeButton.onClick.AddListener(CloseTabPopup);

        Update_ReddotComponent(); // 레드닷 컴포넌트 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemInven.OnAddItem -= Update_ReddotComponent;
        ItemEnhanceManager.OnItemEnhance -= Update_ReddotComponent;

        _openButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
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
    /// 레드닷 컴포넌트 업데이트
    /// </summary>
    public void Update_ReddotComponent(Item item = null)
    {
        switch (_bottomTabType)
        {
            case BottomTabType.Gear: 
                _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableGear()); // 강화가능한 장비 있는지 확인
                break;
            case BottomTabType.Skill:
                _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableSkill()); // 강화가능한 스킬 있는지 확인
                break;
            case BottomTabType.Shop:
                _reddotComponent.Hide();
                break;
        }
    }
}
