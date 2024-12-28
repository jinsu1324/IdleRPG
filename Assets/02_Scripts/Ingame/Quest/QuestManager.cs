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
        InitializeProgressDict();

        _currentQuest = new Quest(_questDataList[_currentIndex]); // 일단 가장 첫번째 걸로 현재 퀘스트 활성화
        CheckCompleted(_currentQuest);
    }

    // 누적 진행 상황 초기화
    private void InitializeProgressDict()
    {
        _questTypeProgressDict = new Dictionary<QuestType, int>();
        foreach (QuestType type in Enum.GetValues(typeof(QuestType)))
        {
            _questTypeProgressDict[type] = 0;
        }
    }

    // 퀘스트 진행 상황 업데이트
    public void UpdateQuestProgress(QuestType questType, int amount)
    {
        // 현재 퀘스트가 슬라임 처치일 경우, 누적하지 않고 활성화된 퀘스트에서만 진행
        if (questType == QuestType.KillEnemy)
        {
            if (_currentQuest != null
                && _currentQuest.QuestData.QuestType == QuestType.KillEnemy
                && !_currentQuest.IsCompleted)
            {
                _currentQuest.CurrentValue += amount;

                if (_currentQuest.CurrentValue >= _currentQuest.QuestData.TargetValue)
                {
                    CompleteQuest(_currentQuest);
                }
            }
            return; // 슬라임 처치 조건은 누적하지 않음
        }


        // 누적 진행 상황 업데이트
        if (_questTypeProgressDict.ContainsKey(questType))
        {
            _questTypeProgressDict[questType] += amount;
        }

        // 현재 활성화된 퀘스트와 동일한 유형이라면 활성화된 퀘스트도 동시에 업데이트
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
        if (_currentIndex >= _questDataList.Count)
        {
            Debug.Log("모든 퀘스트 완료");
            _currentQuest = null;
            return;
        }

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























