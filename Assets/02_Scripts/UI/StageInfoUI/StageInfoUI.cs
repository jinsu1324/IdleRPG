using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour
{
    // LayoutGroup은 Unity의 UI 시스템에서 OnEnable 이후 LateUpdate 단계에서 레이아웃을 업데이트하기 때문에
    // Start 시점에 UI 위치가 올바르지 않을 수 있습니다.
    [InfoBox("StagePos가 모여있는 부모의 LayoutGroup을 꺼주세요.", InfoMessageType.Info)]

    [SerializeField] private TextMeshProUGUI _currentText;       // 현재 챕터 스테이지 텍스트
    [SerializeField] private RectTransform _currentPosArrow;     // 현재 내 위치 화살표
    [SerializeField] private List<RectTransform> _stagePosList;  // 스테이지들 위치
    [SerializeField] private GameObject _infiniteAnimIcon;       // 무한반복 애니메이션 아이콘
    [SerializeField] private Button _challangeButton;            // 도전 버튼

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageChanged += UpdateStageInfoUI;   // 스테이지 변경될 때 -> 스테이지 정보 UI 업데이트

        _challangeButton.onClick.AddListener(StageManager.Instance.ChallangeRestartGame);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageChanged -= UpdateStageInfoUI;

        _challangeButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    private void Init()
    {
        // Todo 임시데이터
        OnStageChangedArgs args = new OnStageChangedArgs() { CurrentChapter = 1, CurrentStage = 1 };
        UpdateStageInfoUI(args);
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UpdateStageInfoUI(OnStageChangedArgs args)
    {
        int currentChapter = args.CurrentChapter;
        int currentStage = args.CurrentStage;

        _currentPosArrow.localPosition = _stagePosList[currentStage - 1].localPosition + new Vector3(0, -30, 0);
        _currentText.text = $"{currentChapter}-{currentStage}";

        InfiniteGOs_OnOff(); // 무한 스테이지 애니메이션 켜기 / 끄기
    }

    /// <summary>
    /// 무한 스테이지관련 게임오브젝트들 켜기 / 끄기
    /// </summary>
    private void InfiniteGOs_OnOff()
    {
        _infiniteAnimIcon.gameObject.SetActive(StageManager.Instance.IsInfiniteStage());
        _challangeButton.gameObject.SetActive(StageManager.Instance.IsInfiniteStage());
    }
}
