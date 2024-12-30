using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : SingletonBase<QuestManager>
{
    public static event Action<Quest> OnUpdateCurrentQuest;          // 현재 퀘스트 정보들 업데이트됐을때 이벤트
    public static event Action<bool> OnCheckQuestCompleted;          // 퀘스트 완료여부 체크할때 이벤트
    
    [SerializeField] private QuestDatasSO _questDatasSO;             // 퀘스트 데이터

    private List<QuestData> _questDataList = new List<QuestData>();  // 모든 퀘스트 데이터 리스트
    private Quest _currentQuest;                                     // 현재 활성화된 퀘스트
    private int _currentIndex = 0;                                   // 현재 퀘스트 인덱스
    private Dictionary<QuestType, int> _questProgressDict;           // 퀘스트 유형별 누적 진행 상황 딕셔너리

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        InitQuestDataList(); // 퀘스트 데이터 리스트 초기화
        InitQuestProgressDict(); // 누적진행상황 딕셔너리 초기화

        _currentQuest = new Quest(_questDataList[_currentIndex]); // 일단 가장 첫번째 걸로 현재 퀘스트 활성화
        OnUpdateCurrentQuest?.Invoke(_currentQuest); // 현재 퀘스트 정보 업데이트 이벤트 실행
        CheckQuestComplete(_currentQuest); // 완료여부 체크
    }

    /// <summary>
    /// 퀘스트 데이터 리스트 초기화
    /// </summary>
    private void InitQuestDataList()
    {
        _questDataList = _questDatasSO.QuestDataList;
    }

    /// <summary>
    /// 누적진행상황 딕셔너리 초기화
    /// </summary>
    private void InitQuestProgressDict()
    {
        _questProgressDict = new Dictionary<QuestType, int>();
        foreach (QuestType questType in Enum.GetValues(typeof(QuestType)))
        {
            _questProgressDict[questType] = 0;
        }
    }

    /// <summary>
    /// 퀘스트 진행상황 업데이트
    /// </summary>
    public void UpdateQuestProgress(QuestType questType, int amount)
    {
        // 누적 진행 상황 업데이트
        if (_questProgressDict.ContainsKey(questType))
            _questProgressDict[questType] += amount;

        // 현재퀘스트가 없으면 그냥 리턴
        if (_currentQuest == null)
            return;

        // 파라미터 퀘스트타입이 = 현재 퀘스트타입과 동일하다면, 현재 퀘스트도 업데이트
        if (_currentQuest.GetQuestType() == questType && _currentQuest.IsCompleted() == false)
        {
            _currentQuest.SetCurrentValue(_questProgressDict[questType]); // 누적된 수치를 현재퀘스트 수치로
            OnUpdateCurrentQuest?.Invoke(_currentQuest); // 현재 퀘스트 정보 업데이트 이벤트 실행
            CheckQuestComplete(_currentQuest); // 완료여부 체크
        }
    }

    /// <summary>
    /// 퀘스트 완료 여부 확인
    /// </summary>
    private void CheckQuestComplete(Quest quest)
    {
        OnCheckQuestCompleted?.Invoke(quest.IsCompleted()); // 퀘스트 완료여부 체크할때 이벤트 실행
    }

    /// <summary>
    /// 퀘스트 완료
    /// </summary>
    public void CompleteCurrentQuest()
    {
        Debug.Log($"퀘스트 완료: {_currentQuest.GetDesc()}");
        
        Reward(_currentQuest); // 보상 지급
        StartNextQuest(); // 다음 퀘스트로
    }

    /// <summary>
    /// 다음 퀘스트 시작
    /// </summary>
    private void StartNextQuest()
    {
        _currentIndex++;
        if (_currentIndex >= _questDataList.Count)
        {
            Debug.Log("모든 퀘스트 완료");
            _currentQuest = null;
            return;
        }
        
        _currentQuest = new Quest(_questDataList[_currentIndex]); // 다음 퀘스트로 현재퀘스트 설정
        OnUpdateCurrentQuest?.Invoke(_currentQuest); // 현재 퀘스트 정보 업데이트 이벤트 실행
        CheckQuestComplete(_currentQuest); // 완료 여부 체크
    }

    /// <summary>
    /// 보상 지급
    /// </summary>
    private void Reward(Quest quest)
    {
        GemManager.AddGem(quest.GetRewardGem());

        Debug.Log($"보상지급 : {quest.GetRewardGem()}");
    }

    /// <summary>
    /// 현재 퀘스트 가져오기
    /// </summary>
    public Quest GetCurrentQuest()
    {
        return _currentQuest;
    }

    /// <summary>
    /// 퀘스트 진행상태 누적값 가져오기
    /// </summary>
    public int GetQuestProgressAmount(QuestType questType)
    {
        if (_questProgressDict.TryGetValue(questType, out int amount))
            return amount;
        else
            return 0;
    }
}
