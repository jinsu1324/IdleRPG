using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    private QuestType _questType;   // 퀘스트 타입
    private string _desc;           // 퀘스트 설명
    private int _targetValue;       // 퀘스트 타겟 수치
    private int _rewardGold;        // 보상
    private int _currentValue;      // 현재 진행 값
    private bool _isCompleted;      // 완료 여부

    /// <summary>
    /// 생성자
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

    // Get Set 함수들로
    public QuestType GetQuestType() => _questType;  // 퀘스트 타입 가져오기
    public string GetDesc() => _desc;   // 퀘스트 설명 가져오기
    public int GetTargetValue() => _targetValue;    // 타겟 수치 가져오기
    public int GetRewardGold() => _rewardGold;  // 보상 가져오기
    public int GetCurrentValue() => _currentValue;  // 현재 값 가져오기
    public int SetCurrentValue(int amount) => _currentValue = amount;   // 현재 값 설정하기
    public bool IsCompleted() // 완료 여부 가져오기
    {
        _isCompleted = _currentValue >= _targetValue;
        return _isCompleted;
    }
}
