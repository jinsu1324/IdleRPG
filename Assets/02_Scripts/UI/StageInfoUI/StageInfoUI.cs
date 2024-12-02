using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageInfoUI : MonoBehaviour
{
    // LayoutGroup은 Unity의 UI 시스템에서 OnEnable 이후 LateUpdate 단계에서 레이아웃을 업데이트하기 때문에
    // Start 시점에 UI 위치가 올바르지 않을 수 있습니다.
    [InfoBox("StagePos가 모여있는 부모의 LayoutGroup을 꺼주세요.", InfoMessageType.Info)]

    [SerializeField] private TextMeshProUGUI _currentText;       // 현재 챕터 스테이지 텍스트

    private int _currentChapter;                                 // 현재 챕터
    private int _currentStage;                                   // 현재 스테이지
    private PlayerManager _playerManager;                        // 플레이어 매니저 캐싱용

    [SerializeField] private RectTransform _currentPosArrow;
    [SerializeField] private List<RectTransform> _stagePosList;

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _playerManager = PlayerManager.Instance;
        
        _playerManager.PlayerData.OnStageChange += UpdateUI;    // 스테이지 변경 이벤트 구독

        UpdateUI();
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _playerManager.PlayerData.OnStageChange -= UpdateUI;    // 스테이지 변경 이벤트 구독해제
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UpdateUI()
    {
        _currentStage = _playerManager.GetCurrentStage();
        _currentPosArrow.localPosition = _stagePosList[_currentStage - 1].localPosition + new Vector3(0, -30, 0);

        _currentText.text = $"{_playerManager.GetCurrentChapter()}-{_playerManager.GetCurrentStage()}";
    }
}
