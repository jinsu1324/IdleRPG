using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ������
/// </summary>
[System.Serializable]
public class GoldManager : ISavable
{
    public static event Action<int> OnGoldChange;       // ��� ���� �Ǿ��� �� �̺�Ʈ
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
    public string Key => nameof(GoldManager);           // Firebase ������ ����� ���� Ű ����

    /// <summary>
    /// ��� �߰�
    /// </summary>
    public static void AddGold(int amount)
    {
        CurrentGold += amount;
        QuestManager.Instance.UpdateQuestProgress(QuestType.CollectGold, amount);
    }

    /// <summary>
    /// ��� ����
    /// </summary>
    public static void ReduceGold(int amount)
    {
        if (CurrentGold >= amount)
        {
            CurrentGold -= amount;
            QuestManager.Instance.UpdateQuestProgress(QuestType.CollectGold, -amount);
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
    /// �ε�Ǿ����� �̺�Ʈ ȣ��
    /// </summary>
    public void NotifyLoaded() => OnGoldChange?.Invoke(CurrentGold);
}
