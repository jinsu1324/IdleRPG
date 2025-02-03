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
        QuestManager.OnUpdateQuest += UpdateQuestUI; // ���� ����Ʈ ������ ������Ʈ������, ����ƮUI ������ ������Ʈ
        QuestManager.OnCheckComplete += CompleteButtonONOFF; // ����Ʈ �ϷῩ�� üũ�Ҷ�, �Ϸ��ư On / Off

        _completeButton.onClick.AddListener(QuestCompleteUITask); // �Ϸ��ư ������ ����Ʈ �Ϸ� �½�ũ ����
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
    /// ����Ʈ UI ������Ʈ
    /// </summary>
    private void UpdateQuestUI(Quest quest)
    {
        QuestData questData = QuestDataManager.GetQuestData(quest.QuestIndex);

        _descText.text = questData.Desc;
        _rewardText.text = $"{questData.RewardGem}";
        _currentValueText.text = $"{quest.CurrentValue}";
        _targetValueText.text = $"{questData.TargetValue}";

        // �������� ���� ����Ʈ�� �󼼼�ġ�� ������
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
    /// �Ϸ��ư On / Off
    /// </summary>
    private void CompleteButtonONOFF(bool isComplete)
    {
        _completeButton.gameObject.SetActive(isComplete);
    }

    /// <summary>
    /// ����Ʈ �Ϸ� UI �½�ũ
    /// </summary>
    private void QuestCompleteUITask()
    {
        QuestManager.CompleteCurrentQuest();    // ����Ʈ �Ϸ�
        CurrencyIconMover.Instance.MoveCurrency_Multi(CurrencyType.Gem, transform.position); // ���� �̵� �ִϸ��̼�
    }
}
