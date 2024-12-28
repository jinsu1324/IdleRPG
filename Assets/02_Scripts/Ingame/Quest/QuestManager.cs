using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : SingletonBase<QuestManager>
{
    private List<QuestData> _questDataList = new List<QuestData>();  // 모든 퀘스트 데이터 테이블
    private Quest _currentQuest;                                     // 현재 활성화된 퀘스트
    private int _currentIndex = 0;                                   // 현재 퀘스트 인덱스
    private Dictionary<QuestType, int> _questTypeProgressDict;       // 퀘스트 유형별 누적 진행 상황

    private void Start()
    {
        _questDataList = DataManager.Instance.QuestDatasSO.QuestDataList;
        _currentQuest = new Quest(_questDataList[_currentIndex]); // 일단 가장 첫번째 걸로 현재 퀘스트 활성화
        CheckCompleted(_currentQuest);
    }


    // 퀘스트 진행 상황 업데이트
    public void UpdateQuestProgress(QuestType questType, int amount)
    {
        // 누적 진행 상황 업데이트
        if (_questTypeProgressDict.ContainsKey(questType))
        {
            _questTypeProgressDict[questType] += amount;
        }

        // 현재 활성화된 퀘스트와 동일한 유형이라면
        if (_currentQuest != null 
            && _currentQuest.QuestData.QuestType == questType 
            && _currentQuest.IsCompleted == false)
        {
            _currentQuest.CurrentValue = _questTypeProgressDict[questType];

            if (_currentQuest.CurrentValue >= _currentQuest.QuestData.TargetValue)
            {
                CompleteQuest(_currentQuest);
            }
        }
    }

    // 퀘스트 완료 처리
    private void CompleteQuest(Quest quest)
    {
        quest.IsCompleted = true; // 완료 상태로 설정
        Debug.Log($"퀘스트 완료: {quest.QuestData.Desc}");


        Reward(quest);

        StartNextQuest();
    }

    // 다음 퀘스트 시작
    private void StartNextQuest()
    {
        _currentIndex++;
        _currentQuest = new Quest(_questDataList[_currentIndex]);

        CheckCompleted(_currentQuest);
    }

    // 즉시 완료 여부 확인
    private void CheckCompleted(Quest quest)
    {
        // 누적 값이랑 비교하고 이미 지났다면 즉시 완료
        if (_questTypeProgressDict[quest.QuestData.QuestType] >= quest.CurrentValue)
        {
            CompleteQuest(quest);
        }
    }


    // 보상 지급
    private void Reward(Quest quest)
    {
        Debug.Log($"보상지급 : {quest.QuestData.RewardGold}");
        // 실제로는 연결하여 보상을 지급
    }

}























