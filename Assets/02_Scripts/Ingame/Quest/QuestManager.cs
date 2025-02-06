using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : ISavable
{
    public static event Action<Quest> OnUpdateQuest;            // 현재 퀘스트 정보들 업데이트됐을때 이벤트
    public static event Action<bool> OnCheckComplete;           // 퀘스트 완료여부 체크 이벤트

    public string Key => nameof(QuestManager);                  // Firebase 데이터 저장용 고유 키 설정
    [SaveField] private static int _currentQuestIndex = 1;      // 현재 퀘스트 인덱스
    [SaveField] private static Quest _currentQuest;             // 현재 퀘스트
    public static Quest CurrentQuest { get { return _currentQuest; } set { _currentQuest = value; } }

    // 퀘스트 유형별 누적 진행 상황 딕셔너리
    [SaveField]
    private static Dictionary<QuestType, int> _questStackDict = new Dictionary<QuestType, int>()
    {
        { QuestType.KillEnemy, 0},
        { QuestType.CollectGold, 0},
        { QuestType.UpgradeAttackPower, 1},
        { QuestType.UpgradeAttackSpeed, 1},
        { QuestType.UpgradeMaxHp, 1},
        { QuestType.UpgradeCriticalRate, 1},
        { QuestType.UpgradeCriticalMultiple, 1},
        { QuestType.ReachStage, 1},
    };

    /// <summary>
    /// 데이터 불러오기할때 태스크들
    /// </summary>
    public void DataLoadTask()
    {
        OnUpdateQuest?.Invoke(CurrentQuest);
        OnCheckComplete?.Invoke(CurrentQuest.IsCompleted);
    }

    /// <summary>
    /// 현재 퀘스트 셋팅하기
    /// </summary>
    public static void SetCurrentQuest()
    {
        CurrentQuest = CreateQuest(_currentQuestIndex);

        OnUpdateQuest?.Invoke(CurrentQuest); 
        OnCheckComplete?.Invoke(CurrentQuest.IsCompleted);
    }

    /// <summary>
    /// 퀘스트 생성
    /// </summary>
    public static Quest CreateQuest(int questIndex)
    {
        QuestData questData = QuestDataManager.GetQuestData(questIndex);
        return new Quest(questData);
    }

    /// <summary>
    /// 퀘스트 누적수치 갱신하기
    /// </summary>
    public static void UpdateQuestStack(QuestType questType, int value)
    {
        if (_questStackDict.ContainsKey(questType))
            _questStackDict[questType] = value;

        if (CurrentQuest.QuestType == questType)
        {
            CurrentQuest.SetValue(_questStackDict[questType]);

            OnUpdateQuest?.Invoke(CurrentQuest); 
            OnCheckComplete?.Invoke(CurrentQuest.IsCompleted);
        }
    }

    /// <summary>
    /// 현재 활성화된 '적 처치 퀘스트'의 수치 증가하기
    /// </summary>
    public static void AddValue_KillEnemyQuest(QuestType questType, int amount)
    {
        if (questType != QuestType.KillEnemy)
            return;

        if (CurrentQuest.QuestType != questType)
            return;

        CurrentQuest.AddValue(amount);

        OnUpdateQuest?.Invoke(CurrentQuest);
        OnCheckComplete?.Invoke(CurrentQuest.IsCompleted);
    }
   
    /// <summary>
    /// 퀘스트 완료 (UI버튼에서 콜백 받아오는 함수)
    /// </summary>
    public static void CompleteCurrentQuest()
    {
        GiveReward(CurrentQuest);
        NextQuest();
    }

    /// <summary>
    /// 보상 지급하기
    /// </summary>
    private static void GiveReward(Quest quest)
    {
        QuestData questData = QuestDataManager.GetQuestData(quest.QuestIndex);
        GemManager.AddGem(questData.RewardGem);
    }

    /// <summary>
    /// 다음 퀘스트 시작하기
    /// </summary>
    private static void NextQuest()
    {
        _currentQuestIndex++;
        SetCurrentQuest();
    }

    /// <summary>
    /// 퀘스트 진행상태 누적값 가져오기
    /// </summary>
    public static int Get_QuestStackValue(QuestType questType)
    {
        if (_questStackDict.TryGetValue(questType, out int amount))
            return amount;
        else
            return 0;
    }
}
