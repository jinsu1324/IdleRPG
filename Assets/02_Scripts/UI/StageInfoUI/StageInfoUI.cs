using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentText;       // 현재 챕터 스테이지 텍스트
    [SerializeField] private GameObject _infiniteAnimIcon;       // 무한반복 애니메이션 아이콘
    [SerializeField] private Button _challangeButton;            // 도전 버튼

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageBuildStart += UpdateStageInfoUI;   // 스테이지 변경 시작될때 -> 스테이지 정보 UI 업데이트

        _challangeButton.onClick.AddListener(StageManager.Instance.ChallangeRestartGame);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageBuildStart -= UpdateStageInfoUI;

        _challangeButton.onClick.RemoveAllListeners();
    }
   
    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UpdateStageInfoUI(StageBuildArgs args)
    {
        int currentStage = args.CurrentStage;
        _currentText.text = $"Stage {currentStage}";

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
