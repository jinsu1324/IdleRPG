using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    private int _questIndex;        // 이 퀘스트의 인덱스
    private QuestType _questType;   // 퀘스트 타입
    private int _currentValue;      // 현재 진행 값
    private bool _isCompleted;      // 완료 여부
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
    /// 기본 생성자
    /// </summary>
    public Quest()
    {
    }

    /// <summary>
    /// 매개변수 생성자
    /// </summary>
    public Quest(QuestData questData)
    {
        _questIndex = questData.Index;
        _questType = questData.QuestType;
        _currentValue = QuestManager.Get_QuestStackValue(questData.QuestType);
        _isCompleted = false;
    }

    /// <summary>
    /// 현재 진행값 설정하기 (덮어쓰기)
    /// </summary>
    public int SetValue(int amount) => _currentValue = amount;

    /// <summary>
    /// 현재 진행값 더하기 (누적)
    /// </summary>
    public int AddValue(int amount) => _currentValue += amount;
}
