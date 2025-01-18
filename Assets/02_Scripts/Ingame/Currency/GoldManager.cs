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
    [SaveField] private static int _currentGold;                   // ���� ���
    public static int CurrentGold
    {
        get => _currentGold;
        set
        {
            _currentGold = value;
            NotifyChanged(); // ���� ����� �� �̺�Ʈ ȣ��
        }
    }


    public static event Action<int> OnGoldChanged;     // ��� ���� �Ǿ��� �� �̺�Ʈ
    public string Key => nameof(GoldManager);   // ���� Ű ����


    /// <summary>
    /// ��� �߰�
    /// </summary>
    public static void AddGold(int amount)
    {
        CurrentGold += amount;
        //NotifyChanged();
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
            //NotifyChanged();
            QuestManager.Instance.UpdateQuestProgress(QuestType.CollectGold, -amount);
        }
        else
        {
            Debug.LogWarning("������ ��尡 �����մϴ�!");
        }
    }

    /// <summary>
    /// ��� ��������
    /// </summary>
    public static int GetGold()
    {
        return CurrentGold;
    }

    /// <summary>
    /// ��� ������� üũ
    /// </summary>
    public static bool HasEnoughGold(int cost)
    {
        return CurrentGold >= cost;
    }

    /// <summary>
    /// ��� ���� �̺�Ʈ ȣ��
    /// </summary>
    private static void NotifyChanged()
    {
        Debug.Log("��� ���� �̺�Ʈ ȣ��!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        OnGoldChanged?.Invoke(_currentGold);
    }

    public void NotifyLoaded()
    {
        Debug.Log("��� ���� �̺�Ʈ ȣ��!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        OnGoldChanged?.Invoke(_currentGold);
    }
}
