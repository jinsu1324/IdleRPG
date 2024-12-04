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

    [SerializeField] private RectTransform _currentPosArrow;
    [SerializeField] private List<RectTransform> _stagePosList;

    private void OnEnable()
    {
        StageManager.Instance.OnStageChanged += UpdateUI;
        Init();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    private void Init()
    {
        // Todo 임시데이터
        OnStageChangedArgs args = new OnStageChangedArgs() { CurrentChapter = 1, CurrentStage = 1 };
        UpdateUI(args);
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UpdateUI(OnStageChangedArgs args)
    {
        int currentChapter = args.CurrentChapter;
        int currentStage = args.CurrentStage;

        _currentPosArrow.localPosition = _stagePosList[currentStage - 1].localPosition + new Vector3(0, -30, 0);
        _currentText.text = $"{currentChapter}-{currentStage}";
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.Instance.OnStageChanged -= UpdateUI;
    }
}
