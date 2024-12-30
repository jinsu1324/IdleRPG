using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    private QuestType _questType;   // ����Ʈ Ÿ��
    private string _desc;           // ����Ʈ ����
    private int _targetValue;       // ����Ʈ Ÿ�� ��ġ
    private int _rewardGold;        // ����
    private int _currentValue;      // ���� ���� ��
    private bool _isCompleted;      // �Ϸ� ����

    /// <summary>
    /// ������
    /// </summary>
    public Quest(QuestData questData)
    {
        _questType = questData.QuestType;
        _desc = questData.Desc;
        _targetValue = questData.TargetValue;
        _rewardGold = questData.RewardGold;
        _currentValue = QuestManager.Instance.GetQuestProgressAmount(questData.QuestType);
        _isCompleted = false;
    }

    // Get Set �Լ����
    public QuestType GetQuestType() => _questType;  // ����Ʈ Ÿ�� ��������
    public string GetDesc() => _desc;   // ����Ʈ ���� ��������
    public int GetTargetValue() => _targetValue;    // Ÿ�� ��ġ ��������
    public int GetRewardGold() => _rewardGold;  // ���� ��������
    public int GetCurrentValue() => _currentValue;  // ���� �� ��������
    public int SetCurrentValue(int amount) => _currentValue = amount;   // ���� �� �����ϱ�
    public bool IsCompleted() // �Ϸ� ���� ��������
    {
        _isCompleted = _currentValue >= _targetValue;
        return _isCompleted;
    }
}
