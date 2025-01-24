using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    private int _questIndex;        // �� ����Ʈ�� �ε���
    private QuestType _questType;   // ����Ʈ Ÿ��
    private int _currentValue;      // ���� ���� ��
    private bool _isCompleted;      // �Ϸ� ����
    public int QuestIndex { get { return _questIndex; } set { _questIndex = value; } }
    public QuestType QuestType { get { return _questType; } set { _questType = value; } }
    public int CurrentValue { get { return _currentValue; } set { _currentValue = value; } }
    public bool IsCompleted 
    { 
        get 
        {
            _isCompleted = _currentValue >= QuestDataManager.GetQuestData(_questIndex).TargetValue;
            return _isCompleted;
        } 
        set { _isCompleted = value; } 
    }

    /// <summary>
    /// �⺻ ������
    /// </summary>
    public Quest()
    {
    }

    /// <summary>
    /// �Ű����� ������
    /// </summary>
    public Quest(QuestData questData)
    {
        _questIndex = questData.Index;
        _questType = questData.QuestType;
        _currentValue = QuestManager.Get_QuestStackValue(questData.QuestType);
        _isCompleted = false;
    }

    /// <summary>
    /// ���� ���ప �����ϱ� (�����)
    /// </summary>
    public int SetValue(int amount) => _currentValue = amount;

    /// <summary>
    /// ���� ���ప ���ϱ� (����)
    /// </summary>
    public int AddValue(int amount) => _currentValue += amount;
}
