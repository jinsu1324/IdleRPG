using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : SingletonBase<QuestManager>
{
    public static event Action<Quest> OnUpdateCurrentQuest;          // ���� ����Ʈ ������ ������Ʈ������ �̺�Ʈ
    public static event Action<bool> OnCheckQuestCompleted;          // ����Ʈ �ϷῩ�� üũ�Ҷ� �̺�Ʈ
    
    [SerializeField] private QuestDatasSO _questDatasSO;             // ����Ʈ ������

    private List<QuestData> _questDataList = new List<QuestData>();  // ��� ����Ʈ ������ ����Ʈ
    private Quest _currentQuest;                                     // ���� Ȱ��ȭ�� ����Ʈ
    private int _currentIndex = 0;                                   // ���� ����Ʈ �ε���
    private Dictionary<QuestType, int> _questProgressDict;           // ����Ʈ ������ ���� ���� ��Ȳ ��ųʸ�

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        InitQuestDataList(); // ����Ʈ ������ ����Ʈ �ʱ�ȭ
        InitQuestProgressDict(); // ���������Ȳ ��ųʸ� �ʱ�ȭ

        _currentQuest = new Quest(_questDataList[_currentIndex]); // �ϴ� ���� ù��° �ɷ� ���� ����Ʈ Ȱ��ȭ
        OnUpdateCurrentQuest?.Invoke(_currentQuest); // ���� ����Ʈ ���� ������Ʈ �̺�Ʈ ����
        CheckQuestComplete(_currentQuest); // �ϷῩ�� üũ
    }

    /// <summary>
    /// ����Ʈ ������ ����Ʈ �ʱ�ȭ
    /// </summary>
    private void InitQuestDataList()
    {
        _questDataList = _questDatasSO.QuestDataList;
    }

    /// <summary>
    /// ���������Ȳ ��ųʸ� �ʱ�ȭ
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
    /// ����Ʈ �����Ȳ ������Ʈ
    /// </summary>
    public void UpdateQuestProgress(QuestType questType, int amount)
    {
        // ���� ���� ��Ȳ ������Ʈ
        if (_questProgressDict.ContainsKey(questType))
            _questProgressDict[questType] += amount;

        // ��������Ʈ�� ������ �׳� ����
        if (_currentQuest == null)
            return;

        // �Ķ���� ����ƮŸ���� = ���� ����ƮŸ�԰� �����ϴٸ�, ���� ����Ʈ�� ������Ʈ
        if (_currentQuest.GetQuestType() == questType && _currentQuest.IsCompleted() == false)
        {
            _currentQuest.SetCurrentValue(_questProgressDict[questType]); // ������ ��ġ�� ��������Ʈ ��ġ��
            OnUpdateCurrentQuest?.Invoke(_currentQuest); // ���� ����Ʈ ���� ������Ʈ �̺�Ʈ ����
            CheckQuestComplete(_currentQuest); // �ϷῩ�� üũ
        }
    }

    /// <summary>
    /// ����Ʈ �Ϸ� ���� Ȯ��
    /// </summary>
    private void CheckQuestComplete(Quest quest)
    {
        OnCheckQuestCompleted?.Invoke(quest.IsCompleted()); // ����Ʈ �ϷῩ�� üũ�Ҷ� �̺�Ʈ ����
    }

    /// <summary>
    /// ����Ʈ �Ϸ�
    /// </summary>
    public void CompleteCurrentQuest()
    {
        Debug.Log($"����Ʈ �Ϸ�: {_currentQuest.GetDesc()}");
        
        Reward(_currentQuest); // ���� ����
        StartNextQuest(); // ���� ����Ʈ��
    }

    /// <summary>
    /// ���� ����Ʈ ����
    /// </summary>
    private void StartNextQuest()
    {
        _currentIndex++;
        if (_currentIndex >= _questDataList.Count)
        {
            Debug.Log("��� ����Ʈ �Ϸ�");
            _currentQuest = null;
            return;
        }
        
        _currentQuest = new Quest(_questDataList[_currentIndex]); // ���� ����Ʈ�� ��������Ʈ ����
        OnUpdateCurrentQuest?.Invoke(_currentQuest); // ���� ����Ʈ ���� ������Ʈ �̺�Ʈ ����
        CheckQuestComplete(_currentQuest); // �Ϸ� ���� üũ
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void Reward(Quest quest)
    {
        GemManager.AddGem(quest.GetRewardGem());

        Debug.Log($"�������� : {quest.GetRewardGem()}");
    }

    /// <summary>
    /// ���� ����Ʈ ��������
    /// </summary>
    public Quest GetCurrentQuest()
    {
        return _currentQuest;
    }

    /// <summary>
    /// ����Ʈ ������� ������ ��������
    /// </summary>
    public int GetQuestProgressAmount(QuestType questType)
    {
        if (_questProgressDict.TryGetValue(questType, out int amount))
            return amount;
        else
            return 0;
    }
}
