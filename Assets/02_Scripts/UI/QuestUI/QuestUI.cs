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
        QuestManager.OnUpdateCurrentQuest += UpdateQuestUI; // 현재 퀘스트 정보들 업데이트됐을때, 퀘스트UI 정보들 업데이트
        QuestManager.OnCheckQuestCompleted += CompleteButtonONOFF; // 퀘스트 완료여부 체크할때, 완료버튼 On / Off

        _completeButton.onClick.AddListener(QuestManager.Instance.CompleteCurrentQuest); // 완료버튼 누르면 퀘스트 완료되게 
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        QuestManager.OnUpdateCurrentQuest -= UpdateQuestUI;
        QuestManager.OnCheckQuestCompleted -= CompleteButtonONOFF;

        _completeButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Quest currentQuest = QuestManager.Instance.GetCurrentQuest();
        UpdateQuestUI(currentQuest);
    }

    /// <summary>
    /// 퀘스트 UI 업데이트
    /// </summary>
    private void UpdateQuestUI(Quest quest)
    {
        // 스테이지 도달 퀘스트면 상세수치는 가리기
        if (quest.GetQuestType() == QuestType.ReachStage)
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

        _descText.text = quest.GetDesc();
        _rewardText.text = $"{quest.GetRewardGem()}";
        _currentValueText.text = $"{quest.GetCurrentValue()}";
        _targetValueText.text = $"{quest.GetTargetValue()}";
    }

    /// <summary>
    /// 완료버튼 On / Off
    /// </summary>
    private void CompleteButtonONOFF(bool flag)
    {
        _completeButton.gameObject.SetActive(flag);
    }
}
