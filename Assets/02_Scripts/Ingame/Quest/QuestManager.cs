using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : SingletonBase<QuestManager>
{
    private List<QuestData> _questDataList = new List<QuestData>();  // ��� ����Ʈ ������ ���̺�
    private Quest _currentQuest;                                     // ���� Ȱ��ȭ�� ����Ʈ
    private int _currentIndex = 0;                                   // ���� ����Ʈ �ε���
    private Dictionary<QuestType, int> _questTypeProgressDict;       // ����Ʈ ������ ���� ���� ��Ȳ

    private void Start()
    {
        _questDataList = DataManager.Instance.QuestDatasSO.QuestDataList;
        _currentQuest = new Quest(_questDataList[_currentIndex]); // �ϴ� ���� ù��° �ɷ� ���� ����Ʈ Ȱ��ȭ
        CheckCompleted(_currentQuest);
    }


    // ����Ʈ ���� ��Ȳ ������Ʈ
    public void UpdateQuestProgress(QuestType questType, int amount)
    {
        // ���� ���� ��Ȳ ������Ʈ
        if (_questTypeProgressDict.ContainsKey(questType))
        {
            _questTypeProgressDict[questType] += amount;
        }

        // ���� Ȱ��ȭ�� ����Ʈ�� ������ �����̶��
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

    // ����Ʈ �Ϸ� ó��
    private void CompleteQuest(Quest quest)
    {
        quest.IsCompleted = true; // �Ϸ� ���·� ����
        Debug.Log($"����Ʈ �Ϸ�: {quest.QuestData.Desc}");


        Reward(quest);

        StartNextQuest();
    }

    // ���� ����Ʈ ����
    private void StartNextQuest()
    {
        _currentIndex++;
        _currentQuest = new Quest(_questDataList[_currentIndex]);

        CheckCompleted(_currentQuest);
    }

    // ��� �Ϸ� ���� Ȯ��
    private void CheckCompleted(Quest quest)
    {
        // ���� ���̶� ���ϰ� �̹� �����ٸ� ��� �Ϸ�
        if (_questTypeProgressDict[quest.QuestData.QuestType] >= quest.CurrentValue)
        {
            CompleteQuest(quest);
        }
    }


    // ���� ����
    private void Reward(Quest quest)
    {
        Debug.Log($"�������� : {quest.QuestData.RewardGold}");
        // �����δ� �����Ͽ� ������ ����
    }

}























