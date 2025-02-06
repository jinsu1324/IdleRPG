using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ������
/// </summary>
public class GoldManager : ISavable
{
    public static event Action<int> OnGoldChange;       // ��� ���� �Ǿ��� �� �̺�Ʈ
    public string Key => nameof(GoldManager);           // Firebase ������ ����� ���� Ű ����
    [SaveField] private static int _currentGold;        // ���� ���
    public static int CurrentGold                           
    {
        get => _currentGold;
        set
        {
            _currentGold = value;
            NotifyChanged(); // ���� ����� �� �̺�Ʈ ȣ��
        }
    }

    /// <summary>
    /// ������ �ҷ������Ҷ� �½�ũ��
    /// </summary>
    public void DataLoadTask()
    {
        NotifyChanged();
    }

    /// <summary>
    /// ��� �߰�
    /// </summary>
    public static void AddGold(int amount)
    {
        CurrentGold += amount;
        QuestManager.UpdateQuestStack(QuestType.CollectGold, CurrentGold);
    }

    /// <summary>
    /// ��� ����
    /// </summary>
    public static void ReduceGold(int amount)
    {
        if (CurrentGold >= amount)
        {
            CurrentGold -= amount;
            
            if (CurrentGold <= 0)
                CurrentGold = 0;

            QuestManager.UpdateQuestStack(QuestType.CollectGold, CurrentGold);
        }
        else
            Debug.Log($"������ ���({CurrentGold})�� ������ ���({amount})���� ��� ��尨�Ұ� �Ұ����մϴ�!");
    }

    /// <summary>
    /// ��� ��������
    /// </summary>
    public static int GetGold() => CurrentGold; 

    /// <summary>
    /// ��� ������� üũ
    /// </summary>
    public static bool HasEnoughGold(int cost) => CurrentGold >= cost;

    /// <summary>
    /// ��� ���� �̺�Ʈ ȣ��
    /// </summary>
    private static void NotifyChanged() => OnGoldChange?.Invoke(CurrentGold);

    /// <summary>
    /// �ʱ��� ����
    /// </summary>
    public static void SetDefaultGold() => CurrentGold += 500;
}
