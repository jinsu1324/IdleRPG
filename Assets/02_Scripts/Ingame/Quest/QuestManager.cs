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
        InitializeProgressDict();

        _currentQuest = new Quest(_questDataList[_currentIndex]); // �ϴ� ���� ù��° �ɷ� ���� ����Ʈ Ȱ��ȭ
        CheckCompleted(_currentQuest);
    }

    // ���� ���� ��Ȳ �ʱ�ȭ
    private void InitializeProgressDict()
    {
        _questTypeProgressDict = new Dictionary<QuestType, int>();
        foreach (QuestType type in Enum.GetValues(typeof(QuestType)))
        {
            _questTypeProgressDict[type] = 0;
        }
    }

    // ����Ʈ ���� ��Ȳ ������Ʈ
    public void UpdateQuestProgress(QuestType questType, int amount)
    {
        // ���� ����Ʈ�� ������ óġ�� ���, �������� �ʰ� Ȱ��ȭ�� ����Ʈ������ ����
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
            return; // ������ óġ ������ �������� ����
        }


        // ���� ���� ��Ȳ ������Ʈ
        if (_questTypeProgressDict.ContainsKey(questType))
        {
            _questTypeProgressDict[questType] += amount;
        }

        // ���� Ȱ��ȭ�� ����Ʈ�� ������ �����̶�� Ȱ��ȭ�� ����Ʈ�� ���ÿ� ������Ʈ
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
        if (_currentIndex >= _questDataList.Count)
        {
            Debug.Log("��� ����Ʈ �Ϸ�");
            _currentQuest = null;
            return;
        }

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























