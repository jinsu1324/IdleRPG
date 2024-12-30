using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descText;         // ����Ʈ ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _currentValueText; // ���� �� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _targetValueText;  // Ÿ�� �� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _rewardText;       // ���� �� �ؽ�Ʈ
    [SerializeField] private Button _completeButton;            // �Ϸ� ��ư

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        QuestManager.OnUpdateCurrentQuest += UpdateQuestUI; // ���� ����Ʈ ������ ������Ʈ������, ����ƮUI ������ ������Ʈ
        QuestManager.OnCheckQuestCompleted += CompleteButtonONOFF; // ����Ʈ �ϷῩ�� üũ�Ҷ�, �Ϸ��ư On / Off

        _completeButton.onClick.AddListener(QuestManager.Instance.CompleteCurrentQuest); // �Ϸ��ư ������ ����Ʈ �Ϸ�ǰ� 

        Quest currentQuest = QuestManager.Instance.GetCurrentQuest();
        UpdateQuestUI(currentQuest);
    }

    /// <summary>
    /// ����Ʈ UI ������Ʈ
    /// </summary>
    private void UpdateQuestUI(Quest quest)
    {
        _descText.text = quest.GetDesc();
        _currentValueText.text = $"{quest.GetCurrentValue()}";
        _targetValueText.text = $"{quest.GetTargetValue()}";
        _rewardText.text = $"{quest.GetRewardGem()}";
    }

    /// <summary>
    /// �������� OnDestroy
    /// </summary>
    private void OnDestroy()
    {
        QuestManager.OnUpdateCurrentQuest -= UpdateQuestUI;
        QuestManager.OnCheckQuestCompleted -= CompleteButtonONOFF;
    }

    /// <summary>
    /// �Ϸ��ư On / Off
    /// </summary>
    private void CompleteButtonONOFF(bool flag)
    {
        _completeButton.gameObject.SetActive(flag);
    }
}
