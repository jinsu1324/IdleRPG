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
    [SerializeField] private TextMeshProUGUI _slashText;        // / �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _rewardText;       // ���� �� �ؽ�Ʈ
    [SerializeField] private Button _completeButton;            // �Ϸ� ��ư

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        QuestManager.OnUpdateCurrentQuest += UpdateQuestUI; // ���� ����Ʈ ������ ������Ʈ������, ����ƮUI ������ ������Ʈ
        QuestManager.OnCheckQuestCompleted += CompleteButtonONOFF; // ����Ʈ �ϷῩ�� üũ�Ҷ�, �Ϸ��ư On / Off

        _completeButton.onClick.AddListener(QuestManager.Instance.CompleteCurrentQuest); // �Ϸ��ư ������ ����Ʈ �Ϸ�ǰ� 
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
    /// ����Ʈ UI ������Ʈ
    /// </summary>
    private void UpdateQuestUI(Quest quest)
    {
        // �������� ���� ����Ʈ�� �󼼼�ġ�� ������
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
    /// �Ϸ��ư On / Off
    /// </summary>
    private void CompleteButtonONOFF(bool flag)
    {
        _completeButton.gameObject.SetActive(flag);
    }
}
