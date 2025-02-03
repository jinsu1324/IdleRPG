using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descText;         // 퀘스트 설명 텍스트
    [SerializeField] private TextMeshProUGUI _currentValueText; // 현재 값 텍스트
    [SerializeField] private TextMeshProUGUI _targetValueText;  // 타겟 값 텍스트
    [SerializeField] private TextMeshProUGUI _slashText;        // / 텍스트
    [SerializeField] private TextMeshProUGUI _rewardText;       // 보상 값 텍스트
    [SerializeField] private Button _completeButton;            // 완료 버튼

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        QuestManager.OnUpdateQuest += UpdateQuestUI; // 현재 퀘스트 정보들 업데이트됐을때, 퀘스트UI 정보들 업데이트
        QuestManager.OnCheckComplete += CompleteButtonONOFF; // 퀘스트 완료여부 체크할때, 완료버튼 On / Off

        _completeButton.onClick.AddListener(QuestCompleteUITask); // 완료버튼 누르면 퀘스트 완료 태스크 실행
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        QuestManager.OnUpdateQuest -= UpdateQuestUI;
        QuestManager.OnCheckComplete -= CompleteButtonONOFF;

        _completeButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 퀘스트 UI 업데이트
    /// </summary>
    private void UpdateQuestUI(Quest quest)
    {
        QuestData questData = QuestDataManager.GetQuestData(quest.QuestIndex);

        _descText.text = questData.Desc;
        _rewardText.text = $"{questData.RewardGem}";
        _currentValueText.text = $"{quest.CurrentValue}";
        _targetValueText.text = $"{questData.TargetValue}";

        // 스테이지 도달 퀘스트면 상세수치는 가리기
        if (quest.QuestType == QuestType.ReachStage)
        {
            _currentValueText.gameObject.SetActive(false);
            _slashText.gameObject.SetActive(false);
            _targetValueText.gameObject.SetActive(false);
        }
        else
        {
            _currentValueText.gameObject.SetActive(true);
            _slashText.gameObject.SetActive(true);
            _targetValueText.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 완료버튼 On / Off
    /// </summary>
    private void CompleteButtonONOFF(bool isComplete)
    {
        _completeButton.gameObject.SetActive(isComplete);
    }

    /// <summary>
    /// 퀘스트 완료 UI 태스크
    /// </summary>
    private void QuestCompleteUITask()
    {
        QuestManager.CompleteCurrentQuest();    // 퀘스트 완료
        CurrencyIconMover.Instance.MoveCurrency_Multi(CurrencyType.Gem, transform.position); // 보석 이동 애니메이션
    }
}
