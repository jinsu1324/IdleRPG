using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� ������
/// </summary>
[System.Serializable]
public class GemManager : ISavable
{
    [SaveField] private static int _currentGem;                 // ���� ��
    public static int CurrentGem
    { 
        get => _currentGem;
        set
        {
            _currentGem = value;
            NotifyChanged(); // ���� ����ɶ� �̺�Ʈ ȣ��
        }
    }

    public static event Action<int> OnGemChanged;   // �� ���� �Ǿ��� �� �̺�Ʈ
    public string Key => nameof(GemManager);   // ���� Ű ����

    /// <summary>
    /// �� �߰�
    /// </summary>
    public static void AddGem(int amount)
    {
        CurrentGem += amount;
        Debug.Log($"���� gem {CurrentGem}");
        //NotifyChanged();
    }

    /// <summary>
    /// �� ����
    /// </summary>
    public static void ReduceGem(int amount)
    {
        if (CurrentGem >= amount)
        {
            CurrentGem -= amount;
            //NotifyChanged();
        }
        else
        {
            Debug.LogWarning("������ ���� �����մϴ�!");
        }
    }

    /// <summary>
    /// �� ��������
    /// </summary>
    public static int GetGem()
    {
        return CurrentGem;
    }

    /// <summary>
    /// �� ������� üũ
    /// </summary>
    public static bool HasEnoughGem(int cost)
    {
        return CurrentGem >= cost;
    }

    /// <summary>
    /// �� ���� �̺�Ʈ ȣ��
    /// </summary>
    private static void NotifyChanged()
    {
        Debug.Log("�� ���� �̺�Ʈ ȣ��!!~~~~~~~~~~!!!!!");

        OnGemChanged?.Invoke(_currentGem);
    }

    public void NotifyLoaded()
    {
        Debug.Log("�� ���� �̺�Ʈ ȣ��!!~~~~~~~~~~!!!!!");

        OnGemChanged?.Invoke(_currentGem);
    }
}
