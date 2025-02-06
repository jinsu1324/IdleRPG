using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : ISavable
{
    public static event Action<Quest> OnUpdateQuest;            // ���� ����Ʈ ������ ������Ʈ������ �̺�Ʈ
    public static event Action<bool> OnCheckComplete;           // ����Ʈ �ϷῩ�� üũ �̺�Ʈ

    public string Key => nameof(QuestManager);                  // Firebase ������ ����� ���� Ű ����
    [SaveField] private static int _currentQuestIndex = 1;      // ���� ����Ʈ �ε���
    [SaveField] private static Quest _currentQuest;             // ���� ����Ʈ
    public static Quest CurrentQuest { get { return _currentQuest; } set { _currentQuest = value; } }

    // ����Ʈ ������ ���� ���� ��Ȳ ��ųʸ�
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
    /// ������ �ҷ������Ҷ� �½�ũ��
    /// </summary>
    public void DataLoadTask()
    {
        OnUpdateQuest?.Invoke(CurrentQuest);
        OnCheckComplete?.Invoke(CurrentQuest.IsCompleted);
    }

    /// <summary>
    /// ���� ����Ʈ �����ϱ�
    /// </summary>
    public static void SetCurrentQuest()
    {
        CurrentQuest = CreateQuest(_currentQuestIndex);

        OnUpdateQuest?.Invoke(CurrentQuest); 
        OnCheckComplete?.Invoke(CurrentQuest.IsCompleted);
    }

    /// <summary>
    /// ����Ʈ ����
    /// </summary>
    public static Quest CreateQuest(int questIndex)
    {
        QuestData questData = QuestDataManager.GetQuestData(questIndex);
        return new Quest(questData);
    }

    /// <summary>
    /// ����Ʈ ������ġ �����ϱ�
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
    /// ���� Ȱ��ȭ�� '�� óġ ����Ʈ'�� ��ġ �����ϱ�
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
    /// ����Ʈ �Ϸ� (UI��ư���� �ݹ� �޾ƿ��� �Լ�)
    /// </summary>
    public static void CompleteCurrentQuest()
    {
        GiveReward(CurrentQuest);
        NextQuest();
    }

    /// <summary>
    /// ���� �����ϱ�
    /// </summary>
    private static void GiveReward(Quest quest)
    {
        QuestData questData = QuestDataManager.GetQuestData(quest.QuestIndex);
        GemManager.AddGem(questData.RewardGem);
    }

    /// <summary>
    /// ���� ����Ʈ �����ϱ�
    /// </summary>
    private static void NextQuest()
    {
        _currentQuestIndex++;
        SetCurrentQuest();
    }

    /// <summary>
    /// ����Ʈ ������� ������ ��������
    /// </summary>
    public static int Get_QuestStackValue(QuestType questType)
    {
        if (_questStackDict.TryGetValue(questType, out int amount))
            return amount;
        else
            return 0;
    }
}
