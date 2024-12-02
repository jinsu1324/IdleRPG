using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatUpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float _upgradeInterval = 0.1f;  // 업그레이드 간격
    private bool _isPressed = false;        // 버튼이 눌린 상태 확인
    private Coroutine _upgradeCoroutine;    // 현재 실행중인 Coroutine 참조
    private string _statID;                 // 슬롯의 스탯 ID
    private PlayerManager _playerManager;   // 플레이어 매니저
    private Action _onUpdateUI;             // 업그레이드 하면 UI업데이트 되는 함수 담을 이벤트

    private Vector3 _originalScale;         // 원래 스케일 저장

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize(Action updateUI, string statID)
    {
        _statID = statID;
        _playerManager = PlayerManager.Instance;
        _onUpdateUI = updateUI;

        // 초기 스케일 저장
        _originalScale = transform.localScale;
    }

    /// <summary>
    /// 눌렀을때 1회 호출
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        if (_upgradeCoroutine == null)
        {
            _upgradeCoroutine = StartCoroutine(UgradeRepeat());
        }
    }

    /// <summary>
    /// 뗄 때 1회 호출
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;

        // 눌렀을 때 스케일 애니메이션 실행
        PlayScaleAnimation();

        if (_upgradeCoroutine != null) // Coroutine이 실행 중이면 중지
        {
            StopCoroutine(_upgradeCoroutine);
            _upgradeCoroutine = null;
        }
    }

    /// <summary>
    /// 업그레이드 반복 코루틴
    /// </summary>
    private IEnumerator UgradeRepeat()
    {
        while (_isPressed)
        {
            Upgrade();
            yield return new WaitForSeconds(_upgradeInterval);
        }
    }

    /// <summary>
    /// 업그레이드
    /// </summary>
    private void Upgrade()
    {
        // 레벨업 가능하면 레벨업 로직실행하고 true 반환 (TryLevelUpStatByID 내부에 구현되어있음)
        if (_playerManager.TryLevelUpStatByID(_statID))
        {
            // UI 업데이트
            _onUpdateUI?.Invoke();

            // 토스트 메시지 호출
            ToastManager.Instance.ShowToastCombatPower();

            // 스케일 애니메이션 실행
            PlayScaleAnimation();
        }
        else
        {
            Debug.Log("이 스탯을 업그레이드 할 골드가 충분하지 않습니다!");
        }
    }

    /// <summary>
    /// 버튼 스케일 애니메이션
    /// </summary>
    private void PlayScaleAnimation()
    {
        transform.DOKill(); // 기존 애니메이션 중단
        transform.localScale = _originalScale;  // 스케일 초기화

        // 작아졌다 커지는 애니메이션
        transform.DOScale(_originalScale * 0.8f, 0.05f) // 작아지기
            .OnComplete(() => transform.DOScale(_originalScale, 0.05f)); // 원래 크기로 복귀
            
    }
}
