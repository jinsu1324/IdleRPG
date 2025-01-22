using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� ������
/// </summary>
public class GemManager : ISavable
{
    public static event Action<int> OnGemChange;    // �� ���� �Ǿ��� �� �̺�Ʈ
    public string Key => nameof(GemManager);        // Firebase ������ ����� ���� Ű ����
    [SaveField] private static int _currentGem;     // ���� ��
    public static int CurrentGem
    { 
        get => _currentGem;
        set
        {
            _currentGem = value;
            NotifyChanged(); // ���� ����ɶ� �̺�Ʈ ȣ��
        }
    }

    /// <summary>
    /// �� �߰�
    /// </summary>
    public static void AddGem(int amount)
    {
        CurrentGem += amount;
    }

    /// <summary>
    /// �� ����
    /// </summary>
    public static void ReduceGem(int amount)
    {
        if (CurrentGem >= amount)
        {
            CurrentGem -= amount;
        }
        else
            Debug.Log($"������ ��({CurrentGem})�� ������ ��({amount})���� ��� �����Ұ� �Ұ����մϴ�!");
    }

    /// <summary>
    /// �� ��������
    /// </summary>
    public static int GetGem() => CurrentGem;

    /// <summary>
    /// �� ������� üũ
    /// </summary>
    public static bool HasEnoughGem(int cost) => CurrentGem >= cost;

    /// <summary>
    /// �� ���� �̺�Ʈ ȣ��
    /// </summary>
    private static void NotifyChanged() => OnGemChange?.Invoke(CurrentGem);
}
